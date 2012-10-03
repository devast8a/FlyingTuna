using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlyingTuna.Factories;
using FlyingTuna.Services;

namespace FlyingTuna
{
    public interface IHost
    {
        ServiceManager ServiceManager { get; }
        FactoryManager FactoryManager { get; }
    }
}
