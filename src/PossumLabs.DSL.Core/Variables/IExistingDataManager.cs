using System;
using System.Reflection;

namespace PossumLabs.DSL.Core.Variables
{
    public interface IExistingDataManager
    {
        void Initialize(Assembly assembly);
        object ProcessVariable(Type type, string name, string template, dynamic options);
    }
}