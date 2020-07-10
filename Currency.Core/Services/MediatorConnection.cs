using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Core.Services
{
    /// <summary>
    /// This attribute allows a method or a property to be targeted as a recipient for a message, and 
    /// also a property to broadcast a message. Property senders must implement INotifyChanged
    /// 
    /// <seealso cref="Mediator.Register"/> method
    /// </summary>
    /// <example>
    /// <![CDATA[
    /// [MediatorConnection("DoBackgroundCheck")]
    /// void OnBackgroundCheck(object parameter) { ... }
    /// 
    ///  private string m_Name;
    ///  [MediatorConnection("DoBackgroundCheck")]
    ///  public string Name
    ///  {
    ///    get { return m_Name; }
    ///    set
    ///    {
    ///        if (m_Name != value)
    ///        {
    ///            m_Name = value;
    ///            OnPropertyChanged("Name");
    ///        }
    ///    }
    ///  }
    /// 
    /// ]]>
    /// </example>
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Property)]
    public sealed class MediatorConnection : Attribute
    {      

        /// <summary>
        /// Message key
        /// </summary>
        public string MessageKey { get; private set; }

        /// <summary>
        /// Update Async
        /// </summary>
        public bool SendAsync { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MediatorConnection()
        {
            MessageKey = null;
            SendAsync = false;
        }

        /// <summary>
        /// Constructor that takes a message key
        /// </summary>
        /// <param name="messageKey">Message Key</param>
        public MediatorConnection(string messageKey,bool sendAsync = true)
        {
            MessageKey = messageKey;
            SendAsync = sendAsync;
        }
    }
}
