using FTMS.Services;
using FTMS.Services.BLL;
using FTMS.Services.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace QMS.GUI.Views
{
    /// <summary>
    /// Interaction logic for TokenView.xaml
    /// </summary>
    public partial class ServiceList : UserControl
    {
        String service_name;
        double btnWidth;
        double btnHeight;
        double initMarginLeft;
        double initMarginTop;
        int btnCount;
        int columnNumber = 1;
        int maxButtonsPerCol = 1;

        public ServiceList()
        {
            InitializeComponent();
        }
        private void getBtnVariables()
        {
            columnNumber = Convert.ToInt32(Math.Ceiling((btnCount * 1.0) / 5)); //Get the number of columns with max 5 per column
            maxButtonsPerCol = Convert.ToInt32(Math.Ceiling((btnCount * 1.0) / (columnNumber * 1.0))); //get the maximum number of buttons per column
            initMarginLeft = ucBtnMenu.ActualWidth * 0.1;
            initMarginTop = ucBtnMenu.ActualHeight * 0.1;
            btnWidth = ucBtnMenu.ActualWidth * (0.9 / (columnNumber * 1.0) - 0.1);
            btnHeight = ucBtnMenu.ActualHeight * 0.8 / (1.5 * maxButtonsPerCol - 0.5);
        }

        private void populateServices()
        {
            try
            {
                string _buttonColor = Helpers.getQmsSetting(Helpers.Constants.buttonColor);
                string _textFGColor = Helpers.getQmsSetting(Helpers.Constants.TextFGColor);
                List<QmsService> LsServices = QMSManager.GetQmsService(Properties.Settings.Default.region_code);
                btnCount = LsServices.Count;
                getBtnVariables();
                int k = 0;
                for (int i = 0; i < columnNumber; i++)
                {
                    for (int j = 0; j < maxButtonsPerCol && k < btnCount; j++, k++)
                    {
                        Button newBtn = new Button();
                        newBtn.Content = LsServices[k].ServiceTypeDescription;
                        newBtn.Width = btnWidth;
                        newBtn.Height = btnHeight;
                        newBtn.Margin = new Thickness(initMarginLeft, initMarginTop + 1.5 * j * btnHeight, 0, 0);
                        int par = LsServices[k].ServiceID;
                        newBtn.Click += (sender, e) => btnPrintTicket(sender, e, par);
                        newBtn.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_textFGColor));
                        newBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_buttonColor));
                        newBtn.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_buttonColor));
                        newBtn.FontSize = 30;
                        newBtn.HorizontalAlignment = HorizontalAlignment.Left;
                        newBtn.VerticalAlignment = VerticalAlignment.Top;
                        myGrid.Children.Add(newBtn);
                    }
                    initMarginLeft = initMarginLeft + btnWidth + ucBtnMenu.ActualWidth * 0.1;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnPrintTicket(object sender, RoutedEventArgs e, int serviceID)
        {
            try
            {
                //if (Helpers.printerExists(Properties.Settings.Default.printer_name))
                //{
                    int tokenNo = QMSManager.getNextTokenForService(serviceID);
                    Button btn = (Button)sender;
                    service_name = btn.Content.ToString();
                    //ManageTicket tick = new ManageTicket(tokenNo, DateTime.Now, Properties.Settings.Default.region_code, service_name, Properties.Settings.Default.printer_name);
                    //tick.Tickprint();
                //}
                //else
                //    MessageBox.Show(Properties.Settings.Default.printer_name + " printer is not connected, Please contact your administrator!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error while printing due to: {0}, Please contact your administrator!", ex.Message));
            }
        }

        private void ucLoaded(object sender, RoutedEventArgs e)
        {
            //wait for window render before rendering the buttons
            populateServices();
        }

        private void myGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                myGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Helpers.getQmsSetting(Helpers.Constants.TabletBGColor)));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in backgroundColor due to " + ex.Message);
            }
        }
    }
}
