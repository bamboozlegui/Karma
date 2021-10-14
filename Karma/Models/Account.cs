using System.Collections.Generic;

namespace Karma.Models
{
    public class Account
    {
        public string Name { get; set; }

	public IEnumerable<string> Items { get; set; }

	public IEnumerable<string> Requests { get; set; }
    }
}
