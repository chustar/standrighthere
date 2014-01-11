using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace standrighthere.Utilities
{
    public static class TimeAgo
    {
        public static string GetTimeAgo(DateTime time)
        {
            DateTime startDate = DateTime.Now;
            TimeSpan deltaMinutes = startDate.Subtract(time);
            string distance = string.Empty;
            distance = DistanceOfTimeInWords(deltaMinutes.TotalMinutes);
            if (deltaMinutes.Minutes < 0)
            {
                return distance + " from  now";
            }
            else
            {
                return distance;
            }
        }

        static string DistanceOfTimeInWords(double minutes)
        {
            decimal value = 0;
            string suffix = string.Empty;

            if (minutes < 1)
            {
                suffix = "now";
            }
            else if (minutes < 60)
            {

                value = (int)minutes;
                suffix = "min";
            }
            else if (minutes < 60 * 48)
            {
                value = Math.Ceiling(new decimal((minutes / 60)));
                suffix = "hour";
            }
            else if (minutes < 60 * 48 * 31)
            {
                value = Math.Ceiling(new decimal((minutes / 1440)));
                suffix = "day";
            }
            else if (minutes < 60 * 48 * 31 * 12)
            {
                value = Math.Ceiling(new decimal((minutes / (60 * 24 * 31 * 12))));
                suffix = "month";
            }
            else 
            {
                value = Math.Ceiling(new decimal((minutes / (60 * 24 * 31 * 12))));
                suffix = "year";
            }

            return (value == 1 ? value + " " + suffix : value + " " + suffix + "s") + (value < 0 ? "" : " ago");

        }
    }
}
