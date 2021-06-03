using WebShop.PL.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebShop.PL.Components
{
    public class SystemStatusPage : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            #region Alternate function
            //var client = new HttpClient();

            //HttpResponseMessage response =
            //    await client.GetAsync("http://www.google.com");
            //if (response.StatusCode == HttpStatusCode.OK)
            //    return View(true);
            //return View(false);
            #endregion

            return View(await ConnectionState.IsConnectedToInternet());
        }
    }
}
