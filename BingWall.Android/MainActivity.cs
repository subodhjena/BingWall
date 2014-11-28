using System;
using System.Globalization;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;

using BingWall.Core.Helpers;
using BingWall.Core.Implementations;
using BingWall.Core.Interfaces;
using BingWall.Core.Models;

namespace BingWall.Android
{
	[Activity (Label = "BingWall.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		IBingRepository bingRepo = new BingRepository (BingServer.bingAddress, String.Empty);
		BingImageURL bingURL =new BingImageURL ();

		Button sync;
		Button wallpaper;
		ImageView bingImage;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			sync = FindViewById<Button> (Resource.Id.syncBtn);
			wallpaper = FindViewById<Button> (Resource.Id.wallBtn);
			bingImage = FindViewById<ImageView> (Resource.Id.todaysImage);
			
			sync.Click += LoadBingBackgroud;
		}

		public async void LoadBingBackgroud (object sender, EventArgs e)
		{
			var response = await bingRepo.GetBingHomepageBackground ();
			var image = bingURL.GetTodaysBingImageURL (response);
			string url = String.Format (CultureInfo.InvariantCulture, "{0}"+image.url, BingServer.bingAddress);
			ShowTodaysWallapaper (url);
		}

		public async void ShowTodaysWallapaper(string imageURL)
		{
			byte[] todaysImage = await bingRepo.GetImage(imageURL);
			Bitmap bitmap = BitmapFactory.DecodeByteArray (todaysImage, 0, todaysImage.Length);
			bingImage.SetImageBitmap (bitmap);
		}
	}
}


