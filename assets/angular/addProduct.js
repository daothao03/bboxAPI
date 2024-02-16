

var app = angular.module("AppBanHang", []);

app.controller("AddProductCtrl", function ($scope, $http) {

    $scope.formData = {};
    $scope.LoadBrand = function () {
        $http({
            method: "GET",
            url: url + "/api/Brand/get-all-brand",
        })
            .then(function (response) {
                if(response.data.length > 0){
                    // $scope.brand = response.data;
                    console.log("brand",$scope.brand)
                }
            })
    
            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.LoadBrand();
    
    $scope.listCategory = null;
    $scope.LoadCategory = function () {
        var key = "get-all-brand";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );
    
        $http({
            method: "GET",
            url: url + "/api/Brand/get-all-category",
        })
            .then(function (response) {
                $scope.listCategory = response.data;
                console.log("category" + $scope.listCategory);
                console.log("Response Data detail:", response.data);
            })
    
            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.LoadCategory();

    
    $scope.submitFormAddProduct = function() {
        console.log("Form Data:", $scope.formData);
       
        $http({
            method: "POST",
            url: url + "/api/Product/create-prod",
            data: $scope.formData,
            headers: {
                "Content-Type": "application/json"
            }
        })
        .then(function(response) {          
            console.log("Response:", response.data);
            if(response.data){
                alert("Tạo sản phẩm thành công")
            }
        })
        .catch(function(error) {
            console.log("Error:", error);
        });
        
    };
    $scope.submitFormAddProduct();
});
