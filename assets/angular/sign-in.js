var app = angular.module("AppBanHang", []);

app.controller("SignInCtrl", function ($scope, $http) { 
    $scope.formData = {}

    $scope.submitFormLogin = function() {      

        $http({
            method: "POST",
            url: url + "/api/Account/Login?email="+$scope.formData.email + "&password="+$scope.formData.password,
        })
        .then(function(response) { 
            console.log("Response:", response.data);
            if(response.data.token) {
                document.cookie = "user="+response.data.token+"path=/";
                // window.location.href = "/beaty-box-cosmetic/index-logined.html"
                var userRole = response.data.user.role;

                // Redirect based on user role
                if (userRole === "admin") {
                    window.location.href = "/beaty-box-cosmetic/admin.html";
                } else if (userRole === "client") {
                    window.location.href = "/beaty-box-cosmetic/index-logined.html";
                } else {
                    // Handle other roles or scenarios
                    console.log("Unknown role:", userRole);
                }
            }
            
        })
        .catch(function(error) {
           
            console.log("Error:", error);
        });
    };
    $scope.submitFormLogin();
});
