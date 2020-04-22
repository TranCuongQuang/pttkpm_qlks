var app = angular.module('QLKS', ['ui.select2', 'ui.bootstrap', 'datatables', 'angular.filter']);
app.controller('BookingRoomManagerCtrl', function ($scope, $http, $timeout, $window) {
    $scope.RoomList = [];
    $scope.CustomerList = [];
    $scope.BookingRoomList = [];
    $scope.ChooseProductList = [];
    $scope.ChooseServiceList = [];
    $scope.ddlCustomer = "";
    $scope.ddlRoom = "";
    $scope.ddlService = "";
    $scope.ddlProduct = "";
    $scope.txtPhone = "";
    $scope.txtRoomId = "";
    $scope.txtCustomer = "";
    $scope.txtFromDate = "";
    $scope.txtToDate = "";
    $scope.txtUnitPrice = 0;
    $scope.txtTotalMoney = 0;
    $scope.txtQuantityService = 1;
    $scope.txtQuantityProduct = 1;
    $scope.lblTotalMoneyProduct = 0;
    $scope.lblTotalMoneyService = 0;
    $scope.ChooseBookingRoom = {};
    $scope.txtAllTotalMoney = 0;

    $scope.dtOptions = {
        "bStateSave": true,
        "aLengthMenu": [[15, 50, 100, -1], [15, 50, 100, 'All']],
        "bSort": true,
        "language": window.datatableLanguage
    };

    angular.element(document).ready(function () {
        $scope.GetRoom();
        $scope.GetCustomer();
        $scope.GetService();
        $scope.GetProduct();
    });

    $scope.GetRoom = function () {
        var params = {
            MaPhong: "",
            TenPhong: "",
            TrangThai: "1"
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

    $scope.GetBookingRoom = function () {
        var roomId = $scope.ddlRoom;
        var customerId = $scope.ddlCustomer;
        var phone = $scope.txtPhone;

        if (!roomId || roomId == "") {
            toastr.warning("Vui lòng chọn phòng.");
            return;
        }

        if (!customerId || customerId == "") {
            toastr.warning("Vui lòng chọn khách hàng.");
            return;
        }

        $http({
            url: `/WebServiceCTQ.aspx?action=GetBookingRoom&RoomId=${roomId}&CustomerId=${customerId}&Phone=${phone}`,
            method: "GET",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: {}
        }).then(function (response) {
            var temp = response.data.Data;
            console.log("GetBookingRoom", response);
            $scope.BookingRoomList = temp;
            if (temp.length == 0) {
                toastr.warning("Không tìm thấy danh sách phòng đã đặt.")
            }
        }, function (err) {
            $scope.BookingRoomList = [];
            console.log(err);
        });
    }

    $scope.ClearData = function () {
        $scope.ChooseProductList = [];
        $scope.ChooseServiceList = [];
        $scope.ddlService = "";
        $scope.ddlProduct = "";
        $scope.txtRoomId = "";
        $scope.txtCustomer = "";
        $scope.txtFromDate = "";
        $scope.txtToDate = "";
        $scope.txtUnitPrice = 0;
        $scope.txtTotalMoney = 0;
        $scope.txtQuantityService = 1;
        $scope.txtQuantityProduct = 1;
        $scope.lblTotalMoneyProduct = 0;
        $scope.lblTotalMoneyService = 0;
        $scope.ChooseBookingRoom = {};
    }

    $scope.EditBookingRoom = function (item) {
        console.log("EditBookingRoom", item);

        $scope.ClearData();
        $scope.ChooseBookingRoom = item;
        $scope.txtRoomId = item.MaPhong;
        $scope.txtCustomer = item.TenKH;
        $scope.txtFromDate = moment(item.NgayBD).format("DD/MM/YYYY");
        $scope.txtToDate = moment(item.NgayKT).format("DD/MM/YYYY");
        $scope.txtUnitPrice = item.DonGia;
        $scope.txtTotalMoney = item.TongTien;
        $scope.ChooseProductList = item.SanPhamPhong;
        $scope.ChooseServiceList = item.DichVuPhong;
        $scope.SumMoneyProduct();
        $scope.SumMoneyService();

        $("#modalBooking").modal();
    }

    $scope.ChooseProduct = function () {
        if (!$scope.ddlProduct || $scope.ddlProduct == "") {
            toastr.warning("Vui lòng chọn sản phẩm.");
            return;
        }

        if ($scope.txtQuantityProduct < 1) {
            toastr.warning("Số lượng phải lớn hơn 0.");
            return;
        }

        var itemExist = _.find($scope.ChooseProductList, { MaSP: parseInt($scope.ddlProduct) });
        if (itemExist) {
            //toastr.warning("Sản phẩm đã được chọn.");
            var item = _.find($scope.ChooseProductList, { MaSP: parseInt($scope.ddlProduct) });
            item.SoLuong = $scope.txtQuantityProduct;
            item.ThanhTien = item.SoLuong * item.DonGia;
            $scope.ddlProduct = "";
            $scope.SumMoneyProduct();
            return;
        }

        var item = _.find($scope.ProductList, { MaSP: parseInt($scope.ddlProduct) });
        item.SoLuong = $scope.txtQuantityProduct;
        item.ThanhTien = item.SoLuong * item.DonGia;
        $scope.ChooseProductList.push(item);
        $scope.ddlProduct = "";
        $scope.SumMoneyProduct();
        console.log("ChooseProduct", $scope.ChooseProductList);
    }

    $scope.RemoveChooseProduct = function (item) {
        var evens = _.remove($scope.ChooseProductList, function (n) {
            return n.MaSP == item.MaSP;
        });
        $scope.SumMoneyProduct();
        //console.log("RemoveChooseProduct", evens);
    }

    $scope.SumMoneyProduct = function () {
        var money = _.sumBy($scope.ChooseProductList, function (o) { return o.SoLuong * o.DonGia; });
        $scope.lblTotalMoneyProduct = money;
        $scope.txtAllTotalMoney = $scope.lblTotalMoneyService + $scope.lblTotalMoneyProduct + $scope.txtTotalMoney;
        //console.log("SumMoneyProduct", money);
    }

    $scope.EditChooseProduct = function (item) {
        console.log("EditChooseProduct", item);

        $scope.txtQuantityProduct = item.SoLuong;
        $scope.ddlProduct = item.MaSP.toString();
    }

    $scope.ChooseService = function () {
        if (!$scope.ddlService || $scope.ddlService == "") {
            toastr.warning("Vui lòng chọn dịch vụ.");
            return;
        }

        if ($scope.txtQuantityService < 1) {
            toastr.warning("Số lượng phải lớn hơn 0.");
            return;
        }

        var itemExist = _.find($scope.ChooseServiceList, { MaDV: parseInt($scope.ddlService) });
        if (itemExist) {
            //toastr.warning("Dịch vụ đã được chọn.");
            var item = _.find($scope.ChooseServiceList, { MaDV: parseInt($scope.ddlService) });
            item.SoLuong = $scope.txtQuantityService;
            item.ThanhTien = item.SoLuong * item.DonGia;
            $scope.ddlService = "";
            $scope.SumMoneyService();
            return;
        }

        var item = _.find($scope.ServiceList, { MaDV: parseInt($scope.ddlService) });
        item.SoLuong = $scope.txtQuantityService;
        item.ThanhTien = item.SoLuong * item.DonGia;
        $scope.ChooseServiceList.push(item);
        $scope.ddlService = "";
        $scope.SumMoneyService();

        console.log("ChooseService", $scope.ChooseServiceList, $scope.ddlService);
    }

    $scope.RemoveChooseService = function (item) {
        var evens = _.remove($scope.ChooseServiceList, function (n) {
            return n.MaDV == item.MaDV;
        });
        $scope.SumMoneyService();
        //console.log("RemoveChooseService", evens);
    }

    $scope.SumMoneyService = function () {
        var money = _.sumBy($scope.ChooseServiceList, function (o) { return o.SoLuong * o.DonGia; });
        $scope.lblTotalMoneyService = money;
        $scope.txtAllTotalMoney = $scope.lblTotalMoneyService + $scope.lblTotalMoneyProduct + $scope.txtTotalMoney;
        //console.log("SumMoneyService", money);
    }

    $scope.EditChooseService = function (item) {
        console.log("EditChooseService", item);

        $scope.txtQuantityService = item.SoLuong;
        $scope.ddlService = item.MaDV.toString();
    }

    $scope.UpdateBookingRoom = function () {
        var params = {
            MaPhieuDP: $scope.ChooseBookingRoom.MaPhieuDP,
            DichVuPhong: $scope.ChooseServiceList,
            SanPhamPhong: $scope.ChooseProductList,
        }
        $http({
            url: `/WebServiceCTQ.aspx?action=UpdateBookingRoom`,
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: params
        }).then(function (response) {
            console.log("UpdateBookingRoom", response);
            if (response.data.Status == 0) {
                toastr.success(response.data.Message);
                $scope.GetBookingRoom();
                $("#modalBooking").modal("hide");
            } else {
                toastr.warning(response.data.Message);
            }
        }, function (err) {
            console.log(err);
        });
    }

    $scope.PaymentBookingRoom = function () {
        var params = {
            MaPhieuDP: $scope.ChooseBookingRoom.MaPhieuDP
        }
        $http({
            url: `/WebServiceCTQ.aspx?action=PaymentBookingRoom`,
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: params
        }).then(function (response) {
            console.log("PaymentBookingRoom", response);
            if (response.data.Status == 0) {
                toastr.success(response.data.Message);
                $scope.GetBookingRoom();
                $scope.GetRoom();
                $("#modalBooking").modal("hide");
            } else {
                toastr.warning(response.data.Message);
            }
        }, function (err) {
            console.log(err);
        });
    }
})