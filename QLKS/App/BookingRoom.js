var app = angular.module('QLKS', ['ui.select2', 'ui.bootstrap', 'datatables']);
app.controller('BookingRoomCtrl', function ($scope, $http, $timeout, $window) {

    $scope.showModal = function () {
        $("#modalBooking").modal();
    }
})