using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIoc.Common
{

    public enum LifeTime
    {
        Transient,
        Sington,
        Scoped
    }

    public class RegisterInfo
    {
        public Type ImplementType { get; set; }
        public LifeTime LifeTime { get; set; }

        public Func<object> Factory { get; set; }

        public object Instance { get; set; }

        public object[] ConstantParaList { get; set; }
    }
}
