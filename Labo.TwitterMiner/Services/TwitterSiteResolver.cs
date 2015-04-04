namespace Labo.TwitterMiner.Services
{
    using System;

    internal sealed class TwitterSiteResolver : ITwitterSiteResolver
    {
        public string Resolve(string url)
        {
            Uri uri = new Uri(url);
            return string.Format("{0}://{1}", uri.Scheme, uri.Authority);
        }
    }
}
