using System;
using System.Data;
using System.Windows;
using System.Windows.Forms;
using Prism.Regions;
using Prism.Modularity;
using QMS.DAL;
using System.Data.Entity.Core;

namespace QMS.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);
            if (QMS.GUI.Properties.Settings.Default.printer_name != "")
            {
                try
                {
                    using (var context = new QmsdbEntities())
                    {
                        context.Database.Connection.Open();
                        if (context.Database.Connection.State == ConnectionState.Open)
                        {
                            Bootstrapper trap = new Bootstrapper();
                            trap.Run();
                            var regionManager = RegionManager.GetRegionManager(App.Current.MainWindow);
                            WaitingRoomScreen mainWindow = new WaitingRoomScreen(regionManager);
                            if (Screen.AllScreens.Length > 1)
                            {
                                foreach (var screen in Screen.AllScreens)
                                {
                                    if (!screen.Primary)
                                    {
                                        mainWindow.Show();
                                        var workingArea = screen.WorkingArea;
                                        mainWindow.Left = workingArea.Left;
                                        mainWindow.Top = workingArea.Top;
                                        mainWindow.Width = workingArea.Width;
                                        mainWindow.Height = workingArea.Height;
                                        if (mainWindow.IsLoaded)
                                        {
                                            mainWindow.WindowState = WindowState.Maximized;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Screen s1 = Screen.AllScreens[0];
                                mainWindow.Show();
                            }

                        }
                        
                        context.Database.Connection.Close();
                    }
                }
                catch (EntityException ex)
                {
                    System.Windows.MessageBox.Show("Database is Unreachable, Please Confirm the Connection String. " + ex.Message);
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                    Environment.Exit(0);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Path to Installed Printer or Printer Name Not Specified in Config File");
                Environment.Exit(0);
            }


        }
    }
}
