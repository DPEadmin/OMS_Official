(function () {
    'use strict';

    angular
        .module('app')
        .factory('factory_common', factory_common);

    factory_common.$inject = ['$http'];

    function factory_common($http) {
        var service = {
            getAuthen: getDataByAuthen(_url,_param),
            postAuthen: postDataByAuthen(_url, _param),
            post:postData(_url,_param)
        };

        return service;
        function postData(url, param) {
            return $http({
                method: 'POST',
                url: $root + url,
                headers: {
                    'Content-Type': 'application/json'
                },
                data: param
            });
        }
        function getDataByAuthen(url,param) {
            return $http({
                method: 'GET',
                url: $root + url,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + JSON.parse($.base64.atob(window.localStorage.getItem('fm-token-set'))).access_token// '' //$.api.token().access_token
                },
                data: param
            });
        }
        function postDataByAuthen(url,param) {
            return $http({
                method: 'POST',
                url: $root + url,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + JSON.parse($.base64.atob(window.localStorage.getItem('fm-token-set'))).access_token
                },
                data: param
            });
        }
        function uploadDataFile() {
            return $http({
                method: 'POST',
                url: $root + '',
                cache: false,
                headers: {
                    'Content-Type': undefined
                },
                contentType: false,
                processData: false,
                data: idata
            });
        }
    }
})();