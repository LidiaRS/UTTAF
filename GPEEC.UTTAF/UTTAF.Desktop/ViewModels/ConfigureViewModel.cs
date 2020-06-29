using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using UTTAF.Dependencies.Clients;
using UTTAF.Dependencies.Clients.Helpers;
using UTTAF.Dependencies.Data.VOs;
using UTTAF.Dependencies.Enums;
using UTTAF.Desktop.Commands;
using UTTAF.Desktop.Services;
using UTTAF.Desktop.Services.Interfaces;
using UTTAF.Desktop.Views;
using UTTAF.Desktop.Views.DialogHost;

namespace UTTAF.Desktop.ViewModels
{
	public class ConfigureViewModel : ViewModelBase
	{
		private MainView mainView;

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

		public ConfigureViewModel(SessionService sessionService, MainView mainView, IBarCodeService barCodeService)
		{
			_sessionService = sessionService;
			this.mainView = mainView;
			_barCodeService = barCodeService;

			CancelSessionCreationCommand = new Command<ConfigureView>(async x => await CancelSessionCreation(x));
			StartSessionCommand = new Command<ConfigureView>(async x => await StartSession(x));
			CreateSessionCommand = new Command<ConfigureView>(async x => await CreateSession(x));
			ContinueCommand = new Command<ConfigureView>(async (x) => await InitSessionAsync(x));

			Initialize();
		}

		private void Initialize()
		{
			_sessionService.CreatedSession((session, message) =>
			{
				DataHelper.AuthSession = session;

				StartCreateSessionVisibility = Visibility.Collapsed;
				NextCreateSessionVisibility = Visibility.Visible;
			});

			_sessionService.AlreadyExistsSession(message =>
			{
				MessageBox.Show(message);
			});

			_sessionService.NotCreatedSession(message =>
			{
				MessageBox.Show(message);
			});

			_sessionService.UpdatedSessionStatus((_, message) =>
			{
				MessageBox.Show(message);

				mainView.Show();
			});

			_sessionService.NotExistsThisSession(message =>
			{
				MessageBox.Show(message);
			});

			_sessionService.NotUpdatedSessionStatus(message =>
			{
				MessageBox.Show(message);
			});

			_sessionService.RemovedSession(message =>
			{
				MessageBox.Show(message);
			});
		}

		private async Task CreateSession(ConfigureView configureView)
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

		private async Task StartSession(ConfigureView configureView)
		{
			SessionVO currentSession = DataHelper.AuthSession;
			currentSession.SessionStatus = SessionStatusEnum.InProgress;

			await _sessionService.MarkSessionWithStartedAsync(currentSession);
			//if (await _startSessionService.StartSessionAsync(timer))
			//	configureView.Close();
		}

		private async Task InitSessionAsync(ConfigureView configureView)
		{
			Attendees.Clear();
			SessionReference = DataHelper.AuthSession.SessionReference;
			QrCode = _barCodeService.GenerateQrCodeTaskAsync(SessionReference);
			configureView.NextCreateSession.Command.Execute(null);

			//timer = new DispatcherTimer()
			//{
			//	Interval = new TimeSpan(0, 0, 0, 0, 500)
			//};
			//timer.Tick += async (sender, e) =>
			//{
			//	IRestResponse response = await AttendeeRequestService.GetAttendeesTaskAsync(DataHelper.AuthSession);

			//	if (response.StatusCode == HttpStatusCode.OK)
			//	{
			//		List<AttendeeVO> attendees = JsonSerializer.Deserialize<List<AttendeeVO>>(response.Content, new JsonSerializerOptions
			//		{
			//			PropertyNameCaseInsensitive = true
			//		});

			//		Attendees.ToList().ForEach(att =>
			//		{
			//			if (!attendees.Any(x => x.Name == att.Name))
			//				Attendees.Remove(att);
			//		});

			//		attendees.ForEach(att =>
			//		{
			//			if (!Attendees.Any(x => x.Name == att.Name))
			//				Attendees.Add(att);
			//		});
			//	}
			//};

			//timer.Start();
		}

		private async Task CancelSessionCreation(ConfigureView configureView)
		{
			await _sessionService.DeleteSessionAsync(SessionReference);
			//IRestResponse response = await _sessionService.DeleteSessionTaskAsync(DataHelper.AuthSession);

			//if (response.StatusCode == HttpStatusCode.OK)
			//	configureView.CancelSession();
			//else if (response.StatusCode == HttpStatusCode.NotFound)
			//	MessageBox.Show(response.Content.Replace("\"", string.Empty));

			//timer.Stop();
		}
	}
}