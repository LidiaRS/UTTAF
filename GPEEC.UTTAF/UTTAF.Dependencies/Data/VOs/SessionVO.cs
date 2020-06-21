using System;

using UTTAF.Dependencies.Enums;

namespace UTTAF.Dependencies.Data.VOs
{
	public class SessionVO
	{
		public string SessionReference { get; set; }

		public SessionStatusEnum SessionStatus { get; set; }

		public DateTime SessionDate { get; set; }
	}
}