using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Globalization;
using BingWall.Core.Interfaces;
using BingWall.Core.Web;
using BingWall.Core.Helpers;

namespace BingWall.Core.Implementations
{
    public class BingRepository : BaseRequest,IBingRepository
    {
        public BingRepository(string urlPrefix, string securityToken)
            : base (urlPrefix, securityToken)
        {
            urlPrefix = BingServer.bingAddress;
        }

        public async Task<object> GetBingHomepageBackground()
        {
            string url = String.Format (CultureInfo.InvariantCulture, "{0}HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US", _urlPrefix);
            return await base.GetAsync<object> (url);
        }
    }
}