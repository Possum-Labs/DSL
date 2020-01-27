using PossumLabs.DSL.Core.Variables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PossumLabs.DSL.Core.FluidDataCreation
{
    public abstract class ValueObjectSetupBase<T> : INotifyPropertyChanged, IValueObjectSetup where T:IValueObject
    {
        protected T Item { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyChange([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
            => InvokePropertyChanged(propertyName);

        protected void InvokePropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private class Propigator
        {
            public Propigator(string propertyName, Action<string> notify)
            {
                PropertyName = propertyName;
                Notify = notify;
            }

            public string PropertyName { get; set; }
            public Action<string> Notify { get; set; }
            public void Propigate(object sender, PropertyChangedEventArgs e)
                => Notify($"{PropertyName}.{e.PropertyName}");
        }

        private DictionaryWithDefault<string, Propigator> Propigators { get; set; }

        protected void Unsubscribe<X>(ValueObjectSetupBase<X> child, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
           where X : IValueObject
        {
            if (child?.PropertyChanged != null)
                child.PropertyChanged -= Propigators[propertyName].Propigate;
        }

        public void Initialize(T item)
        {
            Item = item;
            Propigators = new DictionaryWithDefault<string, Propigator>((propertyName) => new Propigator(propertyName, NotifyChange));
        }

        protected void Subscribe<X>(ValueObjectSetupBase<X> child, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
            where X : IValueObject
            => child.PropertyChanged += Propigators[propertyName].Propigate;
    }
}
