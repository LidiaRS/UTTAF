using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;

using Syncfusion.Licensing;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace UTTAF.Mobile.Droid
{
	[Activity(ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			SyncfusionLicenseProvider.RegisterLicense("MzM3MTQzQDMxMzgyZTMzMmUzMFRhVFQ2cGFkS08yd1pQc2hRd0dDQWF2eGRjb1BUaEtoc05Denl1bHZ2Nms9");

			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(savedInstanceState);

			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			Forms.Init(this, savedInstanceState);
			FormsMaterial.Init(this, savedInstanceState);
			ZXing.Net.Mobile.Forms.Android.Platform.Init();

			LoadApplication(new App());
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}