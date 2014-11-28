using System;
using System.Collections.Generic;

namespace BingWall.Core.Models
{
	public class BingBackground
	{
		public BingBackground ()
		{
		}

		public IList<Image> images { get; set; }
		public Tooltips tooltips { get; set; }
	}
}

