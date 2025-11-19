(function () {
    'use strict';

    angular
        .module('app-authen',[])
        .factory('core_authenicate', core_authenicate);

    core_authenicate.$inject = ['$http'];

    function core_authenicate($http) {
        var service = {
            authenicate: signin
        };

        return service;

        function signin(username,password,callback,error) {
            var _authen = {
                "grant_type": "password",
                username: encodeURI($.base64.btoa(username)),
                password: encodeURI($.base64.btoa(password))
            }
            $http({
                method: 'POST',
                url: $root + "token",
                headers: {
                    "cache-control": "no-cache",
                    "content-type": "application/x-www-form-urlencoded"
                },
                data: _authen
            }).then(function successCallback(response) {
                window.localStorage.setItem('fm-token-set', $.base64.btoa(JSON.stringify(response.data)));
                callback(response.data);
            }, function errorCallback(response) {
                if (error !== undefined) {
                    error(response);
                } else {
                    alert('token authenication error message?');
                }
            });
        }
    }
})();