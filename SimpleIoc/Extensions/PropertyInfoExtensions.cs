using SimpleIoc.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIoc.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static string GetPropertyInfoAliasMask(this PropertyInfo property)
        {
            return property.IsDefined(typeof(AliasAttribute), true) ? property.GetCustomAttribute<AliasAttribute>().Alias : "";
        }
        public static string GetFieldInfoAliasMask(this FieldInfo field)
        {
            return field.IsDefined(typeof(AliasAttribute), true) ? field.GetCustomAttribute<AliasAttribute>().Alias : "";
        }

    }
}
