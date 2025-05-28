using Microsoft.EntityFrameworkCore;
using SchoolPortal.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Common.DTOs
{
    public class CreateStateDto : BaseEntity
    {
        public CreateStateDto()
        {
            Id = Guid.NewGuid();
            CreatedBy = new Guid("368BD1DF-42D6-4F05-9CA0-8DF22191B917");
            Status = "Create";
            StatusMessage = "State Added Successfully";
            CreatedDate = DateTime.UtcNow;
        }

        [Display(Name = "State name")]
        [StringLength(100)]
        [Unicode(false)]
        public string StateName { get; set; } = string.Empty;

        [Display(Name = "Country")]
        public Guid CountryId { get; set; }
    }

    public class UpdateStateDto : BaseEntity
    {
        public UpdateStateDto()
        {
            CreatedBy = new Guid("368BD1DF-42D6-4F05-9CA0-8DF22191B917");
            Status = "Modified";
            StatusMessage = "State Updated Successfully";
            CreatedDate = DateTime.UtcNow;
            ModifiedBy = new Guid("368BD1DF-42D6-4F05-9CA0-8DF22191B917");
            // ModifiedDate will be set in the service layer when actually updating
        }

        [Display(Name = "State name")]
        public string StateName { get; set; } = string.Empty;

        [Display(Name = "Country")]
        public Guid CountryId { get; set; }
    }

    public class ViewStateDto : BaseEntity
    {
        [Display(Name = "State name")]
        public string? StateName { get; set; }

        [Display(Name = "Country")]
        public Guid CountryId { get; set; }

        [Display(Name = "Country name")]
        public string? CountryName { get; set; }
    }
}
