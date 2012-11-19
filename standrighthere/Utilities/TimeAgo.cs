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
            if (minutes < 1)
            {
                return "now";
            }
            else if (minutes < 60)
            {
                return (int)minutes + " mins";
            }
            else if (minutes < 60 * 48)
            {
                return Math.Round(new decimal((minutes / 60))).ToString() + " hours";
            }
            else if (minutes < 60 * 48 * 31)
            {
                return Math.Round(new decimal((minutes / 1440))).ToString() + " days";
            }
            else if (minutes < 60 * 48 * 31 * 12)
            {
                return Math.Round(new decimal((minutes / (60 * 24 * 31 * 12)))).ToString() + " months";
            }
            else 
            {
                return Math.Round(new decimal((minutes / (60 * 24 * 31 * 12)))).ToString() + "years";
            }
        }
    }
}
