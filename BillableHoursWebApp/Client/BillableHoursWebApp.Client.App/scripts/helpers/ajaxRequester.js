var ajaxRequester = (function () {

    function send(method, url, options) {
        options = options || {};

        url = constants.server.SERVER_URL + url;

        var headers = options.headers || { 'Cache-Control': 'no-cache' },
            data = options.data || undefined,
            contentType = options.contentType || 'application/json';

        if (!options.noStringify) {
            data = JSON.stringify(options.data);
        }

        var promise = new Promise(function (resolve, reject) {
            $.ajax({
                url: url,
                method: method,
                contentType: contentType,
                headers: headers,
                data: data,
                success: function (res) {
                    console.log(res);
                    resolve(res);
                },
                error: function (err) {
                    console.log(err);
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