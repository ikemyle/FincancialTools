using System;
using CurrencyConverter.Helpers;
using Prism.Ioc;
using Prism.Modularity;

namespace CurrencyConverter
{
    public class CurrencyModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.Register<InterfaceName, ClassName>();
            Logger.Log.Info($"{nameof(CurrencyModule)} has been initialized; at {DateTime.Now.ToString()}");

        }
    }
}   