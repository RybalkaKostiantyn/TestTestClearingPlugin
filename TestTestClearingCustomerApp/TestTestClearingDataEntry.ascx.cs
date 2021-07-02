using System;
using System.Text;
using System.Xml;
using uStore.Common.BLL;
using uStore.Common.BLL.Clearing;

namespace TestTestClearingCustomerApp
{
    public partial class TestTestClearingDataEntry : ClearingUserDataBase
    {
        public string DefaultClearingResult
        {
            get
            {
                XmlNode xmlNode = ClearingConfigXml.SelectSingleNode("//DefaultClearingResult");
                return xmlNode.InnerText;
            }
        }

        public string Markup
        {
            get
            {
                XmlNode xmlNode = ClearingConfigXml.SelectSingleNode("//Markup");
                return xmlNode.InnerText;
            }
        }

        public override XmlDocument UserData
        {
            get
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml("<UserData/>");
                if (String.IsNullOrWhiteSpace(hfDefaultClearingResult.Value) == false)
                {
                    byte[] data = Convert.FromBase64String(hfDefaultClearingResult.Value);
                    document.DocumentElement.AppendChild(document.CreateElement("ClearingResult")).InnerText = Encoding.UTF8.GetString(data);
                }
                return document;
            }
        }

        public override ValidationResult Validate()
        {
            return new ValidationResult(true);
        }
    }
}