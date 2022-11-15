  interval  = window.setInterval(getData, 3000);
 

$(document).ready(function () {
    getData(); 
}); 

function getData() {
   
    var gparam = getUrlParameter('jobType');

    if (gparam == false)
        gparam = ""; 
    var ddls = $("#ddlLocation").val();

    //var paran1 = [gparam, ddls]
    //var obj = { "param1": paran1 }

    var obj = { "param1": gparam, "param2": $("#ddlLocation").val(), "param3": $("#ddlDays").val() };
   // var obj = { "param1": gparam };
     
    var $tbl = $("#tbl");
    $.ajax({
        url: 'Frm_Requisition_List.aspx/GetDataXR',
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        success: function (data) {
            

            $tbl.empty();
            $tbl.append('<thead class="thead-dark"><tr class ="table-info"><th>RequisitionNo</th><th>Status</th><th>Approve by</th><th>Transfer to</th><th>Delivery Station</th><th>Detail</th></tr></thead>');
     
            $tbl.append('<tbody class="table-striped" >')
            if (data.d.length > 0) {
                var newdata = data.d;
                var rows = [];

                for (var i = 0; i < newdata.length; i++) {
                    // var   mmd = $.md5(newdata[i].REQ_NUM);
                      
                    if (newdata[i].REQ_APPROVE_BY == "") {
                        var strApprove = '';
                    } else { 
                       var strApprove = newdata[i].REQ_APPROVE_BY;
                    }

                    var vpacking = newdata[i].PACKING;

                    if (newdata[i].PACKING == "") {
                        var strPacking = '';
                    } else {

                        var strPacking = newdata[i].PACKING;
                    }
                                         
                    var strDetail = '';
                    if (strApprove == '' && strPacking == '') {
                        strDetail = '<a href="Frm_report_R.aspx?RequestNo=' + newdata[i].REQ_NUM + '" class="btn btn-info    mb-1 mb-lg-0"><i class="fas fa-info-circle"></i> Detail</a>';
                    }

                    var strPrints = '<a href="Frm_report_R_print.aspx?RequestNo=' + newdata[i].REQ_NUM + '" class="btn btn-warning    mb-1 mb-lg-0" target="_blank"><i class="fas fa-print"></i> Print</a>'


                    rows.push('<tr><td>' + newdata[i].REQ_NUM + (newdata[i].STATE_NEW == 'new' ? '<kbd>' + newdata[i].STATE_NEW + '</kbd>' : '') + ' </td><td><b>' + newdata[i].STATUS + '</b></td><td>' + newdata[i].APPROVED_BY + '</td><td>' + newdata[i].TRANSFER_TO + '</td><td>' + newdata[i].DELIVERY_STATE + '</td><td class="text-right">' + strPacking + '' + strApprove + strDetail + ' ' + strPrints + '</td></tr>')
                }

                $tbl.append(rows.join(''));
            } else {
                $tbl.append('<tr><td colspan="6"><i class="far fa-folder"></i> data not found</td></tr>');
            }




            $tbl.append('</tbody>')
        },
        error: function (ex) {
            alert('error!' + ex.responseText);
            console.log(ex.responseText);
        }
    });

};

$(function () {
    $('#slide-submenu').on('click', function () {
        $(this).closest('.list-group').fadeOut('slide', function () {
            $('.mini-submenu').fadeIn();
        });
    });
    $('.mini-submenu').on('click', function () {
        $(this).next('.list-group').toggle('slide');
        $('.mini-submenu').hide();
    });
});





function readTableData() {
    var marks = [];
    var table = document.getElementById("dataTable");
    var column_count = table.rows[1].cells.length;
    var row = table.rows[1];
    if (column_count > 0) {
        for (var index = 0; index < column_count; index++) {
            marks[index] = row.cells[index].getElementsByName('inputcell' + index)[0].value;
            //Or marks[index] = document.getElementsByName('inputcell' + index)[0].value;
        }
    }
    return marks;
}

  function getUrlParameter(sParam) {
        var sPageURL = window.location.search.substring(1),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
            }
        }
        return false;
    };


function ReversePicking(strReq_nun) {


    var confirms = confirm("Are you sure to 'Reverse Picking ??")
    if (confirms ==true) {

    
    var obj = { Requisition : strReq_nun}

        $.ajax({
            url: 'Frm_Requisition_List.aspx/ReversePickingProcess',
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
        });
    }
};