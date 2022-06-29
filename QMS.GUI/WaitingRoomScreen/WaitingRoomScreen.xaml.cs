using System;
using System.Windows;
using System.Windows.Media;
using Prism.Regions;
using Prism.Modularity;
using FTMS.Services;

namespace QMS.GUI
{
    /// <summary>
    /// Interaction logic for CustomerScreen.xaml
    /// </summary>
    public partial class WaitingRoomScreen : Window
    {
        public WaitingRoomScreen(IRegionManager regionManager)
        {
            InitializeComponent();
            RegionManager.SetRegionManager(this, regionManager);
            setBackGroundColor();
        }
        private void setBackGroundColor()
        {
            try
            {
                WaitingHeaderMenu.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Helpers.getQmsSetting(Helpers.Constants.WaitingHeaderColor)));           
            }
            catch (Exception ex)
            {
                MessageBox.Show("WaitingRoomScreen.setBackGroundColor: {0}", ex.Message);
            }
        }
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
