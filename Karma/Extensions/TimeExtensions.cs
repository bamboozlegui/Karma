using System;

namespace Karma.Extensions
{
    public static class TimeExtensions
    {
        public static string ShowTimeSpan(this DateTime postDate)
        {
            TimeSpan interval = DateTime.Now - postDate;
            string intervalString = "";
            if(interval.Days == 0)
                intervalString = "Today";
            else
            {
                intervalString = interval.Days.ToString() + " days ago";
            }
            return intervalString;
        }
    }

}
