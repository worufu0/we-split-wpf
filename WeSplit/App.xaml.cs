using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WeSplit
{
    public partial class App : Application
    {
        private void application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                Global.data = Global.data.load(Global.data, "Data/Data.ntp");

                if (Global.data.parameterSettings.splashScreenDisabled == false)
                {
                    SplashScreen splashScreen = new SplashScreen();
                    splashScreen.Show();
                }
                else
                {
                    HomeScreen homeScreen = new HomeScreen();
                    homeScreen.Show();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                this.Shutdown();
            }
        }
    }
}
