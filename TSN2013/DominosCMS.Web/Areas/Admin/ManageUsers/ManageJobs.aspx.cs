using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace DominosCMS.Web.Areas.Admin.ManageUsers
{
    public partial class ManageJobs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvJobs.DataBind();
            }
            else
            {
                lblmsg.Visible = false;
            }
        }

        private void GetJobRules(int JobID)
        {
            string sql = @"SELECT  Sec_JOBRIGHTS_RULES.RULEID, Sec_RULES.RULEDESC
                         FROM         Sec_JOBRIGHTS_RULES INNER JOIN
                         Sec_RULES ON Sec_JOBRIGHTS_RULES.RULEID = Sec_RULES.RULEID
                         WHERE     (Sec_JOBRIGHTS_RULES.JOBID = " + JobID.ToString() +")";
            sdsGeneric.SelectCommand = sql;
            DataView dv = (DataView)sdsGeneric.Select(DataSourceSelectArguments.Empty);
            lbRules.DataSource = dv.Table.DefaultView;
            lbRules.DataTextField = "RULEDESC";
            lbRules.DataValueField = "RULEID";
            lbRules.DataBind();
        }


        private void GetUsersFromJob(int JOBID)
        {
            string sql = @"SELECT     Sec_USER_JOBRIGHTS.PK_NO, Sec_USER_JOBRIGHTS.JOBID, Sec_A_1.FIRST_NAME + ' ' + Sec_A_1.LAST_NAME as Full_NAME
                         FROM         Sec_USER_JOBRIGHTS INNER JOIN
                         Sec_A_1 ON Sec_USER_JOBRIGHTS.PK_NO = Sec_A_1.PK_NO
                         WHERE     (Sec_USER_JOBRIGHTS.JOBID = " + JOBID.ToString() +")";
            sdsGeneric.SelectCommand = sql;
            DataView dv = (DataView)sdsGeneric.Select(DataSourceSelectArguments.Empty);
            lbUsers.DataSource = dv.Table.DefaultView;
            lbUsers.DataTextField = "Full_NAME";
            lbUsers.DataValueField = "PK_NO";
            lbUsers.DataBind();
        }

        
        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = gvJobs.Rows[index];
                int JOBID = int.Parse(gvJobs.DataKeys[row.RowIndex].Value.ToString()); // value of the datakey
                GetUsersFromJob(JOBID);
                GetJobRules(JOBID);
              
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (txtboxDesc.Text != "")
            {
                string sql = @" INSERT INTO [Sec_JobsRIGHTS]
                ([JOBID] ,[DESCRIPTION] ,[NOTES])
                VALUES
                ( (SELECT MAX(JOBID) FROM [Sec_JobsRIGHTS]) + 1
                 ,'"+ txtboxDesc.Text + 
                  "','"+ txtboxNotes.Text +
                "' )";
                // Execute Command//
                sdsGeneric.SelectCommand = sql;
                sdsGeneric.Select(DataSourceSelectArguments.Empty);
                gvJobs.DataBind();
                lblmsg.Visible = true;
                txtboxDesc.Text = "";
                txtboxNotes.Text = "";
            }
        }

        protected void gvJobs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState != DataControlRowState.Edit)
            {
                if (e.Row.RowState.ToString() != "Alternate, Selected, Edit" && e.Row.RowState.ToString() != "Selected, Edit" && e.Row.RowState.ToString() != "Alternate, Edit")
                {
                    //
                    e.Row.Attributes.Add("onMouseOver", "this.style.background='#ff9900'; this.style.cursor='pointer'");
                    //e.Row.Attributes.Add("onMouseOut", "if(this.getElementsByTagName('input')[0].checked){this.style.background='ff9900'}else{this.style.background='#ffffff'}");
                    e.Row.Attributes.Add("onMouseOut", "{this.style.background='#ffffff'}");
                    //
                    e.Row.Cells[0].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvJobs, "Select$" + e.Row.RowIndex);
                    e.Row.Cells[0].ToolTip = "Click to select this row.";

                    e.Row.Cells[1].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvJobs, "Select$" + e.Row.RowIndex);
                    e.Row.Cells[1].ToolTip = "Click to select this row.";

                    e.Row.Cells[2].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvJobs, "Select$" + e.Row.RowIndex);
                    e.Row.Cells[2].ToolTip = "Click to select this row.";
                }

            }


        }

       
    }
}