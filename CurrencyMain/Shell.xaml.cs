using System;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Presentation;
using Currency.Constants;
using Currency.Views;
using log4net;

namespace Currency
{
    public partial class Shell : ModernWindow
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(App));
        
        public Shell()
        {
            InitializeComponent();
            AppearanceManager.Current.ThemeSource = new Uri(ThemesPath.Light, UriKind.Relative);
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"));
            log.Info("Application started");
        }

        public void AddLinkGroups(LinkGroupCollection linkGroupCollection)
        {
            CreateMenuLinkGroup();

            foreach (LinkGroup linkGroup in linkGroupCollection)
            {
                this.MenuLinkGroups.Add(linkGroup);
            }
        }

        private void CreateMenuLinkGroup()
        {
            this.MenuLinkGroups.Clear();

            LinkGroup linkGroup = new LinkGroup
            {
                DisplayName = "Settings",
                GroupKey = "settings"
            };

            linkGroup.Links.Add(new Link
            {
                DisplayName = "Settings options",
                Source = GetUri(typeof(SettingsView))
            });

            this.MenuLinkGroups.Add(linkGroup);

            linkGroup = new LinkGroup
            {
                DisplayName = "Home"
            };

            linkGroup.Links.Add(new Link
            {
                DisplayName = "Currency Countries",
                Source = GetUri(typeof(DataGrid))
            });

            linkGroup.Links.Add(new Link
            {
                DisplayName = "About",
                Source = GetUri(typeof(IntroductionView))
            });

            this.MenuLinkGroups.Add(linkGroup);
        }

        private Uri GetUri(Type viewType)
        {
            return new Uri($"/Views/{viewType.Name}.xaml", UriKind.Relative);
        }
    }
}