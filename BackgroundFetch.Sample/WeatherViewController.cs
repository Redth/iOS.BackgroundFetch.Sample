using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace BackgroundFetchSample
{
	public partial class WeatherViewController : UIViewController
	{
		public WeatherViewController () : base ("WeatherViewController", null)
		{
		}

		public void UpdateWeather(WeatherInfo weatherInfo)
		{
			InvokeOnMainThread (() =>
			{
				this.labelTemp.Text = weatherInfo.main.temp.ToString ();
				this.labelLocation.Text = weatherInfo.name;
			});
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var weatherInfo = AppDelegate.GetCachedWeather ();

			if (weatherInfo != null)
				UpdateWeather (weatherInfo);
		}
	}
}

