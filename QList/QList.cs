using System;
using System.Collections;
using System.Collections.Generic;

namespace QList
{
    public class QList<T> : IEnumerable<T>, IList<T>
    {
        public IEnumerable<T> Items
        {
            get
            {
                QListItem<T> qItem = qFirst;
                while (qItem != null)
                {
                    yield return qItem.Item;
                    qItem = qItem.Next;
                }
            }
        }

        public Int32 Count
        {
            get { return itemQItemDict.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public T this[int index]
        {
            get
            {
                QListItem<T> qItem = GetByIndex(index);
                return qItem.Item;
            }
            set
            {
                QListItem<T> qItem = GetByIndex(index);
                itemQItemDict.Remove(qItem.Item);
                itemQItemDict.Add(value, qItem);
                qItem.Item = value;
            }
        }

        //////////////////////////////////////////////////////////////////

        Dictionary<T, QListItem<T>> itemQItemDict;

        QListItem<T> qFirst;

        QListItem<T> qLast;

        //////////////////////////////////////////////////////////////////

        public QList()
        {
            itemQItemDict = new Dictionary<T, QListItem<T>>();
        }

        //////////////////////////////////////////////////////////////////

        public bool Contains(T Item)
        {
            QListItem<T> qItem = null;
            if (!itemQItemDict.TryGetValue(Item, out qItem))
                return false;
            return true;
        }

        public bool Remove(T Item)
        {
            QListItem<T> qItem = null;
            if (!itemQItemDict.TryGetValue(Item, out qItem))
                return false;

            return RemoveQItem(qItem);
        }

        public void Clear()
        {
            this.itemQItemDict.Clear();

            QListItem<T> qItem = qFirst;
            while (qItem != null)
            {
                qItem.Clear();
                qItem = qItem.Next;
            }

            this.qFirst = null;
            this.qLast = null;
        }

        public void AddRange(IEnumerable<T> Items)
        {
            foreach (T item in Items)
                Add(item);
        }

        public bool Add(T Item)
        {
            if (itemQItemDict.ContainsKey(Item))
                return false;

            if (qFirst == null)
            {
                qLast = qFirst = new QListItem<T>()
                {
                    Item = Item,
                    Next = null,
                    Prev = null
                };
                itemQItemDict[Item] = qLast;
            }
            else
            {
                var newLast = new QListItem<T>()
                {
                    Item = Item,
                    Prev = qLast,
                    Next = null
                };

                qLast.Next = newLast;
                qLast = newLast;
                itemQItemDict[Item] = qLast;
            }

            return true;
        }

        public bool Replace(T OldItem, T NewItem)
        {
            QListItem<T> foundItem = null;
            if (!itemQItemDict.TryGetValue(OldItem, out foundItem))
                return false;

            itemQItemDict.Remove(OldItem);
            itemQItemDict.Add(NewItem, foundItem);
            foundItem.Item = NewItem;
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(T item)
        {
            Int32 i = -1;
            QListItem<T> qitem = qFirst;
            while (qitem != null)
            {
                i++;
                if (item.Equals(qitem.Item))
                    return i;
                qitem = qitem.Next;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (itemQItemDict.ContainsKey(item))
                return;

            if (index == Count)
            {
                Add(item);
                return;
            }

            if (index == 0)
            {
                QListItem<T> qNew = new QListItem<T>()
                {
                    Item = item,
                    Prev = null,
                    Next = qFirst
                };
                itemQItemDict[item] = qNew;
                qFirst.Prev = qNew;
                qFirst = qNew;
                return;
            }

            Int32 i = -1;
            QListItem<T> qitem = qFirst;
            while (qitem != null)
            {
                i++;
                if (i == index)
                {
                    QListItem<T> qNew = new QListItem<T>()
                    {
                        Item = item,
                        Prev = qitem,
                        Next = qitem.Next
                    };
                    qitem.Next.Prev = qNew;
                    qitem.Next = qNew;
                    itemQItemDict[item] = qNew;

                    return;
                }
                qitem = qitem.Next;
            }
        }

        public void RemoveAt(int index)
        {
            Int32 i = -1;
            QListItem<T> qitem = qFirst;
            while (qitem != null)
            {
                i++;
                if (i == index)
                {
                    RemoveQItem(qitem);
                    return;
                }
                qitem = qitem.Next;
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var i = arrayIndex;
            foreach (T item in Items)
                array[i++] = item;
        }

        //////////////////////////////////////////////////////////////////

        bool RemoveQItem(QListItem<T> foundItem)
        {
            itemQItemDict.Remove(foundItem.Item);

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

            if (this.qFirst == foundItem)
            {
                this.qFirst = next;
            }

            if (this.qLast == foundItem)
            {
                this.qLast = prev;
            }

            foundItem.Clear();

            return true;
        }

        QListItem<T> GetByIndex(Int32 Index)
        {
            if (Index >= Count || Index < 0)
                return null;

            if (Index < Count / 2)
            {
                Int32 i = -1;
                QListItem<T> qItem = qFirst;
                while (qItem != null)
                {
                    i++;
                    if (i == Index)
                        return qItem;
                    qItem = qItem.Next;
                }
            }
            else
            {
                Int32 i = Count;
                QListItem<T> qItem = qLast;
                while (qItem != null)
                {
                    i--;
                    if (i == Index)
                        return qItem;
                    qItem = qItem.Prev;
                }
            }
            return null;
        }

        void ICollection<T>.Add(T item)
        {
            this.Add(item);
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
