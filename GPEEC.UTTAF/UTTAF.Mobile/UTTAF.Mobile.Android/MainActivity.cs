using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Widget;

using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace UTTAF.Mobile.Droid
{
	[Activity(Label = "UTTAF.Mobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
	public class MainActivity : FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			try
			{
				TabLayoutResource = Resource.Layout.Tabbar;
				ToolbarResource = Resource.Layout.Toolbar;

				base.OnCreate(savedInstanceState);

				Xamarin.Essentials.Platform.Init(this, savedInstanceState);
				Forms.Init(this, savedInstanceState);
				LoadApplication(new App());
			}
			catch (Exception ex)
			{
				HandlerException(ex);
			}
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		private void HandlerException(Exception ex)
		{
			var alertBuilder = new AlertDialog.Builder(this);
			AlertDialog alert = alertBuilder.Create();

			alert.SetTitle("Algo de errado aconteceu!");
			alert.SetIcon(Android.Resource.Drawable.IcDialogAlert);
			alert.SetMessage($"Erro: {ex.Message}\n\nDetail:\n{ex.StackTrace}");

			alert.SetButton("Ok", (sender, eventArgs) =>
			{
				Toast
					.MakeText(this, "A aplicaçao nao funcionará corretamente!", ToastLength.Short)
					.Show();
			});

			alert.Show();
		}
	}
}