using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DominosCMS.Web.Areas.Admin.ManageUsers
{
    public partial class ManageEntities : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
            }

        }

        protected void ddlEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            int EntityID = int .Parse(ddlEntities.SelectedValue);
            //
            string sql = @"SELECT Sec_RULES.RULEID, Sec_RULES.RULEDESC
            FROM       Sec_RULES INNER JOIN
                       Sec_Entities ON Sec_RULES.EntityID = Sec_Entities.EntityID
            WHERE     (Sec_Entities.EntityID = " + EntityID.ToString() + ")";
            sdsGeneric.SelectCommand = sql;
            DataView dv = (DataView)sdsGeneric.Select(DataSourceSelectArguments.Empty);
            lboxRules.DataSource = dv.Table.DefaultView;
            lboxRules.DataTextField = "RULEDESC";
            lboxRules.DataValueField = "RULEID";
            lboxRules.DataBind();
  
        }

        protected void lboxRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            int RuleID = int.Parse(lboxRules.SelectedValue);
            
            string sql = @"SELECT  Sec_JobsRIGHTS.JOBID, Sec_JobsRIGHTS.DESCRIPTION
            FROM      Sec_RULES INNER JOIN
                      Sec_JOBRIGHTS_RULES ON Sec_RULES.RULEID = Sec_JOBRIGHTS_RULES.RULEID INNER JOIN
                      Sec_JobsRIGHTS ON Sec_JOBRIGHTS_RULES.JOBID = Sec_JobsRIGHTS.JOBID
            WHERE     (Sec_RULES.RULEID = " + RuleID.ToString() + ")";

            sdsGeneric.SelectCommand = sql;
            DataView dv = (DataView)sdsGeneric.Select(DataSourceSelectArguments.Empty);
            lboxJobs.DataSource = dv.Table.DefaultView;
            lboxJobs.DataTextField = "DESCRIPTION";
            lboxJobs.DataValueField = "JOBID";
            lboxJobs.DataBind();
            
         
        }

        protected void lboxJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            int JobID = int.Parse(lboxJobs.SelectedValue);
            string sql = @"SELECT   Sec_A_1.PK_NO,  Sec_A_1.FIRST_NAME + ' ' + Sec_A_1.LAST_NAME as Full_NAME
            FROM       Sec_JobsRIGHTS INNER JOIN
                       Sec_USER_JOBRIGHTS ON Sec_JobsRIGHTS.JOBID = Sec_USER_JOBRIGHTS.JOBID INNER JOIN
                       Sec_A_1 ON Sec_USER_JOBRIGHTS.PK_NO = Sec_A_1.PK_NO
                       WHERE     (Sec_JobsRIGHTS.JOBID = " + JobID.ToString() + ")";
            sdsGeneric.SelectCommand = sql;
            DataView dv = (DataView)sdsGeneric.Select(DataSourceSelectArguments.Empty);
            lboxUsers.DataSource = dv.Table.DefaultView;
            lboxUsers.DataTextField = "Full_NAME";
            lboxUsers.DataValueField = "PK_NO";
            lboxUsers.DataBind();
           
        }

        protected void lboxRules_DataBound(object sender, EventArgs e)
        {
            if (lboxRules.Items.Count > 0)
            {
                lboxRules.SelectedIndex = 0;
                lboxRules_SelectedIndexChanged(this, null);
            }
            else
            {
                lboxJobs.Items.Clear();
                lboxUsers.Items.Clear();
            }
         
        }

        protected void lboxJobs_DataBound(object sender, EventArgs e)
        {
            if (lboxJobs.Items.Count > 0)
            {
                lboxJobs.SelectedIndex = 0;
                lboxJobs_SelectedIndexChanged(this, null);
            }
            else
            {
                lboxUsers.Items.Clear();
            }
        }

        protected void ddlEntities_DataBound(object sender, EventArgs e)
        {
            if (ddlEntities.Items.Count > 0)
            {
                ddlEntities.SelectedIndex = 0;
                ddlEntities_SelectedIndexChanged(this, null);
            }
        }
    }
}