﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SchoolPortal.Common.Models.dboSchema;


namespace SchoolPortal.Common.Models.dboSchema;

[Table("StudentAttendanceDetailsHistory")]
public partial class StudentAttendanceDetailsHistory
{
    [Key]
    public Guid Id { get; set; }

    public Guid StudentGUID { get; set; }

    public Guid ClassId { get; set; }

    public Guid SectionId { get; set; }

    public int? Month { get; set; }

    public int? Year { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    public int? Reason_Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Time { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Status { get; set; }

    [StringLength(255)]
    public string StatusMessage { get; set; }
}

