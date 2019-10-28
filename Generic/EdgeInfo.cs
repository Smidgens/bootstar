namespace bootstar.Generic
{
	public struct EdgeInfo<T> where T : GraphNode
	{
		public float Cost { get { return _cost; } }
		public T Node { get { return _node; } }
		public EdgeInfo(float c, T n) { _cost = c; _node = n; }
		private float _cost;
		private T _node;
	}
}
