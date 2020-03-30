﻿using System.Collections.Generic;
using System.Threading.Tasks;

using UTTAF.Dependencies.Models;

namespace UTTAF.API.Repository.Interfaces
{
    public interface IAttendeeRepository
    {
        Task<bool> AddAttendeeTaskAsync(AttendeeModel attendee);

        Task<IEnumerable<AttendeeModel>> GetAttendersTaskAsync(string reference);

        Task<bool> ClearAttendeersTaskAsync(AuthSessionModel model);
    }
}