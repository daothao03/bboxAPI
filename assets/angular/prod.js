var app = angular.module("AppBanHang", []);

app.controller("ProdCtrl", function ($scope, $http) {
    $scope.listDetail = [];
    $scope.LoadProd = function () {
        var key = "id";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );

        $http({
            method: "GET",
            url: url + "/api/Product/id" + value,
        })
            .then(function (response) {
                $scope.listMenu = response.data.products;
                console.log("listDetail" + $scope.listDetail);
                console.log("Response Data detail:", response.data.products);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.LoadProd();
    
});
