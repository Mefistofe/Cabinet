using Cabinet.Resources;
using Cabinet.Miscellaneous;
using DatabaseEntities;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cabinet.PageContent
{
    public partial class Site : MasterPage
    {
        protected void RedirectToPageButton_Init(object sender, EventArgs e)
        {
            Button redirectToPageButton = sender as Button;
            string webPageName = redirectToPageButton.CommandArgument;

            string redirectToPageButtonTextResourceName = string.Format(Constants.ButtonTextFormatConvention, webPageName);
            string redirectToPageButtonText = Site_Master.ResourceManager.GetString(redirectToPageButtonTextResourceName);

            redirectToPageButton.Text = redirectToPageButtonText;
        }

        protected void RedirectToPageButton_Click(object sender, CommandEventArgs e)
        {
            string webPageName = e.CommandArgument as string;
            if (string.IsNullOrWhiteSpace(webPageName))
                return;

            Button redirectToPageButton = sender as Button;

            string webPageURL = null;
            using (CabinetEntities cabinetEntities = new CabinetEntities())
            {
                webPageURL = cabinetEntities.WebPagesURLs.SingleOrDefault(x => x.PageName == webPageName).PageURL;
            }
            if (webPageURL == null)
                return;

            Response.Redirect(webPageURL);
        }
    }
}