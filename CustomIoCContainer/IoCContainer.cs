using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CustomIoCContainer
{
    public  class IoCContainer
    {
        private static readonly Dictionary<Type, Type> _typeMappings = new Dictionary<Type, Type>();
        private static readonly Dictionary<Type, object> _singletons = new Dictionary<Type, object>();

        private static bool HasSingletonRegistered(Type key)
        {
            return _singletons.ContainsKey(key);
        }
        private static object GetSingleton(Type key)
        {
            return _singletons[key];
        }

        public static void Register<Tfrom, Tto>()
        {
            Register(typeof(Tfrom), typeof(Tto));
        }
        public static void Register(Type from, Type to)
        {
            if (!from.IsAssignableFrom(to))
                throw new InvalidOperationException("There's something wrong with your types");
            _typeMappings.Add(from, to);
        }

        public static void Register<T>(T obj)
        {
            if (!HasSingletonRegistered(typeof(T)))
                _singletons.Add(typeof(T), obj);
        }

        public static Tfrom Resolve<Tfrom>()
        {
            var exist = _typeMappings.TryGetValue(typeof(Tfrom), out Type type);
            if (exist)
                return (Tfrom)Resolve(typeof(Tfrom));
            else
                throw new Exception("Type not found");
        }

        public static object Resolve(Type type)
        {
            if (HasSingletonRegistered(type))
            {
                var result = GetSingleton(type);
                return result;
            }
            else if (_typeMappings.ContainsKey(type))
            {
                var to = _typeMappings[type];
                return Activator.CreateInstance(to, ParameterResolver(to));
            }
            throw new InvalidOperationException(string.Format("The requested type was not mapped {0}", type.FullName));
        }

        private static object[] ParameterResolver(Type to)
        {
            return ResolveParameters(GetConstructorWithMostParameters(to));
        }

        internal static ParameterInfo[] GetConstructorWithMostParameters(Type type)
        {
            var parameters = type.GetConstructors().Max(p => p.GetParameters());

            return parameters;
        }

        internal static object[] ResolveParameters(ParameterInfo[] parameters)
        {
            List<object> values = new List<object>(parameters.Count());
            foreach (ParameterInfo p in parameters)
            {
            
                values.Add(IoCContainer.Resolve(p.ParameterType));
            }
            return values.ToArray();
        }

        public static bool IsRegistered(Type type)
        {
            return HasSingletonRegistered(type) || _typeMappings.ContainsKey(type);
        }

        public static void Config(JObject mappings, Assembly ass)
        {
            foreach(JToken item in mappings["IoCContainer"])
            {
                Type from = ass.GetType(item["from"].ToString());
                Type to = ass.GetType(item["to"].ToString());
                IoCContainer.Register(from, to);
            }
        }
    }
}
