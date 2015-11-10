var constants = function() {
    return {
        server: {
            SERVER_PORT: "19655",
            // will be replaced by an actual domain when hosted on Azure
            SERVER_URL: "http://localhost:19655"
},
        localStorage: {
            LOCAL_STORAGE_USERNAME: "bh-signed-in-user",
            LOCAL_STORAGE_TOKEN: "bh-signed-in-user-token"
        },
        headers: {
            BEARER: "Bearer"
        }
    }
}();