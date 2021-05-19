using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace WPF_CrossComm_Client.MVVM_Pattern.Models
{
    //parameters object
    public class Parameters : INotifyPropertyChanged
    {
        string _parameterName;
        string _parameterValue;

        //Accessor
        public string Name
        {
            get
            {
               return _parameterName;
            }
            set
            {
                // set value
                _parameterName = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Name");
            }
        }
        public string Value
        {
            get
            {
                return _parameterValue;
            }
            set
            {
                // set value
                _parameterValue = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Value");
            }
        }
        //add event of PropertyChangedEventHandler(delegate) "PropertyChanged" on this object's property "Name"
        // this event is handled by the "OnPropertyChanged" method
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }


    /// <summary>
    ///   Observable collection  with item notify extends the observable collection
    ///   It can be used only as the observable collection of object which implements interface "INotifyPropertyChanged"
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableCollectionWithItemNotify<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        //constructor
        public ObservableCollectionWithItemNotify()
        {
            this.CollectionChanged += items_CollectionChanged;
        }

        //constructor
        public ObservableCollectionWithItemNotify(IEnumerable<T> collection) : base(collection)
        {
            //attach "CollectionChanged" event
            //note that this event is raised only when you add or delete a member. 
            //That's why it is important to attach "PropertyChanged" event when the collection member is added or deleted
            this.CollectionChanged += items_CollectionChanged;
            foreach (INotifyPropertyChanged item in collection)
                item.PropertyChanged += item_PropertyChanged;
        }

        //method on collection change event
        private void items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e != null)
            {
                //detach Collection changed event
                if (e.OldItems != null)
                    foreach (INotifyPropertyChanged item in e.OldItems)
                        item.PropertyChanged -= item_PropertyChanged;

                //attach Collection changed events
                if (e.NewItems != null)
                    foreach (INotifyPropertyChanged item in e.NewItems)
                        item.PropertyChanged += item_PropertyChanged;
            }
        }

        //method on property change event
        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // here add code to handle the property changed event
        }

    }
}
