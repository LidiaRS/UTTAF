﻿using System;
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
				throw new Exception("Nao foi possivel fazer a conversao AttendeeVO -> AttendeeModel");

			if (!(await _attendeeRepository.AddAttendeeTaskAsync(newAttendeeModel) is AttendeeModel addedAttendeeModel))
				throw new Exception("Nao foi possivel adicionar o participante na sessao");

			return _attendeeConverter.Parse(addedAttendeeModel);
		}

		public async Task<AttendeeVO> FindByNameInSessionTaskAsync(AttendeeVO attendee)
		{
			if (!(_attendeeConverter.Parse(attendee) is AttendeeModel attendeeModel))
				throw new Exception("Nao foi possivel fazer a conversao AttendeeVO -> AttendeeModel");

			return _attendeeConverter.Parse(await _attendeeRepository.FindByNameInSessionTaskAsync(attendeeModel));
		}

		public async Task LeaveAtSessionAsync(AttendeeVO attendee)
		{
			if (!(_attendeeConverter.Parse(attendee) is AttendeeModel attendeeModel))
				throw new Exception("Nao foi possivel fazer a conversao AttendeeVO -> AttendeeModel");

			if (!(await _attendeeRepository.FindByNameInSessionTaskAsync(attendeeModel) is AttendeeModel currentAttendee))
				throw new Exception("Nao foi possivel pegar as informaçoes do participante");

			await _attendeeRepository.LeaveAttendeeTaskAsync(currentAttendee);
		}
	}
}