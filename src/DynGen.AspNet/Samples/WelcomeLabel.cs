/* ********************
 * Credits: Walkthrough: Developing and Using a Custom Web Server Control
 * - https://msdn.microsoft.com/en-us/library/yhzc935f.aspx
 * *******************/
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Samples.AspNet.CS.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:WelcomeLabel runat=server></{0}:WelcomeLabel>")]
    public class WelcomeLabel : Label
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Description("The text to display when the user is not logged in.")]
        [Localizable(true)]
        public string DefaultUserName
        {
            get
            {
                string s = (string)ViewState["DefaultUserName"];
                return (s == null) ? String.Empty : s;
            }
            set
            {
                ViewState["DefaultUserName"] = value;
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.WriteEncodedText(Text);

            string displayUserName = DefaultUserName;
            if (Context != null)
            {
                string userName = Context.User.Identity.Name;
                if (!String.IsNullOrEmpty(userName))
                {
                    displayUserName = userName;
                }
            }

            if (!String.IsNullOrEmpty(displayUserName))
            {
                writer.Write(", ");
                writer.WriteEncodedText(displayUserName);
            }

            writer.Write("!");
        }
    }
}
