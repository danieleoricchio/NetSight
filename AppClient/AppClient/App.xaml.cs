using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AppClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            int port=24690;
            foreach (string item in e.Args)
            {   
                if (!int.TryParse(item, out port)){
                    port = 24690;
                }
            }
            MainWindow window = new MainWindow(port);
        }
    }
}
