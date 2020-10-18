using UTTAF.Dependencies.Clients.Models;
using UTTAF.Dependencies.Data.Converters.Interfaces;
using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.Dependencies.Clients.Data.Converters
{
	public class AttendeeConverter : IParser<AttendeeModel, AttendeeVO>, IParser<AttendeeVO, AttendeeModel>
	{
		public AttendeeModel Parse(AttendeeVO origin)
		{
			if (origin is null)
				return null;

			return new AttendeeModel
			{
				AttendeeId = origin.AttendeeId,
				Name = origin.Name,
				SessionReference = origin.SessionReference
			};
		}

		public AttendeeVO Parse(AttendeeModel origin)
		{
			if (origin is null)
				return null;

			return new AttendeeVO
			{
				AttendeeId = origin.AttendeeId,
				Name = origin.Name,
				SessionReference = origin.SessionReference
			};
		}
	}
}