using System;
using System.Linq;
using Greysource;
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
    }
}
