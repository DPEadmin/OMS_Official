using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DOMS_TSR.src.TestCode
{
    public partial class CKE : System.Web.UI.Page
    {
        struct MyStruct
        {
            public string name;
            public string description;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                MyStruct myStruct;
                myStruct.name = nameTextBox.Text;
                myStruct.description = descriptionTextBox.Text;
                Session["MySession"] = myStruct;
                BindDataTable();
            }
        }

        Boolean Validation()
        {
            var errorMessage = "";

            //Name
            if (nameTextBox.Text == "")
                errorMessage += "☺ Enter Name<br/>";
            //End

            //Description
            if (descriptionTextBox.Text == "")
                errorMessage += "☺ Enter Description";
            //End

            if (errorMessage.Length == 0)
                return true;
            else
            {
                errorDiv.InnerHtml = errorMessage;
                return false;
            }
        }

        void BindDataTable()
        {
            string table = "<table>";
            table += "<tr><td class=\"red\">Name: </td></tr><tr><td>" + ((MyStruct)Session["MySession"]).name + "</td></tr>";
            table += "<tr><td class=\"red\">Description: </td></tr><tr><td>" + ((MyStruct)Session["MySession"]).description + "</td></tr>";
            table += "</table>";
            studentDataDiv.InnerHtml = table;

            studentFormDiv.Visible = false;
            editButton.Visible = studentDataDiv.Visible = true;
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            nameTextBox.Text = ((MyStruct)Session["MySession"]).name;
            descriptionTextBox.Text = ((MyStruct)Session["MySession"]).description;

            studentFormDiv.Visible = true;
            editButton.Visible = studentDataDiv.Visible = false;
        }

        protected void reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}