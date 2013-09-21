Xamarin.iOS Recipe 
==================

Background Fetching Data
===============================

This recipe shows how to register your application to perform background fetching on intervals.

1. Recipe
---------
In your application's `Info.plist` file, add the value `fetch` to the `UIBackgroundModes` (*Required background modes*) property.

Next, in your `AppDelegate` class, in the `FinishedLaunching` override method, add the following code to register your application for background fetching:
```csharp
UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval (UIApplication.BackgroundFetchIntervalMinimum);
```

Finally, in your `AppDelegate` class, override the `PerformFetch` method.  

This method will be executed by the operating system when it sees best fit (eg: when the device is awake and connected already).  You do not have complete control over how often or when fetching happens.

You should execute your own code to fetch new data in this method.  It's important to call the `Action<UIBackgroundFetchResult> completionHandler` parameter which is passed into this method with the appropriate result when you are done.  

2. Sample PerformFetch
----------------------

In this sample, a weather service is called by background fetching so that when the user opens the app, recent weather is available.  The weather is cached locally after it's fetched in the background, and the UI is also updated if there is new weather info.

```csharp
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
```

3. Additional Information
-------------------------

 - Your `PerformFetch` has about **30** seconds to run before it's killed
 - The operating system is more likely to grant more time (and more often) to your application for background fetching if you are efficient, which means executing quickly, and always calling `completionHandler` with an accurate result
 - You can tell the operating system  the minimum time to sleep between waking up your application and calling its `PerformFetch` method if you know your app only updates at a certain interval, to avoid extra calls to `PerformFetch` and wasting battery life.  You would specify the minimum time in seconds in the `UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval (double minimumBackgroundFetchInterval)` method
 - You can actually make calls to update your UI from the `PerformFetch` method so that the next time the user launches the app, everything is up to date
 
