
namespace bootstar.Grid2D
{
	using bootstar;

	public static partial class Extensions
	{
		public static PathResult2D FindPath(this Grid2D p, int ax, int ay, int bx, int by)
		{
			var path = AStar<Grid2DNode>.GetPath(p.GetAt(ax, ay), p.GetAt(bx, by), p);
			var indices = new int[path.Length * 2];
			for(var i = 0; i < path.Length; i++)
			{
				var j = i * 2;
				indices[j] = path[i].x;
				indices[j + 1] = path[i].y;
			}
			return new PathResult2D(indices);
		}
	}
}