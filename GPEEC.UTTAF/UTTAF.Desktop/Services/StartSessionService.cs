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
		private readonly SessionService _sessionService;

		public StartSessionService(SessionService sessionService, MainView mainView)
		{
			_sessionService = sessionService;
			_mainView = mainView;
		}

		public async Task StartSessionAsync(DispatcherTimer timer)
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
	}
}