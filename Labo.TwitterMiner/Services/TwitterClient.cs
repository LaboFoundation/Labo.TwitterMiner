namespace Labo.TwitterMiner.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Labo.TwitterMiner.Entity;
    using Labo.TwitterMiner.Model;
    using Labo.TwitterMiner.Services.Twitter.Exceptions;

    using TweetSharp;

    using TwitterHashTag = TweetSharp.TwitterHashTag;
    using TwitterMedia = TweetSharp.TwitterMedia;
    using TwitterPlace = TweetSharp.TwitterPlace;
    using TwitterSearchResult = Labo.TwitterMiner.Model.TwitterSearchResult;
    using TwitterUrl = TweetSharp.TwitterUrl;
    using TwitterUser = TweetSharp.TwitterUser;

    internal sealed class TwitterClient
    {
        private static TwitterService Connect()
        {
            TwitterService service = new TwitterService("IdEn0cIIK7lyibjJ32ESmGYqt", "QK6bClSAxchuoIjH1GiE8iWxUJNYbKE9FTA2koB21kwFCSQ2pK");
            service.AuthenticateWith("2897622959-tOigpkiEdMMR0oG1Nw7C5tkUEYRvXm1iqKfUGiM", "uVYFsIlh2Byv6EY5Q4Ii62MeJ2TRJFYFVc9OHXQEIaEli");
            return service;
        }

        private static TwitterService Connect(string resource, string actionName)
        {
            TwitterService service = Connect();
            TwitterRateLimitStatus limitStatus = GetLimitStatusLimit(service, resource, actionName);
            if (limitStatus != null && limitStatus.RemainingHits < 3)
            {
                throw new TwitterRateLimitReachedException(resource);
            }

            return service;
        }

        private static TwitterRateLimitStatus GetLimitStatusLimit(ITwitterService twitterService, string resource, string actionName)
        {
            GetRateLimitStatusOptions limitStatusOptions = new GetRateLimitStatusOptions { Resources = new[] { resource } };
            TwitterRateLimitStatusSummary limitStatusSummary = twitterService.GetRateLimitStatus(limitStatusOptions);
            return GetLimitStatusLimit(limitStatusSummary, resource, actionName);
        }

        private static TwitterRateLimitStatus GetLimitStatusLimit(TwitterRateLimitStatusSummary limitStatusSummary, string resource, string limitName)
        {
            if (limitStatusSummary.Resources == null || limitStatusSummary.Resources.Count == 0)
            {
                return null;
            }

            TwitterRateLimitStatus rateLimitStatus =
                limitStatusSummary.Resources
                    .Where(x => string.Compare(x.Name, resource, StringComparison.OrdinalIgnoreCase) == 0)
                    .Select(x => x.Limits.Where(y => string.Compare(y.Key, limitName, StringComparison.OrdinalIgnoreCase) == 0).Select(z => z.Value).SingleOrDefault())
                    .SingleOrDefault();

            if (rateLimitStatus != null)
            {
                return rateLimitStatus;
            }

            return null;
        }

        public TwitterSearchResult Search(TwitterSearchOptions searchOptions)
        {
            long? sinceID = searchOptions.SinceID;
            long? maxID = searchOptions.MaxID;

            TwitterService service = Connect("search", "/search/tweets");

            TweetSharp.TwitterSearchResult twitterSearchResult = service.Search(new SearchOptions
                {
                    Q = searchOptions.Q, 
                    Resulttype = TwitterSearchResultType.Recent,
                    Count = 100,
                    SinceId = sinceID,
                    MaxId = maxID
                });

            List<TwitterStatus> twitterStatuses = twitterSearchResult.Statuses.ToList();
            TwitterSearchResult searchResults = new TwitterSearchResult
            {
                MaxID = twitterStatuses.Select(x => x.Id).DefaultIfEmpty().Min(),
                SinceID = twitterStatuses.Select(x => x.Id).DefaultIfEmpty().Max(),
                ApiRateLimitRemainingCount = service.Response.RateLimitStatus.RemainingHits
            };

            for (int i = 0; i < twitterStatuses.Count; i++)
            {
                TwitterStatus twitterStatus = twitterStatuses[i];
                if (twitterStatus.Entities == null ||
                    ((twitterStatus.Entities.Urls == null || twitterStatus.Entities.Urls.Count == 0) &&
                    (twitterStatus.Entities.Media == null || twitterStatus.Entities.Media.Count == 0)))
                {
                    continue;
                }

                TwitterUser twitterUser = twitterStatus.User;
                TwitterTweet tweet = new TwitterTweet
                    {
                        ID = twitterStatus.Id,
                        InReplyToScreenName = twitterStatus.InReplyToScreenName,
                        InReplyToStatusId = twitterStatus.InReplyToStatusId,
                        InReplyToUserId = twitterStatus.InReplyToUserId,
                        RetweetCount = twitterStatus.RetweetCount,
                        Source = twitterStatus.Source,
                        UserID = twitterUser.Id,
                        Text = twitterStatus.Text,
                        TextAsHtml = twitterStatus.TextAsHtml,
                        TextDecoded = twitterStatus.TextDecoded,
                        CreateDate = twitterStatus.CreatedDate,
                        TwitterUser = new Entity.TwitterUser
                            {
                                CreateDate = twitterUser.CreatedDate,
                                Description = twitterUser.Description,
                                ID = twitterUser.Id,
                                Location = twitterUser.Location,
                                Name = twitterUser.Name,
                                ProfileImageUrl = twitterUser.ProfileImageUrl,
                                ProfileImageUrlHttps = twitterUser.ProfileImageUrlHttps,
                                ScreenName = twitterUser.ScreenName,
                                Url = twitterUser.Url
                            }
                    };

                AddTwitterEntities(twitterStatus, tweet);
                SetTwitterLanguage(twitterUser, tweet);
                SetTwitterGeoLocation(twitterStatus, tweet);
                SetTwitterPlace(twitterStatus, tweet);

                searchResults.Tweets.Add(tweet);
            }

            return searchResults;
        }

        private static void SetTwitterPlace(TwitterStatus twitterStatus, TwitterTweet tweet)
        {
            TwitterPlace twitterPlace = twitterStatus.Place;
            if (twitterPlace != null)
            {
                tweet.TwitterPlace = new Entity.TwitterPlace
                                         {
                                             TwitterCountry =
                                                 new TwitterCountry
                                                     {
                                                         Code = twitterPlace.CountryCode,
                                                         Name = twitterPlace.Country,
                                                     },
                                             FullName = twitterPlace.FullName,
                                             Name = twitterPlace.Name,
                                             ID = twitterPlace.Id,
                                             PlaceType = twitterPlace.PlaceType.ToString(),
                                             Url = twitterPlace.Url
                                         };
                tweet.PlaceID = twitterPlace.Id;
            }
        }

        private static void SetTwitterGeoLocation(TwitterStatus twitterStatus, TwitterTweet tweet)
        {
            TwitterGeoLocation twitterGeoLocation = twitterStatus.Location;
            if (twitterGeoLocation != null)
            {
                TwitterGeoLocation.GeoCoordinates geoCoordinates = twitterGeoLocation.Coordinates;
                if (geoCoordinates != null)
                {
                    tweet.Latitude = (decimal?)geoCoordinates.Latitude;
                    tweet.Longitude = (decimal?)geoCoordinates.Longitude;
                }
            }
        }

        private static void SetTwitterLanguage(TwitterUser twitterUser, TwitterTweet tweet)
        {
            if (!string.IsNullOrWhiteSpace(twitterUser.Language))
            {
                tweet.TwitterUser.TwitterLanguage = new TwitterLanguage { Code = twitterUser.Language };
            }
        }

        private static void AddTwitterEntities(TwitterStatus twitterStatus, TwitterTweet tweet)
        {
            TwitterEntities twitterEntities = twitterStatus.Entities;
            
            AddTwitterUrls(tweet, twitterEntities);
            AddTwitterHashtags(tweet, twitterEntities);
            AddTwitterMentions(tweet, twitterEntities);
            AddTwitterMedia(tweet, twitterEntities);
        }

        private static void AddTwitterMedia(TwitterTweet tweet, TwitterEntities twitterEntities)
        {
            IList<TwitterMedia> twitterMediaList = twitterEntities.Media;
            for (int j = 0; j < twitterMediaList.Count; j++)
            {
                TwitterMedia twitterMedia = twitterMediaList[j];
                Entity.TwitterMedia media = new Entity.TwitterMedia
                                                {
                                                    ID = twitterMedia.Id,
                                                    DisplayUrl = twitterMedia.DisplayUrl,
                                                    ExpandedUrl = twitterMedia.ExpandedUrl,
                                                    MediaUrl = twitterMedia.MediaUrl,
                                                    MediaUrlHttps = twitterMedia.MediaUrlHttps,
                                                    TwitterMediaType = twitterMedia.MediaType.ToString(),
                                                    Url = twitterMedia.Url
                                                };
                if (twitterMedia.Sizes != null)
                {
                    if (twitterMedia.Sizes.Small != null)
                    {
                        media.TwitterMediaSizes.Add(
                            new Entity.TwitterMediaSize
                                {
                                    Height = twitterMedia.Sizes.Small.Height,
                                    Width = twitterMedia.Sizes.Small.Width,
                                    Resize = twitterMedia.Sizes.Small.Resize,
                                    SizeID = (int)TwitterMediaSizeEnum.Small
                                });
                    }

                    if (twitterMedia.Sizes.Medium != null)
                    {
                        media.TwitterMediaSizes.Add(
                            new Entity.TwitterMediaSize
                                {
                                    Height = twitterMedia.Sizes.Medium.Height,
                                    Width = twitterMedia.Sizes.Medium.Width,
                                    Resize = twitterMedia.Sizes.Medium.Resize,
                                    SizeID = (int)TwitterMediaSizeEnum.Medium
                                });
                    }

                    if (twitterMedia.Sizes.Large != null)
                    {
                        media.TwitterMediaSizes.Add(
                            new Entity.TwitterMediaSize
                                {
                                    Height = twitterMedia.Sizes.Large.Height,
                                    Width = twitterMedia.Sizes.Large.Width,
                                    Resize = twitterMedia.Sizes.Large.Resize,
                                    SizeID = (int)TwitterMediaSizeEnum.Large
                                });
                    }

                    if (twitterMedia.Sizes.Thumb != null)
                    {
                        media.TwitterMediaSizes.Add(
                            new Entity.TwitterMediaSize
                                {
                                    Height = twitterMedia.Sizes.Thumb.Height,
                                    Width = twitterMedia.Sizes.Thumb.Width,
                                    Resize = twitterMedia.Sizes.Thumb.Resize,
                                    SizeID = (int)TwitterMediaSizeEnum.Thumb
                                });
                    }
                }

                tweet.TwitterMedias.Add(media);
            }
        }

        private static void AddTwitterMentions(TwitterTweet tweet, TwitterEntities twitterEntities)
        {
            IList<TwitterMention> twitterMentions = twitterEntities.Mentions;
            for (int j = 0; j < twitterMentions.Count; j++)
            {
                TwitterMention twitterMention = twitterMentions[j];
                tweet.TwitterMentions.Add(new TwitterTweetMention { UserID = twitterMention.Id, TweetID = tweet.ID });
            }
        }

        private static void AddTwitterHashtags(TwitterTweet tweet, TwitterEntities twitterEntities)
        {
            IList<TwitterHashTag> twitterHashTags = twitterEntities.HashTags;
            for (int j = 0; j < twitterHashTags.Count; j++)
            {
                TwitterHashTag twitterHashTag = twitterHashTags[j];
                tweet.TwitterHashTags.Add(new Entity.TwitterHashTag { Name = twitterHashTag.Text });
            }
        }

        private static void AddTwitterUrls(TwitterTweet tweet, TwitterEntities twitterEntities)
        {
            IList<TwitterUrl> twitterUrls = twitterEntities.Urls;
            for (int j = 0; j < twitterUrls.Count; j++)
            {
                TwitterUrl twitterUrl = twitterUrls[j];
                tweet.TwitterUrls.Add(
                    new Entity.TwitterUrl { ExpandedValue = twitterUrl.ExpandedValue, Value = twitterUrl.Value });
            }
        }
    }
}
