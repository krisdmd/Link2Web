using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Link2Web.Helpers
{
    public class LinkParser
    {
        private string _url;
        private List<Page> _pages = new List<Page>();
        private bool _isCurrentPage = true; 
        private const string LinkRegex = "href=\"[a-zA-Z./:&\\d_-]+\"";
        private List<string> _badUrls = new List<string>();
        private List<string> _exceptions = new List<string>();
        private List<string> _internalUrls { get; set; } = new List<string>();

        /// <summary>
        /// Bad Urls
        /// </summary>
        public List<string> BadUrls
        {
            get { return _badUrls; }
            set { _badUrls = value; }
        }

        /// <summary>
        /// Exceptions
        /// </summary>
        public List<string> Exceptions
        {
            get { return _exceptions; }
            set { _exceptions = value; }
        }

        public void InitializeCrawl(string url)
        {
            _url = url;
            CrawlPage(_url);
        }


        /// <summary>
        /// Crawls a page.
        /// </summary>
        /// <param name="url">The url to crawl.</param>
        private void CrawlPage(string url)
        {
            if (PageHasBeenCrawled(url)) return;

            var htmlText = GetWebText(url);
            var linkParser = new LinkParser();
            var page = new Page
            {
                Text = htmlText,
                Url = url
            };

            _pages.Add(page);

            linkParser.ParseLinks(page, url);

            //Add data to main data lists
            if (_isCurrentPage)
            {
                AddRangeButNoDuplicates(_internalUrls);
            }

            AddRangeButNoDuplicates(linkParser.BadUrls);

            foreach (string exception in linkParser.Exceptions)
                _exceptions.Add(exception);

            _isCurrentPage = false;


            //Crawl all the links found on the page.
            foreach (var link in _internalUrls)
            {
                string formattedLink = link;
                try
                {
                    formattedLink = FixPath(url, formattedLink);

                    if (formattedLink != String.Empty)
                    {
                        CrawlPage(formattedLink);
                    }
                }
                catch (Exception)
                {
                    _badUrls.Add(url);
                }
            }
        }

        /// <summary>
        /// Parses a page looking for links.
        /// </summary>
        /// <param name="page">The page whose text is to be parsed.</param>
        /// <param name="sourceUrl">The source url of the page.</param>
        public void ParseLinks(Page page, string sourceUrl)
        {
            var matches = Regex.Matches(page.Text, LinkRegex);

            for (var i = 0; i <= matches.Count - 1; i++)
            {
                var anchorMatch = matches[i];

                if (anchorMatch.Value == string.Empty)
                {
                    BadUrls.Add("Blank url value on page " + sourceUrl);
                    continue;
                }

                string foundHref = null;
                try
                {
                    foundHref = anchorMatch.Value.Replace("href=\"", "");
                    foundHref = foundHref.Substring(0, foundHref.IndexOf("\"", StringComparison.Ordinal));
                }
                catch (Exception exc)
                {
                    Exceptions.Add("Error parsing matched href: " + exc.Message);
                }


                if (!_internalUrls.Contains(foundHref))
                {
                    if (foundHref != "/")
                    {
                        if (!IsExternalUrl(foundHref) && !_internalUrls.Contains(foundHref))
                        {
                            _internalUrls.Add(foundHref);
                        }
                        else if (!IsAWebPage(foundHref))
                        {
                            foundHref = FixPath(sourceUrl, foundHref);
                            BadUrls.Add(foundHref);
                        }
                        else
                        {
                            _internalUrls.Add(foundHref);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Is the url to an external site?
        /// </summary>
        /// <param name="url">The url whose externality of destination is in question.</param>
        /// <returns>Boolean indicating whether or not the url is to an external destination.</returns>
        private bool IsExternalUrl(string url)
        {
            return _url.Contains(url);
        }

        /// <summary>
        /// Is the value of the href pointing to a web page?
        /// </summary>
        /// <param name="foundHref">The value of the href that needs to be interogated.</param>
        /// <returns>Boolen </returns>
        private bool IsAWebPage(string foundHref)
        {
            if (foundHref.IndexOf("javascript:", StringComparison.Ordinal) == 0)
                return false;

            string extension = foundHref.Substring(foundHref.LastIndexOf(".", StringComparison.Ordinal) + 1,
                foundHref.Length - foundHref.LastIndexOf(".", StringComparison.Ordinal) - 1);
            switch (extension)
            {
                case "jpg":
                case "css":
                    return false;
                default:
                    return true;
            }
        }


        /// <summary>
        /// Fixes a path. Makes sure it is a fully functional absolute url.
        /// </summary>
        /// <param name="originatingUrl">The url that the link was found in.</param>
        /// <param name="link">The link to be fixed up.</param>
        /// <returns>A fixed url that is fit to be fetched.</returns>
        public string FixPath(string originatingUrl, string link)
        {
            string formattedLink = string.Empty;

            if (link.IndexOf("../", StringComparison.Ordinal) > -1)
            {
                formattedLink = ResolveRelativePaths(link, originatingUrl);
            }
            else if (originatingUrl.IndexOf(_url, StringComparison.Ordinal) > -1
                     && link.IndexOf(_url, StringComparison.Ordinal) == -1 &&
                     !link.Contains("http:"))
            {
                formattedLink =
                    originatingUrl.Substring(0, originatingUrl.LastIndexOf("/", StringComparison.Ordinal) + 1) + link;
            }
            else if (link.IndexOf(_url, StringComparison.Ordinal) == -1)
            {
                formattedLink = link;
            }

            return formattedLink;
        }

        /// <summary>
        /// Needed a method to turn a relative path into an absolute path. And this seems to work.
        /// </summary>
        /// <param name="relativeUrl">The relative url.</param>
        /// <param name="originatingUrl">The url that contained the relative url.</param>
        /// <returns>A url that was relative but is now absolute.</returns>
        public string ResolveRelativePaths(string relativeUrl, string originatingUrl)
        {
            var resolvedUrl = string.Empty;

            var relativeUrlArray = relativeUrl.Split(new char[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
            var originatingUrlElements = originatingUrl.Split(new char[] {'/'},
                StringSplitOptions.RemoveEmptyEntries);
            var indexOfFirstNonRelativePathElement = 0;
            for (var i = 0; i <= relativeUrlArray.Length - 1; i++)
            {
                if (relativeUrlArray[i] == "..") continue;
                indexOfFirstNonRelativePathElement = i;
                break;
            }

            int countOfOriginatingUrlElementsToUse = originatingUrlElements.Length - indexOfFirstNonRelativePathElement -
                                                     1;
            for (var i = 0; i <= countOfOriginatingUrlElementsToUse - 1; i++)
            {
                if (originatingUrlElements[i] == "http:" || originatingUrlElements[i] == "https:")
                    resolvedUrl += originatingUrlElements[i] + "//";
                else
                    resolvedUrl += originatingUrlElements[i] + "/";
            }

            for (var i = 0; i <= relativeUrlArray.Length - 1; i++)
            {
                if (i < indexOfFirstNonRelativePathElement) continue;
                resolvedUrl += relativeUrlArray[i];

                if (i < relativeUrlArray.Length - 1)
                    resolvedUrl += "/";
            }

            return resolvedUrl;
        }

        /// <summary>
        /// Checks to see if the page has been crawled.
        /// </summary>
        /// <param name="url">The url that has potentially been crawled.</param>
        /// <returns>Boolean indicating whether or not the page has been crawled.</returns>
        public bool PageHasBeenCrawled(string url)
        {
            return _pages.Any(page => page.Url == url);
        }

        /// <summary>
        /// Merges a two lists of strings.
        /// </summary>
        /// <param name="sourceList">The list whose values need to be merged.</param>
        private void AddRangeButNoDuplicates(IEnumerable<string> sourceList)
        {
            foreach (var str in sourceList.ToList())
            {
                if (!IsExternalUrl(str))
                {
                    _internalUrls.Add(str);
                }

            }
        }

        /// <summary>
        /// Gets the response text for a given url.
        /// </summary>
        /// <param name="url">The url whose text needs to be fetched.</param>
        /// <returns>The text of the response.</returns>
        public string GetWebText(string url)
        {
            var htmlText = string.Empty;
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.UserAgent = "A Web Crawler";

            var response = request.GetResponse();

            var stream = response.GetResponseStream();


            if (stream != null)
            {
                var reader = new StreamReader(stream);
                htmlText = reader.ReadToEnd();
            }


            return htmlText;
        }
    }
}
