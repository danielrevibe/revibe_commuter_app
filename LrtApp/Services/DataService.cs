using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LrtApp.Model;
using RestSharp;

namespace LrtApp.Services
{
    public class DataService : IDataService
    {
        RestSharpRequest c = new RestSharpRequest();

        public bool SendReport(Report data)
        {
            var postRequest = new RestRequest(c.SendReportRequest(), Method.POST);
            postRequest.AddHeader("Content-type", "application/json");

            postRequest.AddJsonBody(new

            {
                train_id = data.train_id,
                username = data.username,
                content = data.content

            });

            var d = c.Client("hidden").Execute(postRequest);

            if(d.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}