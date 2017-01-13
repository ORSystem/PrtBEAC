using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EFCAO
{
    public partial class Pdf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Get the path of the pdf document
                //string path = Request.QueryString["docPath"].ToString();

                if (Session["DocumentEfcaoPath"] != null)
                {
                    string path = Session["DocumentEfcaoPath"].ToString();

                    WebClient client = new WebClient();
                    Byte[] buffer = client.DownloadData(path);

                    if (buffer != null)
                    {
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-length", buffer.Length.ToString());

                        Response.BinaryWrite(buffer);
                        Response.OutputStream.Flush();
                        Response.OutputStream.Close();

                        Response.End();
                    }
                    client.Dispose();
                    buffer = null;
                }
            }
            catch (SystemException ex)
            {

                string error = ex.ToString();
                //ErrorLabel.Text = ex.Message;
                //ErrorLabel.Visible = true;
            }
        }
    }
}