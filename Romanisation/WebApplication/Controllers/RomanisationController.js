var KoreanRomanisationApplication = angular.module("KoreanRomanisation", []);

KoreanRomanisationApplication.controller("RomanisationController", function ($scope, $http) {

    $scope.RomanisationType = "mccune-reischauer";

    $scope.$watchGroup(["RomanisationType", "Korean"], function () {
        $http.post("/WebAPI/Romanisation.ashx", { "RomanisationType": $scope.RomanisationType, "Korean": $scope.Korean }).success(function (Response) {
            $scope.Romanisation = Response;
        });
    });

});

