﻿using System;
using System.ComponentModel.DataAnnotations;

using UTTAF.Dependencies.Enums;

namespace UTTAF.API.Models
{
	public class SessionModel
	{
		[Key]
		[Required]
		public string SessionReference { get; set; }

		[Required]
		public SessionStatusEnum SessionStatus { get; set; }

		[Required]
		public DateTime? SessionDate { get; set; }
	}
}