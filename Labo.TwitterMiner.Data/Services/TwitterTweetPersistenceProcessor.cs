namespace Labo.TwitterMiner.Data.Services
{
    using System.Linq;

    using Labo.Common.Data.Repository;
    using Labo.Common.Data.Session;
    using Labo.TwitterMiner.Entity;
    using Labo.TwitterMiner.Services;

    internal sealed class TwitterTweetPersistenceProcessor : ITwitterTweetProcessor
    {
        private readonly ISessionScopeProvider m_SessionScopeProvider;

        public TwitterTweetPersistenceProcessor(ISessionScopeProvider sessionScopeProvider)
        {
            m_SessionScopeProvider = sessionScopeProvider;
        }

        public void Process(TwitterTweet tweet)
        {
            using (ISessionScope sessionScope = m_SessionScopeProvider.CreateSessionScope())
            {
                IRepository<TwitterTweet> tweetRepository = sessionScope.GetRepository<TwitterTweet>();
                if (!tweetRepository.Query().Any(x => x.ID == tweet.ID))
                {
                    InsertUser(tweet, sessionScope);

                    TwitterTweet newTweet = new TwitterTweet
                                                {
                                                    CreateDate = tweet.CreateDate,
                                                    ID = tweet.ID,
                                                    InReplyToScreenName = tweet.InReplyToScreenName,
                                                    InReplyToStatusId = tweet.InReplyToStatusId,
                                                    InReplyToUserId = tweet.InReplyToUserId,
                                                    Latitude = tweet.Latitude,
                                                    Longitude = tweet.Longitude,
                                                    PlaceID = tweet.PlaceID,
                                                    RetweetCount = tweet.RetweetCount,
                                                    Source = tweet.Source,
                                                    Text = tweet.Text,
                                                    TextAsHtml = tweet.TextAsHtml,
                                                    TextDecoded = tweet.TextDecoded,
                                                    TextClear = tweet.TextClear,
                                                    UserID = tweet.UserID,
                                                };

                    InsertPlace(tweet, sessionScope);
                    InsertHashTags(newTweet, sessionScope, tweet);
                    InsertMedias(sessionScope, newTweet, tweet);
                    InsertUrls(sessionScope, newTweet, tweet);

                    tweetRepository.Insert(newTweet);
                    tweetRepository.SaveChanges();

                    sessionScope.Complete();
                }
            }
        }

        private static void InsertUser(TwitterTweet tweet, ISessionScope sessionScope)
        {
            TwitterUser user = tweet.TwitterUser;
            IRepository<TwitterUser> userRepository = sessionScope.GetRepository<TwitterUser>();
            if (!userRepository.Query().Any(x => x.ID == user.ID))
            {
                TwitterUser newUser = new TwitterUser
                                          {
                                              CreateDate = user.CreateDate,
                                              Description = user.Description,
                                              ID = user.ID,
                                              LanguageID = user.LanguageID,
                                              Location = user.Location,
                                              Name = user.Name,
                                              ProfileImageUrl = user.ProfileImageUrl,
                                              ProfileImageUrlHttps = user.ProfileImageUrlHttps,
                                              ScreenName = user.ScreenName,
                                              Url = user.Url
                                          };

                InsertLanguage(sessionScope, newUser, user);

                userRepository.Insert(newUser);
                userRepository.SaveChanges();
            }
        }

        private static void InsertLanguage(ISessionScope sessionScope, TwitterUser newUser, TwitterUser user)
        {
            TwitterLanguage language = user.TwitterLanguage;
            if (language != null)
            {
                IRepository<TwitterLanguage> languageRepository = sessionScope.GetRepository<TwitterLanguage>();
                int? languageID = languageRepository.Query()
                    .Where(x => x.Code == language.Code)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefault();
                if (!languageID.HasValue)
                {
                    TwitterLanguage newLanguage = new TwitterLanguage
                                                      {
                                                          Code = language.Code
                                                      };
                    languageRepository.Insert(newLanguage);
                    languageRepository.SaveChanges();

                    newUser.LanguageID = newLanguage.ID;
                }
                else
                {
                    newUser.LanguageID = languageID.Value;
                }
            }
        }

        private static void InsertPlace(TwitterTweet tweet, ISessionScope sessionScope)
        {
            TwitterPlace twitterPlace = tweet.TwitterPlace;
            if (twitterPlace != null)
            {
                IRepository<TwitterPlace> placeRepository = sessionScope.GetRepository<TwitterPlace>();
                string placeID = placeRepository.Query().Where(x => x.ID == twitterPlace.ID).Select(x => x.ID).SingleOrDefault();
                if (placeID == null)
                {
                    TwitterPlace newTwitterPlace = new TwitterPlace
                                                       {
                                                           FullName = twitterPlace.FullName,
                                                           ID = twitterPlace.ID,
                                                           Name = twitterPlace.Name,
                                                           PlaceType = twitterPlace.PlaceType,
                                                           Url = twitterPlace.Url
                                                       };
                    InsertCountry(sessionScope, newTwitterPlace, twitterPlace);
                    placeRepository.Insert(newTwitterPlace);
                }
            }
        }

        private static void InsertCountry(ISessionScope sessionScope, TwitterPlace newTwitterPlace, TwitterPlace twitterPlace)
        {
            TwitterCountry twitterCountry = twitterPlace.TwitterCountry;
            if (twitterCountry != null)
            {
                IRepository<TwitterCountry> countryRepository = sessionScope.GetRepository<TwitterCountry>();
                int? countryID = countryRepository
                    .Query()
                    .Where(x => x.Code == twitterCountry.Code)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefault();
                if (countryID.HasValue)
                {
                    newTwitterPlace.CountryID = countryID.Value;
                }
                else
                {
                    TwitterCountry newTwitterCountry = new TwitterCountry
                                                           {
                                                               Code = twitterCountry.Code,
                                                               Name = twitterCountry.Name
                                                           };
                    countryRepository.Insert(newTwitterCountry);
                    countryRepository.SaveChanges();

                    newTwitterPlace.CountryID = newTwitterCountry.ID;
                }
            }
        }

        private void InsertUrls(ISessionScope sessionScope, TwitterTweet newTweet, TwitterTweet tweet)
        {
            foreach (TwitterUrl twitterUrl in tweet.TwitterUrls)
            {
                IRepository<TwitterUrl> urlRepository = sessionScope.GetRepository<TwitterUrl>();
                TwitterUrl currentUrl = urlRepository.Query().SingleOrDefault(x => x.ID == twitterUrl.ID);
                if (currentUrl != null)
                {
                    newTweet.TwitterUrls.Add(currentUrl);
                }
                else
                {
                    string siteUrl = twitterUrl.TwitterSite.SiteUrl;
                    IRepository<TwitterSite> siteRepository = sessionScope.GetRepository<TwitterSite>();
                    TwitterSite twitterSite = siteRepository.Query().SingleOrDefault(x => x.SiteUrl == siteUrl);
                    if (twitterSite == null)
                    {
                        twitterSite = new TwitterSite();
                        twitterSite.SiteUrl = siteUrl;
                        siteRepository.Insert(twitterSite);
                        siteRepository.SaveChanges();
                    }

                    twitterUrl.TwitterSite = twitterSite;
                    twitterUrl.SiteID = twitterSite.ID;
                    urlRepository.Insert(twitterUrl);
                    urlRepository.SaveChanges();

                    newTweet.TwitterUrls.Add(twitterUrl);
                }
            }
        }

        private static void InsertMedias(ISessionScope sessionScope, TwitterTweet newTweet, TwitterTweet tweet)
        {
            foreach (TwitterMedia twitterMedia in tweet.TwitterMedias)
            {
                IRepository<TwitterMedia> mediaRepository = sessionScope.GetRepository<TwitterMedia>();
                TwitterMedia currentMedia = mediaRepository.Query().SingleOrDefault(x => x.ID == twitterMedia.ID);
                if (currentMedia != null)
                {
                    newTweet.TwitterMedias.Add(currentMedia);
                }
                else
                {
                    mediaRepository.Insert(twitterMedia);
                    mediaRepository.SaveChanges();

                    newTweet.TwitterMedias.Add(twitterMedia);
                }
            }
        }

        private static void InsertHashTags(TwitterTweet newTweet, ISessionScope sessionScope, TwitterTweet tweet)
        {
            foreach (TwitterHashTag twitterHashTag in tweet.TwitterHashTags)
            {
                IRepository<TwitterHashTag> twitterRepository = sessionScope.GetRepository<TwitterHashTag>();
                TwitterHashTag currentHashTag = twitterRepository.Query().SingleOrDefault(x => x.Name == twitterHashTag.Name);
                if (currentHashTag != null)
                {
                    newTweet.TwitterHashTags.Add(currentHashTag);
                }
                else
                {
                    TwitterHashTag newHashTag = new TwitterHashTag
                                                    {
                                                        Name = twitterHashTag.Name
                                                    };
                    twitterRepository.Insert(newHashTag);
                    twitterRepository.SaveChanges();

                    newTweet.TwitterHashTags.Add(newHashTag);
                }
            }
        }
    }
}