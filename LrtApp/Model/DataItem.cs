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

namespace LrtApp.Model
{
    public class Coordinates
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
    }

    public class TrainData
    {
        public string train_plate { get; set; }
        public Coordinates coordinates { get; set; }
        public string acceleration { get; set; }
    }

    public class UserCoordinates
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class QRModel
    {
        public string subscription_id { get; set; }
        public string datetime { get; set; }
        
    }

    public class Report
    {
        public string train_id { get; set; }
        public string username { get; set; }
        public string content { get; set; }
      
    }
}