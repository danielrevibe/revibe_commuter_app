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
using ZXing;
using ZXing.Common;

namespace LrtApp
{
    [Activity(Label = "My Ticket", Theme = "@style/AppTheme")]
    public class GenerateQRActivity : AppCompatActivity
    {
        private ImageView qrImage;
        private Button btnShare;
        private TextView txtExpiration;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QRCode);

            qrImage = FindViewById<ImageView>(Resource.Id.imageQR);
            txtExpiration = FindViewById<TextView>(Resource.Id.expiration);
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
            var bitmap = writer.Write("My content");
            qrImage.SetImageBitmap(bitmap);
        }



        private void BtnShare_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}