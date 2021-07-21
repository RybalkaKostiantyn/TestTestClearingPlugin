using System;
using System.Xml;
using TestTestClearingCommonLogic;
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

            XmlNode customHTML_DbBound = document.DocumentElement.AppendChild(document.CreateElement("DbBound"));
            XmlAttribute allowDbBound = customHTML_DbBound.Attributes.Append(document.CreateAttribute("AllowDbBound"));
            allowDbBound.Value = CheckBoxDbBound.Checked.ToString();

            XmlNode customHTML_DbConnection = customHTML_DbBound.AppendChild(document.CreateElement("DbConnection"));
            customHTML_DbConnection.InnerText = hdDatasourceId.Value;

            XmlNode customHTML_DbQuery = customHTML_DbBound.AppendChild(document.CreateElement("DbQuery"));
            customHTML_DbQuery.InnerText = TextBoxSqlQuery.Text.Trim();
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

            // customHTML_DbBound
            XmlNode customHTML_DbBound = pSavedData.SelectSingleNode("//DbBound");
            if (customHTML_DbBound != null)
            {
                bool allow = false;
                if (bool.TryParse(customHTML_DbBound.Attributes["AllowDbBound"].Value, out allow))
                {
                    CheckBoxDbBound.Checked = allow;
                }

                // customHTML_DbConnection
                DropDownListDbConnection.DataSource = CustomHtmlClearingCommonLogic.GetDataSourceList();
                DropDownListDbConnection.DataBind();
                XmlNode customHTML_DbConnection = pSavedData.SelectSingleNode("//DbConnection");
                if (customHTML_DbConnection != null && !string.IsNullOrWhiteSpace(customHTML_DbConnection.InnerText))
                {
                    DropDownListDbConnection.SelectedValue = customHTML_DbConnection.InnerText;
                }

                // customHTML_DbQuery
                XmlNode customHTML_DbQuery = pSavedData.SelectSingleNode("//DbQuery");
                if (customHTML_DbQuery != null)
                {
                    TextBoxSqlQuery.Text = customHTML_DbQuery.InnerText;
                }

                // Show panel
                PanelSqlSettings.Visible = allow;

                LabelSqlQueryResult.Text = string.Empty;
            }
        }

        public override ValidationResult ValidateControl()
        {
            return new ValidationResult(true);
        }
    }
}