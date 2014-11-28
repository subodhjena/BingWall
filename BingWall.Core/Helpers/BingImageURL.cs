using System;
using System.Collections.Generic;

using BingWall.Core.Models;

namespace BingWall.Core.Helpers
{
	public class BingImageURL
	{
		Image image;

		public BingImageURL ()
		{
			image = new Image();
		}

		public Image GetTodaysBingImageURL(BingBackground bingObj)
		{
			foreach (Image img in bingObj.images) {
				image = img;
			}
			return image;
		}
	}
}

