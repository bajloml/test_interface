using System;
using System.Collections.Specialized;
using System.Configuration;

namespace WPF_CrossComm_Client.MVVM_Pattern.Models
{
    class ConfigHandler
    {
        public String ReadCommSetting(string key)
        {
            try
            {
                var CommSettings = ConfigurationManager.AppSettings;
                String result = CommSettings[key] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException e)
            {
                return e.ToString();
                //Console.WriteLine("Error reading app settings");
            }
        }
    }
}
