<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Frm_Login.aspx.cs" Inherits="StoreRequisition.Page.Frm_Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <style type="text/css">
        .auto-style1 {
            text-align: right;
        }

        .auto-style3 {
            text-align: right;
            width: 40%;
            height: 26px;
        }
        .auto-style4 {
            width: 60%;
            height: 26px;
        }
    </style>
    <script >
        $(document).ready(function () { 
            window.history.forward();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Div0" runat="server">
        <fieldset style="width: 100%; margin: auto; padding: 0">
            <legend>Login</legend>
            <table style="width: 100%;">
                <tr>
                    <td class="auto-style1" style="width: 40%;"><strong>
                        <asp:Label ID="Label7" runat="server" Font-Names="Century Gothic" Text="Username : "></asp:Label>
                        </strong>
                        <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                    <td style="width: 60%;">
                        <asp:TextBox ID="txtUsername" runat="server" Width="295px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"><strong>
                        <asp:Label ID="Label11" runat="server" Font-Names="Century Gothic" Text="Password : "></asp:Label>
                        </strong>
                        <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtPassword" runat="server" Width="295px" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                </table>
            <div id="Div3" runat="server">
                <table style="width: 100%; margin-top: 0px;">
                    <tr>
                        <td class="auto-style1" style="width: 40%;">&nbsp;</td>
                        <td style="width: 60%;"><strong>
                            <asp:Button ID="btnLogin" runat="server" Font-Bold="False" Font-Names="Century Gothic" OnClick="btnLogin_Click" TabIndex="30" Text="Login" Width="80px" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnClear" runat="server" Enabled="False" Font-Bold="False" Font-Names="Century Gothic" OnClick="btnClear_Click" Text="Clear" UseSubmitBehavior="False" Width="80px" />
                            </strong></td>
                    </tr>
                    <tr>
                           <td class="auto-style1" style="width: 40%;">&nbsp;</td>
                        <td>
                            <strong>
                                <asp:Label ID="lblMSG" runat="server" Font-Names="Century Gothic" Text=""></asp:Label>
                            </strong>
                        </td>
                    </tr>
                </table>
            </div>
            <div >
                
            </div>
            <br />
        </fieldset>
    </div>
</asp:Content>
