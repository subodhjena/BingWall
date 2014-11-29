// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace BingWall.iOS
{
	[Register ("TodaysBingViewController")]
	partial class TodaysBingViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIActivityIndicatorView activityIndicator { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton syncBtn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView todaysBingWallpaper { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton wallpaperBtn { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (syncBtn != null) {
				syncBtn.Dispose ();
				syncBtn = null;
			}

			if (todaysBingWallpaper != null) {
				todaysBingWallpaper.Dispose ();
				todaysBingWallpaper = null;
			}

			if (wallpaperBtn != null) {
				wallpaperBtn.Dispose ();
				wallpaperBtn = null;
			}

			if (activityIndicator != null) {
				activityIndicator.Dispose ();
				activityIndicator = null;
			}
		}
	}
}
