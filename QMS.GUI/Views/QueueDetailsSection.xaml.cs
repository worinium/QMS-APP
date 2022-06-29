using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;
using FTMS.Services;
using FTMS.Services.BLL;

namespace QMS.GUI.Views
{
    /// <summary>
    /// Interaction logic for TokenSideMenu.xaml
    /// </summary>
    public partial class QueueDetailsSection : UserControl
    {
        public QueueDetailsSection()
        {
            InitializeComponent();
            dispatchTimer();
        }
        private void dispatchTimer()
        {
            try
            {
                string _queueDetailsGridTimer = Helpers.getQmsSetting(Helpers.Constants.QueueDetailsGridTimer);
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(int.Parse(_queueDetailsGridTimer));
                timer.Tick += timer_Tick;
                timer.Start();
                //fire it as soon as things start
                refreshGrid();
            }
            catch (Exception ex)
            {
                throw new Exception("QueueDetailsSection.dispatchTimer: {0} " + ex.Message);
            }

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            refreshGrid();
        }
        private void refreshGrid()
        {
            try
            {
                var queueDetails = QMSManager.getRegionTokens(Properties.Settings.Default.region_code);
                DataGridQueueDetails.Items.Clear();
                foreach (var queueService in queueDetails)
                {
                    DataGridQueueDetails.Items.Add(queueService);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("QueueDetailsSection.refreshGrid: {0} " + ex.Message);
            }
        }
        private System.Windows.Forms.Timer timer1;
        private int _callerTimer = 20;

        public void StartTimerCountDownEvent()
        {
            //Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate
            //{

            timerLbl.Visibility = Visibility.Visible;
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000; // 1 second
            timer1.Start();
            timerLbl.Content = "Next Token in: " + _callerTimer + " seconds";
            //}));
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            Task.Run(() =>
            {
                _callerTimer--;
                timerLbl.Dispatcher.Invoke(() =>
                {
                    timerLbl.Content = "Next Token in: " + _callerTimer + " seconds";
                });
            });

            if (_callerTimer == 0)
            {
                timer1.Stop();
                timerLbl.Visibility = Visibility.Hidden;
                _callerTimer += 20;
            }

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += DisplayDateTime_Tick;
            timer.Start();
        }

        private void DisplayDateTime_Tick(object sender, EventArgs e)
        {
            try
            {
                new Thread(() =>
                {
                    timeLbl.Dispatcher.Invoke(() =>
                    {
                        timeLbl.Content = DateTime.Now.ToLongTimeString();
                    });
                    dateLbl.Dispatcher.Invoke(() =>
                    {
                        dateLbl.Content = DateTime.Now.ToLongDateString();
                    });
                    

                }).Start();

            }
            catch (Exception ex)
            {
                throw new Exception("QueueDetailsSection.DisplayDateTime_Tick: {0} " + ex.Message);
            }
        }

        private void TokenSideMenuGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string _gridColor = Helpers.getQmsSetting(Helpers.Constants.GridColor);
                string _textFGColor = Helpers.getQmsSetting(Helpers.Constants.TextFGColor);
                string _gridLineColor = Helpers.getQmsSetting(Helpers.Constants.GridlineColor);

                TokenSideMenuGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_gridColor));
                DataGridQueueDetails.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_gridColor));
                DataGridQueueDetails.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_textFGColor));
                timeLbl.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_gridColor));
                dateLbl.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_gridColor));
                timeLbl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_textFGColor));
                dateLbl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_textFGColor));

                // Setting the Datagrid Header color
                var style = new Style(typeof(System.Windows.Controls.Primitives.DataGridColumnHeader));
                style.Setters.Add(new Setter { Property = System.Windows.Controls.Primitives.DataGridColumnHeader.BackgroundProperty, Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_gridColor)) });
                style.Setters.Add(new Setter { Property = System.Windows.Controls.Primitives.DataGridColumnHeader.ForegroundProperty, Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_textFGColor)) });
                style.Setters.Add(new Setter { Property = System.Windows.Controls.Primitives.DataGridColumnHeader.BorderBrushProperty, Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_gridLineColor)) });
                style.Setters.Add(new Setter { Property = System.Windows.Controls.Primitives.DataGridColumnHeader.BorderThicknessProperty, Value = new Thickness(1, 1, 1, 1) });
                style.Setters.Add(new Setter { Property = System.Windows.Controls.Primitives.DataGridColumnHeader.HorizontalContentAlignmentProperty, Value = HorizontalAlignment.Center });
                style.Setters.Add(new Setter { Property = System.Windows.Controls.Primitives.DataGridColumnHeader.FontSizeProperty, Value = Convert.ToDouble(Properties.Settings.Default.header_fontSize) });
                style.Seal();
                DataGridQueueDetails.ColumnHeaderStyle = style;

                var style2 = new Style(typeof(DataGridCell));
                style2.Setters.Add(new Setter { Property = DataGridCell.ForegroundProperty, Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_textFGColor)) });
                style2.Setters.Add(new Setter { Property = DataGridCell.HorizontalAlignmentProperty, Value = HorizontalAlignment.Center });
                style2.Setters.Add(new Setter { Property = DataGridCell.FontSizeProperty, Value = Convert.ToDouble(Properties.Settings.Default.header_fontSize) });
                style2.Seal();
                DataGridQueueDetails.CellStyle = style2;

                var style3 = new Style(typeof(DataGridRow));
                style3.Setters.Add(new Setter { Property = DataGridRow.BorderThicknessProperty, Value = new Thickness(0, 1, 0, 1) });
                style3.Setters.Add(new Setter { Property = DataGridRow.BorderBrushProperty, Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_gridLineColor)) });
                style3.Seal();
                DataGridQueueDetails.RowStyle = style3;

            }
            catch (Exception ex)
            {
                throw new Exception("QueueDetailsSection.TokenSideMenuGrid_Loaded: {0} " + ex.Message);
            }
        }
    }
}
