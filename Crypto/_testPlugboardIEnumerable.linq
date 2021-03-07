<Query Kind="Expression" />

//	IEnumerator IEnumerable.GetEnumerator()
//	{
//		return (IEnumerator) GetEnumerator();
//	}
//
//	public PlugEnum GetEnumerator()
//	{
//		return new PlugEnum(plugs);
//	}

//public class PlugEnum : IEnumerator
//{
//	public List<Plug> _plugs;
//	
//	int position = -1;
//	
//	public PlugEnum(List<Plug> list)
//	{
//		_plugs = list;
//	}
//	
//	public bool MoveNext()
//	{
//		position++;
//		return (position < _plugs.Length);
//	}
//	
//	public void Reset()
//	{
//		position = -1;
//	}
//	
//	object IEnumerator.Current
//	{
//		get
//		{
//			return Current;
//		}
//	}
//	
//	public Plug Current
//	{
//		get
//		{
//			try
//			{
//				return _plugs[position];
//			}
//			catch (IndexOutOfRangeException)
//			{
//				throw new InvalidOperationException();
//			}
//		}
//	}
//}