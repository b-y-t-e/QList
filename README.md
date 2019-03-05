# QList<T>
Implementation of Double Linked List with IList<T> interface.
 + in some cases much more faster than standard .net List<T> 
  
Benchmark:
 + collection of 1000000 items
   + time of 'Add(Object)' method
      + List<T> - 2ms
      + QList<T> - 46ms
   + time of 'Delete(Object)' method
      + List<T> - 16141ms
      + QList<T> - 17ms
