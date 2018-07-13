using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace LrtApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        FragmentTransaction ft;
       
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_qr:

                    FragmentManager.BeginTransaction().Remove(FragmentManager.FindFragmentById(Resource.Id.FrameLayout)).Commit();
                    ft = FragmentManager.BeginTransaction();
                    ft.SetCustomAnimations(Resource.Animator.enter_from_left, Resource.Animator.exit_to_right);
                    ft.AddToBackStack(null);
                    ft.Add(Resource.Id.FrameLayout, new QRFragment());
                    ft.Commit();

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
                    ft.Add(Resource.Id.FrameLayout, new RewardsFragment());
                    ft.Commit();

                    return true;
            }

            return false;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            ft = FragmentManager.BeginTransaction();
            ft.SetCustomAnimations(Resource.Animator.enter_from_left, Resource.Animator.exit_to_right);
            ft.AddToBackStack(null);
            ft.Add(Resource.Id.FrameLayout, new QRFragment());
            ft.Commit();

        }
    }
}

