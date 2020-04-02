using Syncfusion.XForms.iOS.Buttons;
using Foundation;

using Syncfusion.Licensing;

using UIKit;

using Xamarin.Forms.Platform.iOS;

namespace UTTAF.Mobile.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            SyncfusionLicenseProvider.RegisterLicense("MjMxNzAyQDMxMzgyZTMxMmUzMFpUWHhFaXA0SkxwMWd5RzhZcnpzMVhsTjBNWDBRYmUvbWc2NmpzZ0gveGc9");

            Xamarin.Forms.Forms.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}