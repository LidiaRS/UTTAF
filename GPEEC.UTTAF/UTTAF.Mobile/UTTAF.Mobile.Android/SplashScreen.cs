using Android.App;
using Android.OS;

namespace UTTAF.Mobile.Droid
{
    [Activity(MainLauncher = true, Theme = "@style/Splash", NoHistory = true)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartActivity(typeof(MainActivity));
        }
    }
}