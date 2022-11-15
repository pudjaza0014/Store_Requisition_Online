<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucSupvApproveds.ascx.cs" Inherits="StoreRequisition.Page.UserControls.ucSupvApproveds" %>
<%--<script src="../../Scripts/Site/Report_SupvApproved.js"></script>--%>
<script type="text/javascript" src="<%= Page.ResolveUrl("~/Scripts/Site/Report_SupvApproved.js") %>"></script>
<div>

    <div class="table-responsive">
        <table id="TblItemList" class="newStyle1  table table-bordered border-dark">
            <thead>
                <tr class="auto-style24">
                    <th class=""><strong>Item</strong></th>
                    <th class=""><strong>P/N</strong></th>
                    <th class=""><strong>Shelf</strong></th>
                    <th class="col-h"><strong>FI-FO/EXPIRE *</strong></th>
                    <th class="col-h"><strong>UOM</strong></th>
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
    <div class="table-responsive">
        <table border="1" cellspacing="0" class=" table table-bordered border-dark">
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
                        <asp:Label ID="vlDeliveryStation12" runat="server" Font-Names="Century Gothic" Text="_____________" Font-Size="Small" Font-Bold="False"></asp:Label>
                    </strong>
                </td>
                <td class="auto-style15">
                    <strong>&nbsp;<asp:Label ID="lbRequestDate12" runat="server" Font-Names="Century Gothic" Text="Load Finish : " Font-Size="X-Small"></asp:Label>
                        <asp:Label ID="vlDeliveryStation11" runat="server" Font-Names="Century Gothic" Text="_____________" Font-Size="Small" Font-Bold="False"></asp:Label>
                    </strong>
                </td>
                <td class="auto-style15">
                    <strong>&nbsp;<asp:Label ID="lbRequestDate13" runat="server" Font-Names="Century Gothic" Text="Load By : " Font-Size="X-Small"></asp:Label>
                        <asp:Label ID="vlDeliveryStation10" runat="server" Font-Names="Century Gothic" Text="_____________" Font-Size="Small" Font-Bold="False"></asp:Label>
                    </strong>
                </td>
            </tr>
        </table>
    </div>
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
            <div class="col text-center">
                <strong>
                    <asp:Label ID="Label1" runat="server" Font-Names="Century Gothic" Text="Approved" Font-Size="Large" CssClass="auto-style8" Font-Italic="False" Font-Overline="False" Font-Underline="True"></asp:Label>
                </strong>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:Label ID="Label2" runat="server" Font-Names="Century Gothic" Text="Supervisor's decision" Font-Size="Medium" CssClass="auto-style8"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-12 pb-1"> 
                <label for="ddlDecision">Decision Approved/Reject</label>
                <select name="ddlDecision" id="ddlDecision" class="form-control" aria-label="" >
                    <option selected="selected" disabled="disabled">Please Select ...</option>
                    <option value="Approved">Approved</option>
                    <option value="Reject">Reject</option>
                </select>
            </div>
            <div class="col-12 pb-1">
                <textarea id="txtRemark" cols="20" rows="2" class="form-control" placeholder="Remark"></textarea>
            </div>
            <div class="col-12">
                <hr />
                <button id="btnApproved" type="button" name="btnApproved" class="btn  btn-primary ">SUBMIT</button> <a class ="btn btn-light" href="Frm_Requisition_List.aspx?jobType=ActOwner">BACK</a>
            </div>
        </div>

    </div>
</div>
