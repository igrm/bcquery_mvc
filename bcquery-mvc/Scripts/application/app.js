//defining application
angular.module('BCQuery', ['ngMaterial', 'md.data.table', 'ngResource'])
    .run(function () {
        console.log("Ready.");
    });

//utilities
function dynamicSort(property) {
    var sortOrder = 1;
    if (property[0] === "-") {
        sortOrder = -1;
        property = property.substr(1);
    }
    return function (a, b) {
        var result = (a[property] < b[property]) ? -1 : (a[property] > b[property]) ? 1 : 0;
        return result * sortOrder;
    }
}

//defining services
angular.module('BCQuery').factory('ApiService', ['$resource', function ($resource) {
    var service = {
        getBlocksByDate: $resource("/api/blocks"),
        getBlockTransactions: $resource("/api/blocktx"),
        getAddressTransactions: $resource("/api/addresstx")
    }
    return service;
}]);

//defining controllers
angular.module('BCQuery').controller('SideMenuCtrl', function ($scope, $mdSidenav) {
    $scope.toggleMenu = function () {
        $mdSidenav('sideMenu').toggle();
    }

    $scope.currentView = "BlocksByDate";
    $scope.toggleView = function (viewName) {
        $scope.currentView = viewName;
    }
});

angular.module('BCQuery').controller('BlocksByDateCtrl', ['$scope', 'ApiService', function ($scope, ApiService) {
    'use strict';
    $scope.selected = [];
    //$scope.blocks = $scope.testData;

    $scope.query = {
        order: 'height',
        reportingValue: new Date(),
        limit: 5,
        page: 1
    };

    function success(item) {
        $scope.total = item.total;
        $scope.blocks = item.blocks.sort(dynamicSort($scope.query.order));
    }

    $scope.getBlocks = function () {
        $scope.promise = ApiService.getBlocksByDate.get($scope.query, success, function (ex) { console.log(ex)}).$promise;
    };
}]);

angular.module('BCQuery').controller('BlockTransactionsCtrl', ['$scope', 'ApiService', function ($scope, ApiService) {
    'use strict';

    $scope.selected = [];

    $scope.query = {
        order: 'hash',
        reportingValue: '',
        limit: 5,
        page: 1
    };

    function success(transactions) {
        $scope.total = transactions.total;
        $scope.transactions = transactions.tx.sort(dynamicSort($scope.query.order));
    }

    $scope.getTransactions = function () {
        $scope.promise = ApiService.getBlockTransactions.get($scope.query, success, function (ex) { console.log(ex) }).$promise;
    };
}]);

angular.module('BCQuery').controller('AddressTransactionsCtrl', ['$scope', 'ApiService', function ($scope, ApiService) {
    'use strict';

    $scope.selected = [];

    $scope.query = {
        order: 'hash',
        reportingValue: '',
        limit: 5,
        page: 1
    };

    function success(transactions) {
        $scope.total = transactions.total;
        $scope.transactions = transactions.tx.sort(dynamicSort($scope.query.order));
    }

    $scope.getTransactions = function () {
        $scope.promise = ApiService.getAddressTransactions.get($scope.query, success, function (ex) { console.log(ex) }).$promise;
    };
}]);
