﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SchoolPortal.Common.Models.dboSchema;


namespace SchoolPortal.Common.Models.dboSchema;

public partial class VehicleExpenseDetail
{
    [Key]
    public Guid Id { get; set; }

    public Guid VehicleMasterId { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string VehicleType { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ExpenseDetail { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string ExpenseDetailDescription { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpenseDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ExpenseAmount { get; set; }

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

