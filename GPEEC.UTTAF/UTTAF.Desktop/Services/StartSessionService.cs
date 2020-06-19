using RestSharp;

using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

using UTTAF.Dependencies.Enums;
using UTTAF.Dependencies.Helpers;
using UTTAF.Desktop.Services.Requests;
using UTTAF.Desktop.Views;

namespace UTTAF.Desktop.Services
{
	public class StartSessionService : IStartSessionService
	{
		private readonly MainView _mainView;
		private readonly SessionRequestService _sessionService;

		public StartSessionService(SessionRequestService sessionService, MainView mainView)
		{
			_sessionService = sessionService;
			_mainView = mainView;
		}

		public async Task<bool> StartSessionAsync(DispatcherTimer timer)
		{
			DataHelper.AuthSession.SessionStatus = SessionStatusEnum.InProgress;
			IRestResponse response = await _sessionService.StartSessionTaskAsync(DataHelper.AuthSession);

			switch (response.StatusCode)
			{
				case HttpStatusCode.OK:
					timer.Stop();
					_mainView.Show();
					return true;

				default:
					MessageBox.Show(response.Content.Replace("\"", string.Empty));
					return false;
			}
		}
	}
}