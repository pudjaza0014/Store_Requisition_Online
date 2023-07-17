$(document).ready(function () {
    autocompletess();
    $("#txtLotNo").on("change", function () {
        var strLotNo = $("#txtLotNo").val();
        if (strLotNo == "") {
            return;
        }
        $("#spError").html("");
        ValidationLotNo(strLotNo);
    });

    $("#txtLotNoToTemp").on("change", function () {
        try {
            var strLotNo = $("#txtLotNoToTemp").val();
            var strSubFrom = $("#txtSubFrom").val();
            var strLocatorFrom = $("#txtLocatorForm").val();
            var strSubTo = $("#txtSubTo").val();
            var strLocatorTo = $("#txtLocatorTo").val();

            if (strLotNo == "") {
                return;
            }
            $("#txtLotNoToTemp").val(strLotNo.toUpperCase());
            if (strSubFrom == "") {
                $("#txtLotNoToTemp").val("");
                $("#txtSubFrom").focus();
                throw '[SubInventory Form] ต้องไม่เป็นค่าว่าง กรุณากรอกข้อมูลแล้วลองอีกครั้ง';
            }
            else if (strSubTo == "") {
                $("#txtLotNoToTemp").val("");
                $("#txtSubTo").focus();
                throw '[SubInventory to] ต้องไม่เป็นค่าว่าง กรุณากรอกข้อมูลแล้วลองอีกครั้ง';
            }
            else if (strLocatorTo == "") {
                $("#txtLotNoToTemp").val("");
                $("#txtLocatorTo").focus();
                throw '[Locator To] ต้องไม่เป็นค่าว่าง กรุณากรอกข้อมูลแล้วลองอีกครั้ง';
            }

            GetLotForTransfer(strLotNo, strSubFrom, strLocatorFrom, strSubTo, strLocatorTo); 
        } catch (ex) {
            alert('error! :' + ex);
            $("#spError").html('error! :' + ex);
        }
    });

    $("#txtSubFrom").on("change", function () {
        try {
            autocompleteLocator($("#txtSubFrom").val(), $("#txtLocatorForm"));

        } catch (e) {
            alert('error! :' + ex);
            $("#spError").html('error! :' + ex);
        }
    });

    $("#txtSubTo").on("change", function () {
        try {
            autocompleteLocator($("#txtSubTo").val(), $("#txtLocatorTo"));

        } catch (e) {
            alert('error! :' + ex);
            $("#spError").html('error! :' + ex);
        }
    });

    $("#btnSubmit").on("click", function () {

        try {

            var result = confirm("โปรดยืนยันว่าคุณต้องการดำเนินการนี้โดยกดปุ่ม OK");
            if (result) {
                submit();
            } else {
                return;
            }
        } catch (e) {
            alert(e);
        }

    });
    $("#btndelError").on("click", function () {

        try {

            var result = confirm("โปรดยืนยันว่าคุณต้องการดำเนินการนี้โดยกดปุ่ม OK");
            if (result) {
                removeErrorRow();
            } else {
                return;
            }
        } catch (e) {
            alert(e);
        }

    });
    

    $("#btnRefresh").on("click", function () {
        location.reload();
    });
});

function submit() {

    try {
        var SubInventoryFrom = $("#txtSubFrom").val();
        var LocatorFrom = $("#txtLocatorForm").val();
        var SubInventoryTo = $("#txtSubTo").val();
        var LocatorTo = $("#txtLocatorTo").val();

        var obj = {
            "param1": SubInventoryFrom,
            "param2": LocatorFrom,
            "param3": SubInventoryTo,
            "param4": LocatorTo,
        };


        $.ajax({
            url: 'Frm_Storage_Suggestion.aspx/transferData',
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                var result = confirm("Transfer ข้อมูลสำเร็จ ");
                location.reload();
            },
            error: function (ex) {

                alert('error! :' + ex.responseJSON.Message);
                $("#spError").html('error! :' + ex.responseJSON.Message);
                console.log(ex.responseJSON.Message);
            }
        });

    } catch (e) {
        throw e;
    }

}



