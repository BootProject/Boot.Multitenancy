using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.ModelFactory.Factory
{
    public abstract class BaseViewModel<T> where T : class
    {
        public dynamic Model { get; set; }
    }
}
