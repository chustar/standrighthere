using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using Parse;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Windows.Devices.Geolocation;

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

            var task = LoadLocation();
        }

        /// <summary>
        /// Sets the maps center to the current location.
        /// </summary>
        /// <returns>An awaitable task.</returns>
        private async Task LoadLocation()
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
            else if (ChallengeTitle.Text.Trim() == "")
            {
                MessageBox.Show("You need to name the comment.");
            }
            else
            {
                App.PhotoResult.ChosenPhoto.Seek(0, SeekOrigin.Begin);
                var image = new ParseFile(System.IO.Path.GetFileName(App.PhotoResult.OriginalFileName), App.PhotoResult.ChosenPhoto);
                await image.SaveAsync(new Progress());
                App.PhotoResult.ChosenPhoto.Close();

                var challenge = new ParseObject("Challenge")
                {
                    {"user", ParseUser.CurrentUser},
                    {"title", ChallengeTitle.Text},
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
                    MessageBox.Show("We couldn't save the comment. Please try again.");
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