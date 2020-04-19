var app = angular.module('QLKS', ['ui.select2', 'ui.bootstrap', 'datatables']);
app.controller('BookingRoomCtrl', function ($scope, $http, $timeout, $window) {
    $scope.RoomList = [];

    angular.element(document).ready(function () {
        $scope.GetRoom();
    });

    $scope.showModal = function (item) {
        console.log("item", item);
        $("#modalBooking").modal();
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
})