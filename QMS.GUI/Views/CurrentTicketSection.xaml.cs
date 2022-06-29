using System;
using System.Windows.Media;
using System.Speech.Synthesis;
using System.Windows.Threading;
using System.Threading.Tasks;
using Prism.Regions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using FTMS.Services;
using FTMS.Services.BLL;

namespace QMS.GUI.Views
{
    /// <summary>
    /// Interaction logic for FooterRegion.xaml
    /// </summary>
    public partial class CurrentTicketSection : UserControl
    {
        private StreamSection streamUC = null;
        private QueueDetailsSection queueUC = null;
        private string _timerValue = Helpers.getQmsSetting(Helpers.Constants.CurrentTicketTimer);

        public CurrentTicketSection()
        {
            InitializeComponent();
            dispatchTimer();
        }

        DispatcherTimer timer = new DispatcherTimer();
        private void dispatchTimer()
        {
            try
            {
                timer.Interval = TimeSpan.FromSeconds(int.Parse(_timerValue));
                timer.Tick += timer_Tick;
                timer.Start();
                //fire it as soon as things start
                getNextTicket();
            }
            catch (Exception ex)
            {
              MessageBox.Show("CurrentTicketSection.dispatchTimer: {0} " + ex.Message);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            getNextTicket();
        }

        private void getNextTicket()
        {
            try
            {

                string _currentTicketTimer = Helpers.getQmsSetting(Helpers.Constants.CurrentTicketTimer);
                var nextTicket = QMSManager.getCalledQueueTokens(Properties.Settings.Default.region_code);
                if (nextTicket != null)
                {
                    Task.Run(() =>
                    {
                        if (queueUC == null)
                        {
                            getStartEventInQueueDetailsSection();
                        }
                       Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            if (queueUC != null)
                                queueUC.StartTimerCountDownEvent();
                        }), DispatcherPriority.Background);
                        
                    });
                    Thread.Sleep(6000);
                    QMSManager.flagQueueAsCalled(nextTicket.queue_id);
                    timer.Interval = TimeSpan.FromSeconds(int.Parse(_currentTicketTimer));
                    serviceNameTxt.Text = nextTicket.qms_service.qms_service_type.description;
                    seatNumberTxt.Text = nextTicket.qms_seat.seat_number.ToString();
                    QueueNumberLbl.Content = nextTicket.queue_number;

                    new Thread(() => {
                        if (streamUC == null)
                        {
                            getVLCUserControl();
                        }
                        this.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate
                        {
                            if (streamUC != null)
                            {
                                streamUC.pauseVLC();
                            }
                            for (int i = 0; i < 2; i++)
                            {
                                Thread.Sleep(1000);
                                AnnounceTicket();
                            }
                        }));

                        if (streamUC != null)
                        {
                            streamUC.pauseVLC();
                        }

                    }).Start();
                }
                else
                    timer.Interval = TimeSpan.FromSeconds(int.Parse(_timerValue));
            }
            catch (Exception ex)
            {
                MessageBox.Show("CurrentTicketSection.getNextTicket: {0} " + ex.Message);
            }
        }

        public void AnnounceTicket()
        {
            try
            {
                SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                synthesizer.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult);
                bool voiceStatus = TextToSpeechManager.voiceExists(Properties.Settings.Default.voices);
                if (voiceStatus)
                    synthesizer.SelectVoice(Properties.Settings.Default.voices);
                else
                {
                    var data = TextToSpeechManager.getDefaultInstalledVoice();
                    synthesizer.SelectVoice(data[0]);
                }
                synthesizer.SetOutputToDefaultAudioDevice();
                synthesizer.Volume = int.Parse(Properties.Settings.Default.voice_volume);
                synthesizer.Rate = int.Parse(Properties.Settings.Default.voice_rate);
                PromptBuilder pBuilder = new PromptBuilder();
                pBuilder.ClearContent();
                var player = new MediaPlayer();
                player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\Content\Sounds\announcer.wav"));
                player.Play();
                Thread.Sleep(1000);
                pBuilder.AppendText("Ticket Number " + QueueNumberLbl.Content + ", please Proceed to " + serviceNameTxt.Text + " Seat Number " + seatNumberTxt.Text);
                var current = synthesizer.GetCurrentlySpokenPrompt();
                if (current != null)
                    synthesizer.SpeakAsyncCancel(current);
                synthesizer.Speak(pBuilder);
            }
            catch (Exception ex)
            {
                MessageBox.Show("CurrentTicketSection.AnnounceTicket: {0} " + ex.Message);
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string _textBGColor = Helpers.getQmsSetting(Helpers.Constants.TextBGColor);
                string _textFGColor = Helpers.getQmsSetting(Helpers.Constants.TextFGColor);
                lblTicket.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_textFGColor));
                lblSeat.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_textFGColor));
                footerGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Helpers.getQmsSetting(Helpers.Constants.FooterColor)));
                QueueNumberLbl.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_textBGColor));
                serviceNameTxt.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_textBGColor));
                seatNumberTxt.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_textBGColor));
                QueueNumberLbl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_textFGColor));
                serviceNameTxt.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_textFGColor));
                seatNumberTxt.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_textFGColor));
            }
            catch (Exception ex)
            {
                MessageBox.Show("CurrentTicketSection.UserControl_Loaded: {0} " + ex.Message);
            }
        }
        private void getStartEventInQueueDetailsSection()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate
            {
                GUI.WaitingRoomScreen wc = null;
                foreach (var wn in Application.Current.Windows)
                {
                    if (wn.GetType() == typeof(GUI.WaitingRoomScreen))
                    {
                        wc = (GUI.WaitingRoomScreen)wn;
                        var t = RegionManager.GetRegionManager((Window)wn);
                        foreach (var region in t.Regions)
                        {
                            foreach (var view in region.Views)
                                if (view.GetType() == typeof(GUI.Views.QueueDetailsSection))
                                {
                                    queueUC = ((GUI.Views.QueueDetailsSection)view);
                                }
                        }
                    }
                }
            }));
        }

        public void getVLCUserControl()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate
            {
                GUI.WaitingRoomScreen wc = null;
                foreach (var wn in Application.Current.Windows)
                {
                    if (wn.GetType() == typeof(WaitingRoomScreen))
                    {
                        wc = (WaitingRoomScreen)wn;
                        var t = RegionManager.GetRegionManager((Window)wn);
                        foreach (var region in t.Regions)
                        {
                            foreach (var view in region.Views)
                                if (view.GetType() == typeof(StreamSection))
                                {
                                    streamUC = ((StreamSection)view);
                                }
                        }
                    }
                }
            }));
        }


    }
}
