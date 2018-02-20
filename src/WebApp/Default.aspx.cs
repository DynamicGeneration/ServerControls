using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //QSB.TextType = typeof(Person).AssemblyQualifiedName;
                QSB.SetReflectedTextType(typeof(Person));
            }
            Label1.Text = (IndexButton1.Index++).ToString();
            Label2.Text = (IndexButton1.IndexInViewState++).ToString();

            VacationHome1.DataBind();
            VacationHome2.DataBind();

            Label3.Visible = false;

            Label4.Visible = false;
        }

        protected void Register_Submit(object sender, EventArgs e)
        {
            // The application developer can implement
            // logic here to enter registration data into
            // a database or write a cookie
            // on the user's computer.
            // This example merely writes a message
            // using the Label control on the page.
            Label1.Text = String.Format(
                "Thank you, {0}! You are registered.", Register1.Name);
            Label1.Visible = true;
            Register1.Visible = false;
        }
        protected void StyledRegister_Submit(object sender, EventArgs e)
        {
            Label1.Text = String.Format("Thank you, {0}! You are registered.",
              StyledRegister1.Name);
            Label1.Visible = true;
            StyledRegister1.Visible = false;
        }

        protected void ObjectInput1_ObjectInputDataSubmittedEvent(object sender, DynGen.AspNet.ObjectInputEventArgs e)
        {
            // I am able to "assume" that the e.DataTypeProxy has the public properties/fields of my Person class.
            MessageLabel.Text = e.DataTypeProxy.FirstName;
        }
    }
    public class Person
    {
        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName_NoSet { get { return $"{FirstName} {LastName}"; } }
        public int Age_PrivateSet { get; private set; } = 42;
        public static long Copies_Static {get;set;}
        #endregion

        #region Fields
        public readonly double PI_ReadOnly = 3.14159;
        public double Height_Field = 5.9;
        public static bool Alive_StaticField = true;
        private DateTime DOB_Private = DateTime.Now;
        #endregion
    }
}