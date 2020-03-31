using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using RestSharp;

using System;
using System.Net;

using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;
using UTTAF.Mobile.Services;
using UTTAF.Mobile.Services.Requests;
using UTTAF.Mobile.Util;

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
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += async () =>
            {
                IRestResponse response = await SessionService.SessionStartedTaskAsync(Attendee.SessionReference);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        CancelTimer();
                        await Application.Current.MainPage.DisplayAlert("Uruu!", "iniciou", "Ok");
                        break;

                    case HttpStatusCode.NotFound:
                        CancelTimer();
                        await Application.Current.MainPage.DisplayAlert("Ops!", response.Content.Replace("\"", string.Empty), "Ok");
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

        private void CancelTimer() => timer.Stop();
    }
}