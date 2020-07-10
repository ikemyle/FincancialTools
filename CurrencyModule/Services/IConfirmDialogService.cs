using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CurrencyConverter.Services
{
    public interface IConfirmDialogService
    {
        bool ConfirmDialog(string message);
    }
}
