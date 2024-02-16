var app = angular.module("AppBanHang", []);

app.controller("SignUpCtrl", function ($scope, $http, $location) {

    $scope.formData = {};
    
    $scope.submitForm = function() {
        console.log("Form Data:", $scope.formData);
       
        $http({
            method: "POST",
            url: url + "/api/Account/Register",
            data: $scope.formData,
            headers: {
                "Content-Type": "application/json"
            }
        })
        .then(function(response) {
           
            console.log("Response:", response.data);
            if(response.data.token) {
                window.location.href = "/beaty-box-cosmetic/sign-in.html"
            }
        })
        .catch(function(error) {
           
            console.log("Error:", error);
        });
    };
    $scope.submitForm();
});
