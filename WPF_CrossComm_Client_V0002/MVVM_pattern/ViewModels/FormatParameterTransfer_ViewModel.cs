using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using WPF_CrossComm_Client.MVVM_Pattern.Models;
using WPF_CrossComm_Client.Structures;



namespace WPF_CrossComm_Client.MVVM_Pattern.ViewModels
{
    public class FormatParameterTransfer_ViewModel : Screen, IScreen, INotifyPropertyChanged, IHandle<NetParameters_Messages>, IHandle<FormatParameters_message>, IFormatParameterTransfer_ViewModel
    {
        //Constructor
        public FormatParameterTransfer_ViewModel( IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            DisplayName = "Format parameters transfer";
        }

        //locals
        #region locals

        //Background worker        
        private BackgroundWorker _transferParameters_Worker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

        //Interface IEventAggregator, this is neccesarry to pass parameters between ViewModels
        private readonly IEventAggregator _eventAggregator;

        // config file handler for default settings
        private static ConfigHandler        _conHandler         = new ConfigHandler();

        private FormatParametersVariables   _formatPar_Struc    = new FormatParametersVariables();
        private FormatParameters            _transferFormatParameters_struc;

        private int                         _percentage;

        private bool                        _transferParameters_BackgroundworkerDisposed;

        private String[]                    _formatValues_StringArray;

        private String                      _tabName             = "Format parameters transfer";
        private String                      _path                = System.IO.Directory.GetCurrentDirectory();
        private String                      _percentageString;
        private String                      _IP_Address_ParTranViewModel;
        private String                      _Port_ParTransViewModel;
        private String                      _status_FormatTransfer_Tab;
        private String                      _exception_FormatTransfer_Tab;

        //lists of type string
        private  List<String> _list_FormatVariables                                                 = new List<String>();
        private  List<String> _list_FormatValues                                                    = new List<String>();
        private  List<String> _list_PointerVariable                                                 = new List<String>();
        private  List<String> _list_TransferReq                                                     = new List<String>();
        private  List<String> _list_ParValid                                                        = new List<String>();
        private  List<String> _list_ParameterToSend                                                 = new List<String>();

        //Observable collections of type Parameters
        //type parameters implements interface "INotifyPropertyChanged" which is important to update the collection item when it has been changed
        private ObservableCollectionWithItemNotify<Parameters> _formatVariables_ObservableCollection  = new ObservableCollectionWithItemNotify<Parameters>();
        private ObservableCollectionWithItemNotify<Parameters> _formatValues_ObservableCollection     = new ObservableCollectionWithItemNotify<Parameters>();
        private ObservableCollectionWithItemNotify<Parameters> _pointer_ObservableCollection          = new ObservableCollectionWithItemNotify<Parameters>();
        private ObservableCollectionWithItemNotify<Parameters> _transferReq_ObservableCollection      = new ObservableCollectionWithItemNotify<Parameters>();
        private ObservableCollectionWithItemNotify<Parameters> _parValid_ObservableCollection         = new ObservableCollectionWithItemNotify<Parameters>();
        private ObservableCollectionWithItemNotify<Parameters> _parameterToSend_ObservableCollection  = new ObservableCollectionWithItemNotify<Parameters>();

        #endregion

        //accessors 
        #region accessors
        //these are used by the view(XAML) to update the collections on change
        public ObservableCollectionWithItemNotify<Parameters> FormatVariables_ObservableCollection
        {
            get
            {
                return _formatVariables_ObservableCollection;
            }
            set
            {
                _formatVariables_ObservableCollection = value;
                NotifyOfPropertyChange(() => FormatVariables_ObservableCollection);
            }
        }
        public ObservableCollectionWithItemNotify<Parameters> FormatValues_ObservableCollection
        {
            get
            {
                return _formatValues_ObservableCollection;
            }
            set
            {
                _formatValues_ObservableCollection = value;
                NotifyOfPropertyChange(() => FormatValues_ObservableCollection);
            }
        }
        public ObservableCollectionWithItemNotify<Parameters> Pointer_ObservableCollection
        {
            get
            {
                return _pointer_ObservableCollection;
            }
            set
            {
                _pointer_ObservableCollection = value;
                NotifyOfPropertyChange(() => Pointer_ObservableCollection);
            }
        }
        public ObservableCollectionWithItemNotify<Parameters> TransferReq_ObservableCollection
        {
            get
            {
                return _transferReq_ObservableCollection;
            }
            set
            {
                _transferReq_ObservableCollection = value;
                NotifyOfPropertyChange(() => TransferReq_ObservableCollection);
            }
        }
        public ObservableCollectionWithItemNotify<Parameters> ParValid_ObservableCollection
        {
            get
            {
                return _parValid_ObservableCollection;
            }
            set
            {
                _parValid_ObservableCollection = value;
                NotifyOfPropertyChange(() => ParValid_ObservableCollection);
            }
        }
        public ObservableCollectionWithItemNotify<Parameters> ParameterToSend_ObservableCollection
        {
            get
            {
                return _parameterToSend_ObservableCollection;
            }
            set
            {
                _parameterToSend_ObservableCollection = value;
                NotifyOfPropertyChange(() => ParameterToSend_ObservableCollection);
            }
        }

