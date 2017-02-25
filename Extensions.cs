using System;
using System.Reflection;

namespace FastDeepCloner
{
    public static class Extensions
    {
        public delegate object CreateInstance(Type type);


        /// <summary>
        /// Determines if the specified type is an internal type.
        /// </summary>
        /// <param name="underlyingSystemType"></param>
        /// <returns><c>true</c> if type is internal, else <c>false</c>.</returns>
        internal static bool IsInternalObject(this object o)
        {
            return o is Enum;
           
        }

        /// <summary>
        /// Determines if the specified type is an internal type.
        /// </summary>
        /// <param name="underlyingSystemType"></param>
        /// <returns><c>true</c> if type is internal, else <c>false</c>.</returns>
        internal static bool IsInternalType(this Type underlyingSystemType)
        {
            return
                underlyingSystemType == typeof(string) |
                underlyingSystemType == typeof(decimal) |
                underlyingSystemType == typeof(int) |
                underlyingSystemType == typeof(double) |
                underlyingSystemType == typeof(float) |
                underlyingSystemType == typeof(bool) |
                underlyingSystemType == typeof(long) |
                underlyingSystemType == typeof(DateTime) |
                underlyingSystemType == typeof(ushort) |
                underlyingSystemType == typeof(short) |
                underlyingSystemType == typeof(sbyte) |
                underlyingSystemType == typeof(byte) |
                underlyingSystemType == typeof(ulong) |
                underlyingSystemType == typeof(uint) |
                underlyingSystemType == typeof(char) |
                underlyingSystemType == typeof(TimeSpan) |
                underlyingSystemType == typeof(decimal?) |
                underlyingSystemType == typeof(int?) |
                underlyingSystemType == typeof(double?) |
                underlyingSystemType == typeof(float?) |
                underlyingSystemType == typeof(bool?) |
                underlyingSystemType == typeof(long?) |
                underlyingSystemType == typeof(DateTime?) |
                underlyingSystemType == typeof(ushort?) |
                underlyingSystemType == typeof(short?) |
                underlyingSystemType == typeof(sbyte?) |
                underlyingSystemType == typeof(byte?) |
                underlyingSystemType == typeof(ulong?) |
                underlyingSystemType == typeof(uint?) |
                underlyingSystemType == typeof(char?) |
                underlyingSystemType == typeof(TimeSpan?);
        }
    }
}
