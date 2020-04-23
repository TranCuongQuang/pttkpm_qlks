var app = angular.module('QLKS', ['ui.select2', 'ui.bootstrap', 'datatables']);
app.controller('EquipmentRoomManagementCtrl', function ($scope, $http, $timeout, $window) {

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
        $scope.searchEquipment();
        $scope.GetEquipment();
        $scope.GetRoom();
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
            var maTB = _self.data('value');
            $scope.getEquipmentByID(maTB);
        } else {
            $("#titleheader").text("Thông tin");
            $("#insert").hide();
            $("#update").hide();
            var maTB = _self.data('value');
            $scope.getEquipmentByID(maTB);
        }
    });

    $(document).on("click", "#DeleteEquipment", function (e) {
        e.preventDefault();
        var _self = $(this);
        var maTB = _self.data('value');
        $scope.DeleteEquipment(maTB);
    });

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
            return n.MaSP == item.MaSP;
        });
        $scope.SumMoneyEquipment();
        //console.log("RemoveChooseEquipment", evens);
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
        $scope.ddlEquipment = item.MaSP.toString();
    }

    $scope.searchEquipment = function () {
        var params = {
            MaTB: $("#EquipmentID").val(),
            TenTB: $("#EquipmentName").val(),
            TinhTrang: $("#Note").val()
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

    $scope.getEquipmentByID = function (ID) {
        var params = {
            MaTB: ID
        }
        $http({
            url: `/WebServiceCP.aspx?action=GetEquipmentByID`,
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

    $scope.InsertEquipment = function () {
        var checkRequired = validForm();
        if (!checkRequired) {
            toastr.warning("Vui lòng điền đầy đủ thông tin !");
            return false;
        }
        var params = {
            TenTB: $("#txtMEquipmentName").val(),
            TinhTrang: parseInt($("#txtMNote").val())
        }
        $http({
            url: `/WebServiceCP.aspx?action=CreateEquipment`,
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: params
        }).then(function (response) {
            if (response.data.Message === "SUCCESS") {
                $scope.searchEquipment();
                toastr.success("Lưu thành công !");
            } else {
                toastr.error("Lưu thất bại !");
            }
        }, function (err) {
            toastr.error("Xảy ra lỗi trong quá trình thực thi.");
            console.log(err);
        });
    }

    $scope.UpdateEquipment = function () {
        var checkRequired = validForm();
        if (!checkRequired) {
            toastr.warning("Vui lòng điền đầy đủ thông tin !");
            return false;
        }
        var params = {
            MaTB: $("#txtMEquipmentID").val(),
            TenTB: $("#txtMEquipmentName").val(),
            TinhTrang: parseInt($("#txtMNote").val())
        }
        $http({
            url: `/WebServiceCP.aspx?action=UpdateEquipment`,
            method: "POST",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            data: params
        }).then(function (response) {
            if (response.data.Message === "SUCCESS") {
                $scope.searchEquipment();
                toastr.success("Lưu thành công !");
            } else {
                toastr.error("Lưu thất bại !");
            }
        }, function (err) {
            toastr.error("Xảy ra lỗi trong quá trình thực thi.");
            console.log(err);
        });
    }

    $scope.DeleteEquipment = function (maTB) {
        if (confirm("Bạn có chắc muốn xoá dữ liệu ???")) {
            var params = {
                MaTB: maTB
            }
            $http({
                url: `/WebServiceCP.aspx?action=DeleteEquipment`,
                method: "POST",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                data: params
            }).then(function (response) {
                console.log("response:", response);
                if (response.data.Message === "SUCCESS") {
                    $scope.searchEquipment();
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
        $("#txtMEquipmentID").val(e.MaThietBi);
        $("#txtMEquipmentName").val(e.TenThietBi);
        $("#txtMNote").val(e.TinhTrang);
    }
    clearValueModal = function () {
        $("#txtMEquipmentID").val("");
        $("#txtMEquipmentName").val("");
        $("#txtMNote").val("");
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