using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingTuna.Factories
{
    public class LambdaFactory : IFactory
    {
        private readonly Func<object> _lambda;

        public LambdaFactory(Func<object> lambda)
        {
            _lambda=lambda;
        }

        public object CreateObject()
        {
            return _lambda();
        }
    }
}
