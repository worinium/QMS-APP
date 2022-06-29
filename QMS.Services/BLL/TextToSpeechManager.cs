using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;

namespace FTMS.Services.BLL
{
    public class TextToSpeechManager
    {
        public static List<string> getDefaultInstalledVoice()
        {
            try
            {
                SpeechSynthesizer talker = new SpeechSynthesizer();
                System.Collections.ObjectModel.ReadOnlyCollection<InstalledVoice> voices = talker.GetInstalledVoices();
                var data = new List<string>();
                foreach (InstalledVoice voice in voices)
                {
                    data.Add(voice.VoiceInfo.Name);
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("TextToSpeechManager.getDefaultInstalledVoice : Error While Getting Default Installed Voice due to: {0}", ex.Message));
            }
        }
        public static bool voiceExists(string voiceName)
        {
            try
            {
                SpeechSynthesizer talker = new SpeechSynthesizer();
                System.Collections.ObjectModel.ReadOnlyCollection<InstalledVoice> voices = talker.GetInstalledVoices();
                var data = new List<string>();
                foreach (InstalledVoice voice in voices)
                {
                    data.Add(voice.VoiceInfo.Name);
                }
                if (data.Any(x => x != voiceName))
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("TextToSpeechManager.voiceExists : Error While checking the Voice due to: {0}", ex.Message));
            }
        }
    }
}
