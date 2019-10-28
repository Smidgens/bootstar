namespace bootstar.Grid2D
{
	using bootstar.Generic;

	public class Grid2DNode : GraphNode
	{
		public int x { get { return _x; } }
		public int y { get { return _y; } }
		public bool blocked = false;
		public Grid2DNode(int x, int y)
		{
			_x = x;
			_y = y;
		}
		private int _x = -1, _y = -1;
	}
}