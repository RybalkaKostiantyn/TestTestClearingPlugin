using System.Xml;
using uStore.Common.BLL;
using uStoreAPI.PluginBase.Clearing;

namespace TestTestClearingAdminApp
{
    public partial class TestTestClearingDataEntryConfig : ClearingPluginConfigurationBase
    {
        public override XmlDocument GetConfigurationData()
        {
            return new XmlDocument();
        }

        public override void LoadSavedData(XmlDocument pSavedData)
        {
        }

        public override ValidationResult ValidateControl()
        {
            return new ValidationResult(true);
        }
    }
}