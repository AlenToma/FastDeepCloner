## a list of internalType property

```csharp
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
        /// Determines if the specified type is a none Class type.
        /// </summary>
        /// <param name="underlyingSystemType"></param>
        /// <returns><c>true</c> if type is internal, else <c>false</c>.</returns>
        public static bool IsInternalType(this Type underlyingSystemType)
        {
            return (TypeDict.ContainsKey(underlyingSystemType) || !underlyingSystemType.GetTypeInfo().IsClass) && !underlyingSystemType.GetTypeInfo().IsInterface;
        }

```