function LotToListTransfer(i) {
    try {

         
        var strLotNo = (i);
        var strSubFrom = $("#txtSubFrom").val();
        var strLocatorFrom = $("#txtLocatorForm").val();
        var strSubTo = $("#txtSubTo").val();
        var strLocatorTo = $("#txtLocatorTo").val();

        if (strLotNo == "") {
            return;
        }
        $("#txtLotNoToTemp").val(strLotNo.toUpperCase());
        if (strSubFrom == "") {
            $("#txtLotNoToTemp").val("");
            $("#txtSubFrom").focus();
            throw '[SubInventory Form] ต้องไม่เป็นค่าว่าง กรุณากรอกข้อมูลแล้วลองอีกครั้ง';
        }
        else if (strSubTo == "") {
            $("#txtLotNoToTemp").val("");
            $("#txtSubTo").focus();
            throw '[SubInventory to] ต้องไม่เป็นค่าว่าง กรุณากรอกข้อมูลแล้วลองอีกครั้ง';
        }
        else if (strLocatorTo == "") {
            $("#txtLotNoToTemp").val("");
            $("#txtLocatorTo").focus();
            throw '[Locator To] ต้องไม่เป็นค่าว่าง กรุณากรอกข้อมูลแล้วลองอีกครั้ง';
        }

        GetLotForTransfer(strLotNo, strSubFrom, strLocatorFrom, strSubTo, strLocatorTo);
    } catch (ex) {
        alert('error! :' + ex);
        $("#spError").html('error! :' + ex);
    }



}
function ValidationLotNo(i) {
    try {
        $("#spError").html("Loading Data...");

        var obj = {
            "param1": i
        };
        var $tbl = document.getElementById("tbl");
        $.ajax({
            url: 'Frm_Storage_Suggestion.aspx/GetData',
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                var it = data.d;
                $("#txtInvNo").val(it.invoiceNo);
                $("#txtItemCode").val(it.itemCode);
                $("#txtTotal").val(it.Total);
                $("#txtShelfPackCount").val(it.ShelfPackCount);
                $("#txtShelfName").val(it.ShelfName);
                $("#txtShelfCap").val(it.ShelfPackCAP);
                $("#txtCountToTemp").val(it.ScanToSTTemp);
                $("#txtTotalCountPack").val(it.TotalCountPack);
                $("#txtShelfStatus").val(it.ShelfStatus);
                $("#txtShelfStatus").addClass(it.shelfStatus_Color);

                $("#txtSubFrom").val(it.Subinventory);
                $("#txtLocatorForm").val(it.locator);
                debugger
                var rowCount = $tbl.rows.length;


                if (rowCount) {
                    var tableBody = document.querySelector('#tbl tbody');
                    while (tableBody.firstChild) {
                        tableBody.removeChild(tableBody.firstChild);
                    }
                }

                $.each(it.materials, function (index, element) {
                    console.log(element);
                    var newRow = $tbl.tBodies[0].insertRow();
                   // var cellSelects = newRow.insertCell();
                    var cellNo = newRow.insertCell();
                    var cellLotNo = newRow.insertCell();
                    var cellQty = newRow.insertCell();
                    var cellExpired = newRow.insertCell();
                    //var cellLocator = newRow.insertCell();
                    //var cellLocatorID = newRow.insertCell();
                    //cellSelects.innerHTML = ' <input type="checkbox" id="option1" class="form-control form-control-sm" name="options[' + element.LotNo + ']" value="' + element.LotNo + '">';
                    //cellSelects.innerHTML = '<input type="button" class="select_item" name="ch' + element.LotNo + '" value="' + element.LotNo + '">'
                    cellNo.innerHTML = element.NO;
                    cellLotNo.innerHTML = element.LotNo;
                    cellQty.innerHTML = element.Qty;
                    cellExpired.innerHTML = element.Expired;
                    //cellLocator.innerHTML = element.Locator;
                    //cellLocatorID.innerHTML = element.LocatorID;
                    $("#lblCntLotInvoice").text(element.NO);

                });

                $("#totalCount").html("Out of " + it.materials.length + " : ");


                $('#tbl tbody tr').click(function () {
                    var rowData = $(this).children('td').map(function () {
                        return $(this).text();
                    }).get();
                    console.log(rowData);
                    var cellData = rowData[1];
                     
                    LotToListTransfer(cellData);
                    console.log('Cell Data:', cellData);
                });


                $("#btnSelectAll").on("click", function () {

                    try {

                        var result = confirm("โปรดยืนยันว่าคุณต้องการดำเนินการนี้โดยกดปุ่ม OK");
                        if (result) {

                            $('#tbl tbody tr').each(function () {
                                // Get the content of each cell in the current row
                                var rowData = $(this).find('td').map(function () {
                                    return $(this).text();
                                }).get();
                                console.log(rowData);
                                var cellData = rowData[1];

                                LotToListTransfer(cellData);
                                console.log('Cell Data:', cellData);
                                // Do something with the row data
                                //console.log(rowData);




                            });




                        } else {
                            return;
                        }
                    } catch (e) {
                        alert(e);
                    }

                });
                 
                $("#txtLotNo").val(i.toUpperCase());
                $("#spError").html("");

                if ($("#txtSubTo").val() == "") {
                    $("#txtSubTo").focus();
                } else {
                    $("#txtLotNo").focus();
                }

            },
            error: function (ex) {

                alert('error! :' + ex.responseJSON.Message);
                $("#spError").html('error! :' + ex.responseJSON.Message);
                console.log(ex.responseJSON.Message);
            }
        });



    } catch (e) {
        alert('error!!! :' + ex.responseJSON.Message);
    }

}

