using SchoolPortal.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Common.DTOs
{
	public class CreateSchoolDto : BaseEntity
	{
		public string SchoolName { get; set; } = string.Empty;

		// These can be set from context/session in your service layer
		public Guid? CreatedBy { get; set; }
	}

	public class UpdateSchoolDto
	{
		public Guid Id { get; set; }              // Required for identifying the School to update
		public string SchoolName { get; set; } = string.Empty;

		// These can be set from context/session in your service layer
		public Guid? ModifiedBy { get; set; }
	}

	public class ViewSchoolDto
	{
		public Guid Id { get; set; }
		public string? SchoolName { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedDate { get; set; }
		public Guid? CreatedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public Guid? ModifiedBy { get; set; }
		public string Status { get; set; }
		public string? StatusMessage { get; set; }
	}
}
