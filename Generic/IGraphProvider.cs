namespace bootstar.Generic
{
	using System.Collections.Generic;

	public interface IGraphProvider<T> where T : GraphNode
	{
		/// <summary>
		/// Heuristics function
		/// </summary>
		float H(T current, T goal);

		/// <summary>
		/// Gathers reachable edges
		/// </summary>
		IEnumerable<EdgeInfo<T>> GetEdges(T node);
	}
}