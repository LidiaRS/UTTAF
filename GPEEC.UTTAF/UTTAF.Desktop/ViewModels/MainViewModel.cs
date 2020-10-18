using UTTAF.Dependencies.Clients.Utils;
using UTTAF.Desktop.Models;

namespace UTTAF.Desktop.ViewModels
{
	public class MainViewModel : PropertyNotifier
	{
		private MainModel __mainModel;

		public MainModel MainModel
		{
			get => __mainModel;
			set => Set(ref __mainModel, value);
		}

		public MainViewModel() => Init();

		private void Init()
		{
		}
	}
}