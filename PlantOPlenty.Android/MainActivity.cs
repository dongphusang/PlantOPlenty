using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Hardware;
using Android.Content;

namespace PlantOPlenty.Droid
{
    [Activity(Label = "PlantOPlenty", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity , ISensorEventListener
    {
        Sensor lightSensor;
        SensorManager sensorService;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            sensorService = (SensorManager)GetSystemService(Context.SensorService);
            lightSensor = sensorService.GetDefaultSensor(SensorType.Light);
            Console.WriteLine($"DEBUG: MainActivity.OnCreate().lightSensor => {lightSensor.ToString()} ");
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnPause()
        {
            base.OnPause();
            sensorService.UnregisterListener(this);
        }

        protected override void OnResume()
        {
            base.OnResume();
            sensorService.RegisterListener(this, lightSensor, SensorDelay.Normal);
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
        }

        // update current lux value to ReportPageData class
        public void  OnSensorChanged(SensorEvent e)
        {
            if (e.Sensor.GetType() == lightSensor.GetType())
            {
                PlantOPlenty.ReportPageData.measuredLuxValue = (int)e.Values[0];
            }
        }
    }
}