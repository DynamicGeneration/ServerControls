using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynGen.AspNet
{
    [DefaultProperty("TextType")]
    [ToolboxData("<{0}:QuickSandBox runat=server></{0}:QuickSandBox>")]
    public class QuickSandBox : TextBox
    {
        #region Rendering
        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(Postfix + ": " + Text);
        }
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            const string b = "<br />";
            writer.WriteLine(b);
            writer.WriteLine(Postfix);

            writer.WriteLine($"{b}TextType='{TextType}'");

            Type t = Type.GetType(TextType);
            //var t = Type.ReflectionOnlyGetType(TextType, false, true) ?? Type.Missing;
            if(t==null)
                foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
                {
                    t = a.GetType(TextType);
                    if (t != null)
                        break;
                }
            else if (t!= null)
                writer.WriteLine($"{b}Its Type: ~{t.FullName}~ ");
            TypeDetails(writer, b, t);
            writer.Write("<hr />");
            TypeDetails(writer, b, rtt);

            writer.WriteLine(b);
        }

        private void TypeDetails(HtmlTextWriter writer, string b, Type givenType)
        {
            if (givenType != null)
            {
                writer.WriteLine($"{b}Reflected Type: ~{givenType.AssemblyQualifiedName}~");

                writer.WriteLine("<h4>Public Fields</h4>- ");
                var members = givenType.GetFields();
                foreach (var item in members)
                    if (!item.IsStatic && !item.IsInitOnly)
                        writer.WriteLine($"<code>{item.Name}</code>,");

                writer.WriteLine("<h4>Public Properties</h4>- ");
                var pmembers = givenType.GetProperties();
                foreach (var item in pmembers)
                    if (item.CanWrite && item.SetMethod.IsPublic && !item.SetMethod.IsStatic)
                        writer.WriteLine($"<code>{item.Name}</code>,");
            }
        }
        #endregion

        #region Sandbox
        private Type rtt;
        public void SetReflectedTextType(Type t)
        {
            rtt = t;
        }
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string TextType
        {
            get
            {
                string s = (string) ViewState["TextType"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["TextType"] = value;
            }
        }
        //[Bindable(true)]
        //[Category("Appearance")]
        //[DefaultValue("")]
        //[Localizable(true)]
        //public Type TextType
        //{
        //    get
        //    {
        //        Type s = ViewState["TextType"] as Type;
        //        return s;
        //        //return ((s == null) ? String.Empty : s);
        //    }

        //    set
        //    {
        //        ViewState["TextType"] = value;
        //    }
        //}
        #endregion

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Postfix
        {
            get
            {
                String s = (String)ViewState["Postfix"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Postfix"] = value;
            }
        }
    }
}
