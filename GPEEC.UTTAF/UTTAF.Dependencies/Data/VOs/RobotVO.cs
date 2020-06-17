using System;

using UTTAF.Dependencies.Enums;

namespace UTTAF.Dependencies.Data.VOs
{
	public class RobotVO
	{
		public Guid RobotId { get; set; }

		public string SessionReference { get; set; }

		public RobotStatusEnum RobotStatus { get; set; }

		public DateTime DataOperation { get; set; }
	}
}