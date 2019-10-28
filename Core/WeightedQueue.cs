/*
 * WeightedQueue
 * --
 * Data structure used to sort items according to weight value
 */

namespace bootstar
{
	using System;
	using System.Collections.Generic;

	internal class WeightedQueue<T>
	{
		/// <summary>
		/// Current size of the queue
		/// </summary>
		public int Count { get { return _weights.Count;  } }

		/// <summary>
		/// Adds new item to the queue
		/// </summary>
		public void Enqueue(float weight, T item)
		{
			if(_weights.Count > 0)
			{
				int i = FindInsertionIndex(weight);
				_weights.Insert(i, weight);
				_items.Insert(i, item);
			}
			else
			{
				_weights.Add(weight);
				_items.Add(item);
			}
		}

		/// <summary>
		/// Dequeues item with the least weight from queue (if possible)
		/// </summary>
		public bool TryDequeue(out T item, out float weight)
		{
			if (_weights.Count > 0) { Dequeue(out item, out weight); return true; }
			item = default(T);
			weight = 0;
			return false;
		}

		/// <summary>
		/// Dequeues item with the least weight from queue (if possible)
		/// </summary>
		public bool TryDequeue(out T item)
		{
			float w;
			if (_weights.Count > 0) { Dequeue(out item, out w); return true; }
			item = default(T);
			return false;
		}

		/// <summary>
		/// Retrieves the items in the queue as an array
		/// </summary>
		public T[] ToArray()
		{
			var arr = new T[Count];
			for(var i = 0; i < Count; i++) { arr[i] = _items[i]; }
			return arr;
		}

		/// <summary>
		/// Checks if item exists in queue
		/// </summary>
		public bool Contains(T item)
		{
			return _items.Contains(item);
		}

		/// <summary>
		/// Checks if item exists in queue
		/// </summary>
		public bool Contains(Predicate<T> condition)
		{
			return _items.Find(condition) != null;
		}

		/// <summary>
		/// Retrieves index of item matching query
		/// </summary>
		public int IndexOf(Predicate<T> condition)
		{
			return -1;
		}

		/// <summary>
		/// Takes item out of queue 
		/// </summary>
		public T TakeItem(Predicate<T> condition)
		{
			int index = _items.FindIndex(condition);
			if(index > -1)
			{
				var t = _items[index];
				_items.RemoveAt(index);
				_weights.RemoveAt(index);
				return t;
			}
			return default(T);
		}


		private List<float> _weights = new List<float>();
		private List<T> _items = new List<T>();

		// finds index in list to insert given weight
		private int FindInsertionIndex(float weight)
		{
			var i = 0;
			for(; i < _weights.Count; i++)
			{
				if(_weights[i] >= weight) { break; }
			}
			return i;
		}

		// dequeues item top item
		private void Dequeue(out T item, out float weight)
		{
			weight = _weights[0];
			item = _items[0];
			_weights.RemoveAt(0);
			_items.RemoveAt(0);
		}
	}
}
