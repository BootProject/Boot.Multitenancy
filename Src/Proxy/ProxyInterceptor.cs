using NHibernate;
using NHibernate.Proxy.DynamicProxy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Proxy
{


    public class NotifyPropertyChangedInterceptor : EmptyInterceptor
    {
        private ISession session = null;

        private static readonly ProxyFactory factory = new ProxyFactory();

        public override void SetSession(ISession session)
        {
            this.session = session;
            base.SetSession(session);
        }

        public override Object Instantiate(String clazz, EntityMode entityMode, Object id)
        {
            Type entityType = Type.GetType(clazz);
            IProxy proxy = factory.CreateProxy(entityType, new _NotifyPropertyChangedInterceptor(), typeof(INotifyPropertyChanged)) as IProxy;

            _NotifyPropertyChangedInterceptor interceptor = proxy.Interceptor as _NotifyPropertyChangedInterceptor;
            interceptor.Proxy = this.session.SessionFactory.GetClassMetadata(entityType).Instantiate(id, entityMode);

            this.session.SessionFactory.GetClassMetadata(entityType).SetIdentifier(proxy, id, entityMode);

            return (proxy);
        }




        class _NotifyPropertyChangedInterceptor : NHibernate.Proxy.DynamicProxy.IInterceptor
        {
            private PropertyChangedEventHandler changed = delegate { };

            public Object Proxy
            {
                get;
                set;
            }


            public Object Intercept(InvocationInfo info)
            {
                Boolean isSetter = info.TargetMethod.Name.StartsWith("set_") == true;
                Object result = null;

                if (info.TargetMethod.Name == "add_PropertyChanged")
                {
                    PropertyChangedEventHandler propertyChangedEventHandler = info.Arguments[0] as PropertyChangedEventHandler;
                    this.changed += propertyChangedEventHandler;
                }
                else if (info.TargetMethod.Name == "remove_PropertyChanged")
                {
                    PropertyChangedEventHandler propertyChangedEventHandler = info.Arguments[0] as PropertyChangedEventHandler;
                    this.changed -= propertyChangedEventHandler;
                }
                else
                {
                    result = info.TargetMethod.Invoke(this.Proxy, info.Arguments);
                }

                if (isSetter == true)
                {
                    String propertyName = info.TargetMethod.Name.Substring("set_".Length);
                    this.changed(this.changed, new PropertyChangedEventArgs(propertyName));
                }

                return (result);
            }


        }

    }

}
