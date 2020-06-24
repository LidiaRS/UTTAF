using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UTTAF.API.Business.Interfaces;
using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.API.Business
{
	public class AttendeeBusiness : IAttendeeBusiness
	{
		public Task<bool> ExistsAtSessionByNameTaskAsync(AttendeeVO attendee)
		{
			throw new NotImplementedException();
		}

		public Task<AttendeeVO> JoinAtSessionTaskAsync(AttendeeVO newAttendee)
		{
			throw new NotImplementedException();
		}

		public Task LeaveAtSessionAsync(AttendeeVO attendee)
		{
			throw new NotImplementedException();
		}
	}
}
