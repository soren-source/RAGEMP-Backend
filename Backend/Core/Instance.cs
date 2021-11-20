using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Models
{
    public class Instance : Script {}

    public abstract class Instance<T> where T : Instance<T>
    {
        private static T _instance;

        public static T GetInstance()
        {
            return _instance;
        }

        public Instance()
        {
            _instance = (T)this;
            Console.WriteLine(_instance.GetType().FullName);
        }
    }
}