        public int Percentage
        {
            get
            {
                return _percentage;
            }
            set
            {
                _percentage = value;
                NotifyOfPropertyChange(() => Percentage);
            }
        }
        public String PercentageString
        {
            get
            {
                return _percentageString;
            }
            set
            {
                _percentageString = value;
                NotifyOfPropertyChange(() => PercentageString);
            }
        }
        public String Status_FormatParameters_Tab
        {
            get
            {
                return _status_FormatTransfer_Tab;
            }
            set
            {
                _status_FormatTransfer_Tab = value;
                NotifyOfPropertyChange(() => _status_FormatTransfer_Tab);
                _eventAggregator.PublishOnUIThread(new StatusExceptionText() { StatusString = Status_FormatParameters_Tab });
            }
        }
        public String Exception_FormatParameters_Tab
        {
            get
            {
                return _exception_FormatTransfer_Tab;
            }
            set
            {
                _exception_FormatTransfer_Tab = value;
                NotifyOfPropertyChange(() => Exception_FormatParameters_Tab);
                _eventAggregator.PublishOnUIThread(new StatusExceptionText() { ExceptionString = Exception_FormatParameters_Tab });
            }
        }
        public String IP_Address_ToFormatParViewModel
        {
            get
            {
                return _IP_Address_ParTranViewModel;
            }
            set
            {
                _IP_Address_ParTranViewModel = value;
                NotifyOfPropertyChange(() => IP_Address_ToFormatParViewModel);
            }
        }
        public String Port_ToFormatParViewModel
        {
            get
            {
                return _Port_ParTransViewModel;
            }
            set
            {
                _Port_ParTransViewModel = value;
                NotifyOfPropertyChange(() => Port_ToFormatParViewModel);
            }
        }
        public String TabName
        {
            get
            {
                return _tabName;
            }
            set
            {
                _tabName = value;
                NotifyOfPropertyChange(() => TabName);
            }
        }

        public FormatParametersVariables FormatPar_Struc_ToFormatParViewModel
        {
            get
            {
                return _formatPar_Struc;
            }
            set
            {
                _formatPar_Struc = value;
                NotifyOfPropertyChange(() => FormatPar_Struc_ToFormatParViewModel);
            }
        }

        #endregion

