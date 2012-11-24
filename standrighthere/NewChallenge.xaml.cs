using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Windows.Devices.Geolocation;

namespace standrighthere
{
    public partial class NewChallenge : PhoneApplicationPage
    {
        public NewChallenge()
        {
            InitializeComponent();
            Challenge = new Challenge();

            OneShotLocation();
        }

        private void StartCameraCapture(object sender, System.Windows.Input.GestureEventArgs e)
        {
            CameraCaptureTask camera = new CameraCaptureTask();
            camera.Completed += new EventHandler<PhotoResult>(OnCameraCaptureCompleted);

            camera.Show();
        }

        private void OnCameraCaptureCompleted(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                Challenge.Image = e.OriginalFileName;
                
                System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                Image.Source = bmp;
            }
        }

        private async void OneShotLocation()
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );

                Challenge.Location = geoposition;
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    MessageBox.Show("Location is disabled in phone settings.");
                }
                else
                {
                    MessageBox.Show("We couldn't find your location. Retrying...");
                    OneShotLocation();
                }
            }
        }

        private void CreateChallenege(object sender, EventArgs e)
        {
            if (Challenge.Location == null)
            {
                MessageBox.Show("Still finding your location. Please try again...");
            }
            Challenge.Title = Title.Text;
            Challenge.Description = Description.Text;
            Challenge.Created = DateTime.Now;
            Challenge.Modified = DateTime.Now;
            return;
        }

        private void CancelChallenge(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("Home.xaml"));
        }

        public Challenge Challenge
        {
            get; set;
        }
    }
}