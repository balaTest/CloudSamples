using MessageBoard_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MessageBoard_WebRole
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                tmrRefreshMsgs.Enabled = true;
            }
		
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {

            // create a new entry in table storage
            MessageBoardEntry entry = new MessageBoardEntry() { GolferName = txtName.Text, GolferMessage = txtMessage.Text };
            MessageBoardDataSource ds = new MessageBoardDataSource();
            ds.AddEntry(entry);

            txtName.Text = "";
            txtMessage.Text = "";

            dlMessages.DataBind();
		
        }

        protected void tmrRefreshMsgs_Tick(object sender, EventArgs e)
        {
            dlMessages.DataBind();
        }
    }
}