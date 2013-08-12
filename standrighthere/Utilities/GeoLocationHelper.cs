using System;
using Parse;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Devices.Geolocation;

namespace standrighthere.Utilities
{
    static class GeoLocationHelper
    {
        /// <summary>
        /// Checks if the user has given permission to use location. If not, ask them for confirmation.
        /// </summary>
        /// <returns>True if the user gives permission to use data.</returns>
        private static bool GetConfirmation()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("doLocation"))
            {
                return Convert.ToBoolean(IsolatedStorageSettings.ApplicationSettings["doLocation"]);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("We're about to use your location. Are you OK with that?", "Location", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    IsolatedStorageSettings.ApplicationSettings["doLocation"] = true;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings["doLocation"] = false;
                }
                IsolatedStorageSettings.ApplicationSettings.Save();
                return result == MessageBoxResult.OK;
            }
        }

        /// <summary>
        /// The last time when the location was updated.
        /// </summary>
        private static DateTime lastUpdateTime = DateTime.Now;

        /// <summary>
        /// Be careful with this function. It should only be called from functions 
        /// that need the location, but can't await.
        /// 
        /// Everyone else should use the other one to update the location if necessary.
        /// </summary>
        public static Geoposition CachedLocation { get; set; }

        /// <summary>
        /// This gets the last known location or gets a new one if the last one has expired.
        /// </summary>
        /// <param name="forceUpdate">Force updates even if the location has not expired.</param>
        /// <returns>The last good location.</returns>
        public static async Task<Geoposition> GetLocation(bool forceUpdate = false)
        {
            if (GetConfirmation())
            {
                var geolocator = new Geolocator();
                geolocator.DesiredAccuracy = PositionAccuracy.High;

                try
                {
                    if (CachedLocation == null || forceUpdate || lastUpdateTime - DateTime.Now < TimeSpan.FromMinutes(5))
                    {
                        CachedLocation = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(10));
                    }
                    return CachedLocation; 
                }
                catch (Exception ex)
                {
                    if ((uint)ex.HResult == 0x80004004)
                    {
                        MessageBox.Show("Location is disabled in phone settings.");
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return null;
        }
        
        public static ParseGeoPoint ToParseGeoPoint(this Geoposition position)
        {
            return new ParseGeoPoint(position.Coordinate.Latitude, position.Coordinate.Longitude);
        }
        
        public static string RelativeDistanceTo(this ParseGeoPoint position1, ParseGeoPoint position2)
        {
            var distance = position1.DistanceTo(position2).Miles;
            if (distance < 0.5)
            {
                return Math.Round((distance * 5280), 0) + " feet";
            }
            else if (distance < 1)
            {
                return Math.Round(distance, 2) + " miles";
            }
            return Math.Round(distance) == 1 ? Math.Round(distance) + " mile" : Math.Round(distance) + " miles";
        }
    }
}
