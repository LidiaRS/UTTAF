using UTTAF.API.Models;
using UTTAF.Dependencies.Data.Converters.Interfaces;
using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.API.Data.Converters
{
	public class RobotConverter : IParser<RobotModel, RobotVO>, IParser<RobotVO, RobotModel>
	{
		public RobotModel Parse(RobotVO origin)
		{
			if (origin is null)
				return null;

			return new RobotModel
			{
				DataOperation = origin.DataOperation,
				RobotId = origin.RobotId,
				RobotStatus = origin.RobotStatus,
				SessionReference = origin.SessionReference
			};
		}

		public RobotVO Parse(RobotModel origin)
		{
			if (origin is null)
				return null;

			return new RobotVO
			{
				RobotId = origin.RobotId,
				SessionReference = origin.SessionReference,
				RobotStatus = origin.RobotStatus,
				DataOperation = origin.DataOperation
			};
		}
	}
}