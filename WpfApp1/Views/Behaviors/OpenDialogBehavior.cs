using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.Views.Behaviors
{
    /// <summary>
    /// Command経由で子Windowを開いてよいかViewModel/Modelで判断する
    /// 判断した結果、開く場合はView側で子Windowを開く
    /// </summary>
    internal class OpenDialogBehavior
    {
        #region DataContext添付プロパティ
        public static readonly DependencyProperty DataContextProperty = DependencyProperty.RegisterAttached(
                                                                                                                                "DataContext",
                                                                                                                                typeof(object),
                                                                                                                                typeof(OpenDialogBehavior),
                                                                                                                                new PropertyMetadata(null));

        /// <summary>
        /// 添付プロパティを取得
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static object GetDataContext(DependencyObject target)
        {
            return (object)target.GetValue(DataContextProperty);
        }

        /// <summary>
        /// 添付プロパティを設定
        /// </summary>
        /// <param name="target">対象とするDependencyObject</param>
        /// <param name="value">設定する値</param>
        public static void SetDataContext(DependencyObject target, object value)
        {
            target.SetValue(DataContextProperty, value);
        }
        #endregion

        #region WindowType添付プロパティ
        public static readonly DependencyProperty WindowTypeProperty = DependencyProperty.RegisterAttached(
                                                                                                                                "WindowType",
                                                                                                                                typeof(Type),
                                                                                                                                typeof(OpenDialogBehavior),
                                                                                                                                new PropertyMetadata(null));

        /// <summary>
        /// 添付プロパティを取得
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Type GetWindowType(DependencyObject target)
        {
            return (Type)target.GetValue(WindowTypeProperty);
        }

        /// <summary>
        /// 添付プロパティを設定
        /// </summary>
        /// <param name="target">対象とするDependencyObject</param>
        /// <param name="value">設定する値</param>
        public static void SetWindowType(DependencyObject target, object value)
        {
            target.SetValue(WindowTypeProperty, value);
        }
        #endregion

        #region Callback添付プロパティ
        public static readonly DependencyProperty CallbackProperty = DependencyProperty.RegisterAttached(
                                                                                                                                "Callback",
                                                                                                                                typeof(Action<bool>),
                                                                                                                                typeof(OpenDialogBehavior),
                                                                                                                                new PropertyMetadata(null, OnCallbackPropertyChanged));

        /// <summary>
        /// 添付プロパティを取得
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Action<bool> GetCallback(DependencyObject target)
        {
            return (Action<bool>)target.GetValue(CallbackProperty);
        }

        /// <summary>
        /// 添付プロパティを設定
        /// </summary>
        /// <param name="target">対象とするDependencyObject</param>
        /// <param name="value">設定する値</param>
        public static void SetCallback(DependencyObject target, object value)
        {
            target.SetValue(CallbackProperty, value);
        }
        #endregion

        private static void OnCallbackPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var callback = GetCallback(sender);
            if (callback != null)
            {
                var type = GetWindowType(sender);
                var obj = type.InvokeMember(null, System.Reflection.BindingFlags.CreateInstance, null, null, null);
                var child = obj as Window;
                if (child != null)
                {
                    child.DataContext = GetDataContext(sender);
                    var result = child.ShowDialog();
                    callback(result.Value);
                }
            }
        }
    }
}
