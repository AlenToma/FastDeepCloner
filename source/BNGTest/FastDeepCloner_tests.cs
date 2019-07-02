using BenchmarkDotNet.Attributes;
using BNGTest.Entitys;
using FastDeepCloner;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNGTest
{
    //[ClrJob, CoreJob]
    //[RPlotExporter, RankColumn]
    public class FastDeepCloner_tests
    {
        [Benchmark]
        public void ProxyTest()
        {
            var pUser = DeepCloner.CreateProxyInstance<User>();
            pUser.Name = "testo";
        }

        [Benchmark]
        public void Clone()
        {
            var user = new List<User>();
            user.Add(new User() { Name = "Alen" });

            var cloned = DeepCloner.Clone(user);
        }

        [Benchmark]
        public void DynamicClone()
        {
            dynamic originalAnonnymousObject = new
            {
                prop1 = "p1"
            ,
                prop2 = 10
            };
            dynamic clonedAnonnymousObject = DeepCloner.CloneDynamic(originalAnonnymousObject);
        }

        [Benchmark]
        public void CloneCollection()
        {
            var user = new UserCollections();
            user.Add(new User() { Name = "Alen" });
            user.TestValue = "Alen";
            user.us = new User() { Name = "Toma" };
            var cloned = DeepCloner.Clone(user);
        }
    }
}
