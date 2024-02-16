var app = angular.module("AppBanHang", []);

app.controller("TaiKhoanCtrl", function ($scope, $http) {
    // Hiển thị toàn bộ user
    $scope.listUser = []
    $scope.LoadUser = function () {
        var key = "search";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );

        $http({
            method: "GET",
            url: url + "/api/User/search?search=" + value,
        })
            .then(function (response) {
                $scope.listUser = response.data.users;
                console.log("listUser" + $scope.listUser);
                console.log("Response Data:", response.data.users);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.LoadUser();
});