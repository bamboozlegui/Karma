using System;

namespace Karma.Extensions
{
    public static class TimeExtensions
    {
	public static TimeSpan GetTimeSpan(this DateTime postDate)
	{
	    return DateTime.Now - postDate;
	}
	
        public static string ShowTimeSpan(this DateTime postDate)
        {
            TimeSpan interval = DateTime.Now - postDate;
	    
            if(interval.Days == 0)
                return "Today";

	    if(interval.Days == 1)
                return "Yesterday";

            return interval.Days.ToString() + " days ago";
        }
    }

}
