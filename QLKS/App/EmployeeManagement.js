var lstRequired = document.getElementsByClassName('entry_required');
//var lstVoucher = [];
var voucherSearchModel = {
    DateFrom: null,
    DateTo: null,
    TransactionNo: null,
    TransactionID: 'V02',
    Description: null,
    Status: null,
    Reviewed: null,
    PartnerID: null,
    CashBookID: null
};

var dom = "<'row'<'col-sm-6'lB><'col-sm-6'f>>" +
    "<'row'<'col-sm-12'tr>>" +
    "<'row'<'col-sm-5'i><'col-sm-7'p>>";

$(document).ready(function () {
    console.log("test");
    //GetTrans();
    var date = new Date();
    var firstDay = new Date(date.getFullYear(), date.getMonth(), 1).format('dd/MM/yyyy');
    var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0).format('dd/MM/yyyy');
    $('#txtTransactionDateFrom').val(firstDay);
    $('#txtTransactionDateTo').val(lastDay);
    //
    //SearchVoucherTop100();
    BindingDataTableVoucher();
    //
    //InitAutoCompletex_Search('cboPartner', 3, AccND.configs.baseApi + '/Common/autocomplex/partner?');
    InitAutoCompletexCombo("partner", null, 2, "cboPartner", "txtPartnerID", "txtPartnerName", null, null, false, $("[id$=txtPartnerID]").val(), $("[id$=txtPartnerName]").val());
    initComboBox('cboCashBookID', AccND.configs.baseApi + "/Common/combobox/cashbookpermiss", false, 300);
});



function GetTrans() {
    $.getJSON(AccND.configs.baseApi + "/Common/combobox/transaction", { type: "VOUCHER" }, function (data) {
        $("#cboTrans").append($("<option></option>").val("").html("---Chọn tất cả---"));
        $.each(data, function (key, val) {
            $("#cboTrans").append($("<option></option>").val(val.Id).html(val.Id + "-" + val.NameCompare));
        });
        $("#cboTrans").select2().val("").trigger("change");
    });
};

//function SearchVoucherTop100() {

//    $.ajax({
//        type: "GET",
//        url: AccND.configs.baseApi + "/ACCE/Voucher/GetListTop100",
//        dataType: "json",
//        data: { transactionID: 'V02' },
//        success: function (msg) {
//            if (msg != "" && msg.length > 0) {
//                var objList = $.parseJSON(msg);
//                lstVoucher = objList;
//                BindingDataTableVoucher(objList);
//            } else {
//                ToastrInfo(iTotal, DSAlert, "00010");
//                lstVoucher = [];
//                DestroyTableVoucher();
//            }
//            ajaxindicatorstop();
//        },
//        error: function (result) {
//            ToastrError(iTotal, DSAlert, "00021");
//            lstVoucher = [];
//            DestroyTableVoucher();
//            ajaxindicatorstop();
//        }
//    });
//}

