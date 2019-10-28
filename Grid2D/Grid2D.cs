namespace bootstar.Grid2D
{
	using System;
	using System.Collections.Generic;
	using bootstar.Generic;

	public class Grid2D : IGraphProvider<Grid2DNode>
	{
		public Heuristic2D HeuristicMethod
		{
			get { return _heuristicsMethod; }
			set { _heuristicsMethod = value; }
		}

		public Grid2D(int width, int height)
		{
			_width = width;
			_height = height;
			_cells = new Grid2DNode[_width][];
			for(var i = 0; i < _width; i++)
			{
				_cells[i] = new Grid2DNode[_height];
				for (var j = 0; j < _height; j++)
				{
					_cells[i][j] = new Grid2DNode(i, j);
				}
			}
		}

		public float H(Grid2DNode current, Grid2DNode goal)
		{
			float x = Math.Abs(goal.x - current.y), y = Math.Abs(goal.y - current.y);
			switch (HeuristicMethod)
			{
				case Heuristic2D.DiagonalDistance:
					return (float) Math.Sqrt((x * x) + (y * y)); // diagonal
				default:
					return x + y; // manhattan
			}
		}

		public IEnumerable<EdgeInfo<Grid2DNode>> GetEdges(Grid2DNode node)
		{
			var edges = new List<EdgeInfo<Grid2DNode>>();
			if (node.blocked) { return edges; }
			for (var i = 0; i < _edgeCoordinates.Length - 2; i += 3)
			{
				var cx = _edgeCoordinates[i] + node.x;
				var cy = _edgeCoordinates[i + 1] + node.y;
				var w = _edgeCoordinates[i + 2];
				if (!WithinBounds(cx, cy) || IsBlocked(cx, cy)) { continue; }
				edges.Add(new EdgeInfo<Grid2DNode>(w, _cells[cx][cy]));
			}
			return edges;
		}

		public Grid2DNode GetAt(int x, int y)
		{
			return WithinBounds(x, y) && !_cells[x][y].blocked ? _cells[x][y] : null;
		}

		public void Block(int x, int y)
		{
			if(WithinBounds(x, y)) { _cells[x][y].blocked = true; }
		}

		private int _width = 0, _height = 0;
		private Grid2DNode[][] _cells = null;
		private Heuristic2D _heuristicsMethod = Heuristic2D.ManhattanDistance;

		// x, y, cost
		private int[] _edgeCoordinates =
		{
			0,1,1,
			0,-1,1,
			1,0,1,
			-1,0,1,
		};

		private class DirectionConfig
		{
			public int x, y, c;
			public DirectionConfig(int x, int y, int c) { this.x = x; this.y = y; this.c = c; }
		}

		private bool WithinBounds(int x, int y)
		{
			return x >= 0 && x < _width && y >= 0 && y < _height;
		}

		private bool IsBlocked(int x, int y)
		{
			return _cells[x][y].blocked;
		}
	}
}