using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using QList.Tests;
using System.Globalization;

namespace QList.PerformanceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var count = 10000;
            object tmpObj = null;

            var items_to_add = new List<Person>();
            for (var i = 0; i < count; i++)
                items_to_add.Add(PersonHelper.CreateTestPerson());

            var list1 = new List<Person>(count);
            var list2 = new QList<Person>();

            Stopwatch st_create_1 = Stopwatch.StartNew();
            foreach (var item in items_to_add)
                list1.Add(item);
            st_create_1.Stop();


            Stopwatch st_create_2 = Stopwatch.StartNew();
            foreach (var item in items_to_add)
                list2.Add(item);
            st_create_2.Stop();


            Stopwatch st_read_1 = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
                tmpObj = list1[i];
            st_read_1.Stop();


            Stopwatch st_read_2 = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
                tmpObj = list2[i];
            st_read_2.Stop();


            Stopwatch st_enumerator_1 = Stopwatch.StartNew();
            foreach (var item in list1)
                tmpObj = item;
            st_enumerator_1.Stop();


            Stopwatch st_enumerator_2 = Stopwatch.StartNew();
            foreach (var item in list2)
                tmpObj = item;
            st_enumerator_2.Stop();


            Stopwatch st_indexof_1 = Stopwatch.StartNew();
            foreach (var item in items_to_add)
                tmpObj = list1.IndexOf(item);
            st_indexof_1.Stop();


            Stopwatch st_indexof_2 = Stopwatch.StartNew();
            foreach (var item in items_to_add)
                tmpObj = list2.IndexOf(item);
            st_indexof_2.Stop();



            Stopwatch st_Contains_1 = Stopwatch.StartNew();
            foreach (var item in items_to_add)
                tmpObj = list1.Contains(item);
            st_Contains_1.Stop();


            Stopwatch st_Contains_2 = Stopwatch.StartNew();
            foreach (var item in items_to_add)
                tmpObj = list2.Contains(item);
            st_Contains_2.Stop();




            var rand = new Random(DateTime.Now.Millisecond);
            var items_to_remove = items_to_add.OrderBy(i => rand.Next(0, count)).ToList();

            Stopwatch st_remove_1 = Stopwatch.StartNew();
            foreach (var item in items_to_remove)
                list1.Remove(item);
            st_remove_1.Stop();


            Stopwatch st_remove_2 = Stopwatch.StartNew();
            foreach (var item in items_to_remove)
                list2.Remove(item);
            st_remove_2.Stop();

            list2.Clear();
            foreach (var item in items_to_add)
                list2.Add(item);

            list1.Clear();
            foreach (var item in items_to_add)
                list1.Add(item);


            Stopwatch st_remove2_1 = Stopwatch.StartNew();
            for (var i = count - 1; i >= 0; i--)
                list1.RemoveAt(i);
            st_remove2_1.Stop();


            Stopwatch st_remove2_2 = Stopwatch.StartNew();
            for (var i = count - 1; i >= 0; i--)
                list2.RemoveAt(i);
            st_remove2_2.Stop();



            var index = 0;
            foreach (var item2 in list2)
            {
                var item1 = list1[index];

                if (item1 != item2 || list1.Count != list2.Count)
                {
                    throw new NotImplementedException();
                }

                index++;
            }

            var c = 4;
            Console.WriteLine($"         |  Insert  |  Delete  |    Read  |  IndexOf  |  RemoveAt  |  Contains   |  Enumerator ");
            Console.WriteLine($" List<>  |  {format(st_create_1.ElapsedMilliseconds, c)}ms  |  {format(st_remove_1.ElapsedMilliseconds, c)}ms  |  {format(st_read_1.ElapsedMilliseconds, c)}ms  |   {format(st_indexof_1.ElapsedMilliseconds, c)}ms  |    {format(st_remove2_1.ElapsedMilliseconds, c)}ms  |  {format(st_Contains_1.ElapsedMilliseconds, c)}ms  |  {format(st_enumerator_1.ElapsedMilliseconds, c)}ms");
            Console.WriteLine($"QList<>  |  {format(st_create_2.ElapsedMilliseconds, c)}ms  |  {format(st_remove_2.ElapsedMilliseconds, c)}ms  |  {format(st_read_2.ElapsedMilliseconds, c)}ms  |   {format(st_indexof_2.ElapsedMilliseconds, c)}ms  |    {format(st_remove2_2.ElapsedMilliseconds, c)}ms  |  {format(st_Contains_2.ElapsedMilliseconds, c)}ms  |  {format(st_enumerator_2.ElapsedMilliseconds, c)}ms");
            Console.ReadKey();
        }

        private static string format(object obj, int count)
        {
            string text = Convert.ToString(obj, CultureInfo.InvariantCulture);
            while (text.Length < count)
                text = " " + text;
            return text;
        }
    }
}
