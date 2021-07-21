using Newtonsoft.Json;
using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;
using System.Xml;
using TestTestClearingCommonLogic;
using uStore.Common.BLL.Clearing;

namespace TestTestClearingCustomerApp
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CustomHtmlClearingPluginWsApi
    {
        readonly string sqlEmail = "SampleEmail@gmail.com";
        readonly string sqlID = "0";
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }

        [WebInvoke(Method = @"POST", UriTemplate = @"fetchData/{modelIdStr}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public string FetchData(string customParameter, string modelIdStr)
        {
            try
            {
                var jsonResult = string.Empty;
                int modelId;
                if (int.TryParse(modelIdStr, out modelId))
                {
                    ClearingConfig conf = ClearingConfig.Get(modelId);

                    var xmlClearingConfPlugin = conf.ClearingConfPlugin.ConfigurationString;

                    XmlDocument docClearingConfPlugin = new XmlDocument();
                    docClearingConfPlugin.LoadXml(xmlClearingConfPlugin);

                    XmlNode connectionStingId = docClearingConfPlugin.SelectSingleNode("//DbConnection");
                    if (connectionStingId != null && !String.IsNullOrWhiteSpace(connectionStingId.InnerText))
                    {
                        XmlNode dbQuery = docClearingConfPlugin.SelectSingleNode("//DbQuery");
                        if (dbQuery != null && !String.IsNullOrWhiteSpace(dbQuery.InnerText))
                        {
                            SqlParameter parameters = new SqlParameter()
                            {
                                EmailParameter = new Email() { Value = sqlEmail },// ((uStore.Common.BLL.CustomerInfo)(HttpContext.Current.Session["CustomerInfo"])).EmailAddress.ToString() },
                                UserIdParameter = new UserId() { Value = sqlID },//((uStore.Common.BLL.CustomerInfo)(HttpContext.Current.Session["CustomerInfo"])).UserID.ToString() },
                                UserExternalIdParameter = new UserExternalId() { Value = sqlID },//((uStore.Common.BLL.CustomerInfo)(HttpContext.Current.Session["CustomerInfo"])).UserExternalId.ToString() },
                                StoreIdParameter = new StoreId() { Value = sqlID },//HttpContext.Current.Session["StoreId"].ToString() },
                                OrderIdParameter = new OrderId() { Value = sqlID },//HttpContext.Current.Session["OrderId"].ToString() },
                                CurrencyIdParameter = new CurrencyId() { Value = sqlID },//HttpContext.Current.Session["CurrencyID"].ToString() },
                                CustomParameter = new CustomParameter() { Value = customParameter }
                            };

                            SqlResult fetchedData = CustomHtmlClearingCommonLogic.FetchData(connectionStingId.InnerText, dbQuery.InnerText, parameters);

                            // Save using the JSON Serializer
                            jsonResult = JsonConvert.SerializeObject(fetchedData, Newtonsoft.Json.Formatting.Indented);
                        }
                    }
                }
                return jsonResult;
            }
            catch (System.Exception ex)
            {
                return ex.Message + " Exception stack: " + ex.StackTrace;
            }
        }
    }
}
