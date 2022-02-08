using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SerialPlotAndLog
{
	public class DefaultDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{
		protected readonly IDictionary<TKey, TValue> _d;
		protected readonly Func<TKey, TValue> _defaultGenerator;
		protected readonly TValue _defaulValue;

		public int Count => _d.Count;
		public bool IsReadOnly => _d.IsReadOnly;

		public DefaultDictionary(IDictionary<TKey, TValue> dictionary, TValue defaultValue)
		{
			_d = dictionary;
			_defaulValue = defaultValue;
		}
		public DefaultDictionary(IDictionary<TKey, TValue> dictionary, Func<TKey, TValue> defaultGenerator)
		{
			if (defaultGenerator == null)
				throw new ArgumentNullException(nameof(defaultGenerator));

			_d = dictionary;
			_defaultGenerator = defaultGenerator;
		}

		public DefaultDictionary(TValue defaultValue)
			: this(new Dictionary<TKey, TValue>(), defaultValue)
		{}
		public DefaultDictionary(Func<TKey, TValue> defaultGenerator)
		: this(new Dictionary<TKey, TValue>(), defaultGenerator)
		{}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _d.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		public void Add(KeyValuePair<TKey, TValue> item) => _d.Add(item);
		public void Add(TKey key, TValue value) => _d.Add(key, value);
		public bool Remove(KeyValuePair<TKey, TValue> item) => _d.Remove(item);
		public bool Remove(TKey key) => _d.Remove(key);
		public bool Contains(KeyValuePair<TKey, TValue> item) => _d.Contains(item);
		public bool ContainsKey(TKey key) => _d.ContainsKey(key);
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => _d.CopyTo(array, arrayIndex);
		public void Clear() => _d.Clear();
		public bool TryGetValue(TKey key, out TValue value) => _d.TryGetValue(key, out value);
		public TValue this[TKey key]
		{
			get => _d.TryGetValue(key, out TValue value) ? value : _d[key] = (_defaultGenerator != null ? _defaultGenerator(key) : _defaulValue);
			set => _d[key] = value;
		}
		public ICollection<TKey> Keys => _d.Keys;
		public ICollection<TValue> Values => _d.Values;
	}
}
