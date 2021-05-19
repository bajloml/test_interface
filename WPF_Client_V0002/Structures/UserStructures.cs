using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_CrossComm_Client.Structures
{
    public class FormatParametersVariables: INotifyPropertyChanged
    {

        private String _numberOfFormatVar;
        private String _settingsFileName;
        private String _path;
        private String _rootElementName;
        private String _numFormatVarElementName;
        private String _numForVarAttributeName;
        private String _formatElementName;
        private String _formatParElementName;
        private String _formatParAttributeName;
        private String _formatParAttributeValue;
        private String _pointerStringElementName;
        private String _pointerStringAttributeName;
        private String _pointerStringAttributeValue;
        private String _transferReqElementName;
        private String _transferReqAttributeName;
        private String _transferReqAttributeValue;
        private String _parValidElementName;
        private String _parValidAttributeName;
        private String _parValidAttributeValue;
        private String _parameterToSendElementName;
        private String _parameterToSendAttributeName;
        private String _parameterToSendAttributeValue;

        public String NumberOfFormatVar
        {
            get { return _numberOfFormatVar; }
            set
            {
                // set value
                _numberOfFormatVar = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("NumberOfFormatVar");
            }
        }
        public String SettingsFileName
        {
            get { return _settingsFileName; }
            set
            {
                // set value
                _settingsFileName = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("SettingsFileName");
            }
        }
        public String path
        {
            get { return _path; }
            set
            {
                // set value
                _path = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("path");
            }
        }
        public String RootElementName
        {
            get { return _rootElementName; }
            set
            {
                // set value
                _rootElementName = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("RootElementName");
            }
        }
        public String NumFormatVarElementName
        {
            get { return _numFormatVarElementName; }
            set
            {
                // set value
                _numFormatVarElementName = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("NumFormatVarElementName");
            }
        }
        public String NumForVarAttributeName
        {
            get { return _numForVarAttributeName; }
            set
            {
                // set value
                _numForVarAttributeName = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("NumForVarAttributeName");
            }
        }
        public String FormatElementName
        {
            get { return _formatElementName; }
            set
            {
                // set value
                _formatElementName = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("FormatElementName");
            }
        }
        public String FormatParElementName
        {
            get { return _formatParElementName; }
            set
            {
                // set value
                _formatParElementName = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("FormatParElementName");
            }
        }
        public String FormatParAttributeName
        {
            get { return _formatParAttributeName; }
            set
            {
                // set value
                _formatParAttributeName = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("FormatParAttributeName");
            }
        }
        public String FormatParAttributeValue
        {
            get { return _formatParAttributeValue; }
            set
            {
                // set value
                _formatParAttributeValue = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("FormatParAttributeValue");
            }
        }
        public String PointerStringElementName
        {
            get { return _pointerStringElementName; }
            set
            {
                // set value
                _pointerStringElementName = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("PointerStringElementName");
            }
        }
        public String PointerStringAttributeName
        {
            get { return _pointerStringAttributeName; }
            set
            {
                // set value
                _pointerStringAttributeName = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("PointerStringAttributeName");
            }
        }
        public String PointerStringAttributeValue
        {
            get { return _pointerStringAttributeValue; }
            set
            {
                // set value
                _pointerStringAttributeValue = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("PointerStringAttributeValue");
            }
        }
        public String TransferReqElementName
        {
            get { return _transferReqElementName; }
            set
            {
                // set value
                _transferReqElementName = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("TransferReqElementName");
            }
        }
        public String TransferReqAttributeName
        {
            get { return _transferReqAttributeName; }
            set
            {
                // set value
                _transferReqAttributeName = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("TransferReqAttributeName");
            }
        }
        public String TransferReqAttributeValue
        {
            get { return _transferReqAttributeValue; }
            set
            {
                // set value
                _transferReqAttributeValue = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("TransferReqAttributeValue");
            }
        }
        public String ParValidElementName
        {
            get { return _parValidElementName; }
            set
            {
                // set value
                _parValidElementName = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("ParValidElementName");
            }
        }
        public String ParValidAttributeName
        {
            get { return _parValidAttributeName; }
            set
            {
                // set value
                _parValidAttributeName = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("ParValidAttributeName");
            }
        }
        public String ParValidAttributeValue
        {
            get { return _parValidAttributeValue; }
            set
            {
                // set value
                _parValidAttributeValue = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("ParValidAttributeValue");
            }
        }
        public String ParameterToSendElementName
        {
            get { return _parameterToSendElementName; }
            set
            {
                // set value
                _parameterToSendElementName = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("ParameterToSendElementName");
            }
        }
        public String ParameterToSendAttributeName
        {
            get { return _parameterToSendAttributeName; }
            set
            {
                // set value
                _parameterToSendAttributeName = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("ParameterToSendAttributeName");
            }
        }
        public String ParameterToSendAttributeValue
        {
            get { return _parameterToSendAttributeValue; }
            set
            {
                // set value
                _parameterToSendAttributeValue = value;
                // add event"OnPropertyChanged" on this object's parameter "Name"
                // this event is handled by the "PropertyChanged" PropertyChangedEventHandler
                OnPropertyChanged("ParameterToSendAttributeValue");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    };

    public struct FormatParameters
    {
        public int FormatParametersCount_int;
        public String TransferReq_String;
        public String ParameterPointer_String;
        public String ParameterValid_String;
        public String ParameterToSend_String;
        public String[] FormatValues_StringArray;

        public FormatParameters(int formatParametersCount, String transferReq, String parameterPointer, String parameterValid, String parameterToSend, String[] formatValue)
        {
            this.FormatParametersCount_int = formatParametersCount;
            this.TransferReq_String = transferReq;
            this.ParameterPointer_String = parameterPointer;
            this.ParameterValid_String = parameterValid;
            this.ParameterToSend_String = parameterToSend;
            this.FormatValues_StringArray = formatValue;
        }
    };
}
