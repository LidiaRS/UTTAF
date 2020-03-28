using Foundation;

using UIKit;

using Xamarin.Forms.Platform.iOS;

namespace UTTAF.Mobile.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}