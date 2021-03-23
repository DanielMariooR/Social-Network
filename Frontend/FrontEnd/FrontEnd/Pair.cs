using System;

namespace FrontEnd
{
	public class Pair
	{
		public string vertex1 { get; set; }
		public string vertex2 { get; set; }

		public Pair(string v1, string v2)
		{
			this.vertex1 = v1;
			this.vertex2 = v2;
		}
	}
}