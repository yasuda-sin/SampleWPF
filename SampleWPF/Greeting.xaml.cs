using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SampleWPF
{
    /// <summary>
    /// </summary>
    public partial class Greeting : Window
    {
        public Greeting()
        {
            InitializeComponent();
        }

        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {
            if (HelloButton.IsChecked ?? false)
            {
                MessageBox.Show("Hello.");
            }
            else if (GoodbyeButton.IsChecked ?? false)
            {
                MessageBox.Show("Goodbye.");
            }
        }
    }
}