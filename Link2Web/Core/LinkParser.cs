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
        private List<string> InternalUrls { get; set; } = new List<string>();

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
            if (IsExternalUrl(url)) return;
            if (PageHasBeenCrawled(url)) return;

            var htmlText = GetWebText(url);
            var page = new Page
            {
                Text = htmlText,
                Url = url
            };

            _pages.Add(page);

            ParseLinks(page, url);


            _isCurrentPage = false;


            //Crawl all the links found on the page.
            foreach (var link in InternalUrls)
            {
                var formattedLink = link;
                try
                {
                    formattedLink = FixPath(url, formattedLink);

                    if (formattedLink != string.Empty)
                    {
                        CrawlPage(formattedLink);
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        /// <summary>
        /// Parses a page looking for links.
        /// </summary>
        /// <param name="page">The page whose text is to be parsed.</param>
        /// <param name="sourceUrl">The source url of the page.</param>
        private void ParseLinks(Page page, string sourceUrl)
        {
            var matches = Regex.Matches(page.Text, LinkRegex);

            for (var i = 0; i <= matches.Count - 1; i++)
            {
                var anchorMatch = matches[i];

                //                if (anchorMatch.Value == string.Empty)
                //                {
                //                    BadUrls.Add("Blank url value on page " + sourceUrl);
                //                    continue;
                //                }

                string foundHref = null;
                foundHref = anchorMatch.Value.Replace("href=\"", "");
                foundHref = foundHref.Substring(0, foundHref.IndexOf("\"", StringComparison.Ordinal));


                if (!InternalUrls.Contains(foundHref) && !IsExternalUrl(foundHref) && IsAWebPage(foundHref))
                {
                    InternalUrls.Add(foundHref);
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
            if (GetHostFromUrl(_url).Contains(GetHostFromUrl(url)))
            {
                return false;
            }
            else
            {
                return true;
            }

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
                case "xml":
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
        private string FixPath(string originatingUrl, string link)
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
        private string ResolveRelativePaths(string relativeUrl, string originatingUrl)
        {
            var resolvedUrl = string.Empty;

            var relativeUrlArray = relativeUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var originatingUrlElements = originatingUrl.Split(new char[] { '/' },
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
        private bool PageHasBeenCrawled(string url)
        {
            return _pages.Any(page => page.Url == url);
        }

        /// <summary>
        /// Gets the response text for a given url.
        /// </summary>
        /// <param name="url">The url whose text needs to be fetched.</param>
        /// <returns>The text of the response.</returns>
        private string GetWebText(string url)
        {
            var htmlText = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(url);
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

        private string GetHostFromUrl(string url)
        {
            var uri = new Uri(url);
            return uri.Host;
        }

    }
}