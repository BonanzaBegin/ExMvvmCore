using SimpleIoc.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIoc.Extensions
{
    public static class ParameterInfoExtension
    {
        public static string GetParameterInfoAliasMask(this ParameterInfo parameter)
        {
            return parameter.IsDefined(typeof(AliasAttribute), true) ? parameter.GetCustomAttribute<AliasAttribute>().Alias : "";
        }
    }
}
