using GalaSoft.MvvmLight;

using UTTAF.Desktop.Models;

namespace UTTAF.Desktop.ViewModels
{
	public class MainViewModel : ViewModelBase
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