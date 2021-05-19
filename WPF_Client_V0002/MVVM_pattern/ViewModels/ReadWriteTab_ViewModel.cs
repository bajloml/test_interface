using Caliburn.Micro;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WPF_CrossComm_Client.MVVM_Pattern.Models;


namespace WPF_CrossComm_Client.MVVM_Pattern.ViewModels
{
    public class ReadWriteTab_ViewModel : Screen, IScreen, INotifyPropertyChanged, IHandle<NetParameters_Messages>, IReadWriteTab_ViewModel
    {
        //Constructor
        public ReadWriteTab_ViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            DisplayName = "Read Write";
        }

        //locals
        #region locals
        private readonly IEventAggregator _eventAggregator;

        //background workers 
        private BackgroundWorker _toggleButtonWorker    = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
        private BackgroundWorker _writeVarWorker        = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
        private BackgroundWorker _readVarWorker         = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

        private int _tag;

        private String _tabName = "Read Write";
        private String _IP_address_ReadWriteTab;
        private String _Port_ReadWriteTab;
        private String _Status = "";
        private String _Exception = "";

        //Observable collections of type Parameters
        //type parameters implements interface "INotifyPropertyChanged" which is important to update the collection item when it has been changed
        private ObservableCollectionWithItemNotify<Parameters> _writeVarBOOL_ObservableCollection = new ObservableCollectionWithItemNotify<Parameters>();
        private ObservableCollectionWithItemNotify<Parameters> _writeValBOOL_ObservableCollection = new ObservableCollectionWithItemNotify<Parameters>();
        private ObservableCollectionWithItemNotify<Parameters> _readVar_ObservableCollection = new ObservableCollectionWithItemNotify<Parameters>();
        private ObservableCollectionWithItemNotify<Parameters> _readVal_ObservableCollection = new ObservableCollectionWithItemNotify<Parameters>();
        private ObservableCollectionWithItemNotify<Parameters> _writeVar_ObservableCollection = new ObservableCollectionWithItemNotify<Parameters>();
        private ObservableCollectionWithItemNotify<Parameters> _writeVal_ObservableCollection = new ObservableCollectionWithItemNotify<Parameters>();

        #endregion

        //Accesorrs
        #region Accessors

        public String IP_address_ReadWriteTab
        {
            get
            {
                return _IP_address_ReadWriteTab;
            }
            set
            {
                _IP_address_ReadWriteTab = value;
                NotifyOfPropertyChange(() => IP_address_ReadWriteTab);
            }
        }
        public String Port_ReadWriteTab
        {
            get
            {
                return _Port_ReadWriteTab;
            }
            set
            {
                _Port_ReadWriteTab = value;
                NotifyOfPropertyChange(() => _Port_ReadWriteTab);
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
        public String Status_ReadWrite_ViewModel
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                NotifyOfPropertyChange(() => Status_ReadWrite_ViewModel);
                //notify other ViewModels which are subscribed
                _eventAggregator.PublishOnUIThread(new StatusExceptionText() { StatusString = Status_ReadWrite_ViewModel });
            }
        }
        public String Exception_ReadWrite_ViewModel
        {
            get
            {
                return _Exception;
            }
            set
            {
                _Exception = value;
                NotifyOfPropertyChange(() => Exception_ReadWrite_ViewModel);
                //notify other ViewModels which are subscribed
                _eventAggregator.PublishOnUIThread(new StatusExceptionText() { ExceptionString = Exception_ReadWrite_ViewModel });
            }
        }

