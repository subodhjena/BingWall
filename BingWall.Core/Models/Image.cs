using System;
using System.Collections.Generic;

namespace BingWall.Core.Models
{
	public class Image
	{
		public Image ()
		{
		}

		public string startdate { get; set; }
		public string fullstartdate { get; set; }
		public string enddate { get; set; }
		public string url { get; set; }
		public string urlbase { get; set; }
		public string copyright { get; set; }
		public string copyrightlink { get; set; }
		public bool wp { get; set; }
		public string hsh { get; set; }
		public int drk { get; set; }
		public int top { get; set; }
		public int bot { get; set; }
		public IList<H> hs { get; set; }
		public IList<object> msg { get; set; }
	}
}

