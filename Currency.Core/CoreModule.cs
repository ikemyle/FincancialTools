using System;
using Prism.Ioc;
using Prism.Modularity;
using Currency.Core.Services;

namespace Currency.Core
{
    public class CoreModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ICountryService, CountryService>();
        }
    }
}