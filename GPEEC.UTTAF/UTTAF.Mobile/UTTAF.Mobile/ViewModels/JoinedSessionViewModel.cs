using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace UTTAF.Mobile.ViewModels
{
    internal class JoinedSessionViewModel : ViewModelBase
    {
        public RelayCommand ExitSessionCommand { get; private set; }

        public JoinedSessionViewModel() => Init();

        private void Init()
        {
            //commands
            ExitSessionCommand = new RelayCommand(ExitSession);
        }

        private async void ExitSession()
        {
            //IRestResponse response = await AttendeeService.JoinAtSessionTaskAsync()
        }
    }
}