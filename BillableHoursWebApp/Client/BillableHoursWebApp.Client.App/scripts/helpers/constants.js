var constants = function () {
    return {
        server: {
            SERVER_PORT: "19655",
            // will be replaced by an actual domain when hosted on Azure
            SERVER_URL: "http://localhost:19655" //local
            // SERVER_URL: "http://clockit.azurewebsites.net"
        },
        localStorage: {
            LOCAL_STORAGE_USERNAME: "bh-signed-in-user",
            LOCAL_STORAGE_TOKEN: "bh-signed-in-user-token",
            LOCAL_STORAGE_ROLE: "bh-jkhe23h8c9ui1ojp9i12k-d23iodj-12dmk"
        },
        headers: {
            BEARER: "Bearer"
        },
        Pubnub: {
            SKEY: "sub-c-d96ad3e0-8c48-11e5-83e3-02ee2ddab7fe",
            PKEY: "pub-c-a7abcd95-879f-4933-b9fd-cefb7f1e0641",
            DEFAULT_CHANNEL: "clockit-webapi-history-client",
            ACTIVITY_CHANNEL: "clockit-webapi-activity-client"
        }
    }
}();