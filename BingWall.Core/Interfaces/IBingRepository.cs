using System;
using System.Threading.Tasks;
using BingWall.Core.Web;
using BingWall.Core.Models;

namespace BingWall.Core.Interfaces
{
    public interface IBingRepository
    {
		Task<BingBackground> GetBingHomepageBackground();
		Task<byte[]> GetImage (String imageURL);
    }
}

