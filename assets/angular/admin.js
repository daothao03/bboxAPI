var app = angular.module("AppBanHang", []);
app.directive('demoFileModel', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.demoFileModel),
                modelSetter = model.assign; //define a setter for demoFileModel

            //Bind change event on the element
            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
});
app.controller("AdminCtrl", function ($scope, $http) {
    $scope.listMenu = [];
    $scope.listUser = [];
    
    $scope.listContact = [];
    $scope.listNCC = [];
    $scope.DoanhThu = [];

    // Hiển thị toàn bộ sản phẩm
    $scope.LoadMenu = function () {
        var key = "get-all-prod";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );

        $http({
            method: "GET",
            url: url + "/api/Product/get-all-prod/" + value,
        })
            .then(function (response) {
                $scope.listMenu = response.data.products;
                console.log("listMenu" + $scope.listMenu);
                console.log("Response Data:", response.data.products);
            })
            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };

    //tìm kiếm sản phẩm
    $scope.timkiem = [];
    $scope.tiemkiem = function () {
        var key = "search";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );

        $http({
            method: "GET",
            url: url + "/api/Product/search?search="+value,
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

    // Hiển thị toàn bộ user
    $scope.LoadUser = function () {
        var key = "tk";
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

    // Hiển thị toàn bộ liên hệ
    $scope.LoadContact = function () {
        var key = "lh";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );

        $http({
            method: "GET",
            url: url + "/api/Contact/search?search="+value,
        })
            .then(function (response) {
                $scope.listContact = response.data;
                console.log("listContact" + $scope.listContact);
                console.log("Response Data:", response.data);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };

    // hiển thị nhà cung cấp
    $scope.LoadNCC= function () {
        var key = "get-all-ncc";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );

        $http({
            method: "GET",
            url: url + "/api/NhaCungCap/get-all-ncc" + value,
        })
            .then(function (response) {
                $scope.listNCC = response.data.ncc;
                console.log("suppliers" + $scope.listNCC);
                console.log("Response Data:", response.data.ncc);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };

    // load brand ra combobox
    $scope.brand = [];
    $scope.LoadBrand = function () {
        $http({
            method: "GET",
            url: url + "/api/Brand/get-all-brand",
        })
            .then(function (response) {
                if(response.data.length > 0){
                    $scope.brand = response.data;
                    console.log("brand",$scope.brand)
                }
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.LoadBrand();

    // load danh mục ra combobox
    $scope.listCategory = [];
    $scope.LoadCategory = function () {

        $http({
            method: "GET",
            url: url + "/api/Category/get-all-category",
        })
            .then(function (response) {
                console.log("categoryREs",response)
                if(response.data.length > 0){
                    $scope.listCategory = response.data;
                    console.log("category",response.data)
                }
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.LoadCategory();


    $scope.formData = new FormData();
    // thêm sản phẩm
    $scope.submitFormAddProduct = function() { 
        if($scope.imageEvent && $scope.formData.CategoryID && $scope.formData.Name && $scope.formData.Price && $scope.formData.BrandID && $scope.formData.Description){
            $scope.formData.append("imageFileName",$scope.imageEvent)
            $scope.formData.append("CategoryID",Number($scope.formData.CategoryID))
            $scope.formData.append("Name",$scope.formData.Name)
            $scope.formData.append("Price",$scope.formData.Price)
            $scope.formData.append("BrandID",$scope.formData.BrandID)
            $scope.formData.append("Description",$scope.formData.Description)
        }
        $http({
            method: "POST",
            url: url + "/api/Product/create-prod",
            data: $scope.formData,
            headers: {
                "Content-Type":undefined,
            },
            transformRequest: angular.identity,
        })
        .then(function(response) {          
            console.log("Response:", response.data);
            if(response.data){
                alert("Tạo sản phẩm thành công")
                window.location.href="/beaty-box-cosmetic/admin.html"
            }
        })
        .catch(function(error) {
            console.log("Error:", error);
        });       
    };

    $scope.submitFormAddProduct();


    $scope.LoadMenu();
    $scope.LoadUser();
    $scope.LoadContact();
    $scope.LoadNCC();

    // xóa sản phẩm
    $scope.submitFormDeleteProduct = function() { 
        var key = "id";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );

        console.log("Delete id: ", value)

        $http({
            method: "DELETE",
            url: url + "/api/Product/id?id="+value,
            
            header: {
                "Content-Type":"application/problem+json",
            },
            transformRequest: angular.identity,
        })
        .then(function(response) {          
            console.log("Response:", response);
            if(response){
                alert("Xóa sản phẩm thành công")
                window.location.href="/beaty-box-cosmetic/admin.html"
            }
        })
        .catch(function(error) {
            console.log("Error:", error);
        });
        
    };
    $scope.submitFormDeleteProduct();

    // thêm tài khoản
    $scope.formDataAccount = {}

    $scope.submitFormAddAccount = function() { 
        console.log("form data:", $scope.formDataAccount)   
        $http({
            method:"POST",
            url: url + "/api/Account/Register",
            headers:{
                'Content-Type': 'application/json',
            },
            data:$scope.formDataAccount
        })
        .then(function(response) { 
            const data = response.data;
            if(data){
                alert('Thêm tài khoản thành công')
                window.location.href="/beaty-box-cosmetic/admin.html"
            }
        })
        .catch(function(error) {
            console.log("Error:", error);
        });
    };

    $scope.DoanhThu = null;
    $scope.DoanhThu = function () {
        // var key = "id";
        // var value = window.location.search.substring(
        //     window.location.search.indexOf(key) + key.length + 1
        // );
        //  console.log("value",value)
        $http({
            method: "GET",
            url: url + "/api/ThongKe/DoanhThu",
        })
            .then(function (response) {
                $scope.DoanhThu = response.data;
                console.log("DoanhThu" + $scope.DoanhThu);
                console.log("Response Data DoanhThu:", response.data);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.DoanhThu();

    $scope.TongHoaDon = null;
    $scope.TongHoaDon = function () {
        // var key = "id";
        // var value = window.location.search.substring(
        //     window.location.search.indexOf(key) + key.length + 1
        // );
        //  console.log("value",value)
        $http({
            method: "GET",
            url: url + "/api/ThongKe/TongHoaDon",
        })
            .then(function (response) {
                $scope.TongHoaDon = response.data;
                console.log("TongHoaDon" + $scope.TongHoaDon);
                console.log("Response Data TongHoaDon:", response.data);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.TongHoaDon();

    $scope.TongKhachHang = null;
    $scope.TongKhachHang = function () {
        // var key = "id";
        // var value = window.location.search.substring(
        //     window.location.search.indexOf(key) + key.length + 1
        // );
        //  console.log("value",value)
        $http({
            method: "GET",
            url: url + "/api/ThongKe/TongKhachHang",
        })
            .then(function (response) {
                $scope.TongKhachHang = response.data;
                console.log("TongKhachHang" + $scope.TongKhachHang);
                console.log("Response Data TongKhachHang:", response.data);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.TongKhachHang();

    $scope.TongSanPham = null;
    $scope.TongSanPham = function () {
        // var key = "id";
        // var value = window.location.search.substring(
        //     window.location.search.indexOf(key) + key.length + 1
        // );
        //  console.log("value",value)
        $http({
            method: "GET",
            url: url + "/api/ThongKe/TongSanPham",
        })
            .then(function (response) {
                $scope.TongSanPham = response.data;
                console.log("TongSanPham" + $scope.TongSanPham);
                console.log("Response Data TongSanPham:", response.data);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.TongSanPham();

    $scope.TongUser = null;
    $scope.TongUser = function () {
        // var key = "id";
        // var value = window.location.search.substring(
        //     window.location.search.indexOf(key) + key.length + 1
        // );
        //  console.log("value",value)
        $http({
            method: "GET",
            url: url + "/api/ThongKe/TongUser",
        })
            .then(function (response) {
                $scope.TongUser = response.data;
                console.log("TongUser" + $scope.TongUser);
                console.log("Response Data TongUser:", response.data);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.TongUser();

    $scope.listKH = [];
    // Hiển thị toàn bộ khách hàng
    $scope.LoadKH = function () {
        var key = "tk";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );

        $http({
            method: "GET",
            url: url + "/api/User/search?search=" + value,
        })
            .then(function (response) {
                if(response.data.token) {
                    var userRole = response.data.user.role;
                    if (userRole === "client") {
                        $scope.listKH = response.data.users;
                        console.log("listKH" + $scope.listKH);
                        console.log("Response Data listKH: ", response.data.users);
                    }
                }
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.LoadKH();


    // sửa sản phẩm
    $scope.formDataEdit = new FormData();
    $scope.submitFormEditProduct = function() { 
        console.log("fileScope",$scope.myFile)
        if($scope.myFile && $scope.formDataEdit.Name && $scope.formDataEdit.CategoryID && $scope.formDataEdit.BrandID && $scope.formDataEdit.Description && $scope.formDataEdit.Price  && $scope.formDataEdit.SoLuong){
            $scope.formDataEdit.append("ImageFileName",$scope.myFile)
            $scope.formDataEdit.append("BrandID",Number($scope.formDataEdit.BrandID))
            $scope.formDataEdit.append("Name",$scope.formDataEdit.Name)
            $scope.formDataEdit.append("SoLuong",$scope.formDataEdit.SoLuong)
            $scope.formDataEdit.append("CategoryID",$scope.formDataEdit.CategoryID)
            $scope.formDataEdit.append("Description",$scope.formDataEdit.Description)
            $scope.formDataEdit.append("Price",$scope.formDataEdit.Price)
            $http({
                method:"POST",
                url: url + `/api/Product/id?id=${$scope.prodId}`,
                data: $scope.formDataEdit,
                headers: {
                    "Content-Type":undefined,
                },
                transformRequest: angular.identity,
            })
            .then(function(response) {          
                console.log("Response:", response.data);
                if(response.data){
                    alert("Sửa sản phẩm thành công")
                    window.location.href="/beaty-box-cosmetic/admin.html"
                }
            })
            .catch(function(error) {
                console.log("Error:", error);
            });   
        }    
    };

    $scope.productEdit = null;
    $scope.getProductID = function(id,data){
        $scope.prodId = id;
        $scope.productEdit = data;
        $scope.formDataEdit.Name = data.name;
        $scope.formDataEdit.BrandID = data.brand.id;
        $scope.formDataEdit.CategoryID = data.category.id;
        $scope.formDataEdit.Price = data.price;
        $scope.formDataEdit.Description = data.description;
        $scope.formDataEdit.SoLuong = data.soLuong;
        $scope.formDataEdit.ImageFileName = data.imageFileName;
        console.log("productEdit",$scope.productEdit)
    }

    // sửa quyền người dùng
    $scope.formUserEdit = new FormData();
    $scope.submitFormEditUser = function() { 
        if($scope.formUserEdit.role){
            $scope.formUserEdit.append("Role",$scope.formUserEdit.Role)
            $http({
                method:"POST",
                url: url + `/api/User/UpdateUserRole/${$scope.UserId}`,
                data: $scope.formUserEdit,
                headers: {
                    "Content-Type":undefined,
                },
                transformRequest: angular.identity,
            })
            .then(function(response) {          
                console.log("Response:", response.data);
                if(response.data){
                    alert("Sửa quyền user thành công thành công")
                    window.location.href="/beaty-box-cosmetic/admin.html"
                }
            })
            .catch(function(error) {
                console.log("Error:", error);
            });   
        }    
    };

    $scope.UserEdit = null;
    $scope.getUserID = function(id,data){
        $scope.UserId = id;
        $scope.formUserEdit.Role = data.role;
        console.log("productEdit",$scope.UserEdit)
    }

    // Hiển thị toàn bộ blog
    $scope.listBlogs = [];
    $scope.listBlogs = function () {
        var key = "tintuc";
        var value = window.location.search.substring(
            window.location.search.indexOf(key) + key.length + 1
        );

        $http({
            method: "GET",
            url: url + "/api/Blogs/get-all-blog" + value,
        })
            .then(function (response) {
                $scope.listBlogs = response.data.blogs;
                console.log("listBlogs" + $scope.listBlogs);
                console.log("Response Data:", response.data.blogs);
            })

            .catch(function (error) {
                console.error("Error loading menu:", error);
            });
    };
    $scope.listBlogs();
});



