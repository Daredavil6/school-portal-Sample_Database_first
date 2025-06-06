﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SchoolPortal.Common.Models.dboSchema;


namespace SchoolPortal.Common.Models.dboSchema;

[Table("SMSTaskHistory")]
public partial class SMSTaskHistory
{
    [Key]
    public Guid Id { get; set; }

    public Guid TaskId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime SentDate { get; set; }

    public Guid NotificationReveiverId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NotificationReceiver { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string SendType { get; set; }

    public Guid? StudentGUID { get; set; }

    public Guid? ParentId { get; set; }

    public Guid? TeacherId { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Email { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Phone { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Description { get; set; }

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

    [StringLength(50)]
    [Unicode(false)]
    public string Status { get; set; }

    [StringLength(255)]
    public string StatusMessage { get; set; }
}

