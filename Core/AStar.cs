/*
 * AStar
 * --
 * Generic wrapper for A* pathfinding
 */

namespace bootstar
{
	using bootstar.Generic;
	using System.Collections.Generic;

	public static class AStar<T> where T : GraphNode
	{
		/// <summary>
		/// Computes traversible path between given points
		/// </summary>
		public static T[] GetPath(T start, T goal, IGraphProvider<T> provider)
		{
			if(start == null || goal == null) { return new T[0]; }
			var open = new PList();
			var closed = new PList();
			open.Add(new SearchNode<T>(start, 0f, provider.H(start, goal), null));
			var result = TraversePath(goal, open, closed, provider);
			if(result != null) { return result.RetraceSteps(); }
			return new T[0];
		}

		private static SearchNode<T> TraversePath(T goal, PList open, PList closed, IGraphProvider<T> _provider)
		{
			if(open.Count == 0) { return null; }
			var current = open.FindBest();
			if(current.Node == goal) { return current; }
			closed.Add(current);
			var edges = _provider.GetEdges(current.Node);

			foreach(var e in edges)
			{
				if(closed.HasNode(e.Node)) { continue; }
				var openChild = open.FindNode(e.Node);
				if(!openChild)
				{
					openChild = new SearchNode<T>(e.Node, e.Cost, _provider.H(e.Node, goal), current);
					open.Add(openChild);
				}
				else
				{
					float g = current.G + e.Cost;
					if ((g + openChild.H) < openChild.F)
					{
						openChild.SetParent(current, e.Cost);
					}
				}
			}
			return TraversePath(goal, open, closed, _provider);
		}

		private class PList : List<SearchNode<T>>
		{
			public SearchNode<T> FindBest()
			{
				float min = float.MaxValue;
				int index = -1;
				for (var i = 0; i < Count; i++)
				{
					var f = this[i].F;
					if (f < min) { index = i; min = f; }
				}
				var r = this[index];
				RemoveAt(index);
				return r;
			}

			public SearchNode<T> FindNode(T n) { return Find(pn => pn.Node == n); }

			public bool HasNode(T node)
			{
				return Find(pn => pn.Node == node) != null;
			}
		}
	}
}