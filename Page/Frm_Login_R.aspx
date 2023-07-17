<%@ Page Title="" Language="C#" MasterPageFile="~/Bootstrap_L.Master" AutoEventWireup="true" CodeBehind="Frm_Login_R.aspx.cs" Inherits="StoreRequisition.Page.Frm_Login_R" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        @import url(https://fonts.googleapis.com/css?family=Roboto:300);

        .login-page {
            /*width: 360px;*/
            padding: 12vh 0 0;
            margin: auto;
        }

        .form {
            position: relative;
            z-index: 1;
            background: #465c71 !important;
            max-width: 70vw;
            margin: 0 auto 100px;
            padding: 45px;
            box-shadow: 0 0 20px 0 rgba(0, 0, 0, 0.2), 0 5px 5px 0 rgba(0, 0, 0, 0.24);
        }

            .form .message {
                margin: 15px 0 0;
                color: #b3b3b3;
                font-size: 12px;
            }

                .form .message a {
                    color: #4CAF50;
                    text-decoration: none;
                }

        .container {
            position: relative;
            z-index: 1;
            max-width: 300px;
            margin: 0 auto;
        }

            .container:before, .container:after {
                content: "";
                display: block;
                clear: both;
            }

            .container .info {
                margin: 50px auto;
                text-align: center;
            }

                .container .info h1 {
                    margin: 0 0 15px;
                    padding: 0;
                    font-size: 36px;
                    font-weight: 300;
                    color: #1a1a1a;
                }

                .container .info span {
                    color: #4d4d4d;
                    font-size: 12px;
                }

                    .container .info span a {
                        color: #000000;
                        text-decoration: none;
                    }

                    .container .info span .fa {
                        color: #EF3B3A;
                    }

        body {
            background: #f00; /* fallback for old browsers */
            /*background: rgb(255,255,255);*/
            /*background: rgb(181,209,236);*/
            background: linear-gradient(180deg, rgba(181,215,247,1) 0%, rgba(255,255,255,1) 70%);
            background-attachment: fixed;
            font-family: "Roboto", sans-serif;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-page">
        <div class="form">
            <h1 class="text-white">STORE'S MATERIAL REQUISITION ONLINE </h1>
            <hr />
            <form class="login-form" runat="server">
                <div class="form-group">
                    <label for="txtUsername" class="text-white-50">USERNAME</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtPassword" class="text-white-50">PASSWORD</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control form-control-lg" TextMode="Password"></asp:TextBox>
                </div>
                <asp:Button ID="btnLogin" runat="server" Font-Bold="False" Font-Names="Century Gothic" OnClick="btnLogin_Click" TabIndex="30" Text="Login" CssClass="btn btn-lg btn-primary btn-block" />
                <p class="text-white mt-3">
                    <b>MSG :</b>
                    <asp:Label ID="lblMSG" runat="server" Font-Names="Century Gothic" Text=""></asp:Label>
                </p>


            </form>
        </div>
    </div>
</asp:Content>
