using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;
using UTTAF.Mobile.Services;

namespace UTTAF.Mobile.ViewModels
{
    internal class JoinedSessionViewModel : ViewModelBase
    {
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

            //commands
            ExitSessionCommand = new RelayCommand(ExitSession);
        }

        private async void ExitSession() => await ExitSessionService.ExitAsync(Attendee);
    }
}