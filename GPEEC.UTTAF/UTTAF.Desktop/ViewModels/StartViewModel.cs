using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using RestSharp;

using System.Net;
using System.Text.Json;
using System.Windows;

using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;
using UTTAF.Desktop.Services.Requests;
using UTTAF.Desktop.Views;
using UTTAF.Desktop.Views.DialogHost;

namespace UTTAF.Desktop.ViewModels
{
    internal class StartViewModel : ViewModelBase
    {
        public RelayCommand<StartView> CreateSessionCommand { get; private set; }

        public StartViewModel() => Init();

        private void Init() => CreateSessionCommand = new RelayCommand<StartView>(CreateSession);

        private void CreateSession(StartView startView)
        {
            MaterialDesignThemes.Wpf.DialogHost.Show(new InputNewSessionNameView(startView), "CreateSessionDH", async (s, e) =>
            {
                if ((bool)e.Parameter == true)
                {
                    var view = e.Session.Content as InputNewSessionNameView;
                    string reference = view.Reference.Text;
                    string password = view.Password.Password;

                    IRestResponse response = await InitializeSessionService.InitSessionTaskAsync(new AuthSessionModel { SessionReference = reference, SessionPassword = password });

                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        DataHelper.AuthSession = JsonSerializer.Deserialize<AuthSessionModel>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        startView.StartCreateSession.Visibility = Visibility.Collapsed;
                        startView.NextCreateSession.Visibility = Visibility.Visible;
                    }
                    else if (response.StatusCode == HttpStatusCode.Conflict)
                        MessageBox.Show(response.Content.Replace("\"", string.Empty));
                }
            });
        }
    }
}