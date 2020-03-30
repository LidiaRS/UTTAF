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

using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;
using UTTAF.Desktop.Services.Requests;
using UTTAF.Desktop.Views;

namespace UTTAF.Desktop.ViewModels
{
    internal class CreateSessionViewModel : ViewModelBase
    {
        private DispatcherTimer timer;
        private string _sessionReference;
        private object _qrCodeImg;

        public object QrCode
        {
            get => _qrCodeImg;
            set => Set(ref _qrCodeImg, value);
        }

        public string SessionReference
        {
            get => _sessionReference;
            set => Set(ref _sessionReference, value);
        }

        public ObservableCollection<AttendeeModel> Attendees { get; set; } = new ObservableCollection<AttendeeModel>();

        public RelayCommand<CreateSessionView> CancelSessionCreationCommand { get; private set; }

        public CreateSessionViewModel()
        {
            CancelSessionCreationCommand = new RelayCommand<CreateSessionView>(CancelSessionCreation);
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
                    JsonSerializer.Deserialize<List<AttendeeModel>>(response.Content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }).ForEach(att =>
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
            IRestResponse response = await SessionService.DeleteSessionTaskAsync(DataHelper.AuthSession);

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