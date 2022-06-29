using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Unity;
using Microsoft.Practices.Unity;
using System.Windows;
using QMS.GUI;

namespace QMS.GUI
{
    public class Bootstrapper : UnityBootstrapper
    {
        public override void Run(bool runWithDefaultConfiguration)
        {
            base.Run(runWithDefaultConfiguration);
        }  
        //Create the Shell
        // Show the Shell
        protected override System.Windows.DependencyObject CreateShell()
        {
            return Container.Resolve<TabletScreen>();
        }
        protected override void InitializeShell()
        {
            //Set Main Window for Prism
            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            Type sampleType = typeof(QMS.GUI.RegisterRegion);
            ModuleCatalog.AddModule(new Prism.Modularity.ModuleInfo { ModuleName = sampleType.Name, ModuleType = sampleType.AssemblyQualifiedName });
        }
    }
}
