using System;
using System.ComponentModel; // For INotifyPropertyChanged interface
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices; // For CallerMemberName, could use nameof() otherwise

namespace Simple_ObservableProperty_Demo
{

    public delegate void PropertyChangedEventHandler<T>(object sender, ObservablePropertyEventArgs<T> e);

    public class ObservablePropertyEventArgs<T> : PropertyChangedEventArgs
    {

        /// <summary>
        /// The value of the property that changed.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Event arguments that determine the name and updated value of the property.
        /// </summary>
        /// <param name="propertyName">May be useful in logging, even if we do not need it to check against the property we wish to monitor.</param>
        /// <param name="value">The updated (current) value of the property that changed.</param>
        public ObservablePropertyEventArgs(string propertyName, T value) : base(propertyName)
        {
            this.Value = value;
        }
    }

    public class ObservableProperty<T>
    {

        // INotifyPropertyChanged notifies when any property in the class changes.
        // The user must then compare the property name to that of the property (s)he wishes to observe.
        // In comparison, this event is subscribed to get notified only if this property is changed!!!
        public event PropertyChangedEventHandler<T> Changed;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ObservableProperty() => this.value = default;

        /// <summary>
        /// Initializer constructor
        /// </summary>
        /// <param name="value"></param>
        public ObservableProperty(T value) => this.value = value;


        T value = default;
        /// <summary>
        /// Property value.
        /// </summary>
        public T Value
        {
            get => this.value;
            //set => Set(ref this.value, value);
        }


        /// <summary>
        /// Implicitly casts the <see cref="Value"/> of the <see cref="ObservableProperty{T}"/> to its type.
        /// </summary>
        /// <param name="prop"></param>
        public static implicit operator T(ObservableProperty<T> prop) => prop.value ?? default;

        // Adapted from https://stackoverflow.com/a/36972187/1292918
        protected virtual void OnChanged([CallerMemberName] string propertyName = null) => 
            this.Changed?.Invoke(this, new ObservablePropertyEventArgs< T>(propertyName, this.value));

        // To use only one line for simple properties add the following
        // Adapted from https://stackoverflow.com/a/1316417/1292918
        //protected bool Set(ref T field, T value, [CallerMemberName] string propertyName = null)
        //{
        //    if (EqualityComparer<T>.Default.Equals(field, value)) return false;

        //    // or
        //    //if (field.Equals(value)) return false;
        //    field = value;
        //    OnChanged(propertyName);
        //    return true;
        //}

        // To use only one line for simple properties add the following
        // Adapted from https://stackoverflow.com/a/1316417/1292918
        public bool Set(T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(this.value, value)) return false;

            // or
            //if (field.Equals(value)) return false;
            this.value = value;
            OnChanged(propertyName);
            return true;
        }

        public override string ToString() => this.value.ToString();

    }

}
