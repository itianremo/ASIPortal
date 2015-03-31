<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ManageJobs.aspx.cs" Inherits="DominosCMS.Web.Areas.Admin.ManageUsers.ManageJobs" %>

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
                <td style="text-align: center">
                    <strong>Jobs:</strong></td>
                <td>

                    <asp:GridView ID="gvJobs" runat="server" AutoGenerateColumns="False" CellPadding="3" DataKeyNames="JOBID" DataSourceID="sdsJobs" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" PageSize="15" OnRowCommand="gvUsers_RowCommand" OnRowDataBound="gvJobs_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="JOBID" HeaderText="Job ID" ReadOnly="True" SortExpression="JOBID" />
                            <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" SortExpression="DESCRIPTION" />
                            <asp:BoundField DataField="NOTES" HeaderText="Notes" SortExpression="NOTES" />
                            <asp:CommandField ShowEditButton="True" />
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
                    <asp:SqlDataSource ID="sdsJobs" runat="server" ConnectionString="<%$ ConnectionStrings:TSNOFFICEDOTNETConnectionString %>" SelectCommand="SELECT * FROM [Sec_JobsRIGHTS]" DeleteCommand="DELETE FROM [Sec_JobsRIGHTS] WHERE [JOBID] = @JOBID" InsertCommand="INSERT INTO [Sec_JobsRIGHTS] ([JOBID], [DESCRIPTION], [NOTES]) VALUES (@JOBID, @DESCRIPTION, @NOTES)" UpdateCommand="UPDATE [Sec_JobsRIGHTS] SET [DESCRIPTION] = @DESCRIPTION, [NOTES] = @NOTES WHERE [JOBID] = @JOBID">
                        <DeleteParameters>
                            <asp:Parameter Name="JOBID" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="JOBID" Type="Int32" />
                            <asp:Parameter Name="DESCRIPTION" Type="String" />
                            <asp:Parameter Name="NOTES" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="DESCRIPTION" Type="String" />
                            <asp:Parameter Name="NOTES" Type="String" />
                            <asp:Parameter Name="JOBID" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align: center"><strong>Add New Job:</strong></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="vertical-align: top; text-align: right;">

                    Description:</td>
                <td>
                    <asp:TextBox ID="txtboxDesc" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td style="vertical-align: top">
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td style="vertical-align: top">
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align: right">Notes:</td>
                <td>
                    <asp:TextBox ID="txtboxNotes" runat="server" TextMode="MultiLine" Width="155px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Add Job" OnClick="btnSave_Click" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="#006600" Text="Job added successfully" Visible="False"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <table id="tblRulesnUsers" style="width:100%;">
                        <tr>
                            <td>Rules in Selected Job</td>
                            <td>&nbsp;</td>
                            <td>Users in Selected Job</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="lbRules" runat="server" Width="200px"></asp:ListBox>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:ListBox ID="lbUsers" runat="server" Width="200px"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
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
                <td style="text-align: center">
                    <asp:HyperLink ID="lnkBack" runat="server" Font-Bold="True" NavigateUrl="~/Areas/Admin/ManageUsers/ManageUsers.aspx" Target="_self">Back</asp:HyperLink>
                </td>
                <td>
                    &nbsp;</td>
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
