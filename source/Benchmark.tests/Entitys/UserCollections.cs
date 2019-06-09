using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmarklb.Entitys
{
    public class UserCollections : List<User>
    {
        public string TestValue { get; set; }

        public User us { get; set; }
    }
}