        public ObservableCollectionWithItemNotify<Parameters> WriteVarBOOL_ObservableCollection
        {
            get
            {
                return _writeVarBOOL_ObservableCollection;
            }
            set
            {
                _writeVarBOOL_ObservableCollection = value;
                NotifyOfPropertyChange(() => WriteVarBOOL_ObservableCollection);
            }
        }
        public ObservableCollectionWithItemNotify<Parameters> WriteValBOOL_ObservableCollection
        {
            get
            {
                return _writeValBOOL_ObservableCollection;
            }
            set
            {
                _writeValBOOL_ObservableCollection = value;
                NotifyOfPropertyChange(() => WriteValBOOL_ObservableCollection);
            }
        }
        public ObservableCollectionWithItemNotify<Parameters> ReadVar_ObservableCollection
        {
            get
            {
                return _readVar_ObservableCollection;
            }
            set
            {
                _readVar_ObservableCollection = value;
                NotifyOfPropertyChange(() => ReadVar_ObservableCollection);
            }
        }
        public ObservableCollectionWithItemNotify<Parameters> ReadVal_ObservableCollection
        {
            get
            {
                return _readVal_ObservableCollection;
            }
            set
            {
                _readVal_ObservableCollection = value;
                NotifyOfPropertyChange(() => _readVal_ObservableCollection);
            }
        }
        public ObservableCollectionWithItemNotify<Parameters> WriteVar_ObservableCollection
        {
            get
            {
                return _writeVar_ObservableCollection;
            }
            set
            {
                _writeVar_ObservableCollection = value;
                NotifyOfPropertyChange(() => WriteVar_ObservableCollection);
            }
        }
        public ObservableCollectionWithItemNotify<Parameters> WriteVal_ObservableCollection
        {
            get
            {
                return _writeVal_ObservableCollection;
            }
            set
            {
                _writeVal_ObservableCollection = value;
                NotifyOfPropertyChange(() => WriteVal_ObservableCollection);
            }
        }

        #endregion

        //Overides
        #region Overides
        protected override void OnActivate()
        {
            //subscribe to the event messages
            _eventAggregator.Subscribe(this);

            // Read parent values on activation, after it will be handled by messaging
            MainViewModel ParentViewModel           = (MainViewModel)this.Parent;
            IP_address_ReadWriteTab                 = ParentViewModel.IP_Address;
            Port_ReadWriteTab                       = ParentViewModel.Port;

            //BackgroundWorers Events attach
            _toggleButtonWorker.DoWork              += DoWork_Toggle;
            _toggleButtonWorker.RunWorkerCompleted  += WorkCompleted_Toggle;
            _toggleButtonWorker.ProgressChanged     += ProgressChanged_Toggle;

            _writeVarWorker.DoWork                  += DoWork_WriteVar;
            _writeVarWorker.RunWorkerCompleted      += WorkCompleted_WriteVar;
            _writeVarWorker.ProgressChanged         += ProgressChanged_WriteVar;

            _readVarWorker.DoWork                   += DoWork_ReadVar;
            _readVarWorker.RunWorkerCompleted       += WorkCompleted_ReadVar;
            _readVarWorker.ProgressChanged          += ProgressChanged_ReadVar;

            for (int i = 0; i < 5; i++)
            {
                WriteVarBOOL_ObservableCollection.Add   (new Parameters() { Name = i.ToString(), Value = "" });
                WriteValBOOL_ObservableCollection.Add   (new Parameters() { Name = i.ToString(), Value = "" });
                ReadVar_ObservableCollection.Add        (new Parameters() { Name = i.ToString(), Value = "" });
                ReadVal_ObservableCollection.Add        (new Parameters() { Name = i.ToString(), Value = "" });
                WriteVar_ObservableCollection.Add       (new Parameters() { Name = i.ToString(), Value = "" });
                WriteVal_ObservableCollection.Add       (new Parameters() { Name = i.ToString(), Value = "" });
            }

            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            WriteVarBOOL_ObservableCollection.Clear();
            WriteValBOOL_ObservableCollection.Clear();
            ReadVar_ObservableCollection.Clear();
            ReadVal_ObservableCollection.Clear();
            WriteVar_ObservableCollection.Clear();
            WriteVal_ObservableCollection.Clear();

            //BackgroundWorers Events detach
            _toggleButtonWorker.DoWork              -= DoWork_Toggle;
            _toggleButtonWorker.RunWorkerCompleted  -= WorkCompleted_Toggle;
            _toggleButtonWorker.ProgressChanged     -= ProgressChanged_Toggle;

            _writeVarWorker.DoWork                  -= DoWork_WriteVar;
            _writeVarWorker.RunWorkerCompleted      -= WorkCompleted_WriteVar;
            _writeVarWorker.ProgressChanged         -= ProgressChanged_WriteVar;

            _readVarWorker.DoWork                   -= DoWork_ReadVar;
            _readVarWorker.RunWorkerCompleted       -= WorkCompleted_ReadVar;
            _readVarWorker.ProgressChanged          -= ProgressChanged_ReadVar;

            //Unsubscribe to the event messages
            _eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }

