using System;
using System.Threading.Tasks;
using BingWall.Core.Web;

namespace BingWall.Core.Interfaces
{
    public interface IBingRepository
    {
        Task<object> GetBingHomepageBackground();
    }
}

