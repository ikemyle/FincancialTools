using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Currency.Core.Helpers
{
    /// <summary>
    /// Base bindable interface
    /// </summary>
    public class BaseBindable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Property changed method
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Setting property change of a propertry object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variable"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        protected virtual void SetProperty<T>(ref T variable, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(variable, value))
                return;

            variable = value;
            OnPropertyChanged(propertyName);
        }
    }
}
