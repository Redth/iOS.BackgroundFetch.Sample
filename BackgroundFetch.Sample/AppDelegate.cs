using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;

namespace BackgroundFetchSample
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{

		WeatherViewController weatherViewController;

		// class-level declarations
		UIWindow window;
		//
		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			weatherViewController = new WeatherViewController ();
			window.RootViewController = weatherViewController;
			window.MakeKeyAndVisible ();

			//Tell the operating system to run our PerformFetch occasionally, and let it decide the frequency
			UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval (UIApplication.BackgroundFetchIntervalMinimum);

			return true;
		}
			                                                                  

		public override async void PerformFetch (UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
		{
			Console.WriteLine ("PerformFetch called...");

			//Return no new data by default
			var result = UIBackgroundFetchResult.NoData;

			try
			{
				//Get latest weather
				var w = await GetWeatherAsync("Windsor, Canada");

				if (w != null)
				{
					//Cache the weather locally
					CacheWeatherAsync(w);

					//Update the UI
					if (weatherViewController != null)
						weatherViewController.UpdateWeather(w);

					//Indicate we have new data
					result = UIBackgroundFetchResult.NewData;
				}
			}
			catch 
			{
				//Indicate a failed fetch if there was an exception
				result = UIBackgroundFetchResult.Failed;
			}
			finally
			{
				//We really should call the completion handler with our result
				completionHandler (result);
			}
		}

		void CacheWeatherAsync(WeatherInfo weatherInfo)
		{
			var file = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "weather.json");

			File.WriteAllText(file, JsonConvert.SerializeObject (weatherInfo));	
		}

		async Task<WeatherInfo> GetWeatherAsync(string query)
		{
			var http = new HttpClient ();
			var data = await http.GetStringAsync ("http://api.openweathermap.org/data/2.5/weather?q=" + query);

			return JsonConvert.DeserializeObject<WeatherInfo> (data);
		}

		public static WeatherInfo GetCachedWeather()
		{
			var file = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "weather.json");

			try { return JsonConvert.DeserializeObject<WeatherInfo>(File.ReadAllText (file)); }
			catch { }

			return null;
		}
	}
}
