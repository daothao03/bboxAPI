var app = angular.module("AppBanHang", []);

app.controller("HomeCtrl", function ($scope, $http) {
    $scope.listBlog = [];

    $scope.LoadBlog= function () {
        var key = "get-all-blog";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );

        $http({
            method: "GET",
            url: url + "/api/Blogs/get-all-blog" + value,
        })
            .then(function (response) {
                $scope.listMenu = response.data.blogs;
                console.log("listBlogs" + $scope.listBlog);
                console.log("Response blog:", response.data.blogs);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.LoadBlog();
});
