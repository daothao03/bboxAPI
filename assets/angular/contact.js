var app = angular.module("AppBanHang", []);

app.controller("ContactCtrl", function ($scope, $http) {
    $scope.listSubjects = null;
    $scope.LoadContact = function () {
        var key = "subjects";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );

        $http({
            method: "GET",
            url: url + "/api/Contact/subjects" + value,
        })
            .then(function (response) {
                $scope.listSubjects = response.data;
                console.log("listSubjects" + $scope.listSubjects);
                console.log("Response Data detail:", response.data);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.LoadContact();
    
    $scope.formData = {};
    
    $scope.submitFormContact = function() {
        console.log("Form Data:", $scope.formData);
        $http({
            method: "POST",
            url: url_nd + "/api/Contact/create-contact",
            data: $scope.formData,
            headers: {
                "Content-Type": "application/json"
            }
        })
        .then(function(response) {          
            console.log("Response:", response.data);
            if(response.data){
                alert("Gửi contact thành công")
                window.location.href="./beaty-box-cosmetic/contact.html"
            }
        })
        .catch(function(error) {
            console.log("Error:", error);
        });
        
    };
    $scope.submitFormContact();
});
