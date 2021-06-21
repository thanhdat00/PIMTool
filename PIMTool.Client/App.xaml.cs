using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Ninject;
using PIMTool.Client.DependencyInjection;
using PIMTool.Client.Presentation;
using PIMTool.Client.Presentation.ViewModels;
using PIMTool.Common;
using PIMTool.Common.BusinessObjects;

namespace PIMTool.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Initialize DI / IoC
            IoC.Initialize(
                new StandardKernel(new NinjectSettings { LoadExtensions = true }),
                new ClientBindingModule());

            // Load config for log4net
            log4net.Config.XmlConfigurator.Configure();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var window = new MainWindow();
            window.DataContext = IoC.Get<MainViewModel>();
            window.Show();
        }
    }
}
