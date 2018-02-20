using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynGen.AspNet
{
    [DefaultProperty("ItemType"),
     DefaultEvent("ObjectInputDataSubmittedEvent"),
     ToolboxData("<{0}:ObjectInput runat=server></{0}:ObjectInput>")]
    public class ObjectInput : CompositeControl
    {
        #region Properties/Fields
        private string _itemType;
        private string _submitText;

        [DefaultValue(""),
        Themeable(false)]
        public virtual string ItemType
        {
            get { return _itemType ?? String.Empty; }
            set
            {
                if (!String.Equals(_itemType, value, StringComparison.OrdinalIgnoreCase))
                {
                    _itemType = value;
                    //OnDataPropertyChanged();
                }
            }
        }

        [DefaultValue(""),
        Themeable(false)]
        public virtual string SubmitText
        {
            get { return _submitText ?? String.Empty; }
            set
            {
                if (!String.Equals(_submitText, value, StringComparison.OrdinalIgnoreCase))
                {
                    _submitText = value;
                    //OnDataPropertyChanged();
                }
            }
        }
        #endregion

        #region Private Methods
        private Type GetItemDataType()
        {
            Type t = null;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                t = a.GetType(ItemType);
                if (t != null)
                    break;
            }
            return t;
        }

        private List<MemberInfo> AssignableGetMembers()
        {
            List<MemberInfo> members = new List<MemberInfo>();
            var t = GetItemDataType();
            if (t != null)
                foreach (var m in t.GetMembers())
                    if (m is FieldInfo)
                    {
                        FieldInfo item = m as FieldInfo;
                        if (!item.IsStatic && !item.IsInitOnly)
                            members.Add(m);
                    }
                    else if (m is PropertyInfo)
                    {
                        PropertyInfo pitem = m as PropertyInfo;
                        if (pitem.CanWrite && pitem.SetMethod.IsPublic && !pitem.SetMethod.IsStatic)
                            members.Add(m);
                    }
            return members;
        }
        #endregion

        #region Event Handlers and Event Propegation
        //public event SelectedValueChanged(object sender, ObjectInputEventArgs e);
        private void _button_Click(object source, EventArgs e)
        {
            dynamic dataItemProxy = new ExpandoObject();
            //var dictionary = (IDictionary<string, string>)dataItemProxy;
            foreach (var assignable in AssignableGetMembers())
            {
                string propName = assignable.Name;
                Type propType = assignable.DeclaringType;
                var input = this.FindControl("txt" + assignable.Name) as TextBox;
                if (input != null)
                {
                    //dictionary.Add(propName, input.Text);
                    ((IDictionary<String, Object>)dataItemProxy)[propName] = input.Text;
                }
            }
            //((EventHandler)Events[EventSubmitKey])?.Invoke(this, e);
            var ev = new ObjectInputEventArgs
            {
                DataTypeProxy = dataItemProxy
            };
            OnObjectInputDataSubmittedEvent(ev);
        }
        
        [Browsable(true)]
        public event ObjectInputDataSubmittedEventHandler ObjectInputDataSubmittedEvent;
        protected virtual void OnObjectInputDataSubmittedEvent(ObjectInputEventArgs e)
        {
            //Ensure the event is not null 
            ObjectInputDataSubmittedEvent?.Invoke(this, e);
        }

        //private static readonly object EventSubmitKey =
        //    new object();
        //public event EventHandler Submit
        //{
        //    add
        //    {
        //        Events.AddHandler(EventSubmitKey, value);
        //    }
        //    remove
        //    {
        //        Events.RemoveHandler(EventSubmitKey, value);
        //    }
        //}
        #endregion

        #region Base class overrides
        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();
            foreach(var assignable in AssignableGetMembers())
            {
                var label = new Label();
                label.Text = assignable.Name;
                var textbox = new TextBox();
                textbox.ID = "txt" + assignable.Name;
                label.AssociatedControlID = textbox.ID;
                Controls.Add(label);
                Controls.Add(textbox);
            }
            if(!string.IsNullOrWhiteSpace(SubmitText))
            {
                var link = new LinkButton();
                link.Text = SubmitText;
                link.Click += new EventHandler(_button_Click);
                Controls.Add(link);
            }
            //base.CreateChildControls();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteLine("<div class='dg-input-group'>");

            foreach (Control item in Controls)
                item.RenderControl(writer);

            writer.WriteLine("</div>");
            //base.Render(writer);
        }
        #endregion
        // ----------------------------------------------------------
        private ITemplate templateValue;


        //private TemplateOwner ownerValue;


        ///// <devdoc>
        /////  This method is called when DataMember, DataSource, or DataSourceID is changed.
        ///// </devdoc>
        //protected virtual void OnDataPropertyChanged()
        //{
        //    if (_throwOnDataPropertyChange)
        //    {
        //        throw new HttpException(SR.GetString(SR.DataBoundControl_InvalidDataPropertyChange, ID));
        //    }

        //    if (_inited)
        //    {
        //        RequiresDataBinding = true;
        //    }
        //    _currentViewValid = false;
        //}


        //private object dataItemSource;
        //public virtual object DataItemSource { get { return dataItemSource; } set { dataItemSource = value; } }




        //[Bindable(true)]
        //[Category("Appearance")]
        //[DefaultValue("")]
        //[Localizable(true)]
        //public string Text
        //{
        //    get
        //    {
        //        String s = (String)ViewState["Text"];
        //        return ((s == null) ? String.Empty : s);
        //    }

        //    set
        //    {
        //        ViewState["Text"] = value;
        //    }
        //}

        //protected override void RenderContents(HtmlTextWriter output)
        //{
        //    output.Write(Text);
        //}
    }

    public delegate void ObjectInputDataSubmittedEventHandler(object sender, ObjectInputEventArgs e);
    public class ObjectInputEventArgs : EventArgs
    {
        public dynamic DataTypeProxy { get; set; }
    }
}
