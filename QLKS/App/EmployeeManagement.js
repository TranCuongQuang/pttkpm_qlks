var app = angular.module('QLKS', ['ui.select2', 'ui.bootstrap', 'datatables']);
app.controller('EmployeeManagementCtrl', function ($scope, $http, $timeout, $window) {

    $scope.dataTable = [];
    var requiredList = document.getElementsByClassName('input-required');

    $scope.dtOptions = {
        "bStateSave": true,
        "aLengthMenu": [[15, 50, 100, -1], [15, 50, 100, 'All']],
        "bSort": true,
        "language": window.datatableLanguage
    };

    angular.element(document).ready(function () {
        $scope.searchEmp();
    });

    $(document).on("click", ".modal123", function (e) {
        e.preventDefault();
        var _self = $(this);
        var myBookId = _self.data('id');
        if (myBookId === "Create") {
            $("#titleheader").text("Thêm mới");
            $("#update").hide();
            $("#insert").show();
            clearValueModal();
        } else if (myBookId === "Update") {
            $("#titleheader").text("Cập nhật");
            $("#insert").hide();
            $("#update").show();
            var maNV = _self.data('value');
            $scope.getEmpByID(maNV);
        } else {
            $("#titleheader").text("Thông tin");
            $("#insert").hide();
            $("#update").hide();
            var maNV = _self.data('value');
            $scope.getEmpByID(maNV);
        }
    });

    $(document).on("click", "#DeleteEmp", function (e) {
        e.preventDefault();
        var _self = $(this);
        var maNV = _self.data('value');
        $scope.DeleteEmp(maNV);
    });

    $scope.searchEmp = function () {
        var params = {
            MaNV: $("#EmployeeID").val(),
            TenNV: $("#EmployeeName").val()
        }
        $http({
            url: `/WebServiceCP.aspx?action=GetEmpList`,
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: params
        }).then(function (response) {
            var temp = response.data.Data;
            if (temp.length > 0) {
                $scope.dataTable = temp;
            } else {
                $scope.dataTable = [];
            }
        }, function (err) {
            //toastr.error("Xảy ra lỗi trong quá trình thực thi.");
            $scope.dataTable = [];
            console.log(err);
        });

        //$http({
        //    url: `/WebServiceCP.aspx?action=GetEmpList`,
        //    method: "POST",
        //    headers: {
        //        'Content-Type': 'application/json; charset=utf-8'
        //    },
        //    data: params
        //}).then(function (response) {
        //    console.log("response:", response);
        //}, function (err) {
        //    //toastr.error("Xảy ra lỗi trong quá trình thực thi.");
        //    console.log(err);
        //});

        //$.ajax({
        //    type: "POST",
        //    //contentType: "application/json; charset=utf-8",
        //    url: "./WebServiceCP.aspx/GetEmpList",
        //    data: JSON.stringify(params),
        //    dataType: "json",
        //    success: function (data) {
        //        console.log("response:", data);
        //    },
        //    error: function (err) {
        //        console.log(err);
        //    }
        //});
    }

    $scope.getEmpByID = function (ID) {
        var params = {
            MaNV: ID
        }
        $http({
            url: `/WebServiceCP.aspx?action=GetEmpByID`,
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: params
        }).then(function (response) {
            console.log("response:", response);
            var temp = response.data.Data;
            if (temp.length > 0) {
                setValueModal(temp[0]);
            }
        }, function (err) {
            //toastr.error("Xảy ra lỗi trong quá trình thực thi.");
            console.log(err);
        });
    }

    $scope.InsertEmp = function () {
        var checkRequired = validForm();
        if (!checkRequired) {
            toastr.warning("Vui lòng điền đầy đủ thông tin !");
            return false;
        }
        var params = {
            TenNV: $("#txtMEmployeeName").val(),
            SDT: $("#txtMSDT").val(),
            Email: $("#txtMEmail").val(),
            DiaChi: $("#txtMAddress").val(),
            NgaySinh: moment($("#txtMBirthday").val(), "DD/MM/YYYY").format("YYYY-MM-DD")
            ChucVu: $("#txtMRole").val()
        }
        $http({
            url: `/WebServiceCP.aspx?action=CreateEmp`,
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: params
        }).then(function (response) {
            if (response.data.Message === "SUCCESS") {
                $scope.searchEmp();
                toastr.success("Lưu thành công !");
            } else {
                toastr.error("Lưu thất bại !");
            }
        }, function (err) {
            toastr.error("Xảy ra lỗi trong quá trình thực thi.");
            console.log(err);
        });
    }

    $scope.UpdateEmp = function () {
        var checkRequired = validForm();
        if (!checkRequired) {
            toastr.warning("Vui lòng điền đầy đủ thông tin !");
            return false;
        }
        var params = {
            MaNV: $("#txtMEmployeeID").val().trim(),
            TenNV: $("#txtMEmployeeName").val().trim(),
            SDT: $("#txtMSDT").val().trim(),
            Email: $("#txtMEmail").val().trim(),
            DiaChi: $("#txtMAddress").val().trim(),
            NgaySinh: moment($("#txtMBirthday").val(), "DD/MM/YYYY").format("YYYY-MM-DD")
            ChucVu: $("#txtMRole").val().trim()
        }
        $http({
            url: `/WebServiceCP.aspx?action=UpdateEmp`,
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: params
        }).then(function (response) {
            if (response.data.Message === "SUCCESS") {
                $scope.searchEmp();
                toastr.success("Lưu thành công !");
            } else {
                toastr.error("Lưu thất bại !");
            }
        }, function (err) {
            toastr.error("Xảy ra lỗi trong quá trình thực thi.");
            console.log(err);
        });
    }

    $scope.DeleteEmp = function (maNV) {
        if (confirm("Bạn có chắc muốn xoá dữ liệu ???")) {
            var params = {
                MaNV: maNV
            }
            $http({
                url: `/WebServiceCP.aspx?action=DeleteEmp`,
                method: "POST",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                data: params
            }).then(function (response) {
                console.log("response:", response);
                if (response.data.Message === "SUCCESS") {
                    $scope.searchEmp();
                    toastr.success("Xoá thành công !");
                } else if (response.data.Message === "PDP_EXIST") {
                    toastr.warning("Nhân viên đang còn làm việc !");
                } else {
                    toastr.error("Xoá thất bại !");
                }
            }, function (err) {
                toastr.error("Xảy ra lỗi trong quá trình thực thi.");
                console.log(err);
            });
        }
    }

    setValueModal = function (e) {
        $("#txtMEmployeeID").val(e.MaNV);
        $("#txtMEmployeeName").val(e.TenNV);
        $("#txtMSDT").val(e.SDT);
        $("#txtMEmail").val(e.Email);
        $("#txtMAddress").val(e.DiaChi);
        $("#txtMBirthday").val(e.NgaySinh);
        $("#txtMRole").val(e.ChucVu);
    }
    clearValueModal = function () {
        $("#txtMEmployeeID").val("");
        $("#txtMEmployeeName").val("");
        $("#txtMSDT").val("");
        $("#txtMEmail").val("");
        $("#txtMAddress").val("");
        $("#txtMBirthday").val("");
        $("#txtMRole").val("");
    }

    //valid
    function required(i) {
        requiredList[i].style.borderColor = "red";
    }

    function reset_effect(i) {
        requiredList[i].style.borderColor = "#D5D5D5";
    }

    function validForm() {
        var flag = true;
        if (requiredList.length > 0) {
            for (var i = 0; i < requiredList.length; i++) {
                if (requiredList[i].value.trim() === '') {
                    required(i);
                    flag = false;
                } else {
                    reset_effect(i);
                }
            }
        }

        return flag;
    }

    $scope.showModal = function () {
        //mở modal
        $("#modal-table").modal();
    }
})