function GetLotForTransfer(i, ivf, lf, ivt, lt) {
    try {
        var obj = {
            "param1": i,
            "param2": ivf,
            "param3": lf,
            "param4": ivt,
            "param5": lt,
        };
        var $tbl = document.getElementById("tbl_transfer");
        $.ajax({
            url: 'Frm_Storage_Suggestion.aspx/GetLotToTransfer',
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                 
                var element = data.d;

                displayLotTransfer(element)
                count_DisPlay(); 
                //$("#spError").html("");
                $("#txtLotNoToTemp").val(""); 
                $("#txtLotNoToTemp").focus();
                
            },
            error: function (ex) {

                alert('error! :' + ex.responseJSON.Message);
                $("#spError").html('error! :' + ex.responseJSON.Message);
                console.log(ex.responseJSON.Message);
                $("#txtLotNoToTemp").val("");
                $("#txtLotNoToTemp").focus();
            }
        });
    } catch (e) {
        alert('error! :' + ex);
        $("#spError").html('error! :' + ex);
    }
    
}

function RemovedScan(i) {
     
    //alert(i.value);
    var obj = {
        "param1": i.value,
    }
    try {

        $.ajax({
            url: 'Frm_Storage_Suggestion.aspx/RemoveScan',
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                var $tbl = document.getElementById("tbl_transfer");
                var rowCount = $tbl.rows.length;
                var tbody = $tbl.tBodies[0];
                while (tbody.firstChild) {
                    tbody.removeChild(tbody.firstChild);
                }
                var it = data.d;
                 
                $.each(it, function (index, element) { 
                    displayLotTransfer(element)
                }); 
                count_DisPlay();
                $("#txtLotNoToTemp").val("");
                $("#txtLotNoToTemp").focus();
            },
            error: function (ex) {

                alert('error! :' + ex.responseJSON.Message);
                $("#spError").html('error! :' + ex.responseJSON.Message);
                console.log(ex.responseJSON.Message);
                $("#txtLotNoToTemp").val("");
                $("#txtLotNoToTemp").focus();
            }
        });


    } catch (e) {

        alert('error! :' + ex);
        $("#spError").html('error! :' + ex);

    }
}

