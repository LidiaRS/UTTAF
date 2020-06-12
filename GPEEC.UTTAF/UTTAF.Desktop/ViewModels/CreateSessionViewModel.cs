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
using System.Windows;
using System.Windows.Threading;

using UTTAF.Dependencies.Enums;
using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;
using UTTAF.Desktop.Services.Requests;
using UTTAF.Desktop.Views;

namespace UTTAF.Desktop.ViewModels
{
	public class CreateSessionViewModel : ViewModelBase
	{
		private readonly MainView _mainView;
		private readonly SessionService _sessionService;

		private DispatcherTimer timer;
		private string __sessionReference;
		private object __qrCodeImg;

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

		public ObservableCollection<AttendeeModel> Attendees { get; set; } = new ObservableCollection<AttendeeModel>();

		public RelayCommand<CreateSessionView> CancelSessionCreationCommand { get; private set; }
		public RelayCommand StartSessionCommand { get; private set; }

		public CreateSessionViewModel(MainView mainView, SessionService sessionService)
		{
			_mainView = mainView;
			_sessionService = sessionService;

			CancelSessionCreationCommand = new RelayCommand<CreateSessionView>(CancelSessionCreation);
			StartSessionCommand = new RelayCommand(StartSession);
		}

		private async void StartSession()
		{
			DataHelper.AuthSession.SessionStatus = SessionStatusEnum.InProgress;
			IRestResponse response = await _sessionService.StartSessionTaskAsync(DataHelper.AuthSession);

			switch (response.StatusCode)
			{
				case HttpStatusCode.OK:
					timer.Stop();
					_mainView.Show();
					break;

				case HttpStatusCode.NotFound:
				case HttpStatusCode.BadRequest:
					MessageBox.Show(response.Content.Replace("\"", string.Empty));
					break;
			}
		}

		public void Init()
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
					List<AttendeeModel> attendees = JsonSerializer.Deserialize<List<AttendeeModel>>(response.Content, new JsonSerializerOptions
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

		private async void CancelSessionCreation(CreateSessionView sessionView)
		{
			IRestResponse response = await _sessionService.DeleteSessionTaskAsync(DataHelper.AuthSession);

			if (response.StatusCode == HttpStatusCode.OK)
				sessionView.CancelSession();
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