using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Devices.Geolocation;

namespace standrighthere.Utilities
{
    class GeoLocationHelper
    {
        public static bool GetConfirmation()
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

        public static async Task<Geoposition> GetLocation()
        {
            if (GetConfirmation())
            {
                var geolocator = new Geolocator();
                geolocator.DesiredAccuracy = PositionAccuracy.High;

                try
                {
                    return await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(10));
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
    }
}
