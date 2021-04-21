using Caliburn.Micro;
using WPF_CrossComm_Client.MVVM_Pattern.Models;
using WPF_CrossComm_Client.Structures;

namespace WPF_CrossComm_Client.MVVM_Pattern.ViewModels
{
    public interface IFormatParameterTransfer_ViewModel : IScreen
    {
        FormatParametersVariables FormatPar_Struc_ToFormatParViewModel { get; set; }

        ObservableCollectionWithItemNotify<Parameters> FormatValues_ObservableCollection { get; set; }
        ObservableCollectionWithItemNotify<Parameters> FormatVariables_ObservableCollection { get; set; }
        ObservableCollectionWithItemNotify<Parameters> ParameterToSend_ObservableCollection { get; set; }
        ObservableCollectionWithItemNotify<Parameters> ParValid_ObservableCollection { get; set; }
        ObservableCollectionWithItemNotify<Parameters> TransferReq_ObservableCollection { get; set; }
        ObservableCollectionWithItemNotify<Parameters> Pointer_ObservableCollection { get; set; }

        int Percentage { get; set; }
        string PercentageString { get; set; }
        string IP_Address_ToFormatParViewModel { get; set; }
        string Port_ToFormatParViewModel { get; set; }
        string TabName { get; set; }

        void FormatParameterTransfer_Click();
        void Handle(FormatParameters_message message);
        void Handle(NetParameters_Messages message);
    }
}