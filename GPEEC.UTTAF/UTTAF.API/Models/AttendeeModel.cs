using System;
using System.ComponentModel.DataAnnotations;

namespace UTTAF.API.Models
{
	public class AttendeeModel
	{
		[Key]
		public Guid AttendeeId { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string SessionReference { get; set; }
	}
}