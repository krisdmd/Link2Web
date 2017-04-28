using Link2Web.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;

namespace Link2Web.Core
{
    public class Crawler
    {
        private string _url;
        private List<string> _internalLinks = new List<string>(); 
        private ObservableCollection<string> otherUrlRepository = new ObservableCollection<string>();
        private ObservableCollection<string> failedUrlRepository = new ObservableCollection<string>();
        private ObservableCollection<string> currentPageUrlRepository = new ObservableCollection<string>();


        /// <summary>
        /// List of Pages.
        /// </summary>
        private List<Page> _pages = new List<Page>();

        /// <summary>
        /// List of exceptions.
        /// </summary>
        private List<string> _exceptions = new List<string>();

        /// <summary>
        /// Is current page or not
        /// </summary>
        private bool _isCurrentPage = true;

        /// <summary>
        /// Initializing the crawling process.
        /// </summary>
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
                AddRangeButNoDuplicates(linkParser.InternalUrls);
            }

            AddRangeButNoDuplicates(linkParser.BadUrls);

            foreach (string exception in linkParser.Exceptions)
                _exceptions.Add(exception);

            _isCurrentPage = false;
            //Crawl all the links found on the page.
            foreach (string link in _internalLinks)
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
                catch (Exception exc)
                {
                    failedUrlRepository.Add(formattedLink + " (on page at url " + url + ") - " + exc.Message);
                }
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
                formattedLink = originatingUrl.Substring(0, originatingUrl.LastIndexOf("/", StringComparison.Ordinal) + 1) + link;
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
                    _internalLinks.Add(str);
                }
                else
                {
                    // break;
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
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.UserAgent = "A Web Crawler";

            var response = request.GetResponse();

            var stream = response.GetResponseStream();

            var reader = new StreamReader(stream);
            var htmlText = reader.ReadToEnd();
            return htmlText;
        }

        /// <summary>
        /// Is the url to an external site?
        /// </summary>
        /// <param name="url">The url whose externality of destination is in question.</param>
        /// <returns>Boolean indicating whether or not the url is to an external destination.</returns>
        private bool IsExternalUrl(string url)
        {
            return !url.Contains(_url);
        }
    }
}