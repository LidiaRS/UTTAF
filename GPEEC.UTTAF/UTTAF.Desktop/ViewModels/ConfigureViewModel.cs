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
	public class ConfigureViewModel : ViewModelBase
	{
		private readonly SessionService _sessionService;
		private Visibility __startCreateSessionVisibility = Visibility.Visible;
		private Visibility __nextCreateSessionVisibility = Visibility.Collapsed;

		public Visibility StartCreateSessionVisibility
		{
			get => __startCreateSessionVisibility;
			set => Set(ref __startCreateSessionVisibility, value);
		}

		public Visibility NextCreateSessionVisibility
		{
			get => __nextCreateSessionVisibility;
			set => Set(ref __nextCreateSessionVisibility, value);
		}

		public RelayCommand<ConfigureView> CreateSessionCommand { get; private set; }

		public ConfigureViewModel(SessionService sessionService)
		{
			_sessionService = sessionService;

			CreateSessionCommand = new RelayCommand<ConfigureView>(CreateSession);
		}

		private void CreateSession(ConfigureView configureView)
		{
			MaterialDesignThemes.Wpf.DialogHost.Show(new InputNewSessionNameView(configureView), "CreateSessionDH", async (s, e) =>
			{
				if ((bool) e.Parameter == true)
				{
					var view = e.Session.Content as InputNewSessionNameView;
					string reference = view.Reference.Text;
					string password = view.Password.Password;

					IRestResponse response = await _sessionService.InitSessionTaskAsync(new AuthSessionModel { SessionReference = reference, SessionPassword = password });

					if (response.StatusCode == HttpStatusCode.Created)
					{
						DataHelper.AuthSession = JsonSerializer.Deserialize<AuthSessionModel>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
						DataHelper.AuthSession.SessionPassword = password;

						StartCreateSessionVisibility = Visibility.Collapsed;
						NextCreateSessionVisibility = Visibility.Visible;
					}
					else if (response.StatusCode == HttpStatusCode.Conflict)
						MessageBox.Show(response.Content.Replace("\"", string.Empty));
				}
			});
		}
	}
}