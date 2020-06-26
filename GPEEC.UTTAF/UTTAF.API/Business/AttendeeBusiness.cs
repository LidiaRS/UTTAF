using System;
using System.Threading.Tasks;

using UTTAF.API.Business.Interfaces;
using UTTAF.API.Data.Converters;
using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.API.Business
{
	public class AttendeeBusiness : IAttendeeBusiness
	{
		private readonly AttendeeConverter _attendeeConverter;

		public AttendeeBusiness(AttendeeConverter attendeeConverter)
		{
			_attendeeConverter = attendeeConverter;
		}

		public Task<AttendeeVO> JoinAtSessionTaskAsync(AttendeeVO newAttendee)
		{
			throw new NotImplementedException();
		}

		public Task<AttendeeVO> FindByNameTaskAsync(AttendeeVO attendee)
		{
			throw new NotImplementedException();
		}

		public Task LeaveAtSessionAsync(AttendeeVO attendee)
		{
			throw new NotImplementedException();
		}
	}
}