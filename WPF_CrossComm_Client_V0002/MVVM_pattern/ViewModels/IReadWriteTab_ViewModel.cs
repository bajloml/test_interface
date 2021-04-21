using Caliburn.Micro;
using System.Windows;
using WPF_CrossComm_Client.MVVM_Pattern.Models;

namespace WPF_CrossComm_Client.MVVM_Pattern.ViewModels
{
    public interface IReadWriteTab_ViewModel: IScreen
    {
        ObservableCollectionWithItemNotify<Parameters> ReadVal_ObservableCollection { get; set; }
        ObservableCollectionWithItemNotify<Parameters> ReadVar_ObservableCollection { get; set; }
        ObservableCollectionWithItemNotify<Parameters> WriteVal_ObservableCollection { get; set; }
        ObservableCollectionWithItemNotify<Parameters> WriteValBOOL_ObservableCollection { get; set; }
        ObservableCollectionWithItemNotify<Parameters> WriteVar_ObservableCollection { get; set; }
        ObservableCollectionWithItemNotify<Parameters> WriteVarBOOL_ObservableCollection { get; set; }

        string IP_address_ReadWriteTab { get; set; }
        string Port_ReadWriteTab { get; set; }
        string TabName { get; set; }

        void Handle(NetParameters_Messages message);
        void ToggleVar_Click(object sender, RoutedEventArgs e);
        void WriteVar_Click();
        void ReadVar_Click();

    }
}