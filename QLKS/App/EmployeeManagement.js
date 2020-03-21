var app = angular.module('QLKS', ['ui.select2', 'ui.bootstrap', 'datatables']);
app.controller('LoginCtrl', function ($scope, $http, $timeout, $window) {
    $scope.txtUserName = "";
    $scope.txtPassWord = "";

    angular.element(document).ready(function () {
    });

    $scope.onload = function () {
        //var params = {
        //    userName: $scope.txtUserName,
        //    passWord: $scope.txtPassWord
        //}

        //console.log("params", params);
        //$http({
        //    url: `/WebServiceCTQ.aspx?action=Login&a='abc'`,
        //    method: "POST",
        //    headers: {
        //        'Content-Type': 'application/json; charset=utf-8'
        //    },
        //    data: params
        //}).then(function (response) {
        //    console.log("Login response:", response);
        //}, function (err) {
        //    toastr.error("Xảy ra lỗi trong quá trình thực thi.");
        //    console.log(err);
        //    });

        
    }
})