using RestSharp;

using System;
using System.Net;
using System.Windows.Input;

using UTTAF.Dependencies.Clients;
using UTTAF.Dependencies.Clients.Helpers;
using UTTAF.Dependencies.Data.VOs;
using UTTAF.Mobile.Services;
using UTTAF.Mobile.Services.Requests;
using UTTAF.Mobile.Util;
using UTTAF.Mobile.Views;

using Xamarin.Forms;

namespace UTTAF.Mobile.ViewModels
{
	public class JoinedSessionViewModel : ViewModelBase
	{
		private Timer timer;

		private AttendeeVO __attendee;

		public AttendeeVO Attendee
		{
			get => __attendee;
			set => Set(ref __attendee, value);
		}

		public ICommand ExitSessionCommand { get; private set; }

		public JoinedSessionViewModel() => Init();

		private void Init()
		{
			Attendee = DataHelper.Attendee;

			timer = new Timer()
			{
				Interval = TimeSpan.FromSeconds(1)
			};

			timer.Tick += async () =>
			{
				IRestResponse response = await SessionService.SessionStartedTaskAsync(Attendee.SessionReference);
				switch (response.StatusCode)
				{
					case HttpStatusCode.OK:
						CancelTimer();
						Application.Current.MainPage = new MovingRobotView();
						break;

					case HttpStatusCode.NotFound:
						CancelTimer();
						await Application.Current.MainPage.DisplayAlert("Ops!", response.Content.Replace("\"", string.Empty), "Ok");
						break;
				}
			};

			timer.Start();

			//commands
			ExitSessionCommand = new Command(ExitSession);
		}

		private async void ExitSession()
		{
			if (await ExitSessionService.ExitTaskAsync(Attendee))
				CancelTimer();
		}

		private void CancelTimer() => timer.Stop();
	}
}