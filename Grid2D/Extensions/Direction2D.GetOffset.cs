namespace bootstar.Grid2D
{
	public static partial class Extensions
	{
		/// <summary>
		/// Retrieves array offset for a given direction
		/// </summary>
		public static int[] GetOffset(this Direction2D direction)
		{
			switch(direction)
			{
				case Direction2D.N: return new int[] { 0, -1 };
				case Direction2D.S: return new int[] { 0, 1 };
				case Direction2D.W: return new int[] { -1, 0 };
				case Direction2D.E: return new int[] { 1, 0 };
				case Direction2D.NW: return new int[] { -1, -1 };
				case Direction2D.NE: return new int[] { 1, -1 };
				case Direction2D.SW: return new int[] { -1, 1 };
				case Direction2D.SE: return new int[] { 1, 1 };
			}
			return new int[] { 0, 0 };
		}
	}
}