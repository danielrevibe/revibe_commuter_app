using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using LrtApp.Model;
using Newtonsoft.Json;
using Plugin.Geolocator;
using SocketIO.Client;

namespace LrtApp
{
    public class MapsFragment : Fragment, IOnMapReadyCallback
    {

        public GoogleMap GMap;
        MapView mapView;
        MarkerOptions options;
        MarkerOptions trainOptions;

        Marker trainMarker;

        //needed for location
        LocationManager lm;

        //Socket for realtime data transfer
        Socket socket;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.map_layout, container, false);

            ConnectToServer();


            return view;

        }

        //connect client to tcp, 
        void ConnectToServer()
        {
            try
            {
                // socket = IO.Socket("http://192.168.8.100:5000/commuter/locator");
                socket = IO.Socket("https://navigo.serveo.net/commuter/locator");

                //verify socket connected
                socket.On(Socket.EventConnect, (data) =>
                {

                    //listen to incoming data
                    socket.On("server_response", (d) =>
                    {

                        try
                        {

                            //extract data, handle json deserialize exception, since object returns are sometimes invalid json.
                            foreach (var l in d)
                            {
                                var jsonData = JsonConvert.DeserializeObject<TrainData>(l.ToString());
                                Log.Debug("Coordinates123", jsonData.acceleration);

                                //MapTrain(jsonData);

                                Activity.RunOnUiThread(() =>
                                {

                                    LatLng latlng = new LatLng(double.Parse(jsonData.coordinates.latitude), double.Parse(jsonData.coordinates.longitude));

                                    if(trainMarker != null)
                                    {
                                        trainMarker.Remove();
                                    }

                                    trainOptions = new MarkerOptions().SetPosition(latlng)
                                    .SetSnippet("Speed: " + jsonData.acceleration +"\n\nTrain ID: "+jsonData.train_plate)
                                    .SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.south_bound))
                                    .SetTitle("Nearby Train");
                                    trainMarker = GMap.AddMarker(trainOptions);

                                });
                            }

                        }
                        catch (Newtonsoft.Json.JsonReaderException) { }

                    });

                });

            }
            catch (Exception) { }






            socket.Connect();



        }

        public async void OnMapReady(GoogleMap googleMap)
        {
            try
            {
                //retrieve your current location and current station based on dataset
                UserCoordinates c = new UserCoordinates();
                c = await retrieveUserLocation();




                Activity.RunOnUiThread(() =>
                {
                    MapsInitializer.Initialize(Activity);
                    this.GMap = googleMap;

                    GMap.UiSettings.ZoomControlsEnabled = true;
                    LatLng latlng = new LatLng(c.latitude, c.longitude);
                    CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlng, 18);
                    GMap.AnimateCamera(camera);
                    GMap.UiSettings.ZoomControlsEnabled = false;

                    options = new MarkerOptions().SetPosition(latlng).SetTitle("My location");
                    GMap.AddMarker(options);

                });
            }
            catch (Exception ex)
            {
                Toast.MakeText(Activity, ex.Message, ToastLength.Short).Show();
            }


        }

        void MapTrain(TrainData data)
        {
            try
            {
                LatLng latlng = new LatLng(double.Parse(data.coordinates.latitude), double.Parse(data.coordinates.longitude));
                options = new MarkerOptions().SetPosition(latlng).SetTitle("Train location");
                GMap.AddMarker(options);

            }
            catch (Exception) { }

        }


        //async void UpdateUserPosition()
        //{
        //    Activity.RunOnUiThread(() =>
        //    {
        //        Activity.RunOnUiThread(() =>
        //        {

        //            LatLng latlng = new LatLng(double.Parse(jsonData.coordinates.latitude), double.Parse(jsonData.coordinates.longitude));

        //            if (trainMarker != null)
        //            {
        //                trainMarker.Remove();
        //            }

        //            trainOptions = new MarkerOptions().SetPosition(latlng)
        //            .SetSnippet("Speed: " + jsonData.acceleration)
        //            .SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.south_bound))
        //            .SetTitle("Nearby location");
        //            trainMarker = GMap.AddMarker(trainOptions);

        //        });

        //    });
        //}

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);


            mapView = view.FindViewById<MapView>(Resource.Id.mapView1);

            if (mapView != null)
            {
                // Initialise the MapView
                mapView.OnCreate(null);
                mapView.OnResume();
                // Set the map ready callback to receive the GoogleMap object
                mapView.GetMapAsync(this);


            }
        }


        public async Task<UserCoordinates> retrieveUserLocation()
        {
            UserCoordinates c = new UserCoordinates();

            if (IsGPSRunning())
            {
                //Get current location.
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));



                c.longitude = position.Longitude;
                c.latitude = position.Latitude;

                return c;
            }

            else
            {

                c.longitude = 120.98158;
                c.latitude = 14.60537;

                return c;
            }
        }

        //Method for checking if gps is running.
        private bool IsGPSRunning()
        {
            bool gps_enabled = false;

            try
            {
                gps_enabled = lm.IsProviderEnabled(LocationManager.GpsProvider);
                return gps_enabled;
            }
            catch (Exception) { return false; }
        }
    }
}