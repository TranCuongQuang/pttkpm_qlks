var app = angular.module('QLKS', ['ui.select2', 'ui.bootstrap', 'datatables']);
app.controller('EmployeeManagementCtrl', function ($scope, $http, $timeout, $window) {
    $scope.txtUserName = "";
    $scope.txtPassWord = "";
    $scope.dataTable = []

    angular.element(document).ready(function () {
        $scope.searchEmp();
    });

    $(document).on("click", ".modal123", function (e) {
        e.preventDefault();
        var _self = $(this);
        var myBookId = _self.data('id');
        debugger
        if (myBookId === "Create") {
            $("#titleheader").text("Thêm mới");
            $("#update").remove();
        } else if (myBookId === "Update") {
            $("#titleheader").text("Cập nhật");
            $("#insert").remove();
            var maNV = _self.data('value');
            $scope.getEmpByID(maNV);
        } else {
            $("#titleheader").text("Thông tin");
            $("#insert").remove();
            $("#update").remove();

        }
    });

    $scope.searchEmp = function () {
        var params = {
            userName: "a",
            passWord: "b"
        }
        $http({
            url: `/WebServiceCP.aspx?action=GetEmpList`,
            method: "GET",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        }).then(function (response) {
            console.log("response:", response);
            var temp = response.data.Data;
            if (temp.length > 0) {
                $scope.dataTable = temp;
            }
            
        }, function (err) {
            //toastr.error("Xảy ra lỗi trong quá trình thực thi.");
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
            debugger
            var temp = response.data.Data;
            //if (temp.length > 0) {
            //    $scope.dataTable = temp;
            //}

        }, function (err) {
            //toastr.error("Xảy ra lỗi trong quá trình thực thi.");
            console.log(err);
        });
    }

    $scope.InsertEmp = function () {
        var params = {
            TenNV: $("#txtMEmployeeName").val(),
            SDT: $("#txtMSDT").val(),
            Email: $("#txtMEmail").val(),
            DiaChi: $("#txtMAddress").val(),
            NgaySinh: $("#txtMBirthday").val(),
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
            console.log("response:", response);
            
        }, function (err) {
            //toastr.error("Xảy ra lỗi trong quá trình thực thi.");
            console.log(err);
        });
    }

    $scope.UpdateEmp = function () {
        var params = {
            MaNV: $("#txtMEmployeeID").val(),
            TenNV: $("#txtMEmployeeName").val(),
            SDT: $("#txtMSDT").val(),
            Email: $("#txtMEmail").val(),
            DiaChi: $("#txtMAddress").val(),
            NgaySinh: $("#txtMBirthday").val(),
            ChucVu: $("#txtMRole").val()
        }
        $http({
            url: `/WebServiceCP.aspx?action=UpdateEmp`,
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: params
        }).then(function (response) {
            console.log("response:", response);

        }, function (err) {
            //toastr.error("Xảy ra lỗi trong quá trình thực thi.");
            console.log(err);
        });
    }

    $scope.DeleteEmp = function () {
        var params = {
            MaNV: $("#txtMEmployeeID").val()
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

        }, function (err) {
            //toastr.error("Xảy ra lỗi trong quá trình thực thi.");
            console.log(err);
        });
    }
})