using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
#if !NETSTANDARD1_3
using System.Runtime.Serialization;
#endif

namespace FastDeepCloner
{
    internal static class FastDeepClonerCachedItems
    {
        internal delegate object ObjectActivator();
        internal delegate object ObjectActivatorWithParameters(params object[] args);
        private static readonly SafeValueType<Type, SafeValueType<string, IFastDeepClonerProperty>> CachedFields = new SafeValueType<Type, SafeValueType<string, IFastDeepClonerProperty>>();
        private static readonly SafeValueType<Type, SafeValueType<string, IFastDeepClonerProperty>> CachedPropertyInfo = new SafeValueType<Type, SafeValueType<string, IFastDeepClonerProperty>>();
        private static readonly SafeValueType<Type, Type> CachedTypes = new SafeValueType<Type, Type>();
        private static readonly SafeValueType<string, Func<object[], object>> CachedConstructorWithParameter = new SafeValueType<string, Func<object[], object>>();
        private static readonly SafeValueType<string, Func<object>> CachedConstructor = new SafeValueType<string, Func<object>>();
        private static readonly SafeValueType<string, ObjectActivator> CachedDynamicMethod = new SafeValueType<string, ObjectActivator>();
        private static readonly SafeValueType<string, ObjectActivatorWithParameters> CachedDynamicMethodWithParameters = new SafeValueType<string, ObjectActivatorWithParameters>();
        private static readonly SafeValueType<string, ConstructorInfo> ConstructorInfos = new SafeValueType<string, ConstructorInfo>();
        private static readonly SafeValueType<Type, Type> ProxyTypes = new SafeValueType<Type, Type>();
        private static readonly SafeValueType<Type, MethodInfo> ProxyTypesPropertyChanged = new SafeValueType<Type, MethodInfo>();
        private static readonly SafeValueType<string, Assembly> CachedAssembly = new SafeValueType<string, Assembly>();
        private static readonly SafeValueType<string, Type> CachedStringTypes = new SafeValueType<string, Type>();


        public static void CleanCachedItems()
        {
            CachedFields.Clear();
            CachedTypes.Clear();
            CachedConstructor.Clear();
            CachedPropertyInfo.Clear();
            CachedDynamicMethod.Clear();
            ProxyTypes.Clear();
            CachedConstructorWithParameter.Clear();
            CachedStringTypes.Clear();
            CachedAssembly.Clear();
            CachedDynamicMethodWithParameters.Clear();
            ProxyTypesPropertyChanged.Clear();
        }



        internal static Type GetIListType(this Type type)
        {
            if (CachedTypes.ContainsKey(type))
                return CachedTypes[type];
            if (type.IsArray)
                CachedTypes.Add(type, type.GetElementType());
            else
            {
                if (type.GenericTypeArguments.Any())
                {
                    if (type.FullName.Contains("ObservableCollection`1"))
                        CachedTypes.Add(type, typeof(ObservableCollection<>).MakeGenericType(type.GenericTypeArguments.First()));
                    else
                        CachedTypes.Add(type, typeof(List<>).MakeGenericType(type.GenericTypeArguments.First()));

                }
                else if (type.FullName.Contains("List`1") || type.FullName.Contains("ObservableCollection`1"))
                {
                    if (type.FullName.Contains("ObservableCollection`1"))
                        CachedTypes.Add(type, typeof(ObservableCollection<>).MakeGenericType(type.GetRuntimeProperty("Item").PropertyType));
                    else
                        CachedTypes.Add(type, typeof(List<>).MakeGenericType(type.GetRuntimeProperty("Item").PropertyType));
                }
                else CachedTypes.Add(type, type);
            }
            return CachedTypes[type];
        }

#if !NETSTANDARD1_3


        internal static Type GetFastType(this string typeName, string assembly)
        {
            if (string.IsNullOrEmpty(assembly))
                throw new Exception("AssemblyName cannot be empty");

            if (!assembly.ToLower().EndsWith(".dll"))
                assembly += ".dll";
            var key = typeName + assembly;
            if (CachedStringTypes.ContainsKey(key))
                return CachedStringTypes.Get(key);

            var type = Type.GetType($"{typeName}, {assembly.Substring(0, assembly.ToLower().IndexOf(".dll"))}");
            if (type != null)
            {
                if (!CachedAssembly.ContainsKey(assembly))
                    CachedAssembly.TryAdd(assembly, type.Assembly);
                return CachedStringTypes.GetOrAdd(key, type);
            }
            else
              if (!CachedAssembly.ContainsKey(assembly))
                CachedAssembly.TryAdd(assembly, Assembly.LoadFrom(assembly));

            return CachedStringTypes.GetOrAdd(key, CachedAssembly.Get(assembly).GetType(typeName, true, true));
        }


