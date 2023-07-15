using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Singleton<T> where T : new()
    {
        private static readonly Lazy<T> _lazy = new Lazy<T>(() => new T());
        protected Singleton() { }
        public static T Instance => _lazy.Value;
    }
}
