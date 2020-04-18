var app = angular.module('QLKS', ['ui.select2', 'ui.bootstrap', 'datatables']);
app.controller('RoomManagementCtrl', function ($scope, $http, $timeout, $window) {

    $scope.dataTable = [];
    var requiredList = document.getElementsByClassName('input-required');

    $scope.dtOptions = {
        "bStateSave": true,
        "aLengthMenu": [[15, 50, 100, -1], [15, 50, 100, 'All']],
        "bSort": true,
        "language": window.datatableLanguage
    };

    angular.element(document).ready(function () {
        $scope.searchRoom();
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
            TrangThai: parseInt($("#txtMStatus").val())
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
            TrangThai: parseInt($("#txtMStatus").val())
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
    }
    clearValueModal = function () {
        $("#txtMRoomID").val("");
        $("#txtMRoomName").val("");
        $("#txtMAmount").val("");
        $("#txtMStatus").val("");
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