//------------------------Table--------------------------------------
function BindingDataTableVoucher(data) {
    $("#tableVoucher").DataTable().destroy();
    $("#tableVoucher tbody").empty(); // empty tbody
    $("#tableVoucher").DataTable({
        bAutoWidth: false,
        ordering: true,
        info: true,
        bFilter: false,
        processing: false,
        serverSide: true,
        lengthMenu: [[10, 25, 50, 100], [10, 25, 50, 100]],
        pageLength: AccND.configs.pageSize,
        ajax: {
            type: "POST",
            url: AccND.configs.baseApi + "/ACCE/Voucher/SearchVoucher",
            data: function (data) {
                var dataSearch = {};

                dataSearch.Draw = data.draw;
                dataSearch.PageIndex = data.start;
                dataSearch.PageSize = data.length;

                var colIndex = data.order[0].column;
                var colName = GetSortColumnNameFromIndex(colIndex, data.columns);
                dataSearch.SortItem = colName;
                dataSearch.SortDirection = data.order[0].dir;

                dataSearch.DateFrom = voucherSearchModel.DateFrom;
                dataSearch.DateTo = voucherSearchModel.DateTo;
                dataSearch.TransactionNo = voucherSearchModel.TransactionNo;
                dataSearch.TransactionID = voucherSearchModel.TransactionID;
                dataSearch.Description = voucherSearchModel.Description;
                dataSearch.Status = voucherSearchModel.Status;
                dataSearch.Reviewed = voucherSearchModel.Reviewed;
                dataSearch.PartnerID = voucherSearchModel.PartnerID;
                dataSearch.CashBookID = voucherSearchModel.CashBookID;
                dataSearch.FunctionID = FunctionID;

                return JSON.stringify(dataSearch);
            },
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            dataSrc: function (json) {
                ajaxindicatorstop();

                return json.data;
            },
            error: function (result) {
                ToastrError(iTotal, DSAlert, "00021");
                ajaxindicatorstop();
            }
        },
        "buttons": [
            {
                "text": '<i class="fusion-li-icon fa fa-file-excel-o"></i>&nbsp;Excel',
                "extend": "excel",
                "title": "Danh sách chứng từ thu chi",
                "exportOptions": {
                    "columns": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
                    "orthogonal": "export"
                }
            }
        ],
        "aoColumns": [
            {
                'title': '<label class="pos-rel">' +
                    '<input type="checkbox" class="ace" name="chkAll" id="chkAll" onclick="checkAllInput()"/>' +
                    '<span class="lbl"></span>' +
                    '</label >',
                'width': '10px'
            },
            { "data": "STT", "className": "text-center", "width": 60 },
            {
                "data": "TransactionDate",
                "render": function (data, type, full, meta) {
                    return moment(full.TransactionDate, "YYYY-MM-DD").format("DD/MM/YYYY");
                }
            },
            {
                "data": "TransactionNo",
                "render": function (data, type, full, meta) {
                    return '<span title="Xem chi tiết" onclick="ShowVoucherDetail(\'' +
                        full.RowKey +
                        '\',\'' +
                        full.TransactionId +
                        '\',\'' +
                        moment(full.TransactionDate, "YYYY-MM-DD").format("DD/MM/YYYY") +
                        '\')" class="tooltip-success" style="cursor: pointer;">' +
                        '<i class="red2 bigger-110" style="color: #3f668d!important;">' + data + '</i></span>&nbsp;';
                }
            },
            //{
            //    "data": "TransactionName",
            //    "render": function (data, type, full, meta) {
            //        return "<div class='clRowNotDown'>" + data + "</div>";
            //    }
            //},
            {
                "data": "Status",
                "render": function (data, type, full, meta) {
                    if (full.IsStatus === '1') {
                        return '<div style="text-align: center">' + data + " (" + full.StatusLog + ")" + "</div>";
                    } else {
                        return '<div style="text-align: center">' + data + "</div>";
                    }
                }
            },
            {
                "data": "Reviewed",
                "render": function (data, type, full, meta) {
                    if (full.IsReviewed === 1) {
                        return '<div style="text-align: center">' + data + " (" + full.ReviewedLog + ")" + "</div>";
                    } else {
                        return '<div style="text-align: center">' + data + "</div>";
                    }
                }
            },
            { "data": "Description" },
            { "data": "UserID" },
            //{ "data": "DescriptionDtail" },
            { "data": "DebitAccountID" },
            { "data": "CreditAccountID" },
            {
                "data": "Amount",
                "render": function (data, type, full, meta) {
                    return '<div style="text-align: right">' + ((full.Amount != null) ? accounting.formatNumber(full.Amount) : "") + "</div>";
                }
            },
            { "data": "ExpenseID" }
        ],
        "aaSorting": [[1, 'desc']],
        'columnDefs': [{
            'targets': [0], /* column index */
            'orderable': false, /* true or false */
            'className': 'dt-body-center text-center',
            'render': function (data, type, full, meta) {
                return '<input type="checkbox" class="chkItem" name="chkItem" id="chkItem" value="' + $('<div/>').text(full.RowKey).html() + '">';
            }
        }],
        select: {
            style: "multi"
        }
    });
}

function ShowVoucherDetail(rowKey, transID, tranDate) {
    ShowInsertAndShowDetailInTransaction('V02', rowKey, false, tranDate);
}

function DestroyTableVoucher() {
    $("#tableVoucher").DataTable().destroy();
    $("#tableVoucher tbody").empty(); // empty tbody
}

function SearchVoucher() {
    ajaxindicatorstart(GetAlertText(iTotal, DSAlert, "00019"));

    var rek = Validate();
    if (!rek) {
        ToastrWarning(iTotal, DSAlert, "00009");
        ajaxindicatorstop();
    } else {

        voucherSearchModel = {
            DateFrom: moment($("[id$=txtTransactionDateFrom]").val(), "DD/MM/YYYY").format("YYYYMMDD"),
            DateTo: moment($("[id$=txtTransactionDateTo]").val(), "DD/MM/YYYY").format("YYYYMMDD"),
            TransactionNo: $("[id$=txtTransactionNo]").val(),
            TransactionID: 'V02',
            Description: $("[id$=txtDescription]").val().trim(),
            Status: $("#drpStatus option:selected").val().trim(),
            Reviewed: $("#drpReviewed option:selected").val().trim(),
            PartnerID: $("[id$=cboPartner]").val(),
            CashBookID: $('#cboCashBookID').combobox('getValue').trim(),
            FunctionID: FunctionID
        };

        BindingDataTableVoucher();

    }
}

function ExportExcelVoucher() {
    ajaxindicatorstart(GetAlertText(iTotal, DSAlert, "00019"));

    var rek = Validate();
    if (!rek) {
        ToastrWarning(iTotal, DSAlert, "00009");
        ajaxindicatorstop();
    } else {
        var fileName = "Danh sách phiếu chi tiền mặt.xlsx";
        var exportExcelVoucherSearchViewModel = {
            DateFrom: moment($("[id$=txtTransactionDateFrom]").val(), "DD/MM/YYYY").format("YYYYMMDD"),
            DateTo: moment($("[id$=txtTransactionDateTo]").val(), "DD/MM/YYYY").format("YYYYMMDD"),
            TransactionNo: $("[id$=txtTransactionNo]").val(),
            TransactionID: 'V02',
            Description: $("[id$=txtDescription]").val().trim(),
            Status: $("#drpStatus option:selected").val().trim(),
            Reviewed: $("#drpReviewed option:selected").val().trim(),
            PartnerID: $("[id$=cboPartner]").val(),
            CashBookID: $('#cboCashBookID').combobox('getValue').trim(),
            FunctionID: FunctionID,
            FileName: fileName
        };

        postForm(AccND.configs.baseApi + "/ACCE/Voucher/ExportExcelVoucher", exportExcelVoucherSearchViewModel);
        ajaxindicatorstop();
    }
}

function DeleteMultiVoucher() {
    var jsonDataMultiVoucher = GetMultiRow();
    if (jsonDataMultiVoucher === "") {
        ToastrWarning(iTotal, DSAlert, "00010");
        return;
    }
    if (confirm(GetAlertText(iTotal, DSAlert, "00012"))) {

        var objParams = {
            JsonData: jsonDataMultiVoucher,
            FunctionID: FunctionID,
            TransactionID: "V02"
        };
        ajaxindicatorstart(GetAlertText(iTotal, DSAlert, "00019"));
        $.ajax({
            type: 'DELETE',
            url: AccND.configs.baseApi + '/ACCE/Voucher/' + 'DeleteMultiVoucher',
            headers: { 'FunctionID': FunctionID },
            dataType: 'json',
            data: objParams,
            success: function (msg) {
                switch (msg.Message) {
                    case 'E_EXC':
                        ToastrError(iTotal, DSAlert, "00022");
                        break;
                    case 'F_ERR':
                        ToastrError(iTotal, DSAlert, "00018");
                        break;
                    case 'F_APP_EXIST':
                        ToastrWarning(iTotal, DSAlert, "00030");
                        break;
                    case 'F_NFAPP':
                        ToastrWarning(iTotal, DSAlert, "00014");
                        break;
                    case 'F_APPED':
                        ToastrWarning(iTotal, DSAlert, "00030");
                        break;
                    case 'F_ID_EXIST':
                        ToastrWarning(iTotal, DSAlert, "00011");
                        break;
                    case 'F_ID_NEXIST':
                        ToastrWarning(iTotal, DSAlert, "00010");
                        break;
                    case 'SUCCESS':
                        ToastrSuccess(iTotal, DSAlert, "00017");
                        SearchVoucher();
                        break;
                    default:
                        break;
                }

                ajaxindicatorstop();
            }, error: function (err) {
                if (err.status === 401 || err.status === 403) {
                    toastr.error(err.status + ' ' + err.statusText, 'Thông báo - Alert');
                } else {
                    ToastrError(iTotal, DSAlert, "00021");
                }
                ajaxindicatorstop();
            }
        });
    }
}
//-----------------------checkbox---------------------------------------------

function checkAllInput() {
    if ($('#chkAll').is(":checked")) {
        $('.chkItem').prop("checked", true);
    } else {
        $('.chkItem').prop("checked", false);
    }
}

function GetMultiRow() {
    var values = "";
    $('#tableVoucher').find('input[type="checkbox"]:checked').each(function (index, rowId) {
        var temp = $(this).closest('tr input').attr('value');
        if (String(temp) !== String("undefined")) {
            if (values === "")
                values = temp;
            else
                values = values + "," + temp;
        }
    });
    return values;
}
//------------------------Valid--------------------------------------
function required(i) {
    lstRequired[i].style.borderColor = "red";
}

function reset_effect(i) {
    lstRequired[i].style.borderColor = "#D5D5D5";
}

function Validate() {
    var flag = true;
    if (lstRequired.length > 0) {
        for (var i = 0; i < lstRequired.length; i++) {
            if (lstRequired[i].value.trim() === '') {
                required(i);
                flag = false;
            } else {
                reset_effect(i);
            }
        }
    }
    return flag;
}
//--------------------------------------------------------------------

//Show popup voucher
function onClickShowVoucherNew() {
    ShowInsertAndShowDetailInTransaction('V02', 0, true);
}