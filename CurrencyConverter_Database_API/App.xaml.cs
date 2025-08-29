using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CurrencyConverter_Database_API
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// THis method is called when the application starts up.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Set the DataDirectory to the project directory for database file resolution
            // @"..\..\" is used to navigate to the project root from the output directory (bin\Debug or bin\Release)
            var projectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            AppDomain.CurrentDomain.SetData("DataDirectory", projectDir);
            base.OnStartup(e);
        }

    }
}
