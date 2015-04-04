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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.Infrastructure;
    
    internal partial class Content_Mapping : EntityTypeConfiguration<Content>
    {
        public Content_Mapping()
        {					
    		this.HasKey(t => t.ID);		
    		this.ToTable("Content");
    		this.Property(t => t.ID).HasColumnName("ID");
    		this.Property(t => t.Title).HasColumnName("Title").IsRequired().IsUnicode(false).HasMaxLength(80);
    		this.Property(t => t.Description).HasColumnName("Description");
    		this.Property(t => t.CategoryID).HasColumnName("CategoryID");
    		this.Property(t => t.MetaDescription).HasColumnName("MetaDescription").IsUnicode(false).HasMaxLength(500);
    		this.Property(t => t.MetaKeywords).HasColumnName("MetaKeywords").IsUnicode(false).HasMaxLength(500);
    		this.Property(t => t.UserFriendlyUrl).HasColumnName("UserFriendlyUrl").IsRequired().IsUnicode(false).HasMaxLength(255);
    		this.Property(t => t.ViewPath).HasColumnName("ViewPath").IsUnicode(false).HasMaxLength(500);
    		this.Property(t => t.ViewCount).HasColumnName("ViewCount");
    		this.Property(t => t.Rating).HasColumnName("Rating");
    		this.Property(t => t.VoteCount).HasColumnName("VoteCount");
    		this.Property(t => t.CreateDate).HasColumnName("CreateDate");
    		this.Property(t => t.IsPublished).HasColumnName("IsPublished");
    		this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
    		this.HasOptional(t => t.ContentCategory).WithMany(t => t.Contents).HasForeignKey(d => d.CategoryID);
    		this.HasMany(t => t.ContentTags).WithMany(t => t.Contents)
    			.Map(m => 
    			{
    				m.ToTable("ContentToTag");
    				m.MapLeftKey("ContentID");
    				m.MapRightKey("TagID");
    			});
    	}
    }
}
