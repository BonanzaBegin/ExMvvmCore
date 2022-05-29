using SimpleIoc.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIoc.Extensions
{
    public static class ObjectExtensions
    {
        public static void DoPropertyInjection(this object instance,Func<Type,string,object> factory)
        {
            var properties = instance.GetType().GetProperties().Where(p=>p.IsDefined(typeof(PropertyInjectionAttribute),true));
            foreach (var property in properties)
            {
                property.SetValue(instance, factory.Invoke(property.PropertyType,property.GetPropertyInfoAliasMask()));
            }
        }


        public static void DoFieldInjection(this object instance, Func<Type, string, object> factory)
        {
            var fields = instance.GetType().GetFields().Where(p => p.IsDefined(typeof(FieldInjectionAttribute), true));
            foreach (var f in fields)
            {
                f.SetValue(instance, factory.Invoke(f.FieldType, f.GetFieldInfoAliasMask()));
            }
        }

        public static void DoMethodInjection(this object instance, Func<Type, string, object> factory)
        {
            var methods = instance.GetType().GetMethods().Where(m => m.IsDefined(typeof(MethodInjectionAttribute), true)).ToArray();

            foreach (var method in methods)
            {
                List<object> methodParameters = new List<object>();
                foreach (var item in method.GetParameters())
                {
                    methodParameters.Add(factory.Invoke(item.ParameterType,item.GetParameterInfoAliasMask()));
                }
                method.Invoke(instance,methodParameters.ToArray());
            }
        }




        public static void DoInjection(this object instance,Func<Type,string,object> factory)
        {
            instance.DoPropertyInjection(factory);
            instance.DoFieldInjection(factory);
            instance.DoMethodInjection(factory);
        }
    }
}
