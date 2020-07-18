using Microsoft.Extensions.DependencyInjection;

using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using UTTAF.Dependencies.Clients.Helpers;
using UTTAF.Dependencies.Clients.ViewModels;
using UTTAF.Dependencies.Data.VOs;
using UTTAF.Dependencies.Enums;
using UTTAF.Desktop.Commands;
using UTTAF.Desktop.Services;
using UTTAF.Desktop.Services.Interfaces;
using UTTAF.Desktop.Views;
using UTTAF.Desktop.Views.DialogHost;

namespace UTTAF.Desktop.ViewModels
{
	public class ConfigureViewModel : ViewModel
	{
		private readonly SessionService _sessionService;
		private readonly IBarCodeService _barCodeService;

		private string __sessionReference;
		private object __qrCodeImg;
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

		public object QrCode
		{
			get => __qrCodeImg;
			set => Set(ref __qrCodeImg, value);
		}

		public string SessionReference
		{
			get => __sessionReference;
			set => Set(ref __sessionReference, value);
		}

		public ObservableCollection<AttendeeVO> Attendees { get; set; } = new ObservableCollection<AttendeeVO>();

		public ICommand CreateSessionCommand { get; private set; }
		public ICommand CancelSessionCreationCommand { get; private set; }
		public ICommand StartSessionCommand { get; private set; }
		public ICommand ContinueCommand { get; private set; }

		public ConfigureViewModel(SessionService sessionService, IBarCodeService barCodeService)
		{
			_sessionService = sessionService;
			_barCodeService = barCodeService;

			CancelSessionCreationCommand = new Command(async () => await CancelSessionCreation());
			StartSessionCommand = new Command(async () => await StartSession());
			CreateSessionCommand = new Command(async () => await CreateSession());
			ContinueCommand = new Command<ConfigureView>((x) => InitializeSession(x));

			Initialize();
		}

		private void Initialize()
		{
			_sessionService.CreatedSession((session, message) => Application.Current.Dispatcher.Invoke(() =>
			{
				DataHelper.AuthSession = session;

				StartCreateSessionVisibility = Visibility.Collapsed;
				NextCreateSessionVisibility = Visibility.Visible;
			}));

			_sessionService.AlreadyExistsSession(message => Application.Current.Dispatcher.Invoke(() =>
			{
				MessageBox.Show(message);
			}));

			_sessionService.NotCreatedSession(message => Application.Current.Dispatcher.Invoke(() =>
			{
				MessageBox.Show(message);
			}));

			_sessionService.UpdatedSessionStatus((session, message) => Application.Current.Dispatcher.Invoke(() =>
			{
				MessageBox.Show(message);

				((App)Application.Current).ServiceProvider.GetRequiredService<MainView>().Show();
				((App)Application.Current).ServiceProvider.GetRequiredService<ConfigureView>().Close();
			}));

			_sessionService.NotExistsThisSession(message => Application.Current.Dispatcher.Invoke(() =>
			{
				MessageBox.Show(message);
			}));

			_sessionService.NotUpdatedSessionStatus(message => Application.Current.Dispatcher.Invoke(() =>
			{
				MessageBox.Show(message);
			}));

			_sessionService.RemovedSession(message => Application.Current.Dispatcher.Invoke(() =>
			{
				MessageBox.Show(message);
				StartCreateSessionVisibility = Visibility.Visible;
				NextCreateSessionVisibility = Visibility.Collapsed;
				((App)Application.Current).ServiceProvider.GetRequiredService<ConfigureView>().CancelCreateSession.Command.Execute(null);
			}));
		}

		private async Task CreateSession()
		{
			await MaterialDesignThemes.Wpf.DialogHost.Show(new InputNewSessionNameView(), "CreateSessionDH", async (s, e) =>
			{
				if ((bool)e.Parameter == true)
				{
					var view = e.Session.Content as InputNewSessionNameView;
					string reference = view.Reference.Text;

					await _sessionService.CreateSessionAsync(new SessionVO { SessionReference = reference });
				}
			});
		}

		private async Task StartSession()
		{
			SessionVO currentSession = DataHelper.AuthSession;
			currentSession.SessionStatus = SessionStatusEnum.InProgress;

			await _sessionService.MarkSessionWithStartedAsync(currentSession);
		}

		private void InitializeSession(ConfigureView configureView)
		{
			Attendees.Clear();
			SessionReference = DataHelper.AuthSession.SessionReference;
			QrCode = _barCodeService.GenerateQrCode(SessionReference);
			configureView.NextCreateSession.Command.Execute(null);
		}

		private async Task CancelSessionCreation()
		{
			await _sessionService.DeleteSessionAsync(SessionReference);
		}
	}
}