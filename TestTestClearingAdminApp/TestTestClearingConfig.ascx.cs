using System;
using System.Xml;
using uStore.Common.BLL;
using uStoreAPI.PluginBase.Clearing;

namespace TestTestClearingAdminApp
{
    public partial class TestTestClearingConfig : ClearingPluginConfigurationBase
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public override XmlDocument GetConfigurationData()
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml("<Config/>");

            XmlNode customHTML_PropertyDefault = document.DocumentElement.AppendChild(document.CreateElement("DefaultClearingResult"));
            customHTML_PropertyDefault.InnerText = DefaultClearingResult.Value;

            XmlNode customHTML_Markup = document.DocumentElement.AppendChild(document.CreateElement("Markup"));
            customHTML_Markup.InnerText = Markup.Value;
            return document;
        }

        public override void LoadSavedData(XmlDocument pSavedData)
        {
            //var connStr = CustomHtmlClearingCommonLogic.GetConnectionStringByDataSourceId(2.ToString());

            // CustomHTML_PropertyDefault
            XmlNode customHTML_PropertyDefault = pSavedData.SelectSingleNode("//DefaultClearingResult");
            if (customHTML_PropertyDefault != null)
            {
                DefaultClearingResult.Value = customHTML_PropertyDefault.InnerText;
            }

            // CustomHTML_Markup
            XmlNode customHTML_Markup = pSavedData.SelectSingleNode("//Markup");
            if (customHTML_Markup != null)
            {
                Markup.Value = customHTML_Markup.InnerText;
            }
        }

        public override ValidationResult ValidateControl()
        {
            return new ValidationResult(true);
        }
    }
}