using System;
using FirstFloor.ModernUI.Presentation;
using Currency.Core.Interfaces;
using CurrencyConverter.Views;

namespace CurrencyConverter.Services
{
    /// <summary>
    /// Creates a LinkGroup
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// This is the entry point for the option menu.
    /// </remarks>
    public class LinkGroupService : ILinkGroupService
    {
        public LinkGroup GetLinkGroup()
        {
            LinkGroup linkGroup = new LinkGroup
            {
                DisplayName = "Currency Module"
            };

            linkGroup.Links.Add(new Link
            {
                DisplayName = "Currency Module",
                Source = new Uri($"/CurrencyConverter;component/Views/{nameof(MainView)}.xaml", UriKind.Relative)
            });

            return linkGroup;
        }
    }
}
