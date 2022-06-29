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
using System;

namespace QMS.GUI.Views
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class HeaderSection : UserControl
    {
        public HeaderSection()
        {
            InitializeComponent();
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do You want to close Application?", "Question ??", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result==MessageBoxResult.Yes) 
                Environment.Exit(0);
            else
                return;
        }
        private void btnTestPrintCick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Helpers.printerExists(Properties.Settings.Default.printer_name))
                {
                    ManageTicket ticket = new ManageTicket(1, DateTime.Now, Properties.Settings.Default.region_code, "Printer Testing", Properties.Settings.Default.printer_name);
                    ticket.Tickprint();
                }
                else
                    MessageBox.Show(Properties.Settings.Default.printer_name + " printer is not connected, Please contact your administrator!");                
            }
            catch (Exception ex)
            {
                MessageBox.Show("HeaderSection.btnTestPrintCick: {0}", ex.Message);
            }
        }

        private void btnTestSoundCick(object sender, RoutedEventArgs e)
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
                    MessageBox.Show("A New Default Voice is Selected Because Configured Voice Not Found");
                    var data = TextToSpeechManager.getDefaultInstalledVoice();
                    synthesizer.SelectVoice(data[0]);
                }
                synthesizer.SetOutputToDefaultAudioDevice();
                synthesizer.Volume = int.Parse(Properties.Settings.Default.voice_volume);
                synthesizer.Rate = int.Parse(Properties.Settings.Default.voice_rate);
                synthesizer.SpeakAsync(Properties.Settings.Default.test_voice);
            }
            catch (Exception ex)
            {
                MessageBox.Show("HeaderSection.btnTestSoundCick: {0}", ex.Message);
            }
        }
        private void CustomerScreenMenu_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string _btnColor = Helpers.getQmsSetting(Helpers.Constants.buttonColor);
                CustomerScreenMenu.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Helpers.getQmsSetting(Helpers.Constants.HeaderColor)));
                btnPrinter.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_btnColor));
                version.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_btnColor));
                btnVoice.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_btnColor));
                if (!Properties.Settings.Default.testmode)
                {
                    btnPrinter.Visibility = Visibility.Hidden;
                    btnVoice.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HeaderSection.CustomerScreenMenu_Loaded: {0}",ex.Message);
            }            
        }
    }
}
