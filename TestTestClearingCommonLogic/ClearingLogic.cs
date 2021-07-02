using System.Xml;
using uStore.Common.BLL;
using uStore.Common.BLL.Clearing;

namespace TestTestClearingCommonLogic
{
    public class ClearingLogic : ClearingLogicBase
    {
        public ClearingLogic()
        {

        }

        public ClearingLogic(XmlDocument configXML)
        {
            ConfigXML = configXML;
        }

        public override ValidationResult DoClearing(Currency currency, decimal amount, XmlDocument userData, out XmlDocument resultData)
        {
            ValidationResult result = new ValidationResult(true);

            resultData = new XmlDocument();
            resultData.LoadXml("<ResultData/>");

            var clearingResult = userData.SelectSingleNode("//ClearingResult");
            if (clearingResult != null)
            {
                var elm = resultData.DocumentElement.AppendChild(resultData.CreateElement("ClearingResult"));
                try
                {
                    elm.InnerXml = clearingResult.InnerText;
                }
                catch
                {
                    elm.InnerText = clearingResult.InnerText;
                }
            }

            return result;
        }
    }
}
