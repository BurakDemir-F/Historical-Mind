using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    [Serializable]
    public class TdList<T> : IEnumerable<T>
    {
        private readonly List<T> _list;
        private int _width, _height;

        public TdList(int x,int y, T defaultValue)
        {
            _list = Enumerable.Repeat(defaultValue, x * y).ToList();
            _width = x;
            _height = y;
        }

        public TdList(List<T> oDList, int x, int y)
        {
            _list = oDList;
            _width = x;
            _height = y;
        }

        public T this[int x, int y]
        {
            get
            {
                var count = _list.Count;
                if (x * y >= count && count > 0)
                {
                    throw new ArgumentOutOfRangeException($"x value: {x}, y value: {y}, index value: {GetIndex(x,y, _width)}");
                }
            
                return _list[GetIndex(x, y, _width)];
            }
            set
            {
                var count = _list.Count;
                if (x * y >= count && count > 0)
                {
                    throw new ArgumentOutOfRangeException($"x value: {x}, y value: {y}, index value: {GetIndex(x,y, _width)}");
                }

                _list[GetIndex(x, y, _width)] = value;
            }
        }

        public List<T> GetOdList()
        {
            return _list;
        }
    
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _list)
            {
                yield return item;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void DoAll(Action<T> doAll)
        {
            if(doAll == null) return;
        
            for (var x = 0; x < _width; x++)
            {
                for (var  y = 0; y < _height; y++)
                {
                    doAll(this[x,y]);
                }
            }
        }
    
        public void SetAllValues(T value)
        {
            for (var x = 0; x < _width; x++)
            {
                for (var  y = 0; y < _height; y++)
                {
                    this[x, y] = value;
                }
            }
        }
        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void AddItem(T item)
        {
            _list.Add(item);
        }

        public void RemoveItem(T item)
        {
            _list.Remove(item);
        }
    
        public static int GetIndex(int x, int y, int width)
        {
            return y * width + x;
        }
    }
}