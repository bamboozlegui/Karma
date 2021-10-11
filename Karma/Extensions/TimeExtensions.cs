using System;

namespace Karma.Extensions
{
    public static class TimeExtensions
    {
        public static TimeSpan GetTimeSpan(this DateTime postDate) => DateTime.Now - postDate;

        public static string ShowTimeSpan(this DateTime postDate) =>
            (DateTime.Now - postDate).Days switch
	    {
		0 => "Today",
		1 => "Yesterday",
		_ => (DateTime.Now - postDate).Days.ToString() + " days ago"
	    };
    }

}
