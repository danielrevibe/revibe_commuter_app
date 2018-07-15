using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using LrtApp.Model;
using Newtonsoft.Json;
using ZXing;
using ZXing.Common;

namespace LrtApp
{
    [Activity(Label = "My Ticket", Theme = "@style/AppTheme")]
    public class GenerateQRActivity : AppCompatActivity
    {
        private ImageView qrImage;
        private Button btnShare;
      

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QRCode);

            qrImage = FindViewById<ImageView>(Resource.Id.imageQR);
          
            btnShare = FindViewById<Button>(Resource.Id.btnShare);
            btnShare.Click += BtnShare_Click;



            GenerateQR();

        }

        void GenerateQR()
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Height = 600,
                    Width = 600
                }
            };


            //for prototyping purposes, we just declared the subscription id, 
            //subscription id should come from database.
            string subscription_id = "12345";

            QRModel qr = new QRModel();
            qr.subscription_id = subscription_id;
            qr.datetime = DateTime.Now.ToString();

            var jsonObj = JsonConvert.SerializeObject(qr);

            var bitmap = writer.Write(jsonObj);
            qrImage.SetImageBitmap(bitmap);
        }



        private void BtnShare_Click(object sender, EventArgs e)
        {
            
        }
    }
}