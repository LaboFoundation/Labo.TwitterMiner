namespace Labo.TwitterMiner.Services
{
    using System.Collections.Generic;

    using Labo.TwitterMiner.Entity;

    internal sealed class TwitterTweetUrlExpanderProcessor : ITwitterTweetProcessor
    {
        private readonly ITwitterSiteResolver m_TwitterSiteResolver;

        private readonly IUrlExpander m_UrlExpander;

        public TwitterTweetUrlExpanderProcessor(ITwitterSiteResolver twitterSiteResolver, IUrlExpander urlExpander)
        {
            m_TwitterSiteResolver = twitterSiteResolver;
            m_UrlExpander = urlExpander;
        }

        public void Process(TwitterTweet tweet)
        {
            ICollection<TwitterUrl> urls = tweet.TwitterUrls;
            foreach (TwitterUrl twitterUrl in urls)
            {
                string originalUrl = m_UrlExpander.ExpandUrl(twitterUrl.ExpandedValue);
                string siteUrl = m_TwitterSiteResolver.Resolve(originalUrl);

                twitterUrl.OriginalValue = originalUrl;
                
                TwitterSite twitterSite = new TwitterSite();
                twitterSite.SiteUrl = siteUrl;
                twitterUrl.TwitterSite = twitterSite;
            }
        }
    }
}