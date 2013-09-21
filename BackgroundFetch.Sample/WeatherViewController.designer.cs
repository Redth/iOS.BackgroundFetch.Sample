// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace BackgroundFetchSample
{
	[Register ("WeatherViewController")]
	partial class WeatherViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel labelLocation { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel labelTemp { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (labelTemp != null) {
				labelTemp.Dispose ();
				labelTemp = null;
			}

			if (labelLocation != null) {
				labelLocation.Dispose ();
				labelLocation = null;
			}
		}
	}
}
