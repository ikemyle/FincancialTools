using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CurrencyConverter.Services
{
    public class DialogConfirmService: IConfirmDialogService
    {
        public bool ConfirmDialog(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes ? true : false;
        }
    }
}