        #endregion

        // Methods to which is called when a IHandle interface has been triggered, when the IP address or port has been changed, through this interface this ViewModel will be updated
        #region Interface Handlers
        public void Handle(NetParameters_Messages message)
        {
            IP_address_ReadWriteTab = message.IP_Address_CustomMessage;
            Port_ReadWriteTab       = message.Port_CustomMessage;
        }

        #endregion

        #region Backgroundworkers

        //Toggle Variables
        #region ToggleButton
        public void ToggleVar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //take the tag of the toggle button, to know which variable to write to
                var _objectSender = (Button)sender;
                _tag = Convert.ToInt32(_objectSender.Tag);

                // Nothing thats is related to the UI (WPF) thread should be given to the background worker thread without preparation
                // basically nothing that is part of the controls shouldn't be given to the background worker thread (otherwise Dispatcher is needed, and then UI thread is frozen)
                //Use a Dispatcher just for the fast communication between threads, because that way the UI(WPF) thread will not be froze

                //Start the worker if it is not already running
                if (!_toggleButtonWorker.IsBusy)
                {
                    _toggleButtonWorker.RunWorkerAsync();
                }
            }
            catch (Exception exc)
            {
                Exception_ReadWrite_ViewModel =  exc.ToString();
            }
        }
        private void ProgressChanged_Toggle(object sender, ProgressChangedEventArgs e)
        {
        }
        private void WorkCompleted_Toggle(object sender, RunWorkerCompletedEventArgs e)
        {
            Status_ReadWrite_ViewModel = "Toggled: " + WriteVarBOOL_ObservableCollection[_tag].Value.ToString();
        }
        private void DoWork_Toggle(object sender, DoWorkEventArgs e)
        {
            try
            {
                TCP_Write_Read TCP_WriteRead = new TCP_Write_Read(IP_address_ReadWriteTab, Port_ReadWriteTab);

                // if the button is checked, write true
                if (WriteVarBOOL_ObservableCollection[_tag].Value.ToString() != "")
                {
                    if (TCP_WriteRead.CheckConnection())
                    {
                        TCP_WriteRead.Write_VariableValueString(WriteVarBOOL_ObservableCollection[_tag].Value.ToString(), "TRUE");
                        Status_ReadWrite_ViewModel = "Writing: " + WriteVarBOOL_ObservableCollection[_tag].Value.ToString() + " : " + "TRUE";
                        TCP_WriteRead.Write_VariableValueString(WriteVarBOOL_ObservableCollection[_tag].Value.ToString(), "FALSE");
                        Status_ReadWrite_ViewModel = "Writing: " + WriteVarBOOL_ObservableCollection[_tag].Value.ToString() + " : " + "FALSE";
                    }
                    else
                    {
                        throw new ArgumentException("Cannot connect to the Server", "TCP Client  - ToggleButton");
                    }
                }
            }
            catch (Exception exc)
            {
                Exception_ReadWrite_ViewModel = exc.ToString();
            }
        }
        #endregion
        //Write Variables
        #region WriteVar
        public void WriteVar_Click()
        {
            try
            {
                // Nothing thats is related to the UI (WPF) thread should be given to the background worker thread without preparation
                // basically nothing that is part of the controls shouldn't be given to the background worker thread (otherwise Dispatcher is needed, and then UI thread is frozen)
                //Use a Dispatcher just for the fast communication between threads, because that way the UI(WPF) thread will not be frozen

                //Start the worker if it is not already running
                if (!_writeVarWorker.IsBusy)
                {
                    _writeVarWorker.RunWorkerAsync();
                }
            }
            catch (Exception exc)
            {
                Exception_ReadWrite_ViewModel = exc.ToString();
            }
        }
        private void ProgressChanged_WriteVar(object sender, ProgressChangedEventArgs e)
        {
            int i = e.ProgressPercentage;
            Status_ReadWrite_ViewModel = "Writing: " + WriteVar_ObservableCollection[i].Value.ToString() + " : " + WriteVal_ObservableCollection[i].Value.ToString();
        }
        private void WorkCompleted_WriteVar(object sender, RunWorkerCompletedEventArgs e)
        {
        }
        private void DoWork_WriteVar(object sender, DoWorkEventArgs e)
        {
            try
            {
                TCP_Write_Read TCP_WriteRead = new TCP_Write_Read(IP_address_ReadWriteTab, Port_ReadWriteTab);

                if (TCP_WriteRead.CheckConnection())
                {
                    for (int i = 0; i < WriteVar_ObservableCollection.Count; i++)
                    {
                        if ((WriteVar_ObservableCollection[i].Value.ToString() != "") || (WriteVal_ObservableCollection[i].Value.ToString() != ""))
                        {
                            TCP_WriteRead.Write_VariableValueString(WriteVar_ObservableCollection[i].Value.ToString(), WriteVal_ObservableCollection[i].Value.ToString());
                            _writeVarWorker.ReportProgress(i);
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Cannot connect to the Server", "TCP Client - WriteVarButton");
                }
            }
            catch (Exception exc)
            {
                Exception_ReadWrite_ViewModel = exc.ToString();
            }
        }
        #endregion
        //Read Variables
        #region ReadVar
        public void ReadVar_Click()
        {
            try
            {
                // Nothing thats is related to the UI (WPF) thread should be given to the background worker thread without preparation
                // basically nothing that is part of the controls shouldn't be given to the background worker thread (otherwise Dispatcher is needed, and then UI thread is frozen)
                //Use a Dispatcher just for the fast communication between threads, because that way the UI(WPF) thread will not be froze

                //Start the worker if it is not already running
                if (!_readVarWorker.IsBusy)
                {
                    _readVarWorker.RunWorkerAsync();
                }
            }
            catch (Exception exc)
            {
                Exception_ReadWrite_ViewModel = exc.ToString();
            }
        }
        private void ProgressChanged_ReadVar(object sender, ProgressChangedEventArgs e)
        {
            int i = e.ProgressPercentage;
            Status_ReadWrite_ViewModel = "Reading:  " + ReadVar_ObservableCollection[i].Value.ToString() + " Value: " + ReadVal_ObservableCollection[i].Value.ToString();
        }
        private void WorkCompleted_ReadVar(object sender, RunWorkerCompletedEventArgs e)
        {
        }
        private void DoWork_ReadVar(object sender, DoWorkEventArgs e)
        {
            try
            {
                TCP_Write_Read TCP_WriteRead = new TCP_Write_Read(IP_address_ReadWriteTab, Port_ReadWriteTab);

                if (TCP_WriteRead.CheckConnection())
                {
                    for (int i = 0; i < ReadVar_ObservableCollection.Count; i++)
                    {
                        if (ReadVar_ObservableCollection[i].Value.ToString() != "")
                        {
                            ReadVal_ObservableCollection[i].Value = TCP_WriteRead.Read_VariableValueString(ReadVar_ObservableCollection[i].Value.ToString());
                            _readVarWorker.ReportProgress(i);
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Cannot connect to the Server", "TCP Client - ReadVarButton");
                }
            }

            catch (Exception exc)
            {
                Exception_ReadWrite_ViewModel = exc.ToString();
            }
        }
        #endregion

        #endregion

        //Methods
        #region General Methods

        #endregion
    }
}
