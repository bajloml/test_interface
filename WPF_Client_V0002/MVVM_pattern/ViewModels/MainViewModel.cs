using Caliburn.Micro;
using System;
using System.ComponentModel;
using System.Text;
using WPF_CrossComm_Client.Structures;
using WPF_CrossComm_Client.MVVM_Pattern.Models;
using System.Diagnostics;
using System.IO;

namespace WPF_CrossComm_Client.MVVM_Pattern.ViewModels
{
    public class MainViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<StatusExceptionText>
    {
        //Constructor
        public MainViewModel(IEventAggregator eventAggregator, IFormatParameterTransfer_ViewModel formatParameterTransfer_ViewModel, IReadWriteTab_ViewModel readWriteTab_ViewModel)
        {
            _eventAggregator                                = eventAggregator;
            I_FormatParameterTransfer_ViewModel             = formatParameterTransfer_ViewModel;
            I_ReadWriteTab_ViewModel                        = readWriteTab_ViewModel;
        }

        //locals
        #region locals

        //String builders
        private StringBuilder _statusText                               = new StringBuilder();
        private StringBuilder _exceptionText                            = new StringBuilder();
        private String _status;
        private String _exceptions;

        //Interface IEventAggregator, this is neccesarry to pass parameters between ViewModels
        private readonly IEventAggregator _eventAggregator;

        // config file handler for default settings
        private static ConfigHandler _conHandler                 = new ConfigHandler();

        //Structures to communicate through ViewModels
        private FormatParametersVariables _formatParamters_Struc = new FormatParametersVariables(); 

        // Background Worker 
        private BackgroundWorker CheckConnection_Worker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

        // Default parameters
        private String _IP_Address = _conHandler.ReadCommSetting("Config_IPAddress");
        private String _Port = _conHandler.ReadCommSetting("Config_Port");             
        private String _path = System.IO.Directory.GetCurrentDirectory();

        #endregion

        //Accessors properties
        #region Accessor properties
        public String IP_Address
        {
            get
            {
                return _IP_Address;
            }
            set
            {
                _IP_Address = value;
                //Caliburn.micro notify UI control elements 
                NotifyOfPropertyChange(() => IP_Address);
                //notify other ViewModels which are subscribed, publish is neccesarry only in the view in which the value can be changed
                _eventAggregator.PublishOnUIThread(new NetParameters_Messages() { IP_Address_CustomMessage = IP_Address });
            }
        }
        public String Port
        {
            get
            {
                return _Port;
            }
            set
            {
                _Port = value;
                //Caliburn.micro notify UI control elements
                NotifyOfPropertyChange(() => Port);
                //notify other ViewModels which are subscribed, publish is neccesarry only in the view in which the value can be changed
                _eventAggregator.PublishOnUIThread(new NetParameters_Messages() { Port_CustomMessage = Port });
            }
        }
        public String StatusText
        {
            get
            {
                return _status;
            }
            set
            {
                _status = "";
                _statusText.Append( value);
                _statusText.AppendLine();
                _status = _statusText.ToString();
                NotifyOfPropertyChange(() => StatusText);
            }
        }
        public String ExceptionText
        {
            get
            {
                return _exceptions;
            }
            set
            {
                _exceptions = "";
                _exceptionText.Append(value);
                _exceptionText.AppendLine();
                _exceptions = _exceptionText.ToString();
                NotifyOfPropertyChange(() => ExceptionText);
            }
        }
        public FormatParametersVariables FormatParamters_Struc
        {
            get
            {
                return _formatParamters_Struc;
            }
            set
            {
                _formatParamters_Struc = value;
                //notify UI control elements
                NotifyOfPropertyChange(() => FormatParamters_Struc);
                //notify other ViewModels which are subscribed
                _eventAggregator.PublishOnUIThread(new FormatParameters_message() { FormatParamters_CustomMessage = FormatParamters_Struc });
            }
        }

        public IFormatParameterTransfer_ViewModel   I_FormatParameterTransfer_ViewModel { get; set; }
        public IReadWriteTab_ViewModel              I_ReadWriteTab_ViewModel { get; set; }

        #endregion

