$(function () {
    var ul = $('ul.activityfeed-content');

    pubnub.history({
        channel: constants.Pubnub.ACTIVITY_CHANNEL,
        callback: appendActivity,
        count: 10,
        reverse: false
    });

    pubnub.subscribe({
        channel: constants.Pubnub.ACTIVITY_CHANNEL,
        message: function (message) {
            var nameIndex = message.indexOf(':');
            var name = message.substring(0, nameIndex);
            var messageContent = message.substring(nameIndex + 1, 250);
            S
            ul.append('<li class="shoutbox-whole-comment">' +
                '<span class="shoutbox-username text-info bold">' + name + '</span>' +
                '<p class="shoutbox-comment">' + messageContent + '</p>' +
                '<div class="shoutbox-comment-details"><span class="shoutbox-comment-reply text-primary" data-name="' + name + '">REPLY</span>' +
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
                '<p class="shoutbox-comment">' + messageContent + '</p>' +
                '<div class="shoutbox-comment-details"> <a href="/' + link + '>View project</a>' +
            '</li>');
        });
    }
})