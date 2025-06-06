﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolPortal.Common.Models;
using SchoolPortal.Common.Models.dboSchema;
using System;
using System.Collections.Generic;

namespace SchoolPortal.Common.Models.Configurations
{
    public partial class EmpLeaveDetailsHistoryConfiguration : IEntityTypeConfiguration<EmpLeaveDetailsHistory>
    {
        public void Configure(EntityTypeBuilder<EmpLeaveDetailsHistory> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Status).HasDefaultValue("INC");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<EmpLeaveDetailsHistory> entity);
    }
}
