using FastDeepCloner.tests.Entitys;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FastDeepCloner.tests
{
    [TestClass]
    public class DeepClonerTests
    {
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
        public void ObservableCollectionClone()
        {
            var e = new Enterprise();
            e.Promoties = new ObservableCollection<User>() { new User() { Name = "Alen" } };
            e.OpeningHours.Add(new Entitys.testClasses.OpeningHour());
            e.Addresses.Add(new Entitys.testClasses.Address());
            e.Email = "alen.toma@gmail.com";
            var c = e.Clone();

            c.Promoties.First().Name = "djh";
            Assert.AreNotEqual(e.Promoties.First().Name, c.Promoties.First().Name);
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
