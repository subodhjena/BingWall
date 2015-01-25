using System;
using System.IO;
using System.Globalization;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;

using Android.Support.V4.App;
using Android.Gms.Plus;
using AndroidHUD;

using BingWall.Core.Helpers;
using BingWall.Core.Implementations;
using BingWall.Core.Interfaces;
using BingWall.Core.Models;

using ImageProvider = Android.Provider;
using AndroidNet = Android.Net;

namespace BingWall.Android
{
	[Activity (Label = "BingWall", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		IBingRepository bingRepo = new BingRepository (BingServer.bingAddress, String.Empty);
		BingImageURL bingURL = new BingImageURL ();
		string documents = string.Empty;
		string wallpaperName = string.Empty;

		Button sync;
		Button wallpaper;
		Button share;
		ImageView bingImage;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			documents = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments);
			wallpaperName = System.IO.Path.Combine (documents, "bingWallpaper.jpg");

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			LoadBing ();

			// Get our button from the layout resource,
			// and attach an event to it
			sync = FindViewById < Button > (Resource.Id.syncBtn);
			wallpaper = FindViewById < Button > (Resource.Id.wallBtn);
			share = FindViewById < Button > (Resource.Id.shareGoogle);
			bingImage = FindViewById < ImageView > (Resource.Id.todaysImage);

			share.Click += ShareOnGooglePlus;
			sync.Click += LoadBingBackgroud;
			wallpaper.Click += SetWallpaper;
		}

		private async void LoadBing ()
		{
			var response = await bingRepo.GetBingHomepageBackground ();
			var image = bingURL.GetTodaysBingImageURL (response);
			string url = String.Format (CultureInfo.InvariantCulture, "{0}" + image.url, BingServer.bingAddress);

			ShowTodaysWallapaper (url);
		}

		private async void LoadBingBackgroud (object sender, EventArgs e)
		{
			AndHUD.Shared.Show(this, "Syncing With the Latest Image !! \n Please Wait", -1, MaskType.Clear);

			var response = await bingRepo.GetBingHomepageBackground ();
			var image = bingURL.GetTodaysBingImageURL (response);
			string url = String.Format (CultureInfo.InvariantCulture, "{0}" + image.url, BingServer.bingAddress);
			ShowTodaysWallapaper (url);

			AndHUD.Shared.Dismiss();

		}

		private async void ShowTodaysWallapaper (string imageURL)
		{
			byte[] todaysImage = await bingRepo.GetImage (imageURL);

			File.WriteAllBytes(wallpaperName, todaysImage);
			if (File.Exists (wallpaperName)) {
				Console.WriteLine ("File created successfully");
			}

			Bitmap bitmap = BitmapFactory.DecodeByteArray (todaysImage, 0, todaysImage.Length);
			bingImage.SetImageBitmap (bitmap);
		}

		private void ShareOnGooglePlus (object sender, EventArgs e)
		{
			if (File.Exists (wallpaperName)) {
				Java.IO.File tempFile = new Java.IO.File (wallpaperName);
				string photoUri = ImageProvider.MediaStore.Images.Media.InsertImage (ContentResolver, tempFile.AbsolutePath, null, null);
				try{
					PlusShare.Builder share = new PlusShare.Builder(this);
					share.SetText("hello everyone!, I am using todays bing background as my wallpaper. its cool!!");
					share.AddStream (AndroidNet.Uri.Parse (photoUri));
					share.SetType ("image/jpeg");
					StartActivityForResult (share.Intent, 2);
				}catch(Exception x){
					Console.WriteLine ("Google + Not Installed");
					AndHUD.Shared.ShowError(this, "Oops \n Seems like you don't have Google+ installed.", MaskType.Black,TimeSpan.FromSeconds(3));
				}

			} else {
				Console.WriteLine ("File Doesn't Exists !!");
			}
		}

		private void SetWallpaper(object sender, EventArgs e) {
			WallpaperManager wallManager = WallpaperManager.GetInstance (this);
			try{
				Bitmap bitmap = BitmapFactory.DecodeFile(wallpaperName);
				wallManager.SetBitmap(bitmap);
				AndHUD.Shared.ShowSuccess(this, "Successfully changed your wallpaper !!", MaskType.Clear, TimeSpan.FromSeconds(2));
			}
			catch(Exception x){
				Console.WriteLine (x.StackTrace);
				AndHUD.Shared.ShowError(this, "Oops \n Seems like i have a problem with you storage :(", MaskType.Black, TimeSpan.FromSeconds(2));
			}
		}
	}
}


