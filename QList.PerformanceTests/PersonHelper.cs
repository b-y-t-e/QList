using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QList.Tests
{
    internal static class PersonHelper
    {
        internal static Person CreateTestPerson()
        {
            return new Person()
            {
                Name = "John",
                Created = DateTime.Now
            };
        }
    }
}
