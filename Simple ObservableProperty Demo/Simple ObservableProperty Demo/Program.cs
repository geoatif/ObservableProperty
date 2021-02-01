using System;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel; // For INotifyPropertyChanged interface
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Collections.Generic;


namespace Simple_ObservableProperty_Demo
{

    class Program
    {

        static void Main(string[] args)
        {
            var demo = new Demo_Class_With_An_ObservableProperty();

            Console.WriteLine("\nObservableProperty value [CountProperty.Value] equals...");
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine($"Count = {demo.CountProperty}.   ");

            Console.WriteLine("\nHelper property [Count] equals...");
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine($"Count = {demo.Count}.   ");

            demo.CountProperty.Changed += Print;

            Console.WriteLine("\nHelper property [Count] incrementing...");
            Console.WriteLine("--------------------------------------------------------------------");
            demo.Count++;

            //Console.WriteLine("\nObservableProperty value [CountProperty.Value] incrementing...");
            //Console.WriteLine("--------------------------------------------------------------------");
            //demo.CountProperty.Value++;

            Console.WriteLine("\nHelper property [Count] being set to 4...");
            Console.WriteLine("--------------------------------------------------------------------");
            demo.Count = 4;

            Console.WriteLine("\nObservableProperty value [CountProperty.Value] being set to 14...");
            Console.WriteLine("--------------------------------------------------------------------");
            demo.CountProperty.Set(14, nameof(demo.Count));

            Console.Read();
        }

        public static void Print<T>(object sender, ObservablePropertyEventArgs<T> e) =>
            Console.WriteLine($"Property [{e.PropertyName}] changed to: [{e.Value}]");

    }

    class Demo_Class_With_An_ObservableProperty
    {

        public Demo_Class_With_An_ObservableProperty()
        {
        }

        public readonly ObservableProperty<int> CountProperty = new ObservableProperty<int>();

        /// <summary>
        /// Helper property for <see cref="CountProperty"/>.
        /// </summary>
        public int Count
        {
            get => this.CountProperty.Value;
            set => this.CountProperty.Set(value);
        }


        //public void AddCountChangedHandler(PropertyChangedEventHandler<int> handler) => CountProperty.Changed += handler;

        //public void RemoveCountChangedHandler(PropertyChangedEventHandler<int> handler) => CountProperty.Changed -= handler;

    }

}
