$(function () {
    var ul = $('ul.activityfeed-content');

    pubnub.history({
        channel: constants.Pubnub.ACTIVITY_CHANNEL,
        callback: appendActivity,
        count: 5,
        reverse: false
    });

    pubnub.subscribe({
        channel: constants.Pubnub.ACTIVITY_CHANNEL,
        message: function (message) {
            var nameIndex = message.indexOf(':');
            var name = message.substring(0, nameIndex);
            var separator = message.indexOf('|');
            var messageContent = message.substring(nameIndex + 1, separator - 1);
            var link = message.substring(separator + 2, 250);

            ul.append('<li class="shoutbox-whole-comment">' +
                '<span class="shoutbox-username text-info bold">' + name + '</span>' +
                '<p class="shoutbox-comment">' + messageContent + '<a href="#' + link + '"> View project</a>' + '</p>' +
            '</li>');
        },
        error: function (error) {
            toastr.error('Something happened while attempting to fetch the messages...');
        }
    });

    function appendActivity(messages) {
        messages[0].forEach(function (message) {
            var nameIndex = message.indexOf(':');
            var name = message.substring(0, nameIndex);
            var separator = message.indexOf('|');
            var messageContent = message.substring(nameIndex + 1, separator - 1);
            var link = message.substring(separator + 2, 250);

            ul.append('<li class="shoutbox-whole-comment">' +
                '<span class="shoutbox-username text-info bold">' + name + '</span>' +
                '<p class="shoutbox-comment">' + messageContent + '<a href="#' + link + '"> View project</a>' + '</p>' +
            '</li>');
        });
    }
})