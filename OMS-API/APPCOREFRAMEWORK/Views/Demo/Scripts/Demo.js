(function () {
    'use strict';

    angular
        .module('app',['app-authen'])
        .controller('Demo', Demo);

    Demo.$inject = ['$scope', 'core_authenicate'];

    function Demo($scope, core_authenicate) {
        $scope.title = 'Demo';

        activate();

        function activate() { }
    }
})();
