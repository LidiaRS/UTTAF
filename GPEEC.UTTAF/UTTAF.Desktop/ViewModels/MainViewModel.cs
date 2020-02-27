using GalaSoft.MvvmLight;
using System;
using UTTAF.Desktop.Models;

namespace UTTAF.Desktop.ViewModels
{
    internal class MainViewModel : ViewModelBase
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