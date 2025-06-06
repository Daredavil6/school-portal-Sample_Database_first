﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolPortal.Common.Models;
using SchoolPortal.Common.Models.dboSchema;
using System;
using System.Collections.Generic;

namespace SchoolPortal.Common.Models.Configurations
{
    public partial class PrivilegeConfiguration : IEntityTypeConfiguration<Privilege>
    {
        public void Configure(EntityTypeBuilder<Privilege> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Privileg__3214EC07D1C93BB2");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue("INC");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Privilege> entity);
    }
}
