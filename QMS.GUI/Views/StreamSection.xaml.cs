using System.Windows.Controls;

namespace QMS.GUI.Views
{
    /// <summary>
    /// Interaction logic for TokenView.xaml
    /// </summary>
    public partial class StreamSection : UserControl
    {
        public StreamSection()
        {
            InitializeComponent();
        }

        public void pauseVLC()
        {
            VlcUserControl t = (VlcUserControl)wnfrmHost.Child;
            t.axVLCPlugin21.playlist.togglePause();
        }
    }
}
