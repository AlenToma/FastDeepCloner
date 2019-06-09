using FastDeepCloner.tests.Entitys;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FastDeepCloner.tests
{
    [TestClass]
    public class DeepClonerTests
    {

        [TestMethod]
        public void ProxyTest()
        {
            var pUser = DeepCloner.CreateProxyInstance<User>();
            pUser.Name = "testo";
        }


        [TestMethod]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToBeCloned">Desire object to cloned</param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public void Clone()
        {
            var user = new List<User>();
            user.Add(new User() { Name = "Alen" });

            var cloned = FastDeepCloner.DeepCloner.Clone(user);
            Assert.AreEqual(user.First().Name, cloned.First().Name);
        }

        [TestMethod]

        public void DynamicClone()
        {
            dynamic originalAnonnymousObject = new
            {
                prop1 = "p1"
            ,
                prop2 = 10
            };
            dynamic clonedAnonnymousObject = FastDeepCloner.DeepCloner.CloneDynamic(originalAnonnymousObject);
            Assert.AreEqual(clonedAnonnymousObject.prop1, originalAnonnymousObject.prop1);

        }

        [TestMethod]
        public void CloneCollection()
        {
            var user = new UserCollections();
            user.Add(new User() { Name = "Alen" });
            user.TestValue = "Alen";
            user.us = new User() { Name = "Toma" };
            var cloned = DeepCloner.Clone(user);
            Assert.AreEqual(cloned.us.Name, user.us.Name);
            Assert.AreEqual(cloned.TestValue, "Alen");

        }
    }
}
