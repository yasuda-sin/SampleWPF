using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public static class ProductInfo
    {
        private static Assembly assembly = Assembly.GetExecutingAssembly();
        private static string _title;
        public static string Title
        {
            get { return _title ?? (_title = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyTitleAttribute))).Title); }
        }

        private static string _description;
        public static string Description
        {
            get { return _description ?? (_description = ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyDescriptionAttribute))).Description); }
        }

        private static string _company;
        public static string Company
        {
            get { return _company ?? (_company = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute))).Company); }
        }

        private static string _product;
        public static string Product
        {
            get { return _product ?? (_product = ((AssemblyProductAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyProductAttribute))).Product); }
        }

        private static string _copyright;
        public static string Copyright
        {
            get { return _copyright ?? (_copyright = ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCopyrightAttribute))).Copyright); }
        }

        private static string _trademark;
        public static string Trademark
        {
            get { return _trademark ?? (_trademark = ((AssemblyTrademarkAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyTrademarkAttribute))).Trademark); }
        }

        private static Version _version;
        public static Version Version
        {
            get { return _version ?? (_version = assembly.GetName().Version); }
        }

        private static string _versionString;
        public static string VersionString
        {
            get
            {
                return _versionString ?? (_versionString = string.Format("{0}{1}{2}{3}",
                    Version.ToString(3), IsBetaMode ? " β" : "", Version.Revision == 0 ? "" : " rev." + Version.Revision,
                    IsDebugMode ? " Debug Mode" : ""));
            }
        }

        /// <summary>
        /// ビルド時のCLR versionを取得
        /// </summary>
        public static string CLRBuildVersion
        {
            get { return System.Reflection.Assembly.GetExecutingAssembly().ImageRuntimeVersion; }
        }

        public static string CLRExecuteVersion
        {
            get { return System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion(); }
        }
        
        public static bool IsDebugMode
        {
#if DEBUG
            get { return true; }
#else
            get { return false; }
#endif
        }

        public static bool IsBetaMode
        {
#if BETA
            get { return true; }
#else
            get { return false; }
#endif
        }
    }
}
