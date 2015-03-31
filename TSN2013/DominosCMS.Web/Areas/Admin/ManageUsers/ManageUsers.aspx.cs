using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;

namespace DominosCMS.Web.Areas.Admin.ManageUsers
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvUsers.DataBind();
                gvJobs.DataBind();
                gvRules.DataBind();
            }
        }



        private void GetUserJobs(int UserPK_NO)
        {
            string sql = @" SELECT Sec_USER_JOBRIGHTS.JOBID FROM Sec_A_1 INNER JOIN Sec_USER_JOBRIGHTS ON Sec_A_1.PK_NO = Sec_USER_JOBRIGHTS.PK_NO
            WHERE     (Sec_USER_JOBRIGHTS.PK_NO = " + UserPK_NO.ToString() + ")";
            sdsGeneric.SelectCommand = sql;
            DataView dv = (DataView)sdsGeneric.Select(DataSourceSelectArguments.Empty);
            HighlightUserJobs(dv);

        }

        private void HighlightUserJobs(DataView dv)
        {
            //to clear previous selection // 
            ////////////////////////////////
            gvJobs.DataBind();
            gvRules.DataBind();
            ////////////////////////////////

            if (dv.Table.Rows.Count > 0)
            {
                foreach (DataRowView rowView in dv)
                {
                    DataRow row = rowView.Row;
                    int UserJobID = int.Parse(row["JOBID"].ToString());
                    // HighLight User's Jobs from Jobs Grid view //
                    //---------------------------------------------
                    for (int i = 0; i < gvJobs.Rows.Count; i++)
                    {
                        int JOBID = int.Parse(gvJobs.DataKeys[i].Values["JOBID"].ToString().Trim());
                        if (JOBID == UserJobID)
                        {
                            ((RadioButton)gvJobs.Rows[i].FindControl("rbtnSelectJob")).Checked = true;
                            gvJobs.Rows[i].BackColor = ColorTranslator.FromHtml("#A1DCF2");
                            //
                            GetJobRules(JOBID);
                            //
                        }

                    }

                }

            }
        }

        private void GetJobRules(int JobID)
        {
            string sql = @"SELECT RULEID FROM Sec_JOBRIGHTS_RULES WHERE JOBID =" + JobID.ToString();
            sdsGeneric.SelectCommand = sql;
            DataView dv = (DataView)sdsGeneric.Select(DataSourceSelectArguments.Empty);
            HighlightJobRules(dv);
        }

        private void HighlightJobRules(DataView dv)
        {

            if (dv.Table.Rows.Count > 0)
            {
                foreach (DataRowView rowView in dv)
                {
                    DataRow row = rowView.Row;
                    int JobRuleID = int.Parse(row["RULEID"].ToString());
                    // HighLight Job's Rules from Rules Grid view //
                    //---------------------------------------------
                    for (int i = 0; i < gvRules.Rows.Count; i++)
                    {
                        int RuleID = int.Parse(gvRules.DataKeys[i].Values["RULEID"].ToString().Trim());
                        if (JobRuleID == RuleID)
                        {
                            ((CheckBox)gvRules.Rows[i].FindControl("cbxSelectRule")).Checked = true;
                            gvRules.Rows[i].BackColor = ColorTranslator.FromHtml("#A1DCF2");

                        }

                    }

                }

            }


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Saves Selected Users in a Job // 
            for (int i = 0; i < gvUsers.Rows.Count; i++)
            {
                if (((CheckBox)gvUsers.Rows[i].FindControl("cbxSelectUser")).Checked)
                {
                    int UserPKID = int.Parse(gvUsers.DataKeys[i].Values["PK_NO"].ToString().Trim());
                    //
                    SaveUserInJobs(UserPKID);
                }
                else
                {
                    if (gvUsers.SelectedIndex != -1)
                    {
                        string UserPKID = gvUsers.DataKeys[gvUsers.SelectedIndex].Value.ToString(); // value of the datakey
                        SaveUserInJobs(int.Parse(UserPKID));
                    }
                }
                //--------------------------------
            }
            //
            // Saves Selected Jobs Rules // ///////
            SaveJobRules();
            /////////////////////////////////////
            // Bind Back With the new selection
            if (gvUsers.SelectedIndex != -1)
            {
                string UserPKID = gvUsers.DataKeys[gvUsers.SelectedIndex].Value.ToString(); // value of the datakey
                GetUserJobs(int.Parse(UserPKID));
            }
            else
            {
                foreach (GridViewRow row in gvJobs.Rows)
                {
                    if (((RadioButton)row.FindControl("rbtnSelectJob")).Checked)
                    {
                        int JOBID = int.Parse(gvJobs.DataKeys[row.DataItemIndex].Values["JOBID"].ToString().Trim());
                        gvRules.DataBind();
                        GetJobRules(JOBID);
                        break;
                    }
                   
                }
            }
            //
        }

        private void SaveJobRules()
        {
            for (int i = 0; i < gvJobs.Rows.Count; i++)
            {
                int JOBID = int.Parse(gvJobs.DataKeys[i].Values["JOBID"].ToString().Trim());
                string sql = "";
                if (((RadioButton)gvJobs.Rows[i].FindControl("rbtnSelectJob")).Checked)
                {
                    AssignRuleForJob(JOBID);
                }


                // Execute Command//
                sdsGeneric.SelectCommand = sql;
                sdsGeneric.Select(DataSourceSelectArguments.Empty);
                //

            }


        }

        protected void AssignRuleForJob(int JobID)
        {
            // Save Rules in Selected Jobs from Rules Grid view //
            //---------------------------------------------
            for (int i = 0; i < gvRules.Rows.Count; i++)
            {
                int RuleID = int.Parse(gvRules.DataKeys[i].Values["RULEID"].ToString().Trim());
                string sql = "";
                if (((CheckBox)gvRules.Rows[i].FindControl("cbxSelectRule")).Checked)
                {
                    // Insert // 
                    sql = @"IF NOT EXISTS (SELECT JOBID , RULEID FROM Sec_JOBRIGHTS_RULES where JOBID = " + JobID.ToString() + " And RULEID= " + RuleID.ToString() + ")";
                    sql = sql + @" INSERT INTO Sec_JOBRIGHTS_RULES(JOBID, RULEID) VALUES (" + JobID.ToString() + "," + RuleID.ToString() + ")";
                }
                else
                {
                    // Remove //
                    sql = @"DELETE FROM Sec_JOBRIGHTS_RULES WHERE JOBID = " + JobID.ToString() + " And RULEID= " + RuleID.ToString();
                }

                // Execute Command//
                sdsGeneric.SelectCommand = sql;
                sdsGeneric.Select(DataSourceSelectArguments.Empty);
                //

            }



        }


        protected void SaveUserInJobs(int UserPKID)
        {
            // Save Users in Selected Jobs from Jobs Grid view //
            //---------------------------------------------
            for (int i = 0; i < gvJobs.Rows.Count; i++)
            {
                int JOBID = int.Parse(gvJobs.DataKeys[i].Values["JOBID"].ToString().Trim());
                string sql = "";
                if (((RadioButton)gvJobs.Rows[i].FindControl("rbtnSelectJob")).Checked)
                {
                    // Insert // 
                    sql = @"IF NOT EXISTS (SELECT PK_NO , JOBID FROM Sec_USER_JOBRIGHTS where PK_NO = " + UserPKID.ToString() + ")";
                    sql = sql + @" INSERT INTO Sec_USER_JOBRIGHTS(PK_NO, JOBID) VALUES (" + UserPKID.ToString() + "," + JOBID.ToString() + ")";
                }
                else
                {
                    // Remove //
                    sql = @"DELETE FROM Sec_USER_JOBRIGHTS WHERE PK_NO = " + UserPKID.ToString() + " And JOBID= " + JOBID.ToString();
                }

                // Execute Command//
                sdsGeneric.SelectCommand = sql;
                sdsGeneric.Select(DataSourceSelectArguments.Empty);
                //

            }



        }

        #region gvUsers Events

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //
                e.Row.Attributes.Add("onMouseOver", "this.style.background='#ff9900'; this.style.cursor='pointer'");
                //e.Row.Attributes.Add("onMouseOut", "if(this.getElementsByTagName('input')[0].checked){this.style.background='ff9900'}else{this.style.background='#ffffff'}");
                e.Row.Attributes.Add("onMouseOut", "{this.style.background='#ffffff'}");
                //
                e.Row.Cells[0].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvUsers, "Select$" + e.Row.RowIndex);
                e.Row.Cells[0].ToolTip = "Click to select this row.";

                e.Row.Cells[1].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvUsers, "Select$" + e.Row.RowIndex);
                e.Row.Cells[1].ToolTip = "Click to select this row.";

                e.Row.Cells[2].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvUsers, "Select$" + e.Row.RowIndex);
                e.Row.Cells[2].ToolTip = "Click to select this row.";

                e.Row.Cells[3].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvUsers, "Select$" + e.Row.RowIndex);
                e.Row.Cells[3].ToolTip = "Click to select this row.";


            }
        }

        protected void gvUsers_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < gvUsers.Rows.Count; i++)
            {
                if (((CheckBox)gvUsers.Rows[i].FindControl("cbxSelectUser")).Checked)
                {
                    // Get User Jobs and Rules // 
                    //int PK_NO = int.Parse(gvUsers.DataKeys[i].Values["PK_NO"].ToString().Trim());

                }

            }
        }

        protected void gvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //foreach (GridViewRow row in gvUsers.Rows)
            //{
            //    if (row.RowIndex == gvUsers.SelectedIndex)
            //    {
            //        row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
            //        row.ToolTip = string.Empty;
            //    }
            //    else
            //    {
            //        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            //        row.ToolTip = "Click to select this row.";
            //    }
            //}
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = gvUsers.Rows[index];
                string PK_NO = gvUsers.DataKeys[row.RowIndex].Value.ToString(); // value of the datakey
                GetUserJobs(int.Parse(PK_NO));
            }
        }

        protected void gvJobs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //
                //e.Row.Attributes.Add("onMouseOver", "this.style.background='#ff9900'; this.style.cursor='pointer'");
                //e.Row.Attributes.Add("onMouseOut", "{this.style.background='#ffffff'}");
                //
                //e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvJobs, "Select$" + e.Row.RowIndex);
                //e.Row.ToolTip = "Click to select this row.";

            }
        }

        protected void gvUsers_PageIndexChanged(object sender, EventArgs e)
        {
            gvUsers.SelectedIndex = -1;
            gvJobs.DataBind();
            gvRules.DataBind();
        }

        protected void gvUsers_Sorted(object sender, EventArgs e)
        {
            gvUsers.SelectedIndex = -1;
            gvJobs.DataBind();
            gvRules.DataBind();
        }

        #endregion


        #region gvJobs Events

        protected void rbtnSelectJob_CheckedChanged(object sender, EventArgs e)
        {
            //Clear the existing selected row 
            foreach (GridViewRow oldrow in gvJobs.Rows)
            {
                ((RadioButton)oldrow.FindControl("rbtnSelectJob")).Checked = false;
                oldrow.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }

            //Set the new selected row
            RadioButton rb = (RadioButton)sender;
            GridViewRow row = (GridViewRow)rb.NamingContainer;
            ((RadioButton)row.FindControl("rbtnSelectJob")).Checked = true;
            row.BackColor = ColorTranslator.FromHtml("#A1DCF2");

            //

            int JOBID = int.Parse(gvJobs.DataKeys[row.DataItemIndex].Values["JOBID"].ToString().Trim());
            //
            gvRules.DataBind();
            GetJobRules(JOBID);
            //---------------------
            GetUsersFromJob(JOBID);
            //

        }

        private void GetUsersFromJob(int JOBID)
        {
            string sql = @"SELECT [PK_NO] ,[JOBID] FROM [TSNOFFICEDOTNET].[dbo].[Sec_USER_JOBRIGHTS] Where JOBID =" + JOBID.ToString();
            sdsGeneric.SelectCommand = sql;
            DataView dv = (DataView)sdsGeneric.Select(DataSourceSelectArguments.Empty);
            HighlightUsersInJob(dv);
        }

        private void HighlightUsersInJob(DataView dv)
        {
            gvUsers.DataBind();
            if (dv.Table.Rows.Count > 0)
            {
                foreach (DataRowView rowView in dv)
                {
                    DataRow row = rowView.Row;
                    int dvUserPKID = int.Parse(row["PK_NO"].ToString());
                    // HighLight Users from Users Grid view //
                    //---------------------------------------------
                    for (int i = 0; i < gvUsers.Rows.Count; i++)
                    {
                        int UserPKID = int.Parse(gvUsers.DataKeys[i].Values["PK_NO"].ToString().Trim());
                        if (UserPKID == dvUserPKID)
                        {
                            ((CheckBox)gvUsers.Rows[i].FindControl("cbxSelectUser")).Checked = true;
                            gvUsers.Rows[i].BackColor = ColorTranslator.FromHtml("#A1DCF2");
                        }

                    }

                }

            }
        }



        #endregion
    }
}