using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace Currency.Core.Services
{

    /// <summary>
    /// The propety that is the sender must implement INotifyPropertyChanged or it wont broadcast the message
    ///         <![CDATA[
    ///         public class ViewModelA : NotifierBase
    ///
    ///             public ViewModelA()
    ///             {
    ///                 Mediator.Instance.Register(this);
    ///             }
    ///
    ///             private string m_Name = "Class A";
    ///
    ///             [MediatorConnection("NameMessenger")]
    ///             public string Name
    ///             {
    ///                 get { return m_Name; }
    ///                 set
    ///                 {
    ///                     SetProperty(ref m_Name, value);
    ///                 }
    ///             }
    ///
    ///             ~ViewModelA()
    ///             {
    ///                 Mediator.Instance.Unregister(this);
    ///             }
    ///
    ///          }   
    /// 
    ///     ...
    /// 
    ///     [MediatorConnection("NameMessenger")]
    ///     void OnBackgroundCheck(string parameter) { ... }
    /// 
    ///    ]]>
    /// </summary>
    public class Mediator
    {
        #region Data
        private static readonly Mediator instance = new Mediator();
        private readonly Dictionary<string, List<MediatorHelper>> _registeredListners =
            new Dictionary<string, List<MediatorHelper>>();
        #endregion

        #region Ctor
        static Mediator()
        {

        }

        private Mediator()
        {

        }
        #endregion

        /// <summary>
        /// Singelton instance
        /// </summary>
        public static Mediator Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Internal class that is able to hold referances to a method or a property. 
        /// </summary>
        internal class MediatorHelper
        {
            // VAribales used by either Property or Method calls
            private Mediator _MediatorInstance;
            private WeakReference _WeakBoundObject;
            private Type SendOrListenType;
            private string _MessageKey;
            private bool _SendAsync;
            private bool _IsProperty;

            // Property variables
            private string _PropertyName;
            private EventHandler<EventArgs> handler;

            // Method variables
            private Type _MessengerType;
            private string _methodName;


            /// <summary>
            /// From a property call
            /// </summary>
            /// <param name="MethodOwner">This is a strong referance to the Mediator</param>
            /// <param name="sender">The class that owns the property</param>
            /// <param name="PropertyName"></param>
            /// <param name="PropertyType"></param>
            /// <param name="MessageKey"></param>
            /// <param name="SendAsync"></param>
            public MediatorHelper(Mediator MethodOwner, Object sender, string PropertyName, Type PropertyType, string MessageKey, bool SendAsync)
            {
                _MediatorInstance = MethodOwner;
                _WeakBoundObject = new WeakReference(sender);

                _PropertyName = PropertyName;
                _MessageKey = MessageKey;
                _SendAsync = SendAsync;
                _IsProperty = true;
                _MessengerType = PropertyType;
                handler = new EventHandler<EventArgs>(WeakMediatorHelper_PropertyChanged);

                Type unboundWEMType = typeof(WeakEventManager<,>);
                Type[] typeArgs = { _WeakBoundObject.Target.GetType(), typeof(EventArgs) };
                SendOrListenType = unboundWEMType.MakeGenericType(typeArgs);

                this.AddHandler();
            }

            /// <summary>
            /// From a method call
            /// </summary>
            /// <param name="target"></param>
            /// <param name="actionType"></param>
            /// <param name="mi"></param>
            public MediatorHelper(object target, Type actionType, MethodBase mi)
            {
                _IsProperty = false;
                if (target == null)
                {
                    Debug.Assert(mi.IsStatic);
                    SendOrListenType = mi.DeclaringType;
                }

                _WeakBoundObject = new WeakReference(target);
                _methodName = mi.Name;
                _MessengerType = actionType;

                LastMessage = new object();
            }


            public object LastMessage { get; set; }

            public bool IsProperty
            {
                get { return _IsProperty; }
            }

            public Type MessengerType
            {
                get { return _MessengerType; }
            }

            public string PropertyName
            {
                get { return _PropertyName; }
            }

            public WeakReference WeakBoundObject
            {
                get { return _WeakBoundObject; }
            }

            public bool HasBeenCollected
            {
                get
                {
                    if (IsProperty)
                        return (_WeakBoundObject == null || !_WeakBoundObject.IsAlive);
                    else
                        return (SendOrListenType == null && (_WeakBoundObject == null || !_WeakBoundObject.IsAlive));
                }
            }

            private void WeakMediatorHelper_PropertyChanged(object sender, EventArgs e)
            {
                PropertyChangedEventArgs arg = (PropertyChangedEventArgs)e;
                if (arg.PropertyName == _PropertyName)
                {
                    if (_SendAsync)
                        _MediatorInstance.NotifyOfVauleChangedAsync(_WeakBoundObject.Target, e, _MessageKey, _PropertyName);
                    else
                        _MediatorInstance.NotifyColleaguesOfValueChanged(_WeakBoundObject.Target, e, _MessageKey, _PropertyName);
                }

            }

            public void AddHandler()
            {
                MethodInfo addHandlerMethod = SendOrListenType.GetMethod("AddHandler");
                addHandlerMethod.Invoke(null, new object[] { _WeakBoundObject.Target, "PropertyChanged", handler });
            }

            public void RemoveHandler()
            {
                MethodInfo removeHandlerMethod = SendOrListenType.GetMethod("RemoveHandler");
                removeHandlerMethod.Invoke(null, new object[] { _WeakBoundObject.Target, "PropertyChanged", handler });
            }

            public Delegate GetMethod()
            {
                if (SendOrListenType != null)
                {
                    if (_methodName == null)
                        return null;
                    else
                        return Delegate.CreateDelegate(_MessengerType, SendOrListenType, _methodName);
                }

                if (_WeakBoundObject != null && _WeakBoundObject.IsAlive)
                {
                    object target = _WeakBoundObject.Target;
                    if (target != null)
                        return Delegate.CreateDelegate(_MessengerType, target, _methodName);
                }

                return null;
            }
        }

        #region"NotefyColleagues"
        
        internal void NotifyOfVauleChangedAsync(object sender, EventArgs e, string key, string PropertyName)
        {
            Func<object, EventArgs, string, string, bool> smaFunc = NotifyColleaguesOfValueChanged;
            smaFunc.BeginInvoke(sender, e, key, PropertyName, ia =>
             {
                 try { smaFunc.EndInvoke(ia); }
                 catch { }
             }, null);
        }

        internal bool NotifyColleaguesOfValueChanged(object sender, EventArgs e, string key, string PropertyName)
        {
            object message = sender.GetType().GetProperty(PropertyName).GetValue(sender, null);

            List<MediatorHelper> wr;
            List<MediatorHelper> wrCopy = new List<MediatorHelper>();

            // lock the dictionary and get all the connected methods 
            // and properties out it before relesing it. 
            lock (_registeredListners)
            {
                if (!_registeredListners.TryGetValue(key, out wr))
                    return false;
                else
                {
                    foreach (MediatorHelper item in wr)
                    {
                        wrCopy.Add(item);
                    }
                }
            }

            // Send notifications out to connected properties and methods
            foreach (MediatorHelper item in wrCopy)
            {
                // Is the receiving ViewModel still alive?
                if (!item.HasBeenCollected)
                {
                    if (item.IsProperty)
                    {
                        PropertyInfo MyProperty =
                    item.WeakBoundObject.Target.GetType().
                    GetProperty(item.PropertyName,
                    BindingFlags.Public | BindingFlags.Instance);

                        // Check if not something is seriously wrong
                        if (null != MyProperty)
                        {
                            // If the property is different then the raised events property, change it. Also chack that you actually can write to it
                            if (message != MyProperty.GetValue(item.WeakBoundObject.Target, null) && MyProperty.CanWrite)
                            {
                                MyProperty.SetValue(item.WeakBoundObject.Target, message, null);
                            }
                        }
                    }

                    else //its a method
                    {
                        // This prevents duplicate messages to be send
                        if (item.LastMessage != message)
                        {
                            // I have to set the last message before the delegate call
                            item.LastMessage = message;
                            Delegate action = item.GetMethod();

                            if (action != null)
                            {
                                action.DynamicInvoke(message);
                            }
                            else //in case the message couldnt be sent reset it
                                item.LastMessage = new object();

                        }
                    }
                }
            }

            // Clean up any ViewModels that is not alive
            lock (_registeredListners)
            {
                for (int i = wr.Count - 1; i >= 0; i--)
                {
                    if (wr[i].HasBeenCollected)
                    {
                        if (wr[i].IsProperty)
                            wr[i].RemoveHandler();

                        wr.RemoveAt(i);
                    }
                }
            }
            return true;
        }

        #endregion

        #region "Register"
        /// <summary>
        /// Register the ViewModel and finds all Properties and Methods with that is a MediatorMessage
        /// </summary>
        /// <param name="view">The ViewModel</param>
        public void Register(object view)
        {
            // Look at all instance/static properties on this object type.
            foreach (var mi in view.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                //  var test = mi.GetCustomAttributes(typeof(PropertyMediatorAttribute));
                // See if we have a target attribute - if so, register the method as a handler.
                foreach (var att in mi.GetCustomAttributes(typeof(MediatorConnection)))
                {
                    var mha = (MediatorConnection)att;
                    RegisterProperty(mha.MessageKey, view, mi.Name, mi.PropertyType, mha.SendAsync);
                }
            }

            // Look at all instance/static methods on this object type.
            foreach (var mi in view.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                // See if we have a target attribute - if so, register the method as a handler.
                foreach (var att in mi.GetCustomAttributes(typeof(MediatorConnection), true))
                {
                    var mha = (MediatorConnection)att;
                    var pi = mi.GetParameters();
                    if (pi.Length != 1)
                        throw new InvalidCastException("Cannot cast " + mi.Name + " to Action<T> delegate type.");

                    Type actionType = typeof(Action<>).MakeGenericType(pi[0].ParameterType);
                    string key = mha.MessageKey;

                    if (mi.IsStatic)
                        RegisterHandler(key, actionType, Delegate.CreateDelegate(actionType, mi));
                    else
                        RegisterHandler(key, actionType, Delegate.CreateDelegate(actionType, view, mi.Name));
                }
            }
        }

        /// <summary>
        /// Performs the actual registration of a targets methods
        /// </summary>
        /// <param name="key">Key to store in dictionary</param>
        /// <param name="actionType">Delegate type</param>
        /// <param name="handler">Method</param>
        private void RegisterHandler(string key, Type actionType, Delegate handler)
        {
            var action = new MediatorHelper(handler.Target, actionType, handler.Method);

            lock (_registeredListners)
            {
                List<MediatorHelper> wr;
                if (_registeredListners.TryGetValue(key, out wr))
                {
                    if (wr.Count > 0)
                    {
                        // Check that all types are the same
                        MediatorHelper wa = wr[0];
                        if (wa.MessengerType != actionType.GenericTypeArguments[0])
                            throw new ArgumentException("Invalid key passed to RegisterHandler - existing handler has incompatible parameter type");
                    }

                    wr.Add(action);
                }
                else
                {
                    wr = new List<MediatorHelper> { action };
                    _registeredListners.Add(key, wr);
                }
            }
        }

        /// <summary>
        /// Performs actual registration of targets proeprties
        /// </summary>
        /// <param name="_MessageKey"></param>
        /// <param name="sender"></param>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyType"></param>
        /// <param name="SendAsync"></param>
        private void RegisterProperty(string _MessageKey, object sender, string PropertyName, Type PropertyType, bool SendAsync)
        {
            MediatorHelper WeakPropertyReferance = new MediatorHelper(this, sender, PropertyName, PropertyType, _MessageKey, SendAsync);
            List<MediatorHelper> wr;
            lock (_registeredListners)
            {
                if (_registeredListners.TryGetValue(_MessageKey, out wr))
                {

                    if (wr.Count > 0)
                    {
                        MediatorHelper wa = wr[0];
                        if (wa.MessengerType != PropertyType)
                            throw new ArgumentException("Invalid key passed to RegisterProperty - existing property has incompatible parameter type");
                    }

                    wr.Add(WeakPropertyReferance);
                }
                else
                {
                    wr = new List<MediatorHelper>();
                    wr.Add(WeakPropertyReferance);
                    _registeredListners[_MessageKey] = wr;
                }
            }
        }
        #endregion

        #region "Unregister"
        /// <summary>
        /// A forced clean up of a class, and removes all registerd methods and properties in the Mediator
        /// </summary>
        /// <param name="view">The ViewModel instance</param>
        public void Unregister(object view)
        {
            // Look at all properties on this object type.
            foreach (var mi in view.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                // See if we have a target attribute - if so, register the property.
                foreach (var att in mi.GetCustomAttributes(typeof(MediatorConnection)))
                {
                    var mha = (MediatorConnection)att;
                    UnregisterProperty(mha.MessageKey, view, mi.Name);
                }
            }

            // Look at all mthods on this object type.
            foreach (var mi in view.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                // See if we have a target attribute - if so, unregister the method as a handler.
                foreach (var att in mi.GetCustomAttributes(typeof(MediatorConnection), true))
                {
                    var mha = (MediatorConnection)att;
                    var pi = mi.GetParameters();
                    if (pi.Length != 1)
                        throw new InvalidCastException("Cannot cast " + mi.Name + " to Action<T> delegate type.");

                    Type actionType = typeof(Action<>).MakeGenericType(pi[0].ParameterType);
                    string key = (mha.MessageKey);

                    if (mi.IsStatic)
                        UnregisterHandler(key, actionType, Delegate.CreateDelegate(actionType, mi));
                    else
                        UnregisterHandler(key, actionType, Delegate.CreateDelegate(actionType, view, mi.Name));
                }
            }
        }

        /// <summary>
        /// Performs unregistration from a property target
        /// </summary>
        /// <param name="_MessageKey"></param>
        /// <param name="sender"></param>
        /// <param name="PropertyName"></param>
        private void UnregisterProperty(string _MessageKey, object sender, string PropertyName)
        {
            lock (_registeredListners)
            {
                List<MediatorHelper> wr;
                if (_registeredListners.TryGetValue(_MessageKey, out wr))
                {

                    for (int i = wr.Count - 1; i >= 0; i--)
                    {
                        MediatorHelper wa = wr[i];
                        if (wa.IsProperty)
                        {
                            if (sender == wa.WeakBoundObject.Target && PropertyName == wa.PropertyName)
                            {
                                wr.RemoveAt(i);
                            }
                        }
                    }

                    if (wr.Count == 0)
                        _registeredListners.Remove(_MessageKey);
                }
            }
        }


        /// <summary>
        /// Performs the unregistration from a target method 
        /// </summary>
        /// <param name="key">Key to store in dictionary</param>
        /// <param name="actionType">Delegate type</param>
        /// <param name="handler">Method</param>
        private void UnregisterHandler(string key, Type actionType, Delegate handler)
        {
            lock (_registeredListners)
            {
                List<MediatorHelper> wr;
                if (_registeredListners.TryGetValue(key, out wr))
                {
                    // If wa is a property, the handler would be null!
                    wr.RemoveAll(wa => !wa.IsProperty && handler == wa.GetMethod() && actionType == wa.MessengerType);

                    if (wr.Count == 0)
                        _registeredListners.Remove(key);
                }
            }
        }

        #endregion
    }
}
