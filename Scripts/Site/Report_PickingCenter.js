$(document).ready(function () { 
    $("#spTotal").text("00.00");
    var summit = false;

    $(".numberonly").on("change", function () {

       
        
       
        var arry = readDataTable();
        var strSumm = 0;
        $.each(arry, function (index, vals) {            
            var gx = index;
           
            strSumm += parseInt((vals.ACTUAL_QTY == '' ? 0 : vals.ACTUAL_QTY));
            
        });
        $("#spTotal").text("" + parseFloat(strSumm).toFixed(2) + "");
        console.clear();







        var network = new ActiveXObject('WScript.Network'); 
        alert(network.computerName);


    });


    $("#btn_Pickin_item").on("click", function () {
        var arryItems = readDataTable();        
        var chkColums = CheckColumn(arryItems)        
        if (chkColums == true) {
            if (confirm("Confirm picking item ?")) {
                try {
                    var obj = { pickingItems: arryItems };
                    $.ajax({
                        url: 'Frm_Report_R.aspx/SaveItemsPicked',
                        data: JSON.stringify(obj),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        type: "POST",
                        success: function (data) {
                            if (data.d.length > 0) {
                                if (data.d[0] == 'OK') {
                                    $("#TblItemList").find("input,button,textarea,select").attr("disabled", "disabled");
                                    $("#btn_Pickin_item").attr("disabled", "disabled");
                                    alert('Picking Success');
                                    summit = true;
                                    location.reload();
                                    return true;
                                } else {
                                    alert(data.d[1]);
                                    console.log(data.d[1]);
                                    return false;
                                }
                            }
                        },
                        error: function (ex) {
                            alert('error!' + ex.responseText);
                            console.log(ex.responseText);
                            return;
                        }
                    })
                } catch (ex) {
                    alert('error!' + ex.responseText);
                    console.log(ex.responseText);
                    return;
                }
            } else {
                return;
            }
        } else {
            alert("ActualQty is not input. please checking and try again.");
            return;
        };
    });

    // Closing page
    onbeforeunload = (event) => {
        if (summit == false) {
        recomfirmss();
    }
    };
   
});




$(window).on('beforeunload', function (event) {
    if (summit) {
        return;
    } else {
        var s = "You have unsaved changes. Really leave?";
        event.preventDefault();
        event = event || window.event;
        if (event) {
            // This is for IE
            recomfirmss();
            event.returnValue = s;
        }
        // This is for all other browsers
        recomfirmss();
        return s;
    }
}); 
 
function readDataTable() {
    try {
        var marks = [];
        var table = document.getElementById("TblItemList");

        var column_count = table.rows[1].cells.length;
        var t_rows = $('#TblItemList tr').length - 1;
        if (column_count > 0) {
            for (var t_row = 1; t_row < t_rows; t_row++) {
              
                var row = table.rows[t_row];              
                var cell_Item = row.cells[0];
                var cell_Item_Name = row.cells[1];
                var cell_Req_num = row.cells[7]
                var cell_ReqQty = row.cells[5];
                var cell_ActualQty = row.cells[6];

                var cnActualQty = cell_ActualQty.childNodes[0].id;
                var cnReq_Num = cell_Req_num.childNodes[0].id;
                // Table
                var strItemNo = cell_Item.childNodes[0].textContent;
                var strItemName = cell_Item_Name.childNodes[0].textContent;
                var strReq_Qty = cell_ReqQty.childNodes[0].textContent.replace(",", "");
                // TextBox
                var ActualQty = $("#" + cnActualQty).val();
                var req_num = $("#" + cnReq_Num).val();
                marks.push({ "ITEM_NUM": strItemNo, "ITEM_NAME": strItemName, "REQ_NUM": req_num, "ACTUAL_QTY": ActualQty, "REQ_QTY": strReq_Qty });

                var arrItemp = [];
                arrItemp.push({ "ITEM_NUM": strItemNo, "ITEM_NAME": strItemName, "REQ_NUM": req_num, "ACTUAL_QTY": ActualQty == '' ? "0" : ActualQty, "REQ_QTY": strReq_Qty });
                SavePickingItem(arrItemp);
            }
            console.log(marks);
            return marks;
        }

    } catch (ex) {
        alert('error!' + ex.message);
        console.log(ex.message);
        return;
    }
};

function CheckColumn(arryItems) {
    try {
        var strReqSumm = 0, strActSumm = 0;
        var result;
        $.each(arryItems, function (index, vals) {
           debugger
            if (vals.ACTUAL_QTY == '') {
                
                result= false;
            } else {
                strReqSumm += parseInt((vals.REQ_QTY == '' ? 0 : vals.REQ_QTY));
                strActSumm += parseInt((vals.ACTUAL_QTY == '' ? 0 : vals.ACTUAL_QTY));
                result = true;
            }
        }); 
        return result;
    } catch (ex) {
        alert('error!' + ex.responseText);
        console.log(ex.responseText);
        return false;
    } 
}

function SavePickingItem(arr) {
    try {


        var obj = { pickingItems: arr };
        $.ajax({
            url: 'Frm_Report_R.aspx/SaveItem',
             data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                return data;
            },
            error: function (ex) {
                alert('error!' + ex.responseText);
                console.log(ex.responseText);
                return;
            }
        })






    } catch (ex) {
        alert('error!' + ex.message);
        console.log(ex.message);
        return;
    }
}

function recomfirmss() {    
    $.ajax({
        url: 'Frm_Report_R.aspx/CanclePicking',
       // data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        success: function (data) {
            return data;
        },
        error: function (ex) {
            alert('error!' + ex.responseText);
            console.log(ex.responseText);
            return;
        }
    })     
};

//function confirmitem(p_items) {
//    try {
        
//        var items = p_items.value;
       
//    } catch (ex) {

//    }
//}