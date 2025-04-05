using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class VersionViewModel : ViewModelBase
    {
        public string ProductName
        {
            get { return ProductInfo.Product; }
        }
        public string Title
        {
            get { return ProductInfo.Title; }
        }

        public string Version
        {
            get { return "Ver." +  ProductInfo.VersionString; }
        }

        public string Copyright
        {
            get { return ProductInfo.Copyright; }
        }
    }
}
