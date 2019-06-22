using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Bluetooth;
using Com.Bumptech.Glide;

using Sama2D.Linkers;
using Sama2D.Models;
using Android.Widget;
using Com.Bumptech.Glide.Request;

namespace Sama2D
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private BackgroundWorker bluetooth_worker;
        private BackgroundWorker attention_worker;

        private BluetoothAdapter adapter;
        private BluetoothLinker bluetooth_linker;

        private APILinker api_linker;
        private List<Attention> attention_levels;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
        }

        protected override void OnResume()
        {
            base.OnResume();

            attention_levels = new List<Attention>();

            // InitializeBluetooth();
            InitializeBackgroundWorkers();
            InitializeLinkers();

            attention_worker.RunWorkerAsync();
        }

        #region Initializers

        private void InitializeBackgroundWorkers()
        {
            attention_worker = new BackgroundWorker();
            // bluetooth_worker = new BackgroundWorker();

            attention_worker.DoWork += new DoWorkEventHandler(HandleAttentionLevel);
            attention_worker.RunWorkerCompleted += (sender, e) =>
            {
                if (e.Error != null)
                    Toast.MakeText(this, e.Error.ToString(), ToastLength.Long).Show();
            };
            
            //bluetooth_worker.DoWork += new DoWorkEventHandler(HandleBluetoothSignal);
        }

        private void InitializeBluetooth()
        {
            adapter = BluetoothAdapter.DefaultAdapter;

            if (!adapter.IsEnabled)
                adapter.Enable();

        }

        private void InitializeLinkers()
        {
            api_linker = new APILinker();
            // bluetooth_linker = new BluetoothLinker(adapter);
        }

        #endregion

        #region Callbacks

        private void HandleAttentionLevel(object sender, DoWorkEventArgs e)
        {
            Random random = new Random();

            attention_levels.Add(new Attention() {
                    Ratio = random.NextDouble(),
                    Alpha = random.NextDouble(),
                    Theta = random.NextDouble()
                }
            );
            List<string> ratios = attention_levels.Select(attention => attention.Ratio.ToString()).ToList();
            RunOnUiThread(() =>
            {
                TextView text_view = FindViewById<TextView>(Resource.Id.label_list);
                ImageView sun_rising = FindViewById<ImageView>(Resource.Id.sun_rising);

                text_view.Text = String.Join(" ", ratios);
                Glide.With(this)
                     .AsGif()
                     .Load("https://66.media.tumblr.com/38e6b86d0c8e7fddab224c299fdea129/tumblr_nxctlu1Oy11s8mouyo1_1280.gif")
                     .Apply(new RequestOptions().Override(1000, 1000))
                     .Into(sun_rising);
            });
        }

        private void HandleBluetoothSignal(object sender, DoWorkEventArgs e)
        {
        }

        #endregion

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}