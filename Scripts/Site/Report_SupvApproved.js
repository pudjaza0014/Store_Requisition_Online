//const { Alert } = require("../bootstrap.bundle.js");

$(document).ready(function () {
    $("#btnApproved").on("click", function () {
        var ddl_desicions = $('#ddlDecision').val();
        var txt_remarks = $('#txtRemark').val();
        ;
        if (ddl_desicions == null) {
            
            alert("Please Choose your Desivion");
            return;
        } else if (ddl_desicions == 'Reject' && txt_remarks == '') {
            
            alert("Please Tell the reason in the remarks box.");
            return;
        }
        if (confirm("Confirm your decision ?")) { 
           


            getApproveRequest(ddl_desicions, txt_remarks);
        } else {
            return;
        }
    });
});

function getApproveRequest(ddl_desicions, txt_remarks) {
    try {  
        var obj = { ddl_decision: ddl_desicions, txt_remark: txt_remarks }; 
        $.ajax({
            url: 'Frm_Report_R.aspx/ConfirmDecision',
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                if (data.d.length > 0) {
                    
                    if (data.d[0] == 'OK') {
                        alert('Approved Success');
                        document.location.href = "../Page/Frm_Requisition_List.aspx?jobType=ActOwner";
                        return true;
                    } else {
                        alert(data.d[1]);
                        console.log(data.d[1]);
                        return false;
                    }


                  //  alert(data.d[0]);
                }
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