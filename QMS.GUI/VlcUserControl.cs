using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace QMS.GUI
{
    public partial class VlcUserControl : UserControl
    {
        public VlcUserControl()
        {
            InitializeComponent();
            BuildPlaylist(Properties.Settings.Default.video_path);
        }
        public void BuildPlaylist(string directory)
        {
            try
            {
                if (Directory.Exists(directory))
                {
                    axVLCPlugin21.playlist.clear();
                    axVLCPlugin21.AutoLoop = true;
                    string supportedExtensions = "*.mp4,*.avi,*.flv,*.mov";
                    var dir = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories).Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower()));
                    if (dir.Any(x => x != null))
                    {
                        foreach (var item in dir)
                        {
                            var uri = new Uri(item);
                            var convertedURI = uri.AbsoluteUri;
                            axVLCPlugin21.playlist.add(convertedURI);
                        }
                        axVLCPlugin21.playlist.play();
                    }
                    else
                        MessageBox.Show("Stream Source Invalid or Not Specified Correctly");
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("VlcUserControl.BuildPlaylist: {0}", ex.Message);
            }
           
        }
    }
}