        //Overides
        #region Overides
        protected override void OnActivate()
        {
            // Read parent values on activation, after it will be handled by messaging
            MainViewModel ParentViewModel   = (MainViewModel)this.Parent;
            _IP_Address_ParTranViewModel    = ParentViewModel.IP_Address;
            _Port_ParTransViewModel         = ParentViewModel.Port;

            //Background worker attach events
            _transferParameters_Worker.DoWork               += DoWork_Transfer;
            _transferParameters_Worker.RunWorkerCompleted   += WorkCompleted_Transfer;
            _transferParameters_Worker.ProgressChanged      += ProgressChanged_Transfer;

            //subscribe to the event messages
            _eventAggregator.Subscribe(this);

            XML_Handler XML_FileHandler = new XML_Handler();

            FormatPar_Struc_ToFormatParViewModel.path                           = System.IO.Directory.GetCurrentDirectory();
            FormatPar_Struc_ToFormatParViewModel.NumberOfFormatVar              = _conHandler.ReadCommSetting("NumberOfFormatVar");
            FormatPar_Struc_ToFormatParViewModel.SettingsFileName               = _conHandler.ReadCommSetting("SettingsFileName");
            FormatPar_Struc_ToFormatParViewModel.RootElementName                = _conHandler.ReadCommSetting("RootElementName");
            FormatPar_Struc_ToFormatParViewModel.NumFormatVarElementName        = _conHandler.ReadCommSetting("NumFormatVarElementName");
            FormatPar_Struc_ToFormatParViewModel.NumForVarAttributeName         = _conHandler.ReadCommSetting("NumForVarAttributeName");
            FormatPar_Struc_ToFormatParViewModel.FormatElementName              = _conHandler.ReadCommSetting("FormatElementName");
            FormatPar_Struc_ToFormatParViewModel.FormatParElementName           = _conHandler.ReadCommSetting("FormatParElementName");
            FormatPar_Struc_ToFormatParViewModel.FormatParAttributeName         = _conHandler.ReadCommSetting("FormatParAttributeName");
            FormatPar_Struc_ToFormatParViewModel.FormatParAttributeValue        = _conHandler.ReadCommSetting("FormatParAttributeValue");
            FormatPar_Struc_ToFormatParViewModel.PointerStringElementName       = _conHandler.ReadCommSetting("PointerStringElementName");
            FormatPar_Struc_ToFormatParViewModel.PointerStringAttributeName     = _conHandler.ReadCommSetting("PointerStringAttributeName");
            FormatPar_Struc_ToFormatParViewModel.PointerStringAttributeValue    = _conHandler.ReadCommSetting("PointerStringAttributeValue");
            FormatPar_Struc_ToFormatParViewModel.TransferReqElementName         = _conHandler.ReadCommSetting("TransferReqElementName");
            FormatPar_Struc_ToFormatParViewModel.TransferReqAttributeName       = _conHandler.ReadCommSetting("TransferReqAttributeName");
            FormatPar_Struc_ToFormatParViewModel.TransferReqAttributeValue      = _conHandler.ReadCommSetting("TransferReqAttributeValue");
            FormatPar_Struc_ToFormatParViewModel.ParValidElementName            = _conHandler.ReadCommSetting("ParValidElementName");
            FormatPar_Struc_ToFormatParViewModel.ParValidAttributeName          = _conHandler.ReadCommSetting("ParValidAttributeName");
            FormatPar_Struc_ToFormatParViewModel.ParValidAttributeValue         = _conHandler.ReadCommSetting("ParValidAttributeValue");
            FormatPar_Struc_ToFormatParViewModel.ParameterToSendElementName     = _conHandler.ReadCommSetting("ParameterToSendElementName");
            FormatPar_Struc_ToFormatParViewModel.ParameterToSendAttributeName   = _conHandler.ReadCommSetting("ParameterToSendAttributeName");
            FormatPar_Struc_ToFormatParViewModel.ParameterToSendAttributeValue  = _conHandler.ReadCommSetting("ParameterToSendAttributeValue");

            //create Settings file if it doesn't exists
            if (!File.Exists(FormatPar_Struc_ToFormatParViewModel.SettingsFileName))
            {
                XML_FileHandler.Create_XMLfileStruc(FormatPar_Struc_ToFormatParViewModel);
            }

            _percentage                 = 0;
            _percentageString           = _percentage.ToString() + "%";

            //read settings.xml file
            _list_FormatVariables       = XML_FileHandler.ReturnListOfXMLAttributes(FormatPar_Struc_ToFormatParViewModel.SettingsFileName, _path, FormatPar_Struc_ToFormatParViewModel.FormatElementName, FormatPar_Struc_ToFormatParViewModel.FormatParElementName, FormatPar_Struc_ToFormatParViewModel.FormatParAttributeName);
            _list_FormatValues          = XML_FileHandler.ReturnListOfXMLAttributes(FormatPar_Struc_ToFormatParViewModel.SettingsFileName, _path, FormatPar_Struc_ToFormatParViewModel.FormatElementName, FormatPar_Struc_ToFormatParViewModel.FormatParElementName, FormatPar_Struc_ToFormatParViewModel.FormatParAttributeValue);
            _list_PointerVariable       = XML_FileHandler.ReturnListOfXMLAttributes(FormatPar_Struc_ToFormatParViewModel.SettingsFileName, _path, FormatPar_Struc_ToFormatParViewModel.PointerStringElementName,    "", FormatPar_Struc_ToFormatParViewModel.PointerStringAttributeName);
            _list_TransferReq           = XML_FileHandler.ReturnListOfXMLAttributes(FormatPar_Struc_ToFormatParViewModel.SettingsFileName, _path, FormatPar_Struc_ToFormatParViewModel.TransferReqElementName,      "", FormatPar_Struc_ToFormatParViewModel.TransferReqAttributeName);
            _list_ParValid              = XML_FileHandler.ReturnListOfXMLAttributes(FormatPar_Struc_ToFormatParViewModel.SettingsFileName, _path, FormatPar_Struc_ToFormatParViewModel.ParValidElementName,         "", FormatPar_Struc_ToFormatParViewModel.ParValidAttributeName);
            _list_ParameterToSend       = XML_FileHandler.ReturnListOfXMLAttributes(FormatPar_Struc_ToFormatParViewModel.SettingsFileName, _path, FormatPar_Struc_ToFormatParViewModel.ParameterToSendElementName,  "", FormatPar_Struc_ToFormatParViewModel.ParameterToSendAttributeName);

            //add strings from the lists in the collections
            foreach (String listItem in _list_FormatVariables)
            {
                Parameters par = new Parameters { Name = listItem };
                FormatVariables_ObservableCollection.Add(par);
            }
            foreach (String listItem in _list_FormatValues)
            {
                Parameters par = new Parameters { Name = listItem };
                FormatValues_ObservableCollection.Add(par);
            }
            foreach (String listItem in _list_PointerVariable)
            {
                Parameters par = new Parameters { Name = listItem };
                Pointer_ObservableCollection.Add(par);
            }
            foreach (String listItem in _list_TransferReq)
            {
                Parameters par = new Parameters { Name = listItem };
                TransferReq_ObservableCollection.Add(par);
            }
            foreach (String listItem in _list_ParValid)
            {
                Parameters par = new Parameters { Name = listItem };
                ParValid_ObservableCollection.Add(par);
            }
            foreach (String listItem in _list_ParameterToSend)
            {
                Parameters par = new Parameters { Name = listItem };
                ParameterToSend_ObservableCollection.Add(par);
            }

            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {

            if (FormatVariables_ObservableCollection != null)
            {
                //Read textboxes variables and values and write save them to the Settings.xml file

                XML_Handler XML_FileHandler = new XML_Handler();

                String[] Pointer_StringArray            = new String[Pointer_ObservableCollection.Count];
                String[] TransferReq_StringArray        = new String[TransferReq_ObservableCollection.Count];
                String[] ParValid_StringArray           = new String[ParValid_ObservableCollection.Count];
                String[] ParameterToSend_StringArray    = new String[ParameterToSend_ObservableCollection.Count];

                String[] FormatVariables_StringArray    = new String[FormatVariables_ObservableCollection.Count];
                String[] FormatValues_StringArray       = new String[FormatValues_ObservableCollection.Count];

                int i = 0;
                foreach (Parameters par in FormatVariables_ObservableCollection)
                {
                    FormatVariables_StringArray[i] = par.Name;
                    i++;
                }

                i = 0;
                foreach (Parameters par in FormatValues_ObservableCollection)
                {
                    FormatValues_StringArray[i] = par.Name;
                    i++;
                }

                i = 0;
                foreach (Parameters par in Pointer_ObservableCollection)
                {
                    Pointer_StringArray[i] = par.Name;
                    i++;
                }

                i = 0;
                foreach (Parameters par in TransferReq_ObservableCollection)
                {
                    TransferReq_StringArray[i] = par.Name;
                    i++;
                }

                i = 0;
                foreach (Parameters par in ParValid_ObservableCollection)
                {
                    ParValid_StringArray[i] = par.Name;
                    i++;
                }

                i = 0;
                foreach (Parameters par in ParameterToSend_ObservableCollection)
                {
                    ParameterToSend_StringArray[i] = par.Name;
                    i++;
                }

                // Save Changes to the Settings.xml

                //XML_FileHandler.SaveParameters(FormatPar_Struc_ToFormatParViewModel.SettingsFileName, FormatPar_Struc_ToFormatParViewModel.path, FormatPar_Struc_ToFormatParViewModel.PointerStringElementName, "", FormatPar_Struc_ToFormatParViewModel.PointerStringAttributeName, Pointer_StringArray);
                //XML_FileHandler.SaveParameters(FormatPar_Struc_ToFormatParViewModel.SettingsFileName, FormatPar_Struc_ToFormatParViewModel.path, FormatPar_Struc_ToFormatParViewModel.TransferReqElementName, "", FormatPar_Struc_ToFormatParViewModel.TransferReqAttributeName, TransferReq_StringArray);
                //XML_FileHandler.SaveParameters(FormatPar_Struc_ToFormatParViewModel.SettingsFileName, FormatPar_Struc_ToFormatParViewModel.path, FormatPar_Struc_ToFormatParViewModel.ParValidElementName, "", FormatPar_Struc_ToFormatParViewModel.ParValidAttributeName, ParValid_StringArray);
                //XML_FileHandler.SaveParameters(FormatPar_Struc_ToFormatParViewModel.SettingsFileName, FormatPar_Struc_ToFormatParViewModel.path, FormatPar_Struc_ToFormatParViewModel.ParameterToSendElementName, "", FormatPar_Struc_ToFormatParViewModel.ParValidAttributeName, ParameterToSend_StringArray);
                XML_FileHandler.SaveParameters(FormatPar_Struc_ToFormatParViewModel.SettingsFileName, FormatPar_Struc_ToFormatParViewModel.path, FormatPar_Struc_ToFormatParViewModel.FormatElementName, FormatPar_Struc_ToFormatParViewModel.FormatParElementName, FormatPar_Struc_ToFormatParViewModel.FormatParAttributeName, FormatVariables_StringArray);
                XML_FileHandler.SaveParameters(FormatPar_Struc_ToFormatParViewModel.SettingsFileName, FormatPar_Struc_ToFormatParViewModel.path, FormatPar_Struc_ToFormatParViewModel.FormatElementName, FormatPar_Struc_ToFormatParViewModel.FormatParElementName, FormatPar_Struc_ToFormatParViewModel.FormatParAttributeValue, FormatValues_StringArray);

                //clear lists and collections
                _list_FormatVariables.Clear();
                _list_FormatValues.Clear();
                _list_PointerVariable.Clear();
                _list_TransferReq.Clear();
                _list_ParValid.Clear();
                _list_ParameterToSend.Clear();

                FormatVariables_ObservableCollection.Clear();
                FormatValues_ObservableCollection.Clear();
                Pointer_ObservableCollection.Clear();
                TransferReq_ObservableCollection.Clear();
                ParValid_ObservableCollection.Clear();
                ParameterToSend_ObservableCollection.Clear();

                //Background worker detach events
                _transferParameters_Worker.DoWork               -= DoWork_Transfer;
                _transferParameters_Worker.RunWorkerCompleted   -= WorkCompleted_Transfer;
                _transferParameters_Worker.ProgressChanged      -= ProgressChanged_Transfer;
            }

            _eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }
        #endregion

        //event on button transfer parameters, transfer  format parameters to the KUKA
        #region BackgroundWorker FormatParameters Transfer
        public void FormatParameterTransfer_Click()
        {
            try
            {
                // Nothing thats is related to the UI (WPF) thread should be given to the background worker thread without preparation
                // basically nothing that is part of the controls shouldn't be given to the background worker thread (otherwise Dispatcher is needed, and then UI thread is frozen)
                // Use a Dispatcher just for the fast communication between threads, because that way the UI(WPF) thread will not be frozen

                //Prepare String array out of UI objects, this is needed since the UI objects do not belong to the worker thread and cannot be accessed
                _formatValues_StringArray = new String[_formatVariables_ObservableCollection.Count];
                for (int i = 0; i < _formatValues_StringArray.Length; i++)
                {
                    _formatValues_StringArray[i] = _formatValues_ObservableCollection[i].Name.ToString();
                }
                //Prepare Structure out of UI objects, this is needed since the UI objects do not belong to the worker thread
                _transferFormatParameters_struc = new FormatParameters(_formatVariables_ObservableCollection.Count,
                                                                      _transferReq_ObservableCollection[0].Name.ToString(),
                                                                      _pointer_ObservableCollection[0].Name.ToString(),
                                                                      _parValid_ObservableCollection[0].Name.ToString(),
                                                                      _parameterToSend_ObservableCollection[0].Name.ToString(),
                                                                      _formatValues_StringArray);

                //Start the worker if it is not already running
                if (!_transferParameters_Worker.IsBusy)
                {
                    _transferParameters_Worker.RunWorkerAsync();
                    Status_FormatParameters_Tab = "Format parameters transfer started";
                }
            }
            catch (Exception exc)
            {
                Exception_FormatParameters_Tab = exc.ToString();
            }
        }
        private void ProgressChanged_Transfer(object sender, ProgressChangedEventArgs e)
        {
            //0-100
            Percentage = (e.ProgressPercentage * 100) / _formatValues_ObservableCollection.Count;
            PercentageString = _percentage.ToString() + "%";
        }
        private void WorkCompleted_Transfer(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_transferParameters_BackgroundworkerDisposed)
            {
                Status_FormatParameters_Tab = "FORMAT PARAMETERS ARE NOT TRANSFERED";
                return;
            }
            else
            {
                Status_FormatParameters_Tab = "FORMAT PARAMETERS TRANSFERED";
            }
        }
        private void DoWork_Transfer(object sender, DoWorkEventArgs e)
        {
            _transferParameters_Worker.ReportProgress(0);
            {
                String reqDataKUKA  = "";
                String ReqP         = "";
                String pointerInt   = "0";
                int pointer;
                _transferParameters_BackgroundworkerDisposed = false;
                // string array to send to kuka, it is longer because the array on the kuka is longer than the one on the PLC
                String[] parametersToSend = new String[_transferFormatParameters_struc.FormatParametersCount_int + 2]; //( +2 is because KUKA array is longer than the PLCs array for 2 parameters)
                try
                {
                    TCP_Write_Read TCP_WriteRead = new TCP_Write_Read(IP_Address_ToFormatParViewModel, Port_ToFormatParViewModel);

                    if (TCP_WriteRead.CheckConnection())
                    {
                        reqDataKUKA = TCP_WriteRead.returnVarValue(TCP_WriteRead.Read_VariableValueString(_transferFormatParameters_struc.TransferReq_String), '=', ' ');

                        if (reqDataKUKA == "TRUE")
                        {
                            while (!(Convert.ToInt32(pointerInt) >= _transferFormatParameters_struc.FormatParametersCount_int))
                            {
                                //read reqdata value
                                reqDataKUKA = TCP_WriteRead.returnVarValue(TCP_WriteRead.Read_VariableValueString(_transferFormatParameters_struc.TransferReq_String), '=', ' ');

                                if (reqDataKUKA == "TRUE")
                                {
                                    //read pointer value
                                    pointerInt = TCP_WriteRead.returnVarValue(TCP_WriteRead.Read_VariableValueString(_transferFormatParameters_struc.ParameterPointer_String), '=', ' ');
                                    //report value of the pointer to update the progress bar
                                    pointer = Convert.ToInt32(pointerInt);
                                    _transferParameters_Worker.ReportProgress(pointer);

                                    //prepare parameters to send to KUKA
                                    for (int i = 0; i < parametersToSend.Length; i++)
                                    {
                                        parametersToSend[i] = _transferFormatParameters_struc.FormatValues_StringArray[i];
                                        if (i == _transferFormatParameters_struc.FormatParametersCount_int - 1)
                                        {
                                            // send the rest of the values as zeros
                                            for (int k = _transferFormatParameters_struc.FormatParametersCount_int; k < parametersToSend.Length; k++)
                                            {
                                                parametersToSend[k] = "0";
                                            }
                                            break;
                                        }
                                    }

                                    //write the variable of the "Parameter to send" text with the value "ParametersToSend" at position "pointerInt"
                                    TCP_WriteRead.Write_VariableValueString(_transferFormatParameters_struc.ParameterToSend_String, parametersToSend[Convert.ToInt32(pointerInt)]);
                                    //write par valid to the kuka
                                    TCP_WriteRead.Write_VariableValueString(_transferFormatParameters_struc.ParameterValid_String, "TRUE");
                                    ReqP = TCP_WriteRead.returnVarValue(TCP_WriteRead.Read_VariableValueString(_transferFormatParameters_struc.TransferReq_String), '=', ' ');

                                    if (ReqP == "FALSE")
                                    {
                                        TCP_WriteRead.Write_VariableValueString(_transferFormatParameters_struc.ParameterValid_String, "FALSE");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Status_FormatParameters_Tab = "Format parameters are not requested, check if KUKA is in the loadparPal.src" + DateTime.Now;
                            _transferParameters_Worker.CancelAsync();
                            _transferParameters_Worker.Dispose();
                            _transferParameters_BackgroundworkerDisposed = true;
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Cannot connect to the Server", "TCP Client  - Formar Parameters");
                    }
                }
                catch(Exception exc)
                {
                    Exception_FormatParameters_Tab = exc.ToString();
                }
            }
        }
        #endregion

        // Methods to which is called when a IHandle interface has been triggered. When the IP address or port has been changed, through this interface this ViewModel will be updated
        #region Interface Handlers
        public void Handle(NetParameters_Messages message)
        {
            IP_Address_ToFormatParViewModel = message.IP_Address_CustomMessage;
            Port_ToFormatParViewModel = message.Port_CustomMessage;
        }

        public void Handle(FormatParameters_message message)
        {
            FormatPar_Struc_ToFormatParViewModel = message.FormatParamters_CustomMessage;
        }
        #endregion

        #region General methods

        #endregion

    }
}
