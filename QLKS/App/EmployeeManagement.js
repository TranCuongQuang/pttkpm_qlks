var app = angular.module('QLKS', ['ui.select2', 'ui.bootstrap', 'datatables']);
app.controller('EmployeeManagementCtrl', function ($scope, $http, $timeout, $window) {
    $scope.txtUserName = "";
    $scope.txtPassWord = "";
    $scope.dataTable = []

    angular.element(document).ready(function () {
        $scope.searchEmp();
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

    $scope.SaveEmp = function () {
        var params = {
            nameStaff: $("#txtMEmployeeName").val(),
            phone: $("#txtMSDT").val(),
            email: $("#txtMEmail").val(),
            address: $("#txtMAddress").val(),
            birthday: $("#txtMBirthday").val(),
            role: $("#txtMRole").val()
        }
        $http({
            url: `/WebServiceCP.aspx?action=SaveEmp`,
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