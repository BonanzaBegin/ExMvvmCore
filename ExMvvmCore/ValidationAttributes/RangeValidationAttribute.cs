using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExMvvmCore.ValidationAttributes
{
    public class RangeValidationAttribute : AbstractValidateAttribute
    {
        public int Min = 0;
        public int Max = 0;
        public RangeValidationAttribute(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public override bool Validate(object value)
        {
            if(value!=null&&int.TryParse(value.ToString(),out int result))
            {
                if (result >= Min && result <= Max) return true;
                else
                {
                    Error = $"当前输入值为:{result},不在{Min}-{Max}范围内";
                }
            }
            else
            {
                Error = $"输入数值有误,请重新输入";
            }
            return false;
        }
    }
}
