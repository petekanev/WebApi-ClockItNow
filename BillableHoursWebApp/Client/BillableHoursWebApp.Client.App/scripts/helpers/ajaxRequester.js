var ajaxRequester = (function () {

    function send(method, url, options) {
        options = options || {};
        url = constants.SERVER_URL + url;
        console.log(url);
        var headers = options.headers || { 'Cache-Control': 'no-cache' },
            data = options.data || undefined;

        var promise = new Promise(function (resolve, reject) {
            $.ajax({
                url: url,
                method: method,
                contentType: 'application/json',
                headers: headers,
                data: JSON.stringify(data),
                success: function (res) {
                    resolve(res);
                },
                error: function (err) {
                    reject(err);
                }
            });
        });
        return promise;
    }

    function get(url, options) {
        return send('GET', url, options);
    }

    function post(url, options) {
        return send('POST', url, options);
    }

    function put(url, options) {
        return send('PUT', url, options);
    }

    function del(url, options) {
        return send('DELETE', url, options);
    }

    return {
        get: get,
        post: post,
        put: put,
        delete: del
    };
}());