<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApp._Default" %>

<%@ Register Assembly="DynGen.AspNet" Namespace="DynGen.AspNet" TagPrefix="dg" %>

<%@ Register Assembly="DynGen.AspNet" Namespace="Samples.AspNet.CS.Controls" TagPrefix="aspSample" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .dg-input-group {
            display: grid;
        }
        .dg-input-group > label {
            grid-column: 1;
        }
        .dg-input-group > input {
            grid-column: 2;
        }
        .dg-input-group > a {
            grid-column: 2;
            text-align: right;
        }
    </style>
    <div class="jumbotron">
        <h1>ASP.NET <small>
            <aspSample:WelcomeLabel ID="WelcomeLabel1" runat="server" Text="Welcome"></aspSample:WelcomeLabel></small></h1>
        <p class="lead">
            ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.
            <aspSample:MailLink ID="maillink1" Font-Bold="true" ForeColor="Green" Email="someone@example.com" runat="server">Mail Webmaster</aspSample:MailLink>
        </p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
        <dg:ObjectInput ID="ObjectInput1" runat="server"
            ItemType="WebApp.Person" SubmitText="Save Data"
            OnObjectInputDataSubmittedEvent="ObjectInput1_ObjectInputDataSubmittedEvent"/>
        <blockquote>
            <asp:Label ID="MessageLabel" runat="server" />
        </blockquote>        
        <details>
            <summary>QuickSand Box:</summary>
            <br />
            <dg:QuickSandBox ID="QSB" runat="server"
                Text="Quick-sand Box" Postfix="What?"
                TextType="WebApp.Person">
            </dg:QuickSandBox>
        </details>
    </div>
    <div class="row">
        <div class="col-md-3">
            <h3><a href="https://msdn.microsoft.com/en-us/library/1whwt1k7.aspx">Control State vs. View State</a></h3>
            Click the button:
        <aspSample:IndexButton Text="IndexButton"
            ID="IndexButton1" runat="server" />
            <br />
            <br />
            The value of the Index property of IndexButton is:<br />
            <asp:Label ID="Label1" runat="server" Text="Label">
            </asp:Label>
            <br />
            <br />
            The value of the IndexInViewState property of IndexButton is:
      <br />
            <asp:Label ID="Label2" runat="server" Text="Label">
            </asp:Label>
        </div>
        <div class="col-md-3">
            <h3><a href="https://msdn.microsoft.com/en-us/library/ms178657.aspx">Templated Server Control Example</a></h3>
            <div>
                Vacation Home with Template<br />
                <aspSample:VacationHome ID="VacationHome1"
                    Title="Condo for Rent in Hawaii"
                    Caption="Ocean view starting $200"
                    runat="server" Width="284px" Height="213px">
                    <Template>
                        <table id="TABLE1" runat="server"
                            style="width: 286px; height: 260px; background-color: Aqua; text-align: center">
                            <tr>
                                <td style="width: 404px">
                                    <asp:Label ID="Label1" runat="server"
                                        Text="<%#Container.Title%>"
                                        Font-Names="Arial, Helvetica"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 404px; position: relative;">
                                    <asp:Image ID="Image1" runat="server"
                                        ImageUrl="~/images/hawaii.jpg"
                                        AlternateText="Hawaii home" Width="284px" Height="213px" />
                                    <a href="https://pixabay.com/en/oahu-hawaii-home-hill-tropical-1978335/" target="_blank" style="position: absolute; top: 0; right: 0;">
                                        <q cite="https://pixabay.com/en/oahu-hawaii-home-hill-tropical-1978335/">Image CC0 &#128279;</q>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 404px; height: 26px;">
                                    <asp:Label ID="Label2" runat="server"
                                        Text="<%#Container.Caption%>"
                                        Font-Names="Arial, Helvetica">
                                    </asp:Label>
                                </td>
                            </tr>
                        </table>
                    </Template>
                </aspSample:VacationHome>

            </div>
            <hr />
            <div>
                The VacationHome control rendered with its default template:<br />
                <aspSample:VacationHome ID="VacationHome2"
                    Title="Condo for Rent in Hawaii"
                    Caption="Ocean view starting $200"
                    runat="server" BorderStyle="Solid" BackColor="#66ffff"
                    Height="24px" Width="238px" Font-Names="Arial, Helvetica" />
            </div>

        </div>
        <div class="col-md-3">
            <h3><a href="https://msdn.microsoft.com/en-us/library/ms178656.aspx">Typed Styles for Child Controls Example</a></h3>
            <aspSample:StyledRegister ButtonText="Register"
                OnSubmit="StyledRegister_Submit" ID="StyledRegister1"
                runat="server" NameLabelText="Name:" EmailLabelText="Email:"
                EmailErrorMessage="You must enter your e-mail address."
                NameErrorMessage="You must enter your name."
                BorderStyle="Solid" BorderWidth="1px" BackColor="#E0E0E0">
                <TextBoxStyle Font-Names="Arial" BorderStyle="Solid"
                    ForeColor="#804000"></TextBoxStyle>
                <ButtonStyle Font-Names="Arial" BorderStyle="Outset"
                    BackColor="Silver"></ButtonStyle>
            </aspSample:StyledRegister>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Label">
            </asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1"
                runat="server" DisplayMode="List" />
        </div>
        <div class="col-md-3">
            <h3><a href="https://msdn.microsoft.com/en-us/library/3257x3ea.aspx">Composite Web Control Example</a></h3>
            <aspSample:Register ButtonText="Register"
                OnSubmit="Register_Submit" ID="Register1"
                runat="server" NameLabelText="Name:"
                EmailLabelText="Email:"
                EmailErrorMessage="You must enter your e-mail address."
                NameErrorMessage="You must enter your name." />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Label">
            </asp:Label>
            <asp:ValidationSummary ID="ValidationSummary2"
                runat="server" DisplayMode="List" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
