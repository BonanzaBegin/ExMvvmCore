using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExMvvmCore.ExMessenger
{
    public class Messenger
    {
        private static Dictionary<Type, Dictionary<string, Action<object>>> _List = new Dictionary<Type, Dictionary<string, Action<object>>>();



        public void Register<T>(string name,Action<T> action)
        {
            if (!_List.ContainsKey(typeof(T)))
            {
                _List[typeof(T)] = new Dictionary<string, Action<object>>();
            }
            _List[typeof(T)][name] = new Action<object>(para => action((T)para));
        }

        public void Send<T>(string name,T parameter)
        {
            if (_List.ContainsKey(typeof(T)))
            {
                if (_List[typeof(T)].ContainsKey(name))
                {
                    _List[typeof(T)][name].Invoke(parameter);
                }
            }
        }
    }
}
