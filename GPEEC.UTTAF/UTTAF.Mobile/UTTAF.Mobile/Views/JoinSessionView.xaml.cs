﻿using Logikoz.XamarinUtilities.Utilities;

using System;

using UTTAF.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UTTAF.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JoinSessionView : ContentPage
    {
        public JoinSessionView()
        {
            InitializeComponent();
            BindingContext = new JoinSessionViewModel();
        }

        private void BackToStart(object sender, EventArgs e)
        {
            PopPushViewUtil.PopModal<JoinSessionView>();
        }
    }
}