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
		MonoTouch.UIKit.UIButton bingWallImageDownloadBtn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView bingWallImageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (bingWallImageDownloadBtn != null) {
				bingWallImageDownloadBtn.Dispose ();
				bingWallImageDownloadBtn = null;
			}

			if (bingWallImageView != null) {
				bingWallImageView.Dispose ();
				bingWallImageView = null;
			}
		}
	}
}
