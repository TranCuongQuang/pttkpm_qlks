var app = angular.module('QLKS', ['ui.select2', 'ui.bootstrap', 'datatables', 'angular.filter']);
app.controller('BookingRoomCtrl', function ($scope, $http, $timeout, $window) {
    $scope.RoomList = [];
    $scope.CustomerList = [];
    $scope.ServiceList = [];
    $scope.txtRoomId = "";
    $scope.ddlCustomer = {};
    $scope.ddlService = "";
    $scope.ddlProduct = "";
    $scope.txtFromDate = moment().format("DD/MM/YYYY");
    $scope.txtToDate = moment().format("DD/MM/YYYY");
    $scope.txtUnitPrice = 0;
    $scope.txtTotalMoney = 0;

    angular.element(document).ready(function () {
        $scope.GetRoom();
        $scope.GetCustomer();
        $scope.GetService();
    });

    $scope.showModal = function (item) {
        console.log("item", item);

        if (item.TrangThai == 0) {
            $scope.ItemRoom = item;
            $scope.txtRoomId = item.MaPhong;

            $("#modalBooking").modal();
        } else {
            toastr.warning("Phòng đã đặt không thể đặt nữa. Xin vui lòng chọn phòng trống.");
        }
    }

    $scope.GetRoom = function () {
        var params = {
            MaPhong: "",
            TenPhong: "",
            TrangThai: ""
        }
        $http({
            url: `/WebServiceCP.aspx?action=GetRoomList`,
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: params
        }).then(function (response) {
            var temp = response.data.Data;
            console.log("GetRoom", response);
            $scope.RoomList = temp;
        }, function (err) {
            $scope.RoomList = [];
            console.log(err);
        });
    }

    $scope.GetCustomer = function () {
        var params = {
            MaKH: "",
            TenKH: ""
        }
        $http({
            url: `/WebServiceCP.aspx?action=GetCustomerList`,
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: params
        }).then(function (response) {
            var temp = response.data.Data;
            $scope.CustomerList = temp;
            console.log("GetCustomer", response);
        }, function (err) {
            $scope.CustomerList = [];
            console.log(err);
        });
    }

    $scope.GetService = function () {
        var params = {
            MaDV: "",
            TenDV: ""
        }
        $http({
            url: `/WebServiceCP.aspx?action=GetServiceList`,
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: params
        }).then(function (response) {
            var temp = response.data.Data;
            $scope.ServiceList = temp;
        }, function (err) {
            $scope.ServiceList = [];
            console.log(err);
        });
    }
})