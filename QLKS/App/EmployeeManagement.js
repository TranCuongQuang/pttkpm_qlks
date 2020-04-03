var app = angular.module('QLKS', ['ui.select2', 'ui.bootstrap', 'datatables']);
app.controller('EmployeeManagementCtrl', function ($scope, $http, $timeout, $window) {
    $scope.txtUserName = "";
    $scope.txtPassWord = "";

    angular.element(document).ready(function () {
        //debugger
    });

    $scope.searchEmp = function () {
        var params = {
            userName: "a",
            passWord: "b"
        }
        console.log("params", params);
        $http({
            url: `/WebServiceCP.aspx?action=GetEmpList`,
            method: "GET",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        }).then(function (response) {
            console.log("response:", response);
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
})