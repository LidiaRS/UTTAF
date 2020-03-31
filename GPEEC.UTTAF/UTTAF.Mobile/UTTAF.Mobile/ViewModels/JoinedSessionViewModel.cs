using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using RestSharp;

using System.Net;
using System.Timers;

using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;
using UTTAF.Mobile.Services;
using UTTAF.Mobile.Services.Requests;

using Xamarin.Forms;

namespace UTTAF.Mobile.ViewModels
{
    internal class JoinedSessionViewModel : ViewModelBase
    {
        private Timer timer;
        private AttendeeModel __attendee;

        public AttendeeModel Attendee
        {
            get => __attendee;
            set => Set(ref __attendee, value);
        }

        public RelayCommand ExitSessionCommand { get; private set; }

        public JoinedSessionViewModel() => Init();

        private void Init()
        {
            Attendee = DataHelper.Attendee;

            timer = new Timer()
            {
                Interval = 1000,
                AutoReset = true
            };
            timer.Elapsed += async (sender, e) =>
            {
                IRestResponse response = await SessionService.SessionStartedTaskAsync(Attendee.SessionReference);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        await Application.Current.MainPage.DisplayAlert("Uruh!", "iniciou", "Ok");
                        CancelTimer();
                        break;

                    case HttpStatusCode.BadRequest:
                        await Application.Current.MainPage.DisplayAlert("Ops!", response.Content.Replace("\"", string.Empty), "Ok");
                        break;

                    case HttpStatusCode.NotFound:
                        await Application.Current.MainPage.DisplayAlert("Ops!", response.Content.Replace("\"", string.Empty), "Ok");
                        CancelTimer();
                        break;
                }
            };

            timer.Start();

            //commands
            ExitSessionCommand = new RelayCommand(ExitSession);
        }

        private async void ExitSession()
        {
            if (await ExitSessionService.ExitTaskAsync(Attendee))
                CancelTimer();
        }

        private void CancelTimer()
        {
            timer.Enabled = false;
            timer.Stop();
            timer.Dispose();
        }
    }
}