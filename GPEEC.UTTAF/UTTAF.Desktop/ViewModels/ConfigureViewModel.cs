using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using QRCoder;

using RestSharp;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

using UTTAF.Dependencies.Data.VOs;
using UTTAF.Dependencies.Helpers;
using UTTAF.Desktop.Services;
using UTTAF.Desktop.Services.Requests;
using UTTAF.Desktop.Views;
using UTTAF.Desktop.Views.DialogHost;

namespace UTTAF.Desktop.ViewModels
{
	public class ConfigureViewModel : ViewModelBase
	{
		private readonly SessionService _sessionService;
		private readonly IStartSessionService _startSessionService;

		private DispatcherTimer timer;
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
		public RelayCommand<ConfigureView> CreateSessionCommand { get; private set; }

		public RelayCommand<ConfigureView> CancelSessionCreationCommand { get; private set; }
		public RelayCommand<ConfigureView> StartSessionCommand { get; private set; }

		public ConfigureViewModel(SessionService sessionService, IStartSessionService startSessionService)
		{
			_sessionService = sessionService;
			_startSessionService = startSessionService;

			CancelSessionCreationCommand = new RelayCommand<ConfigureView>(async x => await CancelSessionCreation(x));
			StartSessionCommand = new RelayCommand<ConfigureView>(async x => await StartSession(x));
			CreateSessionCommand = new RelayCommand<ConfigureView>(async x => await CreateSession(x));
		}

		private async Task CreateSession(ConfigureView configureView)
		{
			await MaterialDesignThemes.Wpf.DialogHost.Show(new InputNewSessionNameView(configureView), "CreateSessionDH", async (s, e) =>
			{
				if ((bool) e.Parameter == true)
				{
					var view = e.Session.Content as InputNewSessionNameView;
					string reference = view.Reference.Text;
					string password = view.Password.Password;

					IRestResponse response = await _sessionService.InitSessionTaskAsync(new AuthSessionVO { SessionReference = reference, SessionPassword = password });

					if (response.StatusCode == HttpStatusCode.Created)
					{
						DataHelper.AuthSession = JsonSerializer.Deserialize<AuthSessionVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
						DataHelper.AuthSession.SessionPassword = password;

						StartCreateSessionVisibility = Visibility.Collapsed;
						NextCreateSessionVisibility = Visibility.Visible;
					}
					else if (response.StatusCode == HttpStatusCode.Conflict)
						MessageBox.Show(response.Content.Replace("\"", string.Empty));
				}
			});
		}

		private async Task StartSession(ConfigureView configureView)
		{
			if (await _startSessionService.StartSessionAsync(timer))
				configureView.Close();
		}

		public void InitSession()
		{
			Attendees.Clear();
			SessionReference = DataHelper.AuthSession.SessionReference;
			QrCode = GenerateQrCode();

			timer = new DispatcherTimer()
			{
				Interval = new TimeSpan(0, 0, 0, 0, 500)
			};
			timer.Tick += async (sender, e) =>
			{
				IRestResponse response = await AttendeeService.GetAttendeesTaskAsync(DataHelper.AuthSession);

				if (response.StatusCode == HttpStatusCode.OK)
				{
					List<AttendeeVO> attendees = JsonSerializer.Deserialize<List<AttendeeVO>>(response.Content, new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					});

					Attendees.ToList().ForEach(att =>
					{
						if (!attendees.Any(x => x.Name == att.Name))
							Attendees.Remove(att);
					});

					attendees.ForEach(att =>
					{
						if (!Attendees.Any(x => x.Name == att.Name))
							Attendees.Add(att);
					});
				}
			};

			timer.Start();
		}

		private async Task CancelSessionCreation(ConfigureView configureView)
		{
			IRestResponse response = await _sessionService.DeleteSessionTaskAsync(DataHelper.AuthSession);

			if (response.StatusCode == HttpStatusCode.OK)
				configureView.CancelSession();
			else if (response.StatusCode == HttpStatusCode.NotFound)
				MessageBox.Show(response.Content.Replace("\"", string.Empty));

			timer.Stop();
		}

		private object GenerateQrCode()
		{
			using var generator = new QRCodeGenerator();
			using QRCodeData data = generator.CreateQrCode(SessionReference, QRCodeGenerator.ECCLevel.Q);
			using var qRCode = new QRCode(data);
			return qRCode.GetGraphic(10, System.Drawing.Color.Black, System.Drawing.Color.White, false);
		}
	}
}