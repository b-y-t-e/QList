using System;
using System.Linq;
using QList;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QList.Tests
{
    [TestClass]
    public class QList_AddTests
    {
        [TestMethod]
        public void adding_one_item_sould_work_properly()
        {
            QList<Person> list = new QList<Person>();
            Person newPerson = PersonHelper.CreateTestPerson();

            list.Add(newPerson);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(newPerson, list[0]);
            Assert.AreEqual(newPerson, list.FirstOrDefault());

        }

        [TestMethod]
        public void inserting_sould_work_properly()
        {
            QList<Person> list = new QList<Person>();

            Int32 count = 100;
            for (var i = 0; i < count; i++)
                list.Add(PersonHelper.CreateTestPerson());

            Person firstPerson = PersonHelper.CreateTestPerson();
            list.Insert(0, firstPerson);

            Person lastPerson = PersonHelper.CreateTestPerson();
            list.Insert(list.Count, lastPerson);

            Person middlePerson = PersonHelper.CreateTestPerson();
            list.Insert(50, middlePerson);

            Assert.IsTrue(list.IndexOf(firstPerson) >= 0);
            Assert.IsTrue(list.IndexOf(lastPerson) >= 0);
            Assert.IsTrue(list.IndexOf(middlePerson) >= 0);
            Assert.AreEqual(count + 3, list.Items.Count());


        }

        [TestMethod]
        public void adding_few_item_sould_work_properly()
        {
            QList<Person> list = new QList<Person>();
            Person person1 = PersonHelper.CreateTestPerson();
            Person person2 = PersonHelper.CreateTestPerson();
            Person person3 = PersonHelper.CreateTestPerson();
            Person person4 = PersonHelper.CreateTestPerson();
            Person person5 = PersonHelper.CreateTestPerson();

            list.Add(person1);
            list.Add(person2);
            list.Add(person3);
            list.Add(person4);
            list.Add(person5);

            Assert.AreEqual(5, list.Count);
            Assert.AreEqual(person1, list[0]);
            Assert.AreEqual(person1, list.FirstOrDefault());


            Assert.AreEqual(person2, list[1]);
            Assert.AreEqual(person3, list[2]);
            Assert.AreEqual(person4, list[3]);

            Assert.AreEqual(person5, list[4]);
            Assert.AreEqual(person5, list.LastOrDefault());

        }
    }
}
