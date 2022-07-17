<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="Admin.Logon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <!-- BOOTSTRAP -->
    <link href="Content/bootstrap.min.css" rel="stylesheet" />

    
    <!-- CUSTOM CSS -->
    <style>
        fieldset { border: 1px solid #ced4da; padding: inherit; border-radius: 4px; } 
        fieldset > legend { float: initial; width: auto; padding: revert; font-size: initial; margin: 0; }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container ">

            <h3>
 <font>Logon Page</font>
</h3>
            <fieldset class="p-4">



                <div class="mb-3">
                    <asp:Label ID="lblUsername" runat="server" class="form-label" Text="Username"></asp:Label>
                    <asp:TextBox ID="txtUserName" class="form-control" runat="server"></asp:TextBox>

                    <asp:RequiredFieldValidator ControlToValidate="txtUserName"
                        Display="Static" ErrorMessage="*" runat="server"
                        ID="vUserName" />
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblUserPass" class="form-label" runat="server" Text="Password"></asp:Label>
                    <asp:TextBox ID="txtUserPass" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="txtUserPass"
                        Display="Static" ErrorMessage="*" runat="server"
                        ID="vUserPass" />
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblCookie" runat="server" Text="Persistent Cookie:"></asp:Label>
                    <asp:CheckBox ID="chkPersistCookie" runat="server" AutoPostBack="false" />
                </div>
                <asp:Button ID="btnLogin" meta:resourcekey="btnLogin" class="btn btn-primary" runat="server" Text="Submit" OnClick="btnLogin_Click" />

                <asp:Label ID="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />
            </fieldset>

        </div>
    </form>
</body>
</html>
