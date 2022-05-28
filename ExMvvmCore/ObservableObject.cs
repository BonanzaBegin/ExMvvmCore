using ExMvvmCore.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExMvvmCore
{
    public abstract class ObservableObject : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        protected void Set<T>(ref T target, T value, [CallerMemberName] string propName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(target, value))
            {
                target = value;
                this.RaisePropertyChanged(propName);
            }
        }


        public static event PropertyChangedEventHandler StaticPropertyChanged;
        protected static void RaiseStaticPropertyChanged([CallerMemberName] string propName = "")
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propName));
        }

        private static string staticValue = "123456";
        public static string StaticValue
        {
            get => staticValue;
            set
            {
                staticValue = value;
                //StaticPropertyChanged?.Invoke(null,new PropertyChangedEventArgs(nameof(StaticValue)));
                RaiseStaticPropertyChanged(nameof(StaticValue));
            }
        }

        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                //ValidationContext context = new ValidationContext(this);
                //context.MemberName = columnName;
                //List<ValidationResult> errors = new List<ValidationResult>();
                //bool state = Validator.TryValidateProperty(this.GetType().GetProperty(columnName).GetValue(this), context, errors);
                //if (errors.Count > 0)
                //{
                //    return string.Join(";", errors.Select(e => e.ErrorMessage));
                //}
                //return string.Empty;

                List<string> errors = new List<string>();

                PropertyInfo prop = this.GetType().GetProperty(columnName, BindingFlags.Instance | BindingFlags.Public);
                if (prop != null)
                {
                    if (prop.IsDefined(typeof(AbstractValidateAttribute), true))
                    {
                        var validateRules = prop.GetCustomAttributes(typeof(AbstractValidateAttribute), true).OfType<AbstractValidateAttribute>();
                        foreach (AbstractValidateAttribute item in validateRules)
                        {
                            if (!item.Validate(prop.GetValue(this))) errors.Add(item.Error);
                        }
                    }
                }
                return errors.Count > 0 ? string.Join(";", errors) : string.Empty;
            }
        }
    }
}


