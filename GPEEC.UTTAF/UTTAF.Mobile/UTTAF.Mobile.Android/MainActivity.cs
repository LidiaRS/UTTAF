using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;

using Syncfusion.Licensing;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using ZXing.Mobile;
using ZXing.Net.Mobile.Android;

namespace UTTAF.Mobile.Droid
{
    [Activity(ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SyncfusionLicenseProvider.RegisterLicense("MjMxNzAyQDMxMzgyZTMxMmUzMFpUWHhFaXA0SkxwMWd5RzhZcnpzMVhsTjBNWDBRYmUvbWc2NmpzZ0gveGc9");

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            FormsMaterial.Init(this, savedInstanceState);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            MobileBarcodeScanner.Initialize(Application);

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}