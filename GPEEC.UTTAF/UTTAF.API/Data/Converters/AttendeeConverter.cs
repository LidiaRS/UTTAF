using System.Collections.Generic;
using System.Linq;

using UTTAF.API.Data.Converters.Interfaces;
using UTTAF.API.Models;
using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.API.Data.Converters
{
	public class AttendeeConverter : IParser<AttendeeModel, AttendeeVO>, IParser<AttendeeVO, AttendeeModel>
	{
		public AttendeeModel Parse(AttendeeVO origin)
		{
			if (origin is null)
				return null;

			return new AttendeeModel
			{
				Id = origin.Id,
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
				Id = origin.Id,
				Name = origin.Name,
				SessionReference = origin.SessionReference
			};
		}

		public List<AttendeeModel> ParseList(List<AttendeeVO> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(x => Parse(x)).ToList()
			};
		}

		public List<AttendeeVO> ParseList(List<AttendeeModel> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(x => Parse(x)).ToList()
			};
		}
	}
}