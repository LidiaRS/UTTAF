using RestSharp;

using System.Net;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;
using UTTAF.Desktop.Services.Requests;
using UTTAF.Desktop.Views.DialogHost;

namespace UTTAF.Desktop.Views
{
    public partial class StartView : UserControl
    {
        public StartView()
        {
            InitializeComponent();
            DataContext = (Application.Current.MainWindow as ConfigureView).DataContext;
        }

        private void CreateSession(object sender, RoutedEventArgs e)
        {
            MaterialDesignThemes.Wpf.DialogHost.Show(new InputNewSessionNameView(this), "CreateSessionDH", async (s, e) =>
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

                        StartCreateSession.Visibility = Visibility.Collapsed;
                        NextCreateSession.Visibility = Visibility.Visible;
                    }
                    else if (response.StatusCode == HttpStatusCode.Conflict)
                        MessageBox.Show(response.Content.Replace("\"", string.Empty));
                }
            });
        }
    }
}