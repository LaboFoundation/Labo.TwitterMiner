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
    
    internal partial class ContentTag_Mapping : EntityTypeConfiguration<ContentTag>
    {
        public ContentTag_Mapping()
        {					
    		this.HasKey(t => t.ID);		
    		this.ToTable("ContentTag");
    		this.Property(t => t.ID).HasColumnName("ID");
    		this.Property(t => t.Name).HasColumnName("Name").IsRequired().IsUnicode(false).HasMaxLength(150);
    	}
    }
}
