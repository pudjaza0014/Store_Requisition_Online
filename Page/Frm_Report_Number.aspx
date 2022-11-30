<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Frm_Report_Number.aspx.cs" Inherits="Frm_Report_Number" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            text-align: right;
        }

        .auto-style5 {
            width: 100%;
        }
        .auto-style6 {
            text-align: center;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Div0" runat="server">
        <fieldset style="width: 100%; margin: auto; padding: 0">
            <legend>Store's Material Requisition Report By Number</legend>
            <table class="auto-style5">
                <tr>
                    <td style="width: 40%;" class="auto-style1">
                        <strong>
                            <asp:Label ID="Label29" runat="server" Font-Names="Century Gothic" Text="Number From : "></asp:Label>
                        </strong>
                    </td>
                    <td style="width: 60%;">
                        <asp:TextBox ID="txtRequestNumFrom" runat="server" Width="295px" AutoPostBack="True" OnTextChanged="txtRequestNumFrom_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1">
                        <strong>
                            <asp:Label ID="Label50" runat="server" Font-Names="Century Gothic" Text="Number To : "></asp:Label>
                        </strong>
                    </td>
                    <td style="width: 60%;">
                        <asp:TextBox ID="txtRequestNumTo" runat="server" Width="295px" AutoPostBack="True" Enabled="False" OnTextChanged="txtRequestNumTo_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1"><strong>
                        <asp:Label ID="Label4" runat="server" Font-Names="Century Gothic" Text="Location : "></asp:Label>
                    </strong>
                    </td>
                    <td style="width: 60%;">
                        <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="True" Width="300px" Font-Names="Century Gothic" Enabled="False" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                            <asp:ListItem>Please Select ...</asp:ListItem>
                            <asp:ListItem>MMCT</asp:ListItem>
                            <asp:ListItem>MPCT</asp:ListItem>
                            <asp:ListItem>LF</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1"><strong>
                        <asp:Label ID="Label51" runat="server" Font-Names="Century Gothic" Text="Status : "></asp:Label>
                    </strong>
                    </td>
                    <td style="width: 60%;">
                        <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" Width="300px" Font-Names="Century Gothic" Enabled="False" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                            <asp:ListItem>Please Select ...</asp:ListItem>  
                            <asp:ListItem>DRAFT</asp:ListItem>
                            <asp:ListItem>INCOMPLETE</asp:ListItem>
                            <asp:ListItem>INPROCESS</asp:ListItem>
                            <asp:ListItem>APPROVED</asp:ListItem>
                            <asp:ListItem>ON PROCESS PICKING</asp:ListItem>
                            <asp:ListItem>SUBMIT</asp:ListItem>
                            <asp:ListItem>ISSUE</asp:ListItem>
                            <asp:ListItem>RECEIVED</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1">&nbsp;</td>
                    <td style="width: 60%;">
                        <strong>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="117px" UseSubmitBehavior="False" Font-Bold="False" Font-Names="Century Gothic" OnClick="btnSubmit_Click" Enabled="False" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" Width="117px" UseSubmitBehavior="False" Font-Bold="False" Font-Names="Century Gothic" Enabled="False" />
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="auto-style1">&nbsp;</td>
                    <td style="width: 60%;">
                        &nbsp;</td>
                </tr>
            </table>
        </fieldset>
    </div>
    <br />
    <div runat="server" id="Div4" visible="false">
        <fieldset style="width: 100%; margin: auto; padding: 0">
            <legend>Item List</legend>
            <table style="width: 100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style6">
                        <strong>
                            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" Width="117px" UseSubmitBehavior="False" Font-Bold="False" Font-Names="Century Gothic" OnClick="btnExport_Click" />
                        </strong>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:GridView ID="dgvMachine" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Names="Century Gothic" Font-Size="8pt" HorizontalAlign="Center" ShowFooter="True" Width="50%">
                            <Columns>
                                <asp:BoundField DataField="REQ_DATE" HeaderText="REQ_DATE" />
                                <asp:BoundField DataField="REQ_NUM" HeaderText="REQ_NUM" />
                                <asp:BoundField DataField="ITEM_NUM" HeaderText="ITEM_NUM" />
                                <asp:BoundField DataField="PART_NUM" HeaderText="PART_NUM" />
                                <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" />
                                <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                <asp:BoundField DataField="QTY_REQ" HeaderText="QTY_REQ" />
                                <asp:BoundField HeaderText="ACTUAL" DataField="ACTUAL" />
                                <asp:BoundField DataField="PENDING" HeaderText="PENDING" />
                                <asp:BoundField DataField="ISSUE_TYPE" HeaderText="ISSUE_TYPE" />
                                <asp:BoundField DataField="SUB_INV" HeaderText="SUB_INV" />
                                <asp:BoundField DataField="TRANS_TYPE" HeaderText="TRANS_TYPE" />
                                <asp:BoundField DataField="REQ_BY" HeaderText="REQ_BY" />
                                <asp:BoundField DataField="DELIVERY_STATION" HeaderText="DELIVERY_STATION" />
                            </Columns>
                            <FooterStyle BackColor="#336666" ForeColor="#336666" />
                            <HeaderStyle BackColor="#336666" Font-Bold="True" Font-Names="Century Gothic" Font-Size="9pt" ForeColor="White" />
                            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FFF1D4" />
                            <SortedAscendingHeaderStyle BackColor="#B95C30" />
                            <SortedDescendingCellStyle BackColor="#F1E5CE" />
                            <SortedDescendingHeaderStyle BackColor="#93451F" />
                        </asp:GridView>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                </table>
        </fieldset>
        <br />

    </div>

</asp:Content>
