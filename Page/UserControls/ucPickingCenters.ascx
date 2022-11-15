<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPickingCenters.ascx.cs" Inherits="StoreRequisition.Page.UserControls.ucPickingCenters" %>
<%--<script src="../../Scripts/Site/Report_SupvApproved.js"></script>--%>
<script type="text/javascript" src="<%= Page.ResolveUrl("~/Scripts/Site/Report_PickingCenter.js") %>"></script>
 
<div>
    <div class="table-responsive">
        <table id="TblItemList" class="newStyle1 w-100 table table-bordered border-dark">
            <thead>
                <tr class="auto-style24">
                    <th class=""><strong>Item</strong></th>
                    <th class=""><strong>P/N</strong></th>
                    <th class=""><strong>Shelf</strong></th>
                    <th class="col-h"><strong>FI-FO/EXPIRE *</strong></th>
                    <th class=""><strong>UOM</strong></th>
                    <th class=""><strong>QTY Req</strong></th>
                    <th class=""><strong>Actual</strong></th>
                    <th class="col-h"><strong>Pending</strong></th>
                </tr>
            </thead>
            <tbody>
                <%=getWhileLoopData()%>
            </tbody>
        </table>
    </div>
    <br />
    <table style="width: 100%;" border="1" cellspacing="0" class=" table table-bordered border-dark">
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
</div>
<div>
    <div>
        <hr />
        <div class="row">
            <div class="col mb-3 ">
                <strong>
                    <asp:Label ID="Label1" runat="server" Font-Names="Century Gothic" Text="Picking center" Font-Size="Large" CssClass="auto-style8" Font-Italic="False" Font-Overline="False"  ></asp:Label>
                </strong>
            </div>
        </div>
       <%-- <div class="row">
            <div class="col">
                <asp:Label ID="Label2" runat="server" Font-Names="Century Gothic" Text="Picking Center decision" Font-Size="Medium" CssClass="auto-style8"></asp:Label>
            </div>
        </div>--%>
        <div class="row">
            <div class="col">
                <div class="btn-group" role="group" aria-label="Basic example">
                    <button id="btn_Pickin_item" type="button" name="btn_Pickin_item" class="btn btn-success ">Confirm Picking</button>
                    <button id="btn_Cancel" type="button" name="btn_Pickin_item" class="btn btn-outline-danger">Cancel </button>
                    <%--<a class="btn btn-light" href="Frm_Requisition_List.aspx?jobType=ActOwner">BACK</a>--%>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:Label ID="lblMSG" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</div>
