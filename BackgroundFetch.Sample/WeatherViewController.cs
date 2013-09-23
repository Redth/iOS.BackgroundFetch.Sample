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
				this.labelTemp.Text = weatherInfo.GetTempInCelsius().ToString() + "°";
				this.labelLocation.Text = weatherInfo.name;
			});
		}

		void LoadCachedWeather()
		{
			var weatherInfo = AppDelegate.GetCachedWeather ();

			if (weatherInfo != null)
				UpdateWeather (weatherInfo);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		
			LoadCachedWeather ();
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			LoadCachedWeather ();
		}
	}
}

