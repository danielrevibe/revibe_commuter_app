using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace LrtApp
{
    public class QRFragment : Fragment
    {
        Button btnGenerate;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.QRLayout, container, false);

            btnGenerate = view.FindViewById<Button>(Resource.Id.btnGenerateQR);
            btnGenerate.Click += BtnGenerate_Click;

            return view;
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            var intent = new Intent(Activity, typeof(GenerateQRActivity));
            StartActivity(intent);
        }
    }
}