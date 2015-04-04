namespace Labo.TwitterMiner.Data
{
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Labo.TwitterMiner.Data.Mapping;
    using Labo.TwitterMiner.Entity;

    public sealed class TwitterMinerDbContext : DbContext
    {
        static TwitterMinerDbContext()
        {
            Database.SetInitializer<TwitterMinerDbContext>(null);
        }

        public TwitterMinerDbContext()
            : base("name=TwitterMinerDbContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public TwitterMinerDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public TwitterMinerDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        public TwitterMinerDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public TwitterMinerDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            modelBuilder.Configurations.Add(new TwitterCountryMapping());
            modelBuilder.Configurations.Add(new TwitterCrawlHistoryMapping());
            modelBuilder.Configurations.Add(new TwitterHashTagMapping());
            modelBuilder.Configurations.Add(new TwitterLanguageMapping());
            modelBuilder.Configurations.Add(new TwitterMediaMapping());
            modelBuilder.Configurations.Add(new TwitterMediaSizeMapping());
            modelBuilder.Configurations.Add(new TwitterPlaceMapping());
            modelBuilder.Configurations.Add(new TwitterSiteMapping());
            modelBuilder.Configurations.Add(new TwitterTweetMapping());
            modelBuilder.Configurations.Add(new TwitterTweetMentionMapping());
            modelBuilder.Configurations.Add(new TwitterUrlMapping());
            modelBuilder.Configurations.Add(new TwitterUserMapping());
        }

        public DbSet<TwitterCountry> TwitterCountries { get; set; }

        public DbSet<TwitterHashTag> TwitterHashTags { get; set; }

        public DbSet<TwitterLanguage> TwitterLanguages { get; set; }

        public DbSet<TwitterMedia> TwitterMedias { get; set; }

        public DbSet<TwitterMediaSize> TwitterMediaSizes { get; set; }

        public DbSet<TwitterPlace> TwitterPlaces { get; set; }

        public DbSet<TwitterTweet> TwitterTweets { get; set; }

        public DbSet<TwitterUrl> TwitterUrls { get; set; }

        public DbSet<TwitterUser> TwitterUsers { get; set; }

        public DbSet<TwitterCrawlHistory> TwitterCrawlHistories { get; set; }

        public DbSet<TwitterTweetMention> TwitterTweetMentions { get; set; }

        public DbSet<TwitterSite> TwitterSites { get; set; }
    }
}
