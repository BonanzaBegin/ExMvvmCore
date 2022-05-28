using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExMvvmCore.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class AbstractValidateAttribute : Attribute
    {
        public string Error = string.Empty;
        public abstract bool Validate(object value);
    }
}
