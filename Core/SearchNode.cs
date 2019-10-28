namespace bootstar
{
	using bootstar.Generic;
	using System.Collections.Generic;

	internal class SearchNode<T> where T : GraphNode
	{
		public SearchNode<T> Parent { get { return _parent; } }
		public T Node { get { return _node; } }
		public float F { get { return _g + _h; } }
		public float H { get { return _h; } }
		public float G { get { return _g; } }

		public static implicit operator bool(SearchNode<T> n) { return n != null; }

		public SearchNode(T node, float edgeWeight, float h, SearchNode<T> parent = null)
		{
			_h = h;
			_node = node;
			SetParent(parent, edgeWeight);
		}

		public void SetParent(SearchNode<T> parent, float edgeWeight)
		{
			_parent = parent;
			_g = (parent ? parent.G : 0) + edgeWeight;
		}

		public T[] RetraceSteps()
		{
			var path = new List<T>();
			var current = this;
			while (current != null)
			{
				path.Add(current.Node);
				current = current.Parent;
			}
			var arr = new T[path.Count];
			for (var i = arr.Length - 1; i > -1; i--) { arr[i] = path[i]; }
			return arr;
		}

		private T _node = null;
		private float _h = 0f, _g = 0f;
		private SearchNode<T> _parent = null;
	}
}