        internal static INotifyPropertyChanged ProxyCreator(this Type type)
        {
            lock (ProxyTypes)
            {
                try
                {
                    MethodInfo method;
                    if (ProxyTypesPropertyChanged.ContainsKey(type))
                        method = ProxyTypesPropertyChanged[type];
                    else
                    {
                        method = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.GetParameters().Any(a => a.ParameterType == typeof(object)) && x.GetParameters().Any(a => a.ParameterType == typeof(PropertyChangedEventArgs)));
                        ProxyTypesPropertyChanged.TryAdd(type, method);
                    }
                    INotifyPropertyChanged item;
                    if (ProxyTypes.ContainsKey(type))
                        item = ProxyTypes[type].Creator() as INotifyPropertyChanged;
                    else
                    {
                        ProxyTypes.Add(type, INotifyPropertyChangedProxyTypeGenerator.GenerateProxy(type));
                        item = ProxyTypes[type].Creator() as INotifyPropertyChanged;
                    }

                    if (method != null)
                        item.PropertyChanged += (PropertyChangedEventHandler)Delegate.CreateDelegate(typeof(PropertyChangedEventHandler), item, method);



                    return item;

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
#endif
        internal static ConstructorInfo GetConstructorInfo(this Type type, params object[] parameters)
        {
            var key = type.FullName + string.Join("", parameters?.Select(x => x.GetType()));
            if (ConstructorInfos.ContainsKey(key))
                return ConstructorInfos.Get(key);
#if !NETSTANDARD1_3
            return ConstructorInfos.GetOrAdd(key, parameters == null ? type.GetConstructor(Type.EmptyTypes) : type.GetConstructor(parameters.Select(x => x.GetType()).ToArray()));
#else
            ConstructorInfo constructor = null;

            foreach (var cr in type.GetTypeInfo().DeclaredConstructors)
            {
                var index = 0;
                var args = cr.GetParameters();
                if (args.Length == parameters.Length)
                {
                    var apply = true;
                    foreach (var pr in args)
                    {
                        var prType = pr.ParameterType;
                        var paramType = parameters[index].GetType();

                        if (prType != paramType)
                        {
                            try
                            {
                                Convert.ChangeType(parameters[index], prType);
                            }
                            catch
                            {
                                apply = false;
                                break;
                            }
                        }
                        index++;

                    }
                    if (apply)
                        constructor = cr;
                }
            }
            return ConstructorInfos.GetOrAdd(key, constructor);
#endif
        }



        internal static object Creator(this Type type, params object[] parameters)
        {
            try
            {
                var key = type.FullName + string.Join("", parameters?.Select(x => x.GetType().FullName));
                var constructor = type.GetConstructorInfo(parameters ?? new object[0]);

                if (constructor != null)
                {
                    var constParam = constructor.GetParameters();
                    if (parameters?.Any() ?? false)
                    {
                        for (var i = 0; i < parameters.Length; i++)
                        {
                            if (constParam.Length <= i)
                                continue;
                            if (constParam[i].ParameterType != parameters[i].GetType())
                            {
                                try
                                {
                                    parameters[i] = Convert.ChangeType(parameters[i], constParam[i].ParameterType);
                                }
                                catch
                                {
                                    // Ignore
                                }
                            }
                        }
                    }

#if NETSTANDARD2_0 || NETSTANDARD1_3 || NETSTANDARD1_5
                    if (!constParam.Any())
                    {
                        if (CachedConstructor.ContainsKey(key))
                            return CachedConstructor[key]();
                    }
                    else if (CachedConstructorWithParameter.ContainsKey(key))
                        return CachedConstructorWithParameter[key](parameters);

                    if (!(parameters?.Any() ?? false))
                    {
                        return CachedConstructor.GetOrAdd(key, Expression.Lambda<Func<object>>(Expression.New(type)).Compile())();
                    }
                    else
                    {
                        // Create a single param of type object[].
                        ParameterExpression param = Expression.Parameter(typeof(object[]), "args");

                        // Pick each arg from the params array and create a typed expression of them.
                        Expression[] argsExpressions = new Expression[constParam.Length];

                        for (int i = 0; i < constParam.Length; i++)
                        {
                            Expression index = Expression.Constant(i);
                            Type paramType = constParam[i].ParameterType;
                            Expression paramAccessorExp = Expression.ArrayIndex(param, index);
                            Expression paramCastExp = Expression.Convert(paramAccessorExp, paramType);
                            argsExpressions[i] = paramCastExp;
                        }


                        return CachedConstructorWithParameter.GetOrAdd(key, Expression.Lambda<Func<object[], object>>(Expression.New(constructor, argsExpressions), param).Compile())(parameters);
                    }
#else

                    if (!constParam.Any())
                    {
                        if (CachedDynamicMethod.ContainsKey(key))
                            return CachedDynamicMethod[key]();
                    }
                    else if (CachedDynamicMethodWithParameters.ContainsKey(key))
                        return CachedDynamicMethodWithParameters[key](parameters);

                    lock (CachedDynamicMethod)
                    {
                        var dynamicMethod = new System.Reflection.Emit.DynamicMethod("CreateInstance", type, (constParam.Any() ? new Type[] { typeof(object[]) } : Type.EmptyTypes), true);
                        System.Reflection.Emit.ILGenerator ilGenerator = dynamicMethod.GetILGenerator();


                        if (constructor.GetParameters().Any())
                        {

                            for (int i = 0; i < constParam.Length; i++)
                            {
                                Type paramType = constParam[i].ParameterType;
                                ilGenerator.Emit(System.Reflection.Emit.OpCodes.Ldarg_0); // Push array (method argument)
                                ilGenerator.Emit(System.Reflection.Emit.OpCodes.Ldc_I4, i); // Push i
                                ilGenerator.Emit(System.Reflection.Emit.OpCodes.Ldelem_Ref); // Pop array and i and push array[i]
                                if (paramType.IsValueType)
                                {
                                    ilGenerator.Emit(System.Reflection.Emit.OpCodes.Unbox_Any, paramType); // Cast to Type t
                                }
                                else
                                {
                                    ilGenerator.Emit(System.Reflection.Emit.OpCodes.Castclass, paramType); //Cast to Type t
                                }
                            }
                        }


                        //ilGenerator.Emit(System.Reflection.Emit.OpCodes.Nop);
                        ilGenerator.Emit(System.Reflection.Emit.OpCodes.Newobj, constructor);
                        //ilGenerator.Emit(System.Reflection.Emit.OpCodes.Stloc_1); // nothing
                        ilGenerator.Emit(System.Reflection.Emit.OpCodes.Ret);

                        if (!constParam.Any())
                            return CachedDynamicMethod.GetOrAdd(key, (ObjectActivator)dynamicMethod.CreateDelegate(typeof(ObjectActivator)))();
                        else
                        {
                            return CachedDynamicMethodWithParameters.GetOrAdd(key, (ObjectActivatorWithParameters)dynamicMethod.CreateDelegate(typeof(ObjectActivatorWithParameters)))(parameters);
                        }
                    }

#endif
                }
                else
                {
#if !NETSTANDARD1_3
                    return FormatterServices.GetUninitializedObject(type);
#else
                try
                {
                        if (CachedConstructor.ContainsKey(key))
                            return CachedConstructor[key]();
                    return CachedConstructor.GetOrAdd(key, Expression.Lambda<Func<object>>(Expression.New(type)).Compile())();
                }
                catch
                {
                    throw new Exception("DeepClonerError: Default constructor is require for NETSTANDARD1_3 for type " + type.FullName);
                }
#endif
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal static Dictionary<string, IFastDeepClonerProperty> GetFastDeepClonerFields(this Type primaryType)
        {
            if (!CachedFields.ContainsKey(primaryType))
            {
                var properties = new SafeValueType<string, IFastDeepClonerProperty>();
                if (primaryType.GetTypeInfo().BaseType != null && primaryType.GetTypeInfo().BaseType.Name != "Object")
                {
                    primaryType.GetRuntimeFields().Where(x => properties.TryAdd(x.Name, new FastDeepClonerProperty(x))).ToList();
                    primaryType.GetTypeInfo().BaseType.GetRuntimeFields().Where(x => properties.TryAdd(x.Name, new FastDeepClonerProperty(x))).ToList();

                }
                else primaryType.GetRuntimeFields().Where(x => properties.TryAdd(x.Name, new FastDeepClonerProperty(x))).ToList();
                CachedFields.Add(primaryType, properties);
            }
            return CachedFields[primaryType];
        }


        internal static Dictionary<string, IFastDeepClonerProperty> GetFastDeepClonerProperties(this Type primaryType)
        {
            if (!CachedPropertyInfo.ContainsKey(primaryType))
            {
                var properties = new SafeValueType<string, IFastDeepClonerProperty>();
                if (primaryType.GetTypeInfo().BaseType != null && primaryType.GetTypeInfo().BaseType.Name != "Object")
                {
                    primaryType.GetRuntimeProperties().Where(x => properties.TryAdd(x.Name, new FastDeepClonerProperty(x))).ToList();
                    primaryType.GetTypeInfo().BaseType.GetRuntimeProperties().Where(x => properties.TryAdd(x.Name, new FastDeepClonerProperty(x))).ToList();
                }
                else primaryType.GetRuntimeProperties().Where(x => properties.TryAdd(x.Name, new FastDeepClonerProperty(x))).ToList();
                CachedPropertyInfo.Add(primaryType, properties);
            }
            return CachedPropertyInfo[primaryType];
        }
    }
}
