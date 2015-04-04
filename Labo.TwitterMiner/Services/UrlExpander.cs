namespace Labo.TwitterMiner.Services
{
    using System;
    using System.Net;

    internal sealed class UrlExpander : IUrlExpander
    {
        public string ExpandUrl(string url, bool throwException = false)
        {
            return ExpandUrlInternal(url, null, throwException);
        }

        private static string ExpandUrlInternal(string url, Action<string, HttpWebResponse> urlRetrievedAction = null, bool throwException = false, string forwardFor = null)
        {
            try
            {
                bool isRedirecting;
                int count = 0;
                do
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    if (forwardFor != null)
                    {
                        request.Headers.Add(string.Format("X-Forwarded-For: {0}", forwardFor));
                    }

                    request.Method = "HEAD";
                    request.AllowAutoRedirect = false;
                    request.Timeout = 15 * 1000;

                    HttpWebResponse response = (HttpWebResponse) request.GetResponse();

                    int statusCode = (int) response.StatusCode;
                    isRedirecting = statusCode == 301 || statusCode == 302;
                    if (isRedirecting)
                    {
                        string currentUrl = response.Headers["Location"];

                        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                        {
                            Uri responseUri = response.ResponseUri;
                            Uri baseUri = new Uri(string.Format("{0}://{1}", responseUri.Scheme, responseUri.Authority));
                            currentUrl = new Uri(baseUri, url).AbsoluteUri;
                        }

                        if (urlRetrievedAction != null)
                        {
                            urlRetrievedAction(currentUrl, response);
                        }

                        // if current url equals to previous url break (infinite redirecting)
                        if (currentUrl == url)
                        {
                            break;
                        }

                        if (currentUrl == "/")
                        {
                            break;
                        }

                        url = currentUrl;
                    }
                    else
                    {
                        if (count == 0 && urlRetrievedAction != null)
                        {
                            urlRetrievedAction(url, response);
                        }
                    }

                    count++;

                    if (count >= 50)
                    {
                        break;
                    }
                } 
                while (isRedirecting);
            }
            catch (WebException)
            {
                if (throwException)
                {
                    throw;
                }

                return url;
            }

            return url;
        }
    }
}
