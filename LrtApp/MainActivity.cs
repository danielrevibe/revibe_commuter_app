using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace LrtApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        FragmentTransaction ft;

        public FrameLayout frameLayout;
     
       
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_qr:

                    FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    QRFragment receivedConfirmation = new QRFragment();
                    receivedConfirmation.Show(transaction, "QRDialog");

                    return true;

                case Resource.Id.navigation_rewards:

                    FragmentManager.BeginTransaction().Remove(FragmentManager.FindFragmentById(Resource.Id.FrameLayout)).Commit();
                    ft = FragmentManager.BeginTransaction();
                    ft.SetCustomAnimations(Resource.Animator.enter_from_left, Resource.Animator.exit_to_right);
                    ft.AddToBackStack(null);
                    ft.Add(Resource.Id.FrameLayout, new RewardsFragment());
                    ft.Commit();

                    return true;

                case Resource.Id.navigation_locate:

                    FragmentManager.BeginTransaction().Remove(FragmentManager.FindFragmentById(Resource.Id.FrameLayout)).Commit();
                    ft = FragmentManager.BeginTransaction();
                    ft.SetCustomAnimations(Resource.Animator.enter_from_left, Resource.Animator.exit_to_right);
                    ft.AddToBackStack(null);
                    ft.Add(Resource.Id.FrameLayout, new MapsFragment());
                    ft.Commit();

                    return true;
            }

            return false;
        }

       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            FloatingActionButton fabReport = FindViewById<FloatingActionButton>(Resource.Id.fabReport);
            fabReport.Click += FabReportOnClick;

            Android.Content.Res.ColorStateList csl = new Android.Content.Res.ColorStateList(new int[][] { new int[0] }, new int[] { Android.Graphics.Color.ParseColor("#c62828") });
            fabReport.BackgroundTintList = csl;

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            ft = FragmentManager.BeginTransaction();
            ft.SetCustomAnimations(Resource.Animator.enter_from_left, Resource.Animator.exit_to_right);
            ft.AddToBackStack(null);
            ft.Add(Resource.Id.FrameLayout, new MapsFragment());
            ft.Commit();

        }

        private void FabReportOnClick(object sender, EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            ReportsFragment receivedConfirmation = new ReportsFragment();
            receivedConfirmation.Show(transaction, "ReportsFragmentDialog");
        }

        private void FabOnClick(object sender, EventArgs e)
        {
            Intent intent = PackageManager.GetLaunchIntentForPackage("here.here");
            StartActivity(intent);
            
        }
    }
}

