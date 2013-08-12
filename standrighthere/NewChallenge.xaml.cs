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

using Microsoft.Phone.Maps.Controls;

using standrighthere.ViewModels;
using System.IO;
using Windows.Storage.Streams;
using Parse;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace standrighthere
{
    public partial class NewChallenge : PhoneApplicationPage
    {
        public NewChallenge()
        {
            InitializeComponent();
            if (App.PhotoResult != null)
            {
                var bmp = new BitmapImage();
                bmp.SetSource(App.PhotoResult.ChosenPhoto);
                Image.Source = bmp;
            }

            OneShotLocation();
        }

        private async void OneShotLocation()
        {
            geoPosition = await Utilities.GeoLocationHelper.GetLocation();
            Map.Center = geoPosition.Coordinate.ToGeoCoordinate();
        }

        public class Progress : IProgress<ParseUploadProgressEventArgs>
        {
            public void Report(ParseUploadProgressEventArgs args)
            {
                var x = args;
            }
        }

        private async void SaveChallenge(object sender, EventArgs e)
        {
            var geoPoint = new ParseGeoPoint(geoPosition.Coordinate.Latitude, geoPosition.Coordinate.Longitude);
            if (geoPoint.Equals(new ParseGeoPoint()))
            {
                MessageBox.Show("Still finding your location. Please try again...");
            }
            else if (Title.Text.Trim() == "")
            {
                MessageBox.Show("You need to name the challenge.");
            }
            else
            {
                App.PhotoResult.ChosenPhoto.Seek(0, SeekOrigin.Begin);
                var image = new ParseFile(System.IO.Path.GetFileName(App.PhotoResult.OriginalFileName), App.PhotoResult.ChosenPhoto);
                await image.SaveAsync(new Progress());
                App.PhotoResult.ChosenPhoto.Close();

                var challenge = new ParseObject("Challenge")
                {
                    {"user", App.UserDetails},
                    {"title", Title.Text},
                    {"image", image},
                    {"geoPoint", geoPoint}
                };
                await challenge.SaveAsync();

                if (challenge.ObjectId != "")
                {
                    NavigationService.Navigate(new Uri("/Home.xaml?id=" + challenge.ObjectId, UriKind.Relative));
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

        private Geoposition geoPosition;
    }
}