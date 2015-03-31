<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ManageUsers.aspx.cs" Inherits="DominosCMS.Web.Areas.Admin.ManageUsers.ManageUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Height="20px" Width="155px">
                        <asp:ListItem Selected="True">First Name</asp:ListItem>
                        <asp:ListItem>Last Name</asp:ListItem>
                    </asp:DropDownList>
                &nbsp;<asp:HyperLink ID="lnkEntites" runat="server" Font-Bold="True" NavigateUrl="~/Areas/Admin/ManageUsers/ManageEntities.aspx" Target="_self">View Entities</asp:HyperLink>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Users</td>
                <td>&nbsp;</td>
                <td>
                    <asp:HyperLink ID="lnkJobs" runat="server" Font-Bold="True" NavigateUrl="~/Areas/Admin/ManageUsers/ManageJobs.aspx" Target="_self">Jobs / Groups</asp:HyperLink>
                </td>
                <td>&nbsp;</td>
                <td>Rules</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="vertical-align: top">

                    <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" CellPadding="3" DataKeyNames="PK_NO" DataSourceID="sdsAllUsers" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="gvUsers_RowDataBound" OnDataBound="gvUsers_DataBound" OnRowCommand="gvUsers_RowCommand" OnSelectedIndexChanged="gvUsers_SelectedIndexChanged" PageSize="15" OnPageIndexChanged="gvUsers_PageIndexChanged" OnSorted="gvUsers_Sorted" AllowPaging="True">
                        <Columns>
                            <asp:BoundField DataField="PK_NO" HeaderText="PK_NO" ReadOnly="True" SortExpression="PK_NO" Visible="False" />
                            <asp:BoundField DataField="FIRST_NAME" HeaderText="First Name" SortExpression="FIRST_NAME" />
                            <asp:BoundField DataField="LAST_NAME" HeaderText="Last Name" SortExpression="LAST_NAME" />
                            <asp:BoundField DataField="USERNAME" HeaderText="User Name" SortExpression="USERNAME" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbxSelectUser" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle Font-Bold="True" ForeColor="#003366" Font-Italic="True" Font-Overline="False" Font-Underline="True" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsAllUsers" runat="server" ConnectionString="<%$ ConnectionStrings:TSNOFFICEDOTNETConnectionString %>" SelectCommand="SELECT [PK_NO], [FIRST_NAME], [LAST_NAME], [USERNAME] FROM [Sec_A_1] ORDER BY [FIRST_NAME]"></asp:SqlDataSource>

                </td>
                <td>&nbsp;</td>
                <td style="vertical-align: top">
                    <asp:GridView ID="gvJobs" runat="server" AutoGenerateColumns="False" CellPadding="3" DataKeyNames="JOBID" DataSourceID="sdsJobs" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="gvJobs_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="JOBID" HeaderText="JOBID" ReadOnly="True" SortExpression="JOBID" Visible="False" />
                            <asp:BoundField DataField="DESCRIPTION" HeaderText="Jobs / Groups" SortExpression="DESCRIPTION" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:RadioButton ID="rbtnSelectJob" runat="server" AutoPostBack="True" GroupName="selectJob" OnCheckedChanged="rbtnSelectJob_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsJobs" runat="server" ConnectionString="<%$ ConnectionStrings:TSNOFFICEDOTNETConnectionString %>" SelectCommand="SELECT [JOBID], [DESCRIPTION] FROM [Sec_JobsRIGHTS] ORDER BY [JOBID]"></asp:SqlDataSource>
                </td>
                <td>&nbsp;</td>
                <td style="vertical-align: top">
                    <asp:GridView ID="gvRules" runat="server" AutoGenerateColumns="False" CellPadding="3" DataKeyNames="RULEID" DataSourceID="sdsRules" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                        <Columns>
                            <asp:BoundField DataField="RULEID" HeaderText="RULEID" ReadOnly="True" SortExpression="RULEID" Visible="False" />
                            <asp:BoundField DataField="RULEDESC" HeaderText="Rules" SortExpression="RULEDESC" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbxSelectRule" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsRules" runat="server" ConnectionString="<%$ ConnectionStrings:TSNOFFICEDOTNETConnectionString %>" SelectCommand="SELECT [RULEID], [RULEDESC] FROM [Sec_RULES]"></asp:SqlDataSource>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save Changes" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:SqlDataSource ID="sdsGeneric" runat="server" ConnectionString="<%$ ConnectionStrings:TSNOFFICEDOTNETConnectionString %>" SelectCommand="SELECT * FROM [Sec_A_1]"></asp:SqlDataSource>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>



</body>
</html>
