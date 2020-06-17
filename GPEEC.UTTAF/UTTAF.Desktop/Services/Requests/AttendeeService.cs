﻿using Dependencies.Services;

using RestSharp;

using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;
using UTTAF.Dependencies.Helpers;

namespace UTTAF.Desktop.Services.Requests
{
	public class AttendeeService
	{
		public static async Task<IRestResponse> GetAttendeesTaskAsync(SessionVO model)
		{
			var request = new RequestService()
			{
				URL = DataHelper.URLBase,
				URN = "Attendee",
				Method = Method.GET
			};

			request.Parameters.Add(nameof(model.SessionReference), model.SessionReference);

			return await request.ExecuteTaskAsync();
		}
	}
}