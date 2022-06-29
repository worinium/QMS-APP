using Prism.Regions;
using Prism.Modularity;

namespace QMS.GUI
{
    public class RegisterRegion : IModule
    {
        private IRegionManager _regionManager;
        public RegisterRegion(IRegionManager regionManager)
        {
            this._regionManager = regionManager;
        }
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("HeaderSection", typeof(Views.HeaderSection));
            _regionManager.RegisterViewWithRegion("QueueDetailsSection", typeof(Views.QueueDetailsSection));
            _regionManager.RegisterViewWithRegion("StreamSection", typeof(Views.StreamSection));
            _regionManager.RegisterViewWithRegion("ServiceList", typeof(Views.ServiceList));
            _regionManager.RegisterViewWithRegion("CurrentTicketSection", typeof(Views.CurrentTicketSection));
        }
    }
}
