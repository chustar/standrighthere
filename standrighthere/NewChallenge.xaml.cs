using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Windows.Devices.Geolocation;

using standrighthere.ViewModels;
using System.IO;
using Windows.Storage.Streams;
using Parse;

namespace standrighthere
{
    public partial class NewChallenge : PhoneApplicationPage
    {
        public NewChallenge()
        {
            InitializeComponent();
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
                filename = e.OriginalFileName;
                
                System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                Image.Source = bmp;
                Image.Width = 373;
                Image.Height= 373;
                ChosenPhotoStream = e.ChosenPhoto.AsInputStream();
            }
        }

        private async void OneShotLocation()
        {
            var geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            try
            {
                var geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );
                geoPoint = new ParseGeoPoint(geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude);
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

        private async void SaveChallenge(object sender, EventArgs e)
        {
            if (geoPoint.Equals(new ParseGeoPoint()))
            {
                MessageBox.Show("Still finding your location. Please try again...");
            }
            else if (filename == null)
            {
                MessageBox.Show("You need a picture to create a new challenge.");
            }
            else if (Title.Text.Trim() == "")
            {
                MessageBox.Show("You need to name the challenge.");
            }
            else
            {
                var image = new ParseFile(Title.Text, ChosenPhotoStream.AsStreamForRead());
                await image.SaveAsync();

                var challenge = new ParseObject("Challenge")
                {
                    {"user", App.UserDetails},
                    {"title", Title.Text},
                    {"description", Description.Text},
                    {"image", image},
                    {"geoPoint", geoPoint}
                };
                await challenge.SaveAsync();

                if (challenge.ObjectId != "")
                {
                    NavigationService.Navigate(new Uri("/Challenge.xaml?id=" + challenge.ObjectId, UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("We couldn't save the challenge. Please try again.");
                }
            }
        }

        private void CancelChallenge(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
        }

        private ParseGeoPoint geoPoint;
        private string filename;
        private ChallengeViewModel challengeViewModel;
        private ChallengeViewModel ChallengeViewModel
        {
            get
            {
                if (challengeViewModel == null)
                {
                    challengeViewModel = new ChallengeViewModel();
                }
                return challengeViewModel;
            }
        }

        IInputStream ChosenPhotoStream;

    }
}