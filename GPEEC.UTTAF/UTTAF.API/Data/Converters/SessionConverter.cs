﻿using System.Collections.Generic;
using System.Linq;

using UTTAF.API.Data.Converters.Interfaces;
using UTTAF.API.Models;
using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.API.Data.Converters
{
	public class SessionConverter : IParser<SessionVO, SessionModel>, IParser<SessionModel, SessionVO>
	{
		public SessionVO Parse(SessionModel origin)
		{
			if (origin is null)
				return null;

			return new SessionVO
			{
				SessionDate = origin.SessionDate,
				SessionReference = origin.SessionReference,
				SessionStatus = origin.SessionStatus
			};
		}

		public SessionModel Parse(SessionVO origin)
		{
			if (origin is null)
				return null;

			return new SessionModel
			{
				SessionStatus = origin.SessionStatus,
				SessionReference = origin.SessionReference,
				SessionDate = origin.SessionDate
			};
		}

		public List<SessionVO> ParseList(List<SessionModel> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(x => Parse(x)).ToList()
			};
		}

		public List<SessionModel> ParseList(List<SessionVO> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(x => Parse(x)).ToList()
			};
		}
	}
}