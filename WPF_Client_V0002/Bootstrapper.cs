using System.Windows;
using Caliburn.Micro;
using WPF_CrossComm_Client.MVVM_Pattern.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WPF_CrossComm_Client
{
    class Bootstrapper:BootstrapperBase
    {
        //constructor -> initialize the bootstrapper
        public Bootstrapper()
        {
            Initialize();
        }

        //IoC container
        private SimpleContainer _container = new SimpleContainer();

        // Caliburn.Micro uses naming conventions so be carefull how you are going to name your Views, Models and ViewModels.
        // Coresponding Views and Models should start with the name and end with View or ViewModel
        protected override object GetInstance(Type service, string key)
        {
            var instance = _container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void Configure()
        {
            _container.Instance(new WindowManager());
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.PerRequest<MainViewModel, MainViewModel>();
            _container.PerRequest<IFormatParameterTransfer_ViewModel, FormatParameterTransfer_ViewModel>();
            _container.PerRequest<IReadWriteTab_ViewModel, ReadWriteTab_ViewModel>();

            base.Configure();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            // Start the MainViewModel from the Bootstrapper
            DisplayRootViewFor<MainViewModel>();
        }
    }
}
