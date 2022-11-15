<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucUserReceived.ascx.cs" Inherits="StoreRequisition.Page.UserControls.ucUserReceived" %>
<script type="text/javascript" src="<%= Page.ResolveUrl("~/Scripts/Site/Report_UserRecieved.js") %>"></script>


<%--<script>
    $('.collapse').collapse();
</script>--%>
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
                    <asp:Label ID="Label1" runat="server" Font-Names="Century Gothic" Text="USER RECIEVE MATERIAL REQUISITION" Font-Size="Large" CssClass="auto-style8" Font-Italic="False" Font-Overline="False" Font-Underline="True"></asp:Label>
                </strong>
            </div>
        </div>

        <div class="row">
            <div class="col">
                <div class="accordion" id="accordionExample">
                    <div class=" mt-2">

                        <button class="btn btn-block btn-outline-success " type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                            STORE'S ISSUE SLIP
                        </button>


                        <div id="collapseOne" class="collapse" aria-labelledby="headingOne" data-parent="#accordionExample">
                            <div class="card card-body"> 
                                <div class="row mb-2 ">
                                    <div class="col-md-8 col-12">
                                        <div class=" row ">
                                            <div class="col-md-3 text-right">DATE :</div>
                                            <div class="col-md-9 text-left">
                                                <asp:Label ID="lblDate" runat="server" Font-Names="Century Gothic" Text="_______________" Font-Size="Small" Font-Bold="False"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3 text-right">TIME :</div>
                                            <div class="col-md-9 text-left">
                                                 <asp:Label ID="lblTime" runat="server" Font-Names="Century Gothic" Text="_______________" Font-Size="Small" Font-Bold="False"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3 text-right">TO :</div>
                                            <div class="col-md-9 text-left">
                                                <asp:Label ID="issTO"  runat="server" Font-Names="Century Gothic" Text="_______________" Font-Size="Small" Font-Bold="False"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                               
                                    <div class=" col-md-4 col-12">
                                        <div class="row">
                                           <div class="col-md-3 text-right">NO :</div>
                                           <div class="col-md-9 text-left">
                                                <asp:Label ID="issNO" runat="server" Font-Names="Century Gothic" Text="_______________" Font-Size="Small" Font-Bold="False"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3 text-right">REF :</div>
                                            <div class="col-md-9 text-left">
                                                <asp:Label ID="issREF" runat="server" Font-Names="Century Gothic" Text="_______________" Font-Size="Small" Font-Bold="False"></asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                             <div class="row">
                                    <div class="col table-responsive"> 
                                        <table class=table table-sm table-striped table-hover text-center" style="font-size:1rem;">
                                            <thead class=" ">
                                                <tr class="">
                                                    <th>NO.</th>
                                                    <th>PART NAME</th>
                                                    <th>FORM</th>
                                                    <th>UNIT</th>
                                                    <th>QUANTITY.</th>
                                                    <th>LABEL</th>
                                                    <th>INVOICE NO</th>
                                                    <th>LOTNO</th>
                                                </tr>
                                            </thead>
                                            <tbody class="text-sm">
                                                <%=getMaterialIssue() %>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12"> 
                                        <div class="row">
                                            <div class="col">
                                                <asp:Label ID="Label2" runat="server" Font-Names="Century Gothic" Text="User Recieve Material Requisition" Font-Size="Medium" CssClass="auto-style8"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-12">
                                                <button id="btnApproved" type="button" name="btnApproved" class="btn btn-primary">RECEIVE MATERIAL</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <hr />





    </div>
</div>
