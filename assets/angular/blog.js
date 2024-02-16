var app = angular.module("AppBanHang", []);

app.controller("BlogCtrl", function ($scope, $http) {
    // blog
    $scope.listBlog = [];
    $scope.LoadBlog = function () {
        var key = "get-all-blog";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );

        $http({
            method: "GET",
            url: url + "/api/Blogs/get-all-blog" + value,
        })
            .then(function (response) {
                $scope.listBlog = response.data.blogs;
                console.log("listBlog" + $scope.listBlog);
                console.log("Response Data:", response.data.blogs);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.LoadBlog();

    // sản phẩm
    $scope.listMenu = [];
    $scope.currentPage = 1;
    $scope.pageSize = 10;
    $scope.LoadSP = function () {
        var key = "search";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );

        $http({
            method: "GET",
            url: url_nd + "/api/Product/get-all-prod?search=" + value,
            params: {
                page: $scope.currentPage, // Send the current page as a parameter
                // Add other parameters as needed
            },
        })
            .then(function (response) {
                $scope.listMenu = response.data.products;
                console.log("listMenu" + $scope.listMenu);
                console.log("Response Data:", response.data.products);
                $scope.totalPages = response.data.TotalPages;
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.LoadSP();

    // banner
    $scope.listBanner = [];
    $scope.LoadBanner = function () {
        var key = "get-all-banner";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );

        $http({
            method: "GET",
            url: url + "/api/Banner/get-all-banner" + value,
        })
            .then(function (response) {
                $scope.listBanner = response.data;
                console.log("listBanner" + $scope.listBanner);
                console.log("Response Data:", response.data);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.LoadBanner();
    

    // sản phẩm bán chạy
    $scope.listBanChay = [];
    $scope.LoadBanChay = function () {
        var key = "sanphambanchay";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );

        $http({
            method: "GET",
            url: url_nd + "/api/Product/sanphambanchay/" + value,
        })
            .then(function (response) {
                $scope.listBanChay = response.data;
                console.log("listBanChay bán chạy" + $scope.listBanChay);
                console.log("Response Data bán chạy:", response.data);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.LoadBanChay();

    $scope.listSPMoi = [];
    $scope.listSPMoi = function () {
        var key = "sanphammoi";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );

        $http({
            method: "GET",
            url: url_nd + "/api/Product/sanphammoi" + value,
        })
            .then(function (response) {
                $scope.listSPMoi = response.data;
                console.log("listSPMoi" + $scope.listSPMoi);
                console.log("Response listSPMoi:", response.data);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.listSPMoi();

    //search
    $scope.timkiem = [];
    $scope.tiemkiem = function () {
        var key = "search";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );

        $http({
            method: "GET",
            url: url_nd + "/api/Product/search?search="+value,
        })
            .then(function (response) {
                $scope.timkiem = response.data.products;
                console.log("timkiem" + $scope.timkiem);
                console.log("Response timkiem:", response.data.products);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.tiemkiem();
});
