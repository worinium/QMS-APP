using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Security.Cryptography;
using System.Drawing.Printing;
using QMS.DAL;

namespace FTMS.Services
{
    public class Helpers
    {
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public class Constants
        {
            
            //QMS Color Setting
            public const String FooterColor = "footer";
            public const String TabletBGColor = "tabletAppBackground";
            public const String HeaderColor = "header";
            public const String WaitingHeaderColor = "WaitingRoomheader";
            public const String buttonColor = "buttonsColor";
            public const String GridColor = "gridColor";
            public const String TextBGColor = "textBGColor";
            public const String TextFGColor = "textFGColor";
            public const String GridlineColor = "gridLineColor";
            //timer data
            public const String QueueDetailsGridTimer = "datagrid_timer";
            public const String CurrentTicketTimer = "caller_timer";
            //queue statuses
            public const String StatusWaiting = "waiting";
            public const String StatusProcessing = "processing";

            //Agency Setting
            public const String AgencySetting = "agency";
        }
        public static int extractNumberFromFilenumber(String fileNumber)
        {
            var file_num = new String((from c in fileNumber
                                       where Char.IsDigit(c)
                                       select c).ToArray());
            if (fileNumber.Length == 0)
                return 0;
            return Convert.ToInt32(file_num);
        }

        public static string getPublicSetting(string setting_code)
        {
            try
            {
                using (QmsdbEntities context = new QmsdbEntities())
                {
                    var settingObj = context.settings.Where(s => s.mr_code == setting_code && s.active).FirstOrDefault();
                    if (settingObj != null)
                        return settingObj.value;
                    else
                        return "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error While getting Setting due to: {0}", ex.Message));
            }
        }
        
        // Getting Description Settings from qms_setting table
        public static string getQmsSetting(string setting_code)
        {
            try
            {
                using (QmsdbEntities context = new QmsdbEntities())
                {
                    qms_setting settingObj = context.qms_setting.Where(s => s.mr_code == setting_code && s.active).FirstOrDefault();
                    if (settingObj != null)
                        return settingObj.value;
                    else
                        return "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error While getting QmsSetting due to: {0}", ex.Message));
            }
        }

        private static string Decrypt(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        public static string decryptedConnString()
        {
            var encrConnString = ConfigurationManager.ConnectionStrings["FtmsEntities"].ConnectionString;
            var decrConnString = Decrypt(encrConnString, "ftms@123456").Replace("&quot;", "\"").ToString();
            return decrConnString;
        }

        public static void logMessage(String Message, String Category)
        {
            using (QmsdbEntities context = new QmsdbEntities())
            {
                logger logEntry = new logger();
                logEntry.category = Category;
                logEntry.createdate = DateTime.Now;
                logEntry.machinename = System.Environment.MachineName;
                logEntry.message = Message;

                context.loggers.Add(logEntry);
                context.SaveChanges();
            }
        }

        public static bool printerExists(string printer_name)
        {
            try
            {
                PrinterSettings printer = new PrinterSettings();
                printer.PrinterName = printer_name;
                return printer.IsValid;                   
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error While checking the printer due to: {0}", ex.Message));
            }
        }

       
    }
}