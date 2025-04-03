using Microsoft.Win32;
using System.Windows;

namespace WpfApp1.Views.Behaviors
{
    internal class CommonDialogBehavior
    {
        /// <summary>
        /// Action<bool, string>型のCallback添付プロパティを定義
        /// 引数：プロパティ名, プロパティの型を指定, このプロパティを持つクラス, 初期値とプロパティ変更時のCallbackを指定
        /// </summary>
        public static readonly DependencyProperty CallbackProperty = DependencyProperty.RegisterAttached(
                                                                                                                                    "MyCallback", 
                                                                                                                                    typeof(Action<bool, string>),
                                                                                                                                    typeof(CommonDialogBehavior), 
                                                                                                                                    new PropertyMetadata(null, OnCallbackPropertyChanged));
        
        /// <summary>
        /// Callback添付プロパティを取得する
        /// </summary>
        /// <param name="target">対象とするDependencyObjectを取得する</param>
        /// <returns>取得した値を返す</returns>
        public static Action<bool, string> GetMyCallback(DependencyObject target)
        {
            return (Action<bool, string>)target.GetValue(CallbackProperty);
        }

        /// <summary>
        /// 添付プロパティを設定する
        /// </summary>
        /// <param name="target">対象とするDependencyObjectを指定する</param>
        /// <param name="value">設定する値を指定する</param>
        public static void SetMyCallback(DependencyObject target, Action<bool, string> value)
        {
            target.SetValue(CallbackProperty, value);
        }

        /// <summary>
        /// Title添付プロパティを定義
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.RegisterAttached(
                                                                                                                                    "Title",
                                                                                                                                    typeof(string),
                                                                                                                                    typeof(CommonDialogBehavior),
                                                                                                                                    new PropertyMetadata("ファイルを開く"));
        public static string GetTitle(DependencyObject target)
        {
            return (string)target.GetValue(TitleProperty);
        }
        public static void SetTitle(DependencyObject target, string value)
        {
            target.SetValue(TitleProperty, value);
        }

        /// <summary>
        /// Filter添付プロパティを定義
        /// </summary>
        public static readonly DependencyProperty FilterProperty = DependencyProperty.RegisterAttached(
                                                                                                                                    "Filter",
                                                                                                                                    typeof(string),
                                                                                                                                    typeof(CommonDialogBehavior),
                                                                                                                                    new PropertyMetadata("すべてのファイル(*.*)|*.*"));
        public static string GetFilter(DependencyObject target)
        {
            return (string)target.GetValue(FilterProperty);
        }
        public static void SetFilter(DependencyObject target, string value)
        {
            target.SetValue(FilterProperty, value);
        }

        /// <summary>
        /// MultiSelect添付プロパティを定義
        /// </summary>
        public static readonly DependencyProperty MultiselectProperty = DependencyProperty.RegisterAttached(
                                                                                                                                    "Multiselect",
                                                                                                                                    typeof(bool),
                                                                                                                                    typeof(CommonDialogBehavior),
                                                                                                                                    new PropertyMetadata(true));
        public static bool GetMultiselect(DependencyObject target)
        {
            return (bool)target.GetValue(MultiselectProperty);
        }

        public static void SetMultiselect(DependencyObject target, string value)
        {
            target.SetValue(MultiselectProperty, value);
        }



        public static void OnCallbackPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var callback = GetMyCallback(sender);
            if (callback != null)
            {
                var dlg = new OpenFileDialog()
                {
                    Title = GetTitle(sender),
                    Filter = GetFilter(sender),
                    Multiselect = GetMultiselect(sender),
                };
                var owner = Window.GetWindow(sender);
                var result = dlg.ShowDialog(owner);
                callback(result!.Value, dlg.FileName);
            }
        }
    }
}
