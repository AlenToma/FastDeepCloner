using System;
using System.Collections.Generic;
using System.Reflection;

namespace FastDeepCloner
{
    /// <summary>
    /// FastDeepCloner Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Override create instance default is emit
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public delegate object CreateInstance(Type type);
        private static readonly Dictionary<Type, int> TypeDict = new Dictionary<Type, int>
        {
         {typeof(int),0},
         {typeof(double),0},
         {typeof(float),0},
         {typeof(bool),0},
         {typeof(decimal),0},
         {typeof(long),0},
         {typeof(DateTime),0},
         {typeof(ushort),0},
         {typeof(short),0},
         {typeof(sbyte),0},
         {typeof(byte),0},
         {typeof(ulong),0},
         {typeof(uint),0},
         {typeof(char),0},
         {typeof(TimeSpan),0},
         {typeof(decimal?),0},
         {typeof(int?),0},
         {typeof(double?),0},
         {typeof(float?),0},
         {typeof(bool?),0},
         {typeof(long?),0},
         {typeof(DateTime?),0},
         {typeof(ushort?),0},
         {typeof(short?),0},
         {typeof(sbyte?),0},
         {typeof(byte?),0},
         {typeof(ulong?),0},
         {typeof(uint?),0},
         {typeof(char?),0},
         {typeof(TimeSpan?),0},
         {typeof(string),0},
         {typeof(Enum),0},
         {typeof(byte[]),0},
        };


        /// <summary>
        /// Determines if the specified type is an internal type.
        /// </summary>
        /// <param name="o"></param>
        /// <returns><c>true</c> if type is internal, else <c>false</c>.</returns>
        internal static bool IsInternalObject(this object o)
        {
            return o is Enum;

        }

        /// <summary>
        /// Validate if type is AnonymousType
        /// This is the very basic validation
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAnonymousType(this Type type)
        {
            return type.Name.Contains("AnonymousType") && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"));
        }

        /// <summary>
        /// Determines if the specified type is a Class type.
        /// </summary>
        /// <param name="underlyingSystemType"></param>
        /// <returns><c>true</c> if type is internal, else <c>false</c>.</returns>
        public static bool IsInternalType(this Type underlyingSystemType)
        {
            return (TypeDict.ContainsKey(underlyingSystemType) || !underlyingSystemType.GetTypeInfo().IsClass) && !underlyingSystemType.GetTypeInfo().IsInterface;
        }
    }
}
