var app = angular.module("AppBanHang", []);

app.controller("HomeCtrl", function ($scope, $http) {
    $scope.listProdDetail = null;
    $scope.LoadProdDetail = function () {
        var key = "id";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );
         console.log("value",value)
        $http({
            method: "GET",
            url: url + "/api/Product/get-prod/id?id="+value,
        })
            .then(function (response) {
                $scope.listProdDetail = response.data;
                console.log("listProdDetail" + $scope.listProdDetail);
                console.log("Response Data:", response.data);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.LoadProdDetail();

    // $scope.LoadCart = function () {
    //     var key = "id";
    //     var value = window.location.search.substring(
    //         window.location.search.indexOf(key) + key.length + 1
    //     );

    //     $http({
    //         method: "GET",
    //         url: url_nd + "/api/Carts?productIdentifiers="+value,
    //     })
    //         .then(function (response) {
    //             $scope.listProdDetail = response.data;
    //             console.log("listProdDetail" + $scope.listProdDetail);
    //             console.log("Response Data:", response.data);
    //         })

    //         .catch(function (error) {
    //             console.error("Error loading menu:", error);
    //         });
    // };
    // $scope.LoadCart();
});