function displayLotTransfer(element) {
    try {

        var $tbl = document.getElementById("tbl_transfer");
        console.log(element);
        var newRow = $tbl.tBodies[0].insertRow(0);
        var cellNo = newRow.insertCell();
        var cellLotNo = newRow.insertCell();
        var cellQty = newRow.insertCell();
        var cellExpired = newRow.insertCell();
        var cellLocator = newRow.insertCell(); 
        var cellStatus = newRow.insertCell();
        var cellOP = newRow.insertCell();


        cellNo.innerHTML = element.NO;
        cellLotNo.innerHTML = element.LotNo;
        cellQty.innerHTML = element.Qty;
        cellExpired.innerHTML = element.Expired;
        cellLocator.innerHTML = element.Locator; 
        cellStatus.innerHTML = element.ScanStatus;
        cellOP.innerHTML = "<button class=\"btn btn-sm btn-danger\" value=\"" + element.NO + "\"  onclick=\"RemovedScan(this)\"  >x</button>";

        if (element.ScanStatus == "ERROR") {
            cellExpired.classList.add("bg-danger", "text-white", "small");

            $("#btnSubmit").prop('disabled', true);

        }


       
         
    } catch (ex) {
        alert('error! :' + ex);
        $("#spError").html('error! :' + ex);
    } 
}

function count_DisPlay() {
    try {
        var rowCountWithoutError = $('#tbl_transfer tbody tr:not(:contains("ERROR"))').length;
        console.log("Number of rows without 'Error': " + rowCountWithoutError);
        $("#lblCntOK").text(rowCountWithoutError);

        var errorRowCount = $('#tbl_transfer tbody tr:contains("ERROR")').length;
        console.log("Number of rows without 'Error': " + errorRowCount);
        $("#lblCntError").text(errorRowCount);
        if (errorRowCount == 0) {
            $("#btnSubmit").prop('disabled', false);
        }


    } catch (ex) {
        alert('error! :' + ex);
        $("#spError").html('error! :' + ex);
    }   


}

function removeErrorRow() {


    try {

    
    $.ajax({
        url: 'Frm_Storage_Suggestion.aspx/RemoveErrorRow',
       // data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        success: function (data) {
            var $tbl = document.getElementById("tbl_transfer");
            var rowCount = $tbl.rows.length;
            var tbody = $tbl.tBodies[0];
            while (tbody.firstChild) {
                tbody.removeChild(tbody.firstChild);
            }
            var it = data.d;

            $.each(it, function (index, element) {
                displayLotTransfer(element)     
            }); 
            count_DisPlay();
            $("#txtLotNoToTemp").val("");
            $("#txtLotNoToTemp").focus();
        },
        error: function (ex) {

            alert('error! :' + ex.responseJSON.Message);
            $("#spError").html('error! :' + ex.responseJSON.Message);
            console.log(ex.responseJSON.Message);
            $("#txtLotNoToTemp").val("");
            $("#txtLotNoToTemp").focus();
        }
    });

    } catch (ex) {
        alert('error! :' + ex);
        $("#spError").html('error! :' + ex);
    }

}

function autocompletess() {

    try {
        var obj = {
            "param1": "d"
        };
        $.ajax({
            url: 'Frm_Storage_Suggestion.aspx/GenInventory',
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                var item = data.d;
                $("#txtSubTo").autocomplete({
                    source: item
                });
                $("#txtSubFrom").autocomplete({
                    source: item
                });
            },
            error: function (ex) {

                alert('error! :' + ex);
                $("#spError").html('error! :' + ex);
                console.log(ex);
            }
        });
    } catch (e) {
        alert('error! :' + ex);
        $("#spError").html('error! :' + ex);
    }
}


function autocompleteLocator(i, ddl) {

    try {
        var obj = {
            "param1": i
        };
        $.ajax({
            url: 'Frm_Storage_Suggestion.aspx/GenLocator',
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                 
                var item = data.d;

                ddl.autocomplete({
                    source: item
                });

                //ddl.focus();
            },
            error: function (ex) {

                alert('error! :' + ex);
                $("#spError").html('error! :' + ex);
                console.log(ex);
            }
        });
    } catch (e) {
        alert('error! :' + ex);
        $("#spError").html('error! :' + ex);
    }
}