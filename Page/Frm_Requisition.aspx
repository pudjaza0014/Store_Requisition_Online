<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Frm_Requisition.aspx.cs" Inherits="Frm_Priority_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            text-align: right;
        }

        .auto-style2 {
            text-align: center;
        }

        .auto-style2-MSG {
            text-align: center;
            color:red;
            font-size:larger;
        }
        .auto-style4 {
            width: 60%;
            height: 26px;
        }
        .auto-style5 {
            width: 100%;
        }
        .auto-style6 {
            text-align: right;
            height: 26px;
        }
        .auto-style7 {
            color: rgb(255, 0, 0);
            font-size: xx-small;
        }
    </style>
    <script src="../Scripts/DataTables/jQuery-3.6.0/jquery-3.6.0.min.js"></script>  
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
    <%--<script src="../Scripts/Site/Requisition_List.js"></script>--%>
    <script>
        $("#btnSubmit").click(function () {
            alert("1");
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Div0" runat="server">
        <fieldset style="width: 100%; margin: auto; padding: 0">
            <legend>Store's Material Requisition</legend>
            <table class="auto-style5">
                <tr>
                    <td style="width: 40%;" class="auto-style1">
                        <strong>
                            <asp:Label ID="Label42" runat="server" Font-Names="Century Gothic" Text="Requisition Number : "></asp:Label>
                        </strong>
                    </td>
                    <td style="width: 60%;">
                        <asp:TextBox ID="txtRequestNum" runat="server" Width="295px" OnTextChanged="txtPartNo_TextChanged" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1">
                        &nbsp;</td>
                    <td style="width: 60%;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1"><strong>
                        <asp:Label ID="Label4" runat="server" Font-Names="Century Gothic" Text="Location : "></asp:Label>
                    </strong>
                    </td>
                    <td style="width: 60%;">
                        <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="True" Width="300px" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" Font-Names="Century Gothic">
                            <asp:ListItem>Please Select ...</asp:ListItem>
                            <asp:ListItem>MMCT</asp:ListItem>
                            <%--<asp:ListItem>MPCT</asp:ListItem>--%>
                            <asp:ListItem>LF</asp:ListItem>
                           <%-- <asp:ListItem>Store B4</asp:ListItem>
                            <asp:ListItem>Store B15</asp:ListItem>
                            <asp:ListItem>W/H Hora</asp:ListItem>
                            <asp:ListItem>LF Chemical</asp:ListItem>
                            <asp:ListItem>LF RM</asp:ListItem>
                            <asp:ListItem>Spare Part B4</asp:ListItem>
                            <asp:ListItem>Spare Part B2</asp:ListItem> --%>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1">
                        <strong>
                            <asp:Label ID="Label29" runat="server" Font-Names="Century Gothic" Text="Delivery Station : "></asp:Label>
                        </strong>
                    </td>
                    <td style="width: 60%;">
                        <asp:TextBox ID="txtDeliveryStation" runat="server" Width="295px" OnTextChanged="txtDeliveryStation_TextChanged" AutoPostBack="True" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1">
                        <strong>
                            <asp:Label ID="Label33" runat="server" Font-Names="Century Gothic" Text="Request By : "></asp:Label>
                        </strong>
                    </td>
                    <td style="width: 60%;">
                        <asp:TextBox ID="txtRequestBy" runat="server" Width="295px" OnTextChanged="txtRequestBy_TextChanged"   AutoPostBack="True" Enabled="False"></asp:TextBox>
                            <asp:Label ID="Label45" runat="server" Font-Names="Century Gothic" Text="Ex : 1A016950" CssClass="auto-style7"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1">
                        <strong>
                            <asp:Label ID="Label46" runat="server" Font-Names="Century Gothic" Text="Tel : "></asp:Label>
                        </strong>
                    </td>
                    <td style="width: 60%;">
                        <asp:TextBox ID="txtRequesTel" runat="server" Width="295px" OnTextChanged="txtRequesTel_TextChanged" AutoPostBack="True" Enabled="False"></asp:TextBox>
                            <asp:Label ID="Label47" runat="server" Font-Names="Century Gothic" Text="Ex : 3713" CssClass="auto-style7"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1">
                        <strong>
                            <asp:Label ID="Label2" runat="server" Font-Names="Century Gothic" Text="Request Date : "></asp:Label>
                        </strong>
                    </td>
                    <td style="width: 60%;">
                        <asp:TextBox ID="txtRequestDate" runat="server" Width="295px" OnTextChanged="txtPartNo_TextChanged" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1">
                        <strong>
                            <asp:Label ID="Label44" runat="server" Font-Names="Century Gothic" Text="Issue Type : "></asp:Label>
                        </strong>
                    </td>
                    <td style="width: 60%;">
                        <asp:DropDownList ID="ddlIssueType" runat="server" AutoPostBack="True" Width="300px" OnSelectedIndexChanged="ddlIssueType_SelectedIndexChanged" Font-Names="Century Gothic" Enabled="False">
                            <asp:ListItem>Please Select ...</asp:ListItem>
                            <asp:ListItem>Material Transfer</asp:ListItem>
                            <asp:ListItem>OS Issue</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1">
                        <strong>
                            <asp:Label ID="lbIssue" runat="server" Font-Names="Century Gothic" Text="Sub Inventory : "></asp:Label>
                        </strong>
                    </td>
                    <link href="../css/MMCT.css" rel="stylesheet" />
                    <td style="width: 60%;">
                        <asp:DropDownList ID="ddlIssue" runat="server" AutoPostBack="True" Width="300px" Font-Names="Century Gothic" Enabled="False" OnSelectedIndexChanged="ddlIssue_SelectedIndexChanged">
                            <asp:ListItem>Please Select ...</asp:ListItem>
                        </asp:DropDownList>
                        <strong>
                            <asp:TextBox ID="txtInventory" runat="server" Width="295px" OnTextChanged="txtPartNo_TextChanged" Visible="False"></asp:TextBox>
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1">
                        <strong>
                            <asp:Label ID="Label48" runat="server" Font-Names="Century Gothic" Text="Reason : "></asp:Label>
                        </strong>
                    </td>
                    <td style="width: 60%;">
                        <asp:DropDownList ID="ddlReason" runat="server" AutoPostBack="True" Width="300px" OnSelectedIndexChanged="ddlReason_SelectedIndexChanged" Font-Names="Century Gothic" Enabled="False">
                            <asp:ListItem>Please Select ...</asp:ListItem>
                            <asp:ListItem>Repair</asp:ListItem>
                            <asp:ListItem>PM</asp:ListItem>
                            <asp:ListItem>Production Use</asp:ListItem>
                            <asp:ListItem>Stock</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1">
                        <strong>
                            <asp:Label ID="Label49" runat="server" Font-Names="Century Gothic" Text="Machine Name : "></asp:Label>
                        </strong>
                    </td>
                    <td style="width: 60%;">
                        <asp:TextBox ID="txtMachineName" runat="server" Width="295px" OnTextChanged="txtMachineName_TextChanged" AutoPostBack="True" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1">
                        <strong>
                            <asp:Label ID="Label50" runat="server" Font-Names="Century Gothic" Text="Process Name : "></asp:Label>
                        </strong>
                    </td>
                    <td style="width: 60%;">
                        <asp:TextBox ID="txtProcessName" runat="server" Width="295px" OnTextChanged="txtProcessName_TextChanged" AutoPostBack="True" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1">
                        <strong>
                            <asp:Label ID="Label51" runat="server" Font-Names="Century Gothic" Text="Area : "></asp:Label>
                        </strong>
                    </td>
                    <td style="width: 60%;">
                        <asp:TextBox ID="txtArea" runat="server" Width="295px" OnTextChanged="txtArea_TextChanged" AutoPostBack="True" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1"><strong>
                        <asp:Label ID="Label12" runat="server" Font-Names="Century Gothic" Text="Remark : "></asp:Label>
                    </strong></td>
                    <td style="width: 60%;">
                        <asp:TextBox ID="txtRemark" runat="server" Width="295px" Height="66px" TextMode="MultiLine" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1">&nbsp;</td>
                    <td style="width: 60%;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style6"><strong>
                        <asp:Label ID="Label6" runat="server" Font-Names="Century Gothic" Text="Part No : "></asp:Label>
                    </strong>
                    </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtPartNo" runat="server" Width="295px" AutoPostBack="True" Enabled="False" OnTextChanged="txtPartNo_TextChanged"></asp:TextBox>
                        <strong>
                            <asp:Button ID="btnPartNo" runat="server" Text="..." UseSubmitBehavior="False" TabIndex="30" Width="35px" Font-Bold="False" Font-Names="Century Gothic" OnClick="btnPartNo_Click" Height="22px" Visible="False" />
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1"><strong>
                        <asp:Label ID="Label13" runat="server" Font-Names="Century Gothic" Text="Description : "></asp:Label>
                    </strong>
                    </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtPartName" runat="server" Width="295px" OnTextChanged="txtPartNo_TextChanged" Enabled="False" Height="66px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1"><strong>
                        <asp:Label ID="Label14" runat="server" Font-Names="Century Gothic" Text="Shelf : "></asp:Label>
                    </strong>
                    </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtShelf" runat="server" Width="295px" Enabled="False">-</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1"><strong>
                        <asp:Label ID="Label15" runat="server" Font-Names="Century Gothic" Text="FI-FO/EXPIRE* : "></asp:Label>
                    </strong>
                    </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtExpire" runat="server" Width="295px" Enabled="False"></asp:TextBox>
                        <asp:TextBox ID="txtExpire0" runat="server" Width="295px" Visible="False" Enabled="False"></asp:TextBox>
                        <asp:TextBox ID="txtExpire1" runat="server" Width="295px" Enabled="False" Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6"><strong>
                        <asp:Label ID="Label8" runat="server" Font-Names="Century Gothic" Text="Uom : "></asp:Label>
                    </strong></td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtUom" runat="server" Width="295px" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6"><strong>
                        <asp:Label ID="Label43" runat="server" Font-Names="Century Gothic" Text="On-Hand : "></asp:Label>
                    </strong>
                    </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtOnHand" runat="server" Width="295px" Enabled="False"></asp:TextBox>
                        <asp:TextBox ID="txtOnHand0" runat="server" Width="295px" Visible="False" Enabled="False"></asp:TextBox>
                        <asp:TextBox ID="txtOnHand1" runat="server" Width="295px" Visible="False" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1"><strong>
                        <asp:Label ID="Label7" runat="server" Font-Names="Century Gothic" Text="Qty : "></asp:Label>
                    </strong>
                    </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtQty" runat="server" Width="295px" AutoPostBack="True" OnTextChanged="txtQty_TextChanged" Enabled="False"></asp:TextBox>
                        <asp:TextBox ID="txtQty0" runat="server" Width="295px" AutoPostBack="True" OnTextChanged="txtQty_TextChanged" Enabled="False" Visible="False"></asp:TextBox>
                    </td>
                </tr>
                </table>
            <div id="Div1" runat="server">

                <table style="width: 100%; margin-top: 0px;">
                    <tr>
                        <td style="width: 40%;" class="auto-style1">&nbsp;</td>
                        <td style="width: 60%;">
                            <strong>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" UseSubmitBehavior="False" TabIndex="30" Width="80px" Font-Bold="False" Font-Names="Century Gothic" OnClick="btnAdd_Click" Enabled="False" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" UseSubmitBehavior="False" Font-Bold="False" Font-Names="Century Gothic" OnClick="btnClear_Click" Enabled="False" />
                            </strong>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
        </fieldset>
    </div>
    <br />
    <div runat="server" id="Div4">
        <fieldset style="width: 100%; margin: auto; padding: 0">
            <legend>Item List</legend>
            <asp:DataGrid ID="dtgHeader" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#99CCFF" BorderStyle="Double" BorderWidth="3px" CellPadding="1" CellSpacing="1" Font-Bold="False" Font-Italic="False" Font-Names="Century Gothic" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="Horizontal" ShowFooter="True" Width="100%" OnItemCommand="dtgHeader_ItemCommand">
                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Century Gothic" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <Columns>
                    <asp:BoundColumn DataField="Item" HeaderText="Item"></asp:BoundColumn>
                    <asp:BoundColumn DataField="PN" HeaderText="P/N">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Shelf" HeaderText="Shelf">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="EXPIRE" HeaderText="FI-FO/EXPIRE *">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="UOM" HeaderText="UOM"></asp:BoundColumn>
                    <asp:BoundColumn DataField="QTY" HeaderText="QTY Req">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                    </asp:BoundColumn>
                    <asp:ButtonColumn ButtonType="PushButton" CommandName="Delete" HeaderText="Delete" Text="Delete">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Century Gothic" Font-Overline="False" Font-Size="10pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Century Gothic" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                    </asp:ButtonColumn>
                </Columns>
                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Century Gothic" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <FooterStyle BackColor="#336666" Font-Bold="False" Font-Italic="False" Font-Names="Century Gothic" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                <HeaderStyle BackColor="#336666" Font-Bold="True" Font-Italic="False" Font-Names="Century Gothic" Font-Overline="False" Font-Size="10pt" Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Century Gothic" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle" />
                <PagerStyle BackColor="#336666" Font-Bold="False" Font-Italic="False" Font-Names="Century Gothic" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" VerticalAlign="Middle" />
                <SelectedItemStyle BackColor="#339966" Font-Bold="True" Font-Italic="False" Font-Names="Century Gothic" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
            </asp:DataGrid>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </fieldset>
        <br />
        <table style="width: 100%;">
            <tr>

            </tr>
            <tr>
                
                 <td class="auto-style2"  >
                   <strong>
                            <asp:Label ID="Label1" runat="server" Font-Names="Century Gothic" Text="Approved by"></asp:Label>
                        </strong>
                </td>
            </tr>
            <tr>
                
                 <td class="auto-style2"  >
                    <asp:DropDownList ID="ddlApprovedby" runat="server" AutoPostBack="True" Width="300px" Font-Names="Century Gothic" Enabled="False">
                        <asp:ListItem>Please Select ...</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
            </tr>
             
            <tr> 
                 
                <td class="auto-style2"  >
                    <strong>                      
                        <asp:Button ID="btnSubmit"  runat="server" Text="Submit" Width="117px" UseSubmitBehavior="False" Font-Bold="False" Font-Names="Century Gothic" OnClick="btnSubmit_Click" Enabled="False" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnExport0" runat="server" Text="Clear" Width="117px" UseSubmitBehavior="False" Font-Bold="False" Font-Names="Century Gothic" OnClick="btnClear_Click" Enabled="False" />
                    </strong>




                </td>
                  
            </tr>
             <tr>
                <td class="auto-style2-MSG"><asp:Label ID="lblMSGs" runat="server" Font-Names="Century Gothic" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
            </tr>
        </table>
    </div>

</asp:Content>