        //Background Worker methods and events
        #region Background worker CheckConn 
        public void CheckConn_Clicked()
        {
            try
            {
                // Nothing thats is related to the UI (WPF) thread should be given to the background worker thread without preparation
                // basically nothing that is part of the controls shouldn't be given to the background worker thread (otherwise Dispatcher is needed, and then UI thread is frozen)
                //Use a Dispatcher just for the fast communication between threads, because that way the UI(WPF) thread will not be froze

                //Start the worker if it is not already running
                if (!CheckConnection_Worker.IsBusy)
                {
                    CheckConnection_Worker.RunWorkerAsync();
                }
            }
            catch (Exception exc)
            {
                ExceptionText = exc.ToString();
            }
        }
        private void ProgressChanged_CheckConnections(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        private void WorkCompleted_CheckConnection(object sender, RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        private void DoWork_CheckConnection(object sender, DoWorkEventArgs e)
        {
            try
            {
                StatusText = "Trying to connect...    " + DateTime.Now.ToString();

                TCP_Write_Read TCP_WriteRead = new TCP_Write_Read(IP_Address, Port);

                if (TCP_WriteRead.CheckConnection())
                {
                    StatusText = "Connection works   " + DateTime.Now.ToString();
                }
                else
                {
                    StatusText = "Connection doesn't work   " + DateTime.Now.ToString();
                }
            }
            catch (Exception exc)
            {
                ExceptionText = exc.ToString() + "\n" + DateTime.Now.ToString();
            }
        }
        #endregion

        //Overides
        #region overides
        protected override void OnInitialize()
        {
            _eventAggregator.Subscribe(this);
            Items.Add(I_ReadWriteTab_ViewModel);
            Items.Add(I_FormatParameterTransfer_ViewModel);

            CheckConnection_Worker.DoWork               += DoWork_CheckConnection;
            CheckConnection_Worker.RunWorkerCompleted   += WorkCompleted_CheckConnection;
            CheckConnection_Worker.ProgressChanged      += ProgressChanged_CheckConnections;

            XML_Handler XML_FileHandler                         = new XML_Handler();

            FormatParamters_Struc.path                          = System.IO.Directory.GetCurrentDirectory();
            FormatParamters_Struc.NumberOfFormatVar             = _conHandler.ReadCommSetting("NumberOfFormatVar");
            FormatParamters_Struc.SettingsFileName              = _conHandler.ReadCommSetting("SettingsFileName");
            FormatParamters_Struc.RootElementName               = _conHandler.ReadCommSetting("RootElementName");
            FormatParamters_Struc.NumFormatVarElementName       = _conHandler.ReadCommSetting("NumFormatVarElementName");
            FormatParamters_Struc.NumForVarAttributeName        = _conHandler.ReadCommSetting("NumForVarAttributeName");
            FormatParamters_Struc.FormatElementName             = _conHandler.ReadCommSetting("FormatElementName");
            FormatParamters_Struc.FormatParElementName          = _conHandler.ReadCommSetting("FormatParElementName");
            FormatParamters_Struc.FormatParAttributeName        = _conHandler.ReadCommSetting("FormatParAttributeName");
            FormatParamters_Struc.FormatParAttributeValue       = _conHandler.ReadCommSetting("FormatParAttributeValue");
            FormatParamters_Struc.PointerStringElementName      = _conHandler.ReadCommSetting("PointerStringElementName");
            FormatParamters_Struc.PointerStringAttributeName    = _conHandler.ReadCommSetting("PointerStringAttributeName");
            FormatParamters_Struc.PointerStringAttributeValue   = _conHandler.ReadCommSetting("PointerStringAttributeValue");
            FormatParamters_Struc.TransferReqElementName        = _conHandler.ReadCommSetting("TransferReqElementName");
            FormatParamters_Struc.TransferReqAttributeName      = _conHandler.ReadCommSetting("TransferReqAttributeName");
            FormatParamters_Struc.TransferReqAttributeValue     = _conHandler.ReadCommSetting("TransferReqAttributeValue");
            FormatParamters_Struc.ParValidElementName           = _conHandler.ReadCommSetting("ParValidElementName");
            FormatParamters_Struc.ParValidAttributeName         = _conHandler.ReadCommSetting("ParValidAttributeName");
            FormatParamters_Struc.ParValidAttributeValue        = _conHandler.ReadCommSetting("ParValidAttributeValue");
            FormatParamters_Struc.ParameterToSendElementName    = _conHandler.ReadCommSetting("ParameterToSendElementName");
            FormatParamters_Struc.ParameterToSendAttributeName  = _conHandler.ReadCommSetting("ParameterToSendAttributeName");
            FormatParamters_Struc.ParameterToSendAttributeValue = _conHandler.ReadCommSetting("ParameterToSendAttributeValue");

            //create Settings file if it doesn't exists
            if (!File.Exists(FormatParamters_Struc.SettingsFileName))
            {
                XML_FileHandler.Create_XMLfileStruc(FormatParamters_Struc);
            }

            base.OnInitialize();
        }

        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.Unsubscribe(this);

            CheckConnection_Worker.DoWork               -= DoWork_CheckConnection;
            CheckConnection_Worker.RunWorkerCompleted   -= WorkCompleted_CheckConnection;
            CheckConnection_Worker.ProgressChanged      -= ProgressChanged_CheckConnections;

            base.OnDeactivate(close);
        }
        #endregion

        //Handlers
        #region interface handlers
        //handle messages from another viewmodels
        public void Handle(StatusExceptionText message)
        {
            ExceptionText =  message.ExceptionString;
            StatusText    =  message.StatusString;
        }
        #endregion

        //General Methods
        #region General Methods

        public void Settings_Click()
        {
            ConfigHandler con = new ConfigHandler();
            Process.Start(_path + "\\" + con.ReadCommSetting("SettingsFileName"));
        }

        public void CloseWindow()
        {
            this.TryClose();
        }

        #endregion

    }
}
