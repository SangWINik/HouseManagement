var app = angular.module('myApp', []);
app.filter('square', function () {
    return function (items, value) {
        items = items.filter(function (item) {
            return item.Square > value;
        });
        return items;
    };
});
app.controller('housesCtrl', function ($scope, $http) {
    $http.get("api/houses").then(function (response) {
        $scope.houseList = response.data;
    });
    $scope.Refresh = function () {
        $http.get("api/houses").then(function (response) {
            $scope.houseList = response.data;
        });
    };
    $scope.minSquare = 0;
    $scope.AddHouse = function () {
        var inp = document.getElementById("imageInput");
        var img = new Image();
        img.src = URL.createObjectURL(inp.files[0]);
        img.onload = function () {
            var base64 = getBase64Image(img);
            $scope.newHouse.Photo = base64;
            var data = $scope.newHouse;
            $http.post("api/houses", data).success(function (data, status, headers, config) { $scope.Refresh(); });
        }
    }
    $scope.DeleteHouse = function (id) {
        $http.delete("api/houses/" + id).success(function (data, status, headers, config) { $scope.Refresh(); });
    }
    $scope.UpdateHouse = function () {
        //alert($scope.houseList[$scope.editRowId].Address + " " + $scope.houseList[$scope.editRowId].Square);
        var cont = document.getElementById("container");
        var inputs = cont.getElementsByTagName("input");
        var fileSelector = inputs[$scope.editRowId * 6 + 4];
        if (fileSelector.value != "") {
            $scope.saveDisabled = true;
            var img = new Image();
            img.src = URL.createObjectURL(fileSelector.files[0]);
            img.onload = function () {
                $scope.saveDisabled = false;
                var base64 = getBase64Image(img);
                $scope.houseList[$scope.editRowId].Photo = base64;
                var data = $scope.houseList[$scope.editRowId];
                $http.put("api/houses/" + $scope.houseList[$scope.editRowId].Id, data).success(function (data, status, headers, config) {
                    $scope.editRowId = -1;
                    $scope.Refresh();
                });
            }
        } else {
            var data = $scope.houseList[$scope.editRowId];
            $http.put("api/houses/" + $scope.houseList[$scope.editRowId].Id, data).success(function (data, status, headers, config) {
                $scope.editRowId = -1;
                $scope.Refresh();
            });
        }
    }
    $scope.editRowId = -1;
    $scope.changeEditableRow = function (id) {
        $scope.editRowId = id;
    }
    $scope.isEditable = function (id) {
        if (id == $scope.editRowId)
            return true;
        else
            return false;
    }
    $scope.saveDisabled = false;
});

//Функція переведення зображення в Base64 для збереження в базі даних
function getBase64Image(img) {
    var canvas = document.createElement("canvas");
    canvas.width = img.width;
    canvas.height = img.height;
    var ctx = canvas.getContext("2d");
    ctx.drawImage(img, 0, 0);
    var dataURL = canvas.toDataURL("image/png");
    return dataURL;
}