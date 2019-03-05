using System;
using System.Collections;
using System.Collections.Generic;

namespace Greysource
{
    public class QList<T> : IEnumerable<T>
    {
        public IEnumerable<T> Items
        {
            get
            {
                QListItem<T> item = first;
                while (item != null)
                {
                    yield return item.Item;
                    item = item.Next;
                }
            }
        }

        public Int32 Count
        {
            get { return dict.Count; }
        }

        public T this[int index]
        {
            get
            {
                QListItem<T> item = GetByIndex(index);
                return item.Item;
            }
            set
            {
                QListItem<T> item = GetByIndex(index);
                dict.Remove(item.Item);
                dict.Add(value, item);
                item.Item = value;
            }
        }

        //////////////////////////////////////////////////////////////////
        
        Dictionary<T, QListItem<T>> dict;

        QListItem<T> first;

        QListItem<T> last;

        //////////////////////////////////////////////////////////////////

        public QList()
        {
            dict = new Dictionary<T, QListItem<T>>();
        }

        //////////////////////////////////////////////////////////////////

        public bool Contains(T Item)
        {
            QListItem<T> foundItem = null;
            if (!dict.TryGetValue(Item, out foundItem))
                return false;
            return true;
        }

        public bool Remove(T Item)
        {
            QListItem<T> foundItem = null;
            if (!dict.TryGetValue(Item, out foundItem))
                return false;

            dict.Remove(Item);

            var prev = foundItem.Prev;
            var next = foundItem.Next;

            if (prev != null)
            {
                prev.Next = next;
            }

            if (next != null)
            {
                next.Prev = prev;
            }

            if (this.first == foundItem)
            {
                this.first = next;
            }

            if (this.last == foundItem)
            {
                this.last = prev;
            }

            foundItem.Clear();

            return true;
        }

        public void Clear()
        {
            this.dict.Clear();

            QListItem<T> item = first;
            while (item != null)
            {
                item.Clear();
                item = item.Next;
            }

            this.first = null;
            this.last = null;
        }

        public void AddRange(IEnumerable<T> Items)
        {
            foreach (T item in Items)
                Add(item);
        }

        public bool Add(T Item)
        {
            if (dict.ContainsKey(Item))
                return false;

            if (first == null)
            {
                last = first = new QListItem<T>()
                {
                    Item = Item,
                    Next = null,
                    Prev = null
                };
                dict[Item] = last;
            }
            else
            {
                var newLast = new QListItem<T>()
                {
                    Item = Item,
                    Prev = last,
                    Next = null
                };

                last.Next = newLast;
                last = newLast;
                dict[Item] = last;
            }

            return true;
        }

        public bool Replace(T OldItem, T NewItem)
        {
            QListItem<T> foundItem = null;
            if (!dict.TryGetValue(OldItem, out foundItem))
                return false;

            dict.Remove(OldItem);
            dict.Add(NewItem, foundItem);
            foundItem.Item = NewItem;
            return true;
        }

        //////////////////////////////////////////////////////////////////

        QListItem<T> GetByIndex(Int32 Index)
        {
            if (Index >= Count || Index < 0)
                return null;

            if (Index < Count / 2)
            {
                Int32 index = -1;
                QListItem<T> item = first;
                while (item != null)
                {
                    index++;
                    if (index == Index)
                        return item;
                    item = item.Next;
                }
            }
            else
            {
                Int32 index = Count;
                QListItem<T> item = last;
                while (item != null)
                {
                    index--;
                    if (index == Index)
                        return item;
                    item = item.Prev;
                }
            }
            return null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //////////////////////////////////////////////////////////////////

        internal class QListItem<T>
        {
            public T Item;

            public QListItem<T> Next;

            public QListItem<T> Prev;

            internal void Clear()
            {
                Item = default(T);
                Next = null;
                Prev = null;
            }
        }
    }

}
