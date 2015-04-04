//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Labo.TwitterMiner.Video.Data
{
    #pragma warning disable 1573
    using System;
    using System.Collections.Generic;
    
    public partial class Content
    {
        public Content()
        {
            this.ContentTags = new HashSet<ContentTag>();
        }
    
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string UserFriendlyUrl { get; set; }
        public string ViewPath { get; set; }
        public Nullable<long> ViewCount { get; set; }
        public Nullable<double> Rating { get; set; }
        public Nullable<long> VoteCount { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<bool> IsPublished { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual ContentCategory ContentCategory { get; set; }
        public virtual VideoContent VideoContent { get; set; }
        public virtual ICollection<ContentTag> ContentTags { get; set; }
    }
}