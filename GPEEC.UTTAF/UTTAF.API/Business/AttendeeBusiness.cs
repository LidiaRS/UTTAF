using System.Threading.Tasks;

using UTTAF.API.Business.Interfaces;
using UTTAF.API.Data.Converters;
using UTTAF.API.Models;
using UTTAF.API.Repository.Interfaces;
using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.API.Business
{
	public class AttendeeBusiness : IAttendeeBusiness
	{
		private readonly IAttendeeRepository _attendeeRepository;
		private readonly AttendeeConverter _attendeeConverter;

		public AttendeeBusiness(AttendeeConverter attendeeConverter, IAttendeeRepository attendeeRepository)
		{
			_attendeeRepository = attendeeRepository;
			_attendeeConverter = attendeeConverter;
		}

		public async Task<AttendeeVO> JoinAtSessionTaskAsync(AttendeeVO newAttendee)
		{
			if (!(_attendeeConverter.Parse(newAttendee) is AttendeeModel newAttendeeModel))
				return null;

			if (!(await _attendeeRepository.AddAttendeeTaskAsync(newAttendeeModel) is AttendeeModel addedAttendeeModel))
				return null;

			return _attendeeConverter.Parse(addedAttendeeModel);
		}

		public async Task<AttendeeVO> FindByNameTaskAsync(AttendeeVO attendee)
		{
			if (!(_attendeeConverter.Parse(attendee) is AttendeeModel attendeeModel))
				return null;

			return _attendeeConverter.Parse(await _attendeeRepository.FindByNameTaskAsync(attendeeModel));
		}

		public async Task LeaveAtSessionAsync(AttendeeVO attendee)
		{
			if (!(_attendeeConverter.Parse(attendee) is AttendeeModel attendeeModel))
				return;

			await _attendeeRepository.LeaveAttendeeTaskAsync(attendeeModel);
		}
	}
}