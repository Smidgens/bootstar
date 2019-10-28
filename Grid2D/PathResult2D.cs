namespace bootstar.Grid2D
{
	public struct PathResult2D
	{
		public int[] Path { get { return _path; } }
		public PathResult2D(params int[] path)
		{
			_path = path;
		}
		private int[] _path;
	}
}