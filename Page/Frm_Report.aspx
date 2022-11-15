<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Frm_Report.aspx.cs" Inherits="StoreRequisition.Page.Frm_Report" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style6 {
            text-align: center;
        }

        .auto-style8 {
            font-size: large;
        }

        .auto-style11 {
            width: 25%;
        }

        .auto-style13 {
            text-align: left;
        }

        .auto-style14 {
            text-align: right;
        }

        .auto-style15 {
            width: 33%;
            height: 19px;
        }

        .auto-style19 {
            text-align: right;
            height: 32px;
        }

        .auto-style21 {
            font-size: xx-small;
        }

        .auto-style22 {
            text-align: center;
            height: 19px;
        }

        .auto-style23 {
            height: 19px;
        }

        .auto-style24 {
            font-size: x-small;
        }

        .auto-style25 {
            text-align: left;
            font-size: small;
        }

        .auto-style26 {
            text-align: center;
            font-size: small;
        }

        .auto-style27 {
            text-align: right;
            font-size: small;
        }

        .auto-style28 {
            text-align: left;
            width: 50%;
        }
         @font-face{
             font-family:'3 of 9 Barcode';
             src:url("../Content/custom_fonts/3OF9_NEW.TTF");

            }
        .auto-style29 {
            text-align: right;
            font-size: xx-large;
            font-family: '3 of 9 Barcode';
        }

        .auto-style30 {
            text-align: left;
           
            font-size: x-large;

            font-family: '3 of 9 Barcode';

        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td class="auto-style11">&nbsp;</td>
                <td class="auto-style6">
                    <strong>
                        <asp:Label ID="Label43" runat="server" Font-Names="Century Gothic" Text="Store's Material Requisition" Font-Size="Large" CssClass="auto-style8" Font-Italic="False" Font-Overline="False" Font-Underline="True"></asp:Label>
                    </strong>
                </td>
                <td style="width: 25%;" class="auto-style6">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" rowspan="2">
                    <strong>
                        <asp:Label ID="Label44" runat="server" Font-Names="Century Gothic" Text="Mektec Manufacturing Corporation" Font-Size="Medium" CssClass="auto-style8"></asp:Label>
                    </strong>
                </td>
                <td class="auto-style19">
                    <strong>
                        <asp:Label ID="Label45" runat="server" Font-Names="Century Gothic" Text="Req Number :" Font-Size="Small"></asp:Label>
                        &nbsp;</strong><asp:Label ID="vlRequisitionNumber" runat="server" Font-Names="Century Gothic" Font-Size="Small">2018007201</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style29">
                    <%--  <strong>
                        <asp:Label ID="vlRequisitionNumbers" runat="server" Font-Names="IDAutomationHC39M" Font-Size="8pt" Font-Bold="False" Text="*2018007201*"></asp:Label>
                    </strong>--%>
                    <%=getReqNumber()%>
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td>
                    <strong>
                        <asp:Label ID="lbRequestDate27" runat="server" Font-Names="Century Gothic" Text="Issue Type : " Font-Size="Small"></asp:Label>
                    </strong>
                </td>
                <td>
                    <strong>
                        <asp:Label ID="vlIssueType" runat="server" Font-Names="Century Gothic" Text="OS Issue" Font-Size="Small" Font-Bold="False"></asp:Label>
                    </strong>
                </td>
                <td>
                    <strong>
                        <asp:Label ID="lbRequestDate21" runat="server" Font-Names="Century Gothic" Text="Delivery Status : " Font-Size="Small"></asp:Label>
                    </strong>
                </td>
                <td>
                    <strong>
                        <asp:Label ID="vlDeliveryStation" runat="server" Font-Names="Century Gothic" Text="B15" Font-Size="Small" Font-Bold="False"></asp:Label>
                    </strong>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Label ID="lbRequestDate18" runat="server" Font-Names="Century Gothic" Text="Requisition :   " Font-Size="Small"></asp:Label>
                    </strong>
                </td>
                <td>
                    <strong>
                        <asp:Label ID="vlLocation" runat="server" Font-Names="Century Gothic" Text="MMCT" Font-Size="Small" Font-Bold="False"></asp:Label>
                    </strong>
                </td>
                <td>
                    <strong>
                        <asp:Label ID="lbRequestDate22" runat="server" Font-Names="Century Gothic" Text="Date  : " Font-Size="Small"></asp:Label>
                    </strong>
                </td>
                <td>
                    <strong>
                        <asp:Label ID="vlRequestDate" runat="server" Font-Names="Century Gothic" Text="30/01/2018" Font-Size="Small" Font-Bold="False"></asp:Label>
                    </strong>
                </td>
                <td>
                    <strong>
                        <asp:Label ID="lbRequestDate24" runat="server" Font-Names="Century Gothic" Text="Time : " Font-Size="Small"></asp:Label>
                    </strong>
                </td>
                <td>
                    <strong>
                        <asp:Label ID="vlRequestTime" runat="server" Font-Names="Century Gothic" Text="30/01/2018" Font-Size="Small" Font-Bold="False"></asp:Label>
                    </strong>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Label ID="lbRequestDate19" runat="server" Font-Names="Century Gothic" Text="Request by : " Font-Size="Small"></asp:Label>
                    </strong>
                </td>
                <td>
                    <strong>
                        <asp:Label ID="vlRequestBy" runat="server" Font-Names="Century Gothic" Text="B-446 Somjit" Font-Size="Small" Font-Bold="False" Font-Strikeout="False"></asp:Label>
                    </strong>
                </td>
                <td>
                    <strong>
                        <asp:Label ID="lbRequestDate25" runat="server" Font-Names="Century Gothic" Text="Tel  : " Font-Size="Small"></asp:Label>
                    </strong>
                </td>
                <td>
                    <strong>
                        <asp:Label ID="vlRequestTel" runat="server" Font-Names="Century Gothic" Text="B-446 Somjit" Font-Size="Small" Font-Bold="False" Font-Strikeout="False"></asp:Label>
                    </strong>
                </td>
                <td>
                    <strong>
                        <asp:Label ID="lbRequestDate26" runat="server" Font-Names="Century Gothic" Text="Approve by : " Font-Size="Small"></asp:Label>
                    </strong>
                </td>
                <td>
                    <strong>
                        <asp:Label ID="vlDeliveryStation9" runat="server" Font-Names="Century Gothic" Text="____________" Font-Size="Small" Font-Bold="False"></asp:Label>
                    </strong>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Label ID="lbRequestDate20" runat="server" Font-Names="Century Gothic" Text="Sub Inventory : " Font-Size="Small"></asp:Label>
                    </strong>
                </td>
                <td>
                    <strong>
                        <asp:Label ID="vlSubInventory" runat="server" Font-Names="Century Gothic" Text="AS8-MP" Font-Size="Small" Font-Bold="False"></asp:Label>
                    </strong>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td colspan="5" ><%=getSubInventory()%></td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Label ID="lbRequestDate28" runat="server" Font-Names="Century Gothic" Text="Remark : " Font-Size="Small"></asp:Label>
                    </strong>
                </td>
                <td colspan="5">
                    <strong>
                        <asp:Label ID="vlRemark" runat="server" Font-Names="Century Gothic" Text="Testing New Web Store's Material Requisition" Font-Size="Small" Font-Bold="False"></asp:Label>
                    </strong>
                </td>
            </tr>
        </table>
        <div>
            <table style="width: 100%;" class="newStyle1" border="1" cellspacing="0">
                <tr class="auto-style24">
                    <td class="auto-style26"><strong>Item</strong></td>
                    <td class="auto-style26"><strong>P/N</strong></td>
                    <td class="auto-style26"><strong>Shelf</strong></td>
                    <td class="auto-style26"><strong>FI-FO/EXPIRE *</strong></td>
                    <td class="auto-style26"><strong>UOM</strong></td>
                    <td class="auto-style26"><strong>QTY Req</strong></td>
                    <td class="auto-style26"><strong>Actual</strong></td>
                    <td class="auto-style26"><strong>Pending</strong></td>
                </tr>
                <%=getWhileLoopData()%>
            </table>
        </div>
        <br />
        <table style="width: 100%;" border="1" cellspacing="0">
            <tr>
                <td class="auto-style22" colspan="3">
                    <strong>
                        <asp:Label ID="Label46" runat="server" Font-Names="Century Gothic" Text="Only Staff Store" Font-Size="Small" CssClass="auto-style27"></asp:Label>
                    </strong>
                </td>
            </tr>
            <tr>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style15">
                    <strong>&nbsp;<asp:Label ID="lbRequestDate11" runat="server" Font-Names="Century Gothic" Text="Load Start : " Font-Size="X-Small"></asp:Label>
                        <asp:Label ID="vlDeliveryStation12" runat="server" Font-Names="Century Gothic" Text="_______________" Font-Size="Small" Font-Bold="False"></asp:Label>
                    </strong>
                </td>
                <td class="auto-style15">
                    <strong>&nbsp;<asp:Label ID="lbRequestDate12" runat="server" Font-Names="Century Gothic" Text="Load Finish : " Font-Size="X-Small"></asp:Label>
                        <asp:Label ID="vlDeliveryStation11" runat="server" Font-Names="Century Gothic" Text="_______________" Font-Size="Small" Font-Bold="False"></asp:Label>
                    </strong>
                </td>
                <td class="auto-style15">
                    <strong>&nbsp;<asp:Label ID="lbRequestDate13" runat="server" Font-Names="Century Gothic" Text="Load By : " Font-Size="X-Small"></asp:Label>
                        <asp:Label ID="vlDeliveryStation10" runat="server" Font-Names="Century Gothic" Text="_______________" Font-Size="Small" Font-Bold="False"></asp:Label>
                    </strong>
                </td>
            </tr>
        </table>

        <table style="width: 100%;">
            <tr>
                <td>
                    <strong>
                        <asp:Label ID="Label8" runat="server" Font-Names="Century Gothic" Text="*FI-FO/EXPIRE for suggestion only.Actuals may vary." Font-Size="XX-Small" Font-Bold="False"></asp:Label>
                    </strong>
                </td>
            </tr>
        </table>

    </form>

</body>
</html>
