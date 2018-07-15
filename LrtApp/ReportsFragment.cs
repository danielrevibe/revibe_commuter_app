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
using System.Threading;
using System.Threading.Tasks;
using LrtApp.Services;
using LrtApp.Model;

namespace LrtApp
{
    public class ReportsFragment : DialogFragment
    {
        EditText txtContent;
        Button btnSend;

        IDataService _dataService;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
             base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.ReportLayout, container, false);

            txtContent = view.FindViewById<EditText>(Resource.Id.txtReport);
            btnSend = view.FindViewById<Button>(Resource.Id.btnSend);
            btnSend.Click += BtnSend_Click;

            IDataService dataService = new DataService();
            _dataService = dataService;


            return view;

        }

        private async void BtnSend_Click(object sender, EventArgs e)
        {
            ProgressDialog dialog = ProgressDialog.Show(Activity, "Sending..", "Please wait...", true);

            try
            {
                Report report = new Report();
                report.username = "JB Rillo";
                report.content = txtContent.Text;
                report.train_id = "123";

                bool isSent = false;

                await Task.Run(() =>
                {
                    isSent = _dataService.SendReport(report);
                });

                if (isSent)
                {
                    Toast.MakeText(Activity, "Successfully sent", ToastLength.Short).Show();
                    dialog.Hide();
                    Dismiss();
                }

            }
            catch(Exception ex)
            {
                Toast.MakeText(Activity, ex.Message, ToastLength.Short).Show();
                dialog.Hide();
            }
       
        }
    }
}