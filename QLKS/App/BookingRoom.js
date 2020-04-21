var app = angular.module('QLKS', ['ui.select2', 'ui.bootstrap', 'datatables', 'angular.filter']);
app.controller('BookingRoomCtrl', function ($scope, $http, $timeout, $window) {
    $scope.RoomList = [];
    $scope.CustomerList = [];
    $scope.ServiceList = [];
    $scope.ProductList = [];
    $scope.ChooseProductList = [];
    $scope.txtRoomId = "";
    $scope.ddlCustomer = {};
    $scope.ddlService = {};
    $scope.ddlProduct = {};
    $scope.txtFromDate = moment().format("DD/MM/YYYY");
    $scope.txtToDate = moment().format("DD/MM/YYYY");
    $scope.txtUnitPrice = 0;
    $scope.txtTotalMoney = 0;

    angular.element(document).ready(function () {
        $scope.GetRoom();
        $scope.GetCustomer();
        $scope.GetService();
        $scope.GetProduct();
    });

    $scope.showModal = function (item) {
        //console.log("item", item);

        if (item.TrangThai == 0) {
            $scope.ItemRoom = item;
            $scope.txtRoomId = item.MaPhong;
            $scope.txtUnitPrice = item.DonGia;
            var days = moment($scope.txtToDate, "DD/MM/YYYY").diff(moment($scope.txtFromDate, "DD/MM/YYYY"), "days") + 1;
            $scope.txtTotalMoney = days * item.DonGia;

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
            console.log("GetService", response);
        }, function (err) {
            $scope.ServiceList = [];
            console.log(err);
        });
    }

    $scope.GetProduct = function () {
        var params = {
            MaSP: "",
            TenSP: ""
        }
        $http({
            url: `/WebServiceCP.aspx?action=GetProductList`,
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: params
        }).then(function (response) {
            var temp = response.data.Data;
            $scope.ProductList = temp;
            console.log("GetProduct", response);
        }, function (err) {
            $scope.ProductList = [];
            console.log(err);
        });
    }

    $scope.ChangeToDate = function () {
        if (!$scope.txtToDate || $scope.txtToDate == "") {
            toastr.warning("Không được bỏ trống ngày đến.");
            return;
        }

        if (moment($scope.txtToDate, "DD/MM/YYYY").isBefore(moment($scope.txtFromDate, "DD/MM/YYYY").format("YYYY-MM-DD"))) {
            toastr.warning("Đến ngày phải lớn hơn từ ngày.");
            return;
        }

        var days = moment($scope.txtToDate, "DD/MM/YYYY").diff(moment($scope.txtFromDate, "DD/MM/YYYY"), "days") + 1;
        $scope.txtTotalMoney = days * $scope.txtUnitPrice;
    }

    $scope.ChangeFromDate = function () {
        if (!$scope.txtFromDate || $scope.txtFromDate == "") {
            toastr.warning("Không được bỏ trống ngày đến.");
            return;
        }

        if (moment($scope.txtToDate, "DD/MM/YYYY").isBefore(moment($scope.txtFromDate, "DD/MM/YYYY").format("YYYY-MM-DD"))) {
            toastr.warning("Từ ngày phải nhỏ hơn đến ngày.");
            return;
        }

        var days = moment($scope.txtToDate, "DD/MM/YYYY").diff(moment($scope.txtFromDate, "DD/MM/YYYY"), "days") + 1;
        $scope.txtTotalMoney = days * $scope.txtUnitPrice;
    }

    $scope.ChooseProduct = function () {
        var item = _.find($scope.ProductList, { MaSP: ($scope.ddlProduct != "" && $scope.ddlProduct ? parseInt($scope.ddlProduct) :0 )});
        //$scope.ChooseProductList.push(item);

        //console.log("ChooseProduct", $scope.ddlProduct, item, $scope.ChooseProductList);

        
    }
})