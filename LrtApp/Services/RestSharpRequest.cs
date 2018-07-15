using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RestSharp;

namespace LrtApp.Services
{
    public class RestSharpRequest
    {
        public RestClient Client(string reqType)
        {
            RestClient client;

            switch (reqType)
            {
                case "hidden":
                    client = new RestClient("https://hiddenteamph.com");
                    return client;

                case "places":
                    client = new RestClient("https://places.cit.api.here.com");
                    return client;

                default:
                    client = new RestClient(reqType);
                    return client;
            }

        }

        public string SendReportRequest()
        {
            return "web/train_api/notifications";
        }
    }
}