using System;
using WPF_CrossComm_Client.Structures;

namespace WPF_CrossComm_Client.MVVM_Pattern.Models
{
    public class NetParameters_Messages
    {
        private String _IP_address_Custom;
        private String _Port_Custom;

        public String IP_Address_CustomMessage
        {
            get
            {
                return _IP_address_Custom;
            }
            set
            {
                _IP_address_Custom = value;
            }
        }

        public String Port_CustomMessage
        {
            get
            {
                return _Port_Custom;
            }
            set
            {
                _Port_Custom = value;
            }
        }
    }

    public class FormatParameters_message
    {
        private FormatParametersVariables _formatParamters_Custom;

        public FormatParametersVariables FormatParamters_CustomMessage
        {
            get
            {
                return _formatParamters_Custom;
            }
            set
            {
                _formatParamters_Custom = value;
            }
        }
    }

    public class StatusExceptionText
    {
        private String _statusString;
        private String _exceptionString;

        public String StatusString
        {
            get
            {
                return _statusString;
            }
            set
            {
                _statusString = value;
            }
        }

        public String ExceptionString
        {
            get
            {
                return _exceptionString;
            }
            set
            {
                _exceptionString = value;
            }
        }
    }
}
