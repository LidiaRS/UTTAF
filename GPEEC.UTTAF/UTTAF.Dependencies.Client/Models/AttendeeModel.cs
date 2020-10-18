using System;

using UTTAF.Dependencies.Clients.Utils;

namespace UTTAF.Dependencies.Clients.Models
{
	public class AttendeeModel : PropertyNotifier
	{
		private Guid __attendeeId;
		private string __name;
		private string __sessionReference;

		public Guid AttendeeId
		{
			get => __attendeeId;
			set => Set(ref __attendeeId, value);
		}

		public string Name
		{
			get => __name;
			set => Set(ref __name, value);
		}

		public string SessionReference
		{
			get => __sessionReference;
			set => Set(ref __sessionReference, value);
		}
	}
}