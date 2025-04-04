using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;

namespace WpfApp1.Views.Behaviors
{
    internal class WindowClosingBehavior
    {
        public static readonly DependencyProperty CallbackProperty = DependencyProperty.RegisterAttached(
                                                                                                                                        "Callback",
                                                                                                                                        typeof(Func<bool>),
                                                                                                                                        typeof(WindowClosingBehavior),
                                                                                                                                        new PropertyMetadata(null, OnIsEnablePropertyChanged));

        public static Func<bool> GetCallback(DependencyObject target)
        {
            return (Func<bool>)target.GetValue(CallbackProperty);
        }

        public static void SetCallback(DependencyObject target, Func<bool> value)
        {
            target.SetValue(CallbackProperty, value);
        }

        private static void OnIsEnablePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var w = sender as Window;
            if (w != null)
            {
                var callback = GetCallback(w);
                if(callback != null && e.OldValue == null)
                {
                    w.Closing += OnClosing;
                }
                else if(callback == null)
                {
                    w.Closing -= OnClosing;
                }
            }
        }

        private static void OnClosing(object sender, CancelEventArgs e)
        {
            var callback = GetCallback(sender as DependencyObject);
            if (callback != null)
            {
                //callback処理の結果がfalseの時にキャンセルする
                e.Cancel = !callback();
            }
        }
    }
}
