<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TestTestClearingConfig.ascx.cs" Inherits="TestTestClearingAdminApp.TestTestClearingConfig" %>

<div>
    <asp:Label ID="LabelSqlQueryResult" runat="server" Text=""></asp:Label>
</div>
<table>
    <tr>
        <td>Default Clearing Result:
        </td>
        <td>
            <textarea runat="server" style="height: 50px; width: 530px;" class="formfield" id="DefaultClearingResult"></textarea><br />
        </td>
    </tr>
    <tr>
        <td>Markup:
        </td>
        <td>
            <textarea runat="server" style="height: 370px; width: 530px;" class="formfield" id="Markup"></textarea>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="LabelDbBound" runat="server" Text="Database bound:" AssociatedControlID="CheckBoxDbBound"></asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="CheckBoxDbBound" runat="server" OnCheckedChanged="CheckBoxDbBound_CheckedChanged" AutoPostBack="true" />
        </td>
    </tr>
   <asp:Panel ID="PanelSqlSettings" runat="server" Visible="false">
        <tr>
            <td>
                <asp:Label ID="LabelDbConnection" runat="server" Text="Database connection:" AssociatedControlID="DropDownListDbConnection"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownListDbConnection" runat="server" DataTextField="Value" DataValueField="Key"></asp:DropDownList>
                <asp:HiddenField ID="hdDatasourceId" runat="server" />
                <script>
                    var datasourceDdlId = "#<%: DropDownListDbConnection.ClientID %>",
                        datasourceHfId = "#<%: hdDatasourceId.ClientID%>";
                    $(function () {
                        var comboboxElement = $(datasourceDdlId),
                            hiddenFieldElpement = $(datasourceHfId);
                        hiddenFieldElpement.val(comboboxElement.val());
                        comboboxElement.change(function () {
                            hiddenFieldElpement.val($(this).val());
                        });
                    });
                </script>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelSqlQuery" runat="server" Text="SQL query:" AssociatedControlID="TextBoxSqlQuery"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBoxSqlQuery" runat="server" Style="height: 370px; width: 530px;" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
       <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="ButtonCheckSqlQuery" runat="server" Text="Check Sql query" OnClick="ButtonCheckSqlQuery_Click"></asp:Button>
            </td>
        </tr>
    </asp:Panel>
</table>