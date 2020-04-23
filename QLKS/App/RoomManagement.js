var app = angular.module('QLKS', ['ui.select2', 'ui.bootstrap', 'datatables']);
app.controller('RoomManagementCtrl', function ($scope, $http, $timeout, $window) {

    $scope.dataTable = [];
    var requiredList = document.getElementsByClassName('input-required');
    $scope.ChooseEquipmentList = [];
    $scope.EquipmentList = [];
    $scope.ddlEquipment = "";
    $scope.RoomList = [];
    $scope.ddlRoom = "";
    $scope.txtQuantityEquipment = 1;
    $scope.lblTotalMoneyEquipment = 0;
    $scope.txtAllTotalMoney = 0;

    $scope.dtOptions = {
        "bStateSave": true,
        "aLengthMenu": [[15, 50, 100, -1], [15, 50, 100, 'All']],
        "bSort": true,
        "language": window.datatableLanguage
    };

    angular.element(document).ready(function () {
        $scope.searchRoom();
        $scope.GetEquipment();
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
            var maPhong = _self.data('value');
            $scope.getRoomByID(maPhong);
        } else {
            $("#titleheader").text("Thông tin");
            $("#insert").hide();
            $("#update").hide();
            var maPhong = _self.data('value');
            $scope.getRoomByID(maPhong);
        }
    });

    $(document).on("click", "#DeleteRoom", function (e) {
        e.preventDefault();
        var _self = $(this);
        var maPhong = _self.data('value');
        $scope.DeleteRoom(maPhong);
    });

    $scope.GetEquipment = function () {
        var params = {
            MaTB: "",
            TenTB: "",
            TinhTrang: ""
        }
        $http({
            url: `/WebServiceCP.aspx?action=GetEquipmentList`,
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: params
        }).then(function (response) {
            var temp = response.data.Data;
            $scope.EquipmentList = temp;

        }, function (err) {
            $scope.EquipmentList = [];
            console.log(err);
        });
    }

    $scope.ChooseEquipment = function () {
        if (!$scope.ddlEquipment || $scope.ddlEquipment == "") {
            toastr.warning("Vui lòng chọn thiết bị.");
            return;
        }

        if ($scope.txtQuantityEquipment < 1) {
            toastr.warning("Số lượng phải lớn hơn 0.");
            return;
        }

        var itemExist = _.find($scope.ChooseEquipmentList, { MaThietBi: parseInt($scope.ddlEquipment) });
        if (itemExist) {
            //toastr.warning("Thiêt bị đã được chọn.");
            var item = _.find($scope.ChooseEquipmentList, { MaThietBi: parseInt($scope.ddlEquipment) });
            item.SoLuong = $scope.txtQuantityEquipment;
            item.ThanhTien = item.SoLuong * item.DonGia;
            $scope.ddlEquipment = "";
            $scope.SumMoneyEquipment();
            return;
        }

        var item = _.find($scope.EquipmentList, { MaThietBi: parseInt($scope.ddlEquipment) });
        item.SoLuong = $scope.txtQuantityEquipment;
        item.ThanhTien = item.SoLuong * item.DonGia;
        $scope.ChooseEquipmentList.push(item);
        $scope.ddlEquipment = "";
        $scope.SumMoneyEquipment();
        console.log("ChooseEquipment", $scope.ChooseEquipmentList);
    }

    $scope.RemoveChooseEquipment = function (item) {
        var evens = _.remove($scope.ChooseEquipmentList, function (n) {
            return n.MaThietBi == item.MaThietBi;
        });
        $scope.SumMoneyEquipment();
        //console.log("RemoveChooseEquipment", evens);
        if (item.MaTTBP) {
            $scope.DeleteEquipmentRoom(item.MaTTBP);
        }
    }

    $scope.SumMoneyEquipment = function () {
        var money = _.sumBy($scope.ChooseEquipmentList, function (o) { return o.SoLuong * o.DonGia; });
        $scope.lblTotalMoneyEquipment = money;
        $scope.txtAllTotalMoney = $scope.lblTotalMoneyService + $scope.lblTotalMoneyEquipment + $scope.txtTotalMoney;
        //console.log("SumMoneyEquipment", money);
    }

    $scope.EditChooseEquipment = function (item) {
        console.log("EditChooseEquipment", item);

        $scope.txtQuantityEquipment = item.SoLuong;
        $scope.ddlEquipment = item.MaThietBi.toString();
    }

    $scope.searchRoom = function () {
        var params = {
            MaPhong: $("#RoomID").val(),
            TenPhong: $("#RoomName").val(),
            TrangThai: $("#Status").val()
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
    }

    $scope.getRoomByID = function (ID) {
        var params = {
            MaPhong: ID
        }
        $http({
            url: `/WebServiceCP.aspx?action=GetRoomByID`,
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

    $scope.InsertRoom = function () {
        var checkRequired = validForm();
        if (!checkRequired) {
            toastr.warning("Vui lòng điền đầy đủ thông tin !");
            return false;
        }
        var params = {
            TenPhong: $("#txtMRoomName").val(),
            DonGia: $("#txtMAmount").val(),
            TrangThai: parseInt($("#txtMStatus").val()),
            ThietBiPhong: $scope.ChooseEquipmentList
        }
        $http({
            url: `/WebServiceCP.aspx?action=CreateRoom`,
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: params
        }).then(function (response) {
            if (response.data.Message === "SUCCESS") {
                $scope.searchRoom();
                toastr.success("Lưu thành công !");
            } else {
                toastr.error("Lưu thất bại !");
            }
        }, function (err) {
            toastr.error("Xảy ra lỗi trong quá trình thực thi.");
            console.log(err);
        });
    }

    $scope.UpdateRoom = function () {
        var checkRequired = validForm();
        if (!checkRequired) {
            toastr.warning("Vui lòng điền đầy đủ thông tin !");
            return false;
        }
        var params = {
            MaPhong: $("#txtMRoomID").val(),
            TenPhong: $("#txtMRoomName").val(),
            DonGia: $("#txtMAmount").val(),
            TrangThai: parseInt($("#txtMStatus").val()),
            ThietBiPhong: $scope.ChooseEquipmentList
        }
        $http({
            url: `/WebServiceCP.aspx?action=UpdateRoom`,
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: params
        }).then(function (response) {
            if (response.data.Message === "SUCCESS") {
                $scope.searchRoom();
                toastr.success("Lưu thành công !");
            } else {
                toastr.error("Lưu thất bại !");
            }
        }, function (err) {
            toastr.error("Xảy ra lỗi trong quá trình thực thi.");
            console.log(err);
        });
    }

    $scope.DeleteRoom = function (maPhong) {
        if (confirm("Bạn có chắc muốn xoá dữ liệu ???")) {
            var params = {
                MaPhong: maPhong
            }
            $http({
                url: `/WebServiceCP.aspx?action=DeleteRoom`,
                method: "POST",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                data: params
            }).then(function (response) {
                console.log("response:", response);
                if (response.data.Message === "SUCCESS") {
                    $scope.searchRoom();
                    toastr.success("Xoá thành công !");
                } else if (response.data.Message === "PDP_EXIST") {
                    toastr.warning("Phòng đang được sử dụng !");
                } else {
                    toastr.error("Xoá thất bại !");
                }
            }, function (err) {
                toastr.error("Xảy ra lỗi trong quá trình thực thi.");
                console.log(err);
            });
        }
    }

    $scope.DeleteEquipmentRoom = function (maTTBP) {
        if (confirm("Bạn có chắc muốn xoá dữ liệu ???")) {
            var params = {
                MaTTBP: maTTBP
            }
            $http({
                url: `/WebServiceCP.aspx?action=DeleteEquipmentRoom`,
                method: "POST",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                data: params
            }).then(function (response) {
                console.log("response:", response);
                if (response.data.Message === "SUCCESS") {
                    toastr.success("Xoá thành công !");
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
        $("#txtMRoomID").val(e.MaPhong);
        $("#txtMRoomName").val(e.TenPhong);
        $("#txtMAmount").val(e.DonGia);
        $("#txtMStatus").val(e.TrangThai);
        $scope.ChooseEquipmentList = e.ThietBiPhong;
    }
    clearValueModal = function () {
        $("#txtMRoomID").val("");
        $("#txtMRoomName").val("");
        $("#txtMAmount").val("");
        $("#txtMStatus").val("");
        $scope.ChooseEquipmentList = [];
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
        $("#modal-table").modal();
    }
})