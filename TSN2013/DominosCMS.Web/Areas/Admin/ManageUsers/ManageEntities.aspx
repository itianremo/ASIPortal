<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageEntities.aspx.cs" Inherits="DominosCMS.Web.Areas.Admin.ManageUsers.ManageEntities" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script language="javascript">

        function AdjustOne(invoker) {
            var IsChecked = true;
            if (invoker.value == '0' || invoker.value == '')
                IsChecked = false;
            //invoker.parentElement.parentElement.style.backgroundColor='#BCC9B7'; 
            var gvInputs = invoker.parentElement.parentElement.getElementsByTagName("gvUsers");
            alert(gvInputs);
            for (i = 0; i < gvInputs.length; i++) {
                if (gvInputs[i].type == "checkbox") {
                    if (IsChecked) {
                        //gvInputs[i].checked = true
                        gvInputs[i].setAttribute("disabled", "true");
                        //invoker.parentElement.parentElement.style.background = '#BCC9B7'
                    }
                    else {
                        //gvInputs[i].checked = false
                        gvInputs[i].setAttribute("disabled", "true");
                        //invoker.parentElement.parentElement.style.background = '#ffffff'
                    }
                }
            }
        }

        function SelectOne(invoker) {
            var IsChecked = invoker.checked;
            if (IsChecked) {
                //invoker.parentElement.parentElement.style.backgroundColor='#BCC9B7'; 
                var gvInputs = invoker.parentElement.parentElement.getElementsByTagName("input");
                for (i = 0; i < gvInputs.length; i++) {
                    if (gvInputs[i].type == "text") {
                        if (gvInputs[i].value == 0)
                            gvInputs[i].value = 1
                    }
                }
            }
            else {
                //invoker.parentElement.parentElement.style.backgroundColor='#ffffff'; 
                var gvInputs = invoker.parentElement.parentElement.getElementsByTagName("input");
                for (i = 0; i < gvInputs.length; i++) {
                    if (gvInputs[i].type == "text") {
                        gvInputs[i].value = 0
                    }
                }
            }
        }


    </script>


    <title></title>
    </head>
<body>



    <form id="form1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td style="text-align: right">Entity:</td>
                <td>
                    <asp:DropDownList ID="ddlEntities" runat="server" DataSourceID="sdsAllEntities" DataTextField="EntityName" DataValueField="EntityID" AutoPostBack="True" OnDataBound="ddlEntities_DataBound" OnSelectedIndexChanged="ddlEntities_SelectedIndexChanged" Width="300px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsAllEntities" runat="server" ConnectionString="<%$ ConnectionStrings:TSNOFFICEDOTNETConnectionString %>" SelectCommand="SELECT EntityID, EntityName FROM Sec_Entities"></asp:SqlDataSource>

                            </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">Rules:</td>
                <td>
                                <asp:ListBox ID="lboxRules" runat="server" AutoPostBack="True" OnDataBound="lboxRules_DataBound" OnSelectedIndexChanged="lboxRules_SelectedIndexChanged" Width="300px"></asp:ListBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">Job:</td>
                <td>
                                <asp:ListBox ID="lboxJobs" runat="server" AutoPostBack="True" OnDataBound="lboxJobs_DataBound" OnSelectedIndexChanged="lboxJobs_SelectedIndexChanged" Width="300px"></asp:ListBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">Users:</td>
                <td style="vertical-align: top">

                                <asp:ListBox ID="lboxUsers" runat="server" Width="300px"></asp:ListBox>

                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:SqlDataSource ID="sdsGeneric" runat="server" ConnectionString="<%$ ConnectionStrings:TSNOFFICEDOTNETConnectionString %>" SelectCommand="SELECT * FROM [Sec_A_1]"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:HyperLink ID="lnkBack" runat="server" Font-Bold="True" NavigateUrl="~/Areas/Admin/ManageUsers/ManageUsers.aspx" Target="_self">Back</asp:HyperLink>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>



</body>
</html>
