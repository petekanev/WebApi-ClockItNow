﻿$(function () {
    pubnub.history({
        channel: constants.Pubnub.DEFAULT_CHANNEL,
        callback: appendComments,
        count: 5, // 100 is the default
        reverse: false // false is the default
    });

    // Storing some elements in variables for a cleaner code base
    var shoutboxForm = $('.shoutbox-form'),
     form = shoutboxForm.find('form'),
     closeForm = shoutboxForm.find('h2 span'),
     nameElement = form.find('#shoutbox-name'),
     commentElement = form.find('#shoutbox-comment'),
     ul = $('ul.shoutbox-content');

    nameElement.val(localStorage.getItem(constants.localStorage.LOCAL_STORAGE_USERNAME) || 'Anonymous');
    nameElement.prop('disabled', true);

    pubnub.subscribe({
        channel: constants.Pubnub.DEFAULT_CHANNEL,
        message: function (message) {
            var nameIndex = message.indexOf(':');
            var name = message.substring(0, nameIndex);
            var messageContent = message.substring(nameIndex + 1, 250);

            ul.append('<li class="shoutbox-whole-comment">' +
                '<span class="shoutbox-username text-info bold">' + name + '</span>' +
                '<p class="shoutbox-comment">' + messageContent + '</p>' +
                '<div class="shoutbox-comment-details"><span class="shoutbox-comment-reply text-primary" data-name="' + name + '">REPLY</span>' +
            '</li>');

            ul.scrollTop(ul[0].scrollHeight);
        },
        error: function (error) {
            toastr.error('Something happened while attempting to fetch the messages...');
        }
    });

    var canPostComment = true;

    form.submit(function (e) {
        e.preventDefault();

        if (!canPostComment) {
            return false;
        }

        var name = escapeHTML(nameElement.val().trim());
        var comment = escapeHTML(commentElement.val().trim());

        if (name.length && comment.length && comment.length < 240) {

            // publish to pubnub
            pubnub.publish({
                channel: constants.Pubnub.DEFAULT_CHANNEL,
                message: name + ': ' + comment
            });

            commentElement.val('');

            // Prevent new shouts from being published

            canPostComment = false;

            // Allow a new comment to be posted after 5 seconds

            setTimeout(function () {
                canPostComment = true;
            }, 2000);
        }

        return false;
    });

    // Toggle the visibility of the form.

    shoutboxForm.on('click', 'h2', function (e) {

        if (form.is(':visible')) {
            formClose();
        }
        else {
            formOpen();
        }

    });

    // Clicking on the REPLY button writes the name of the person you want to reply to into the textbox.

    ul.on('click', '.shoutbox-comment-reply', function (e) {

        var replyName = $(this).attr('data-name');

        formOpen();
        commentElement.val('@' + replyName + ' ').focus();

    });

    function formOpen() {

        if (form.is(':visible')) return;

        form.slideDown();
        closeForm.fadeIn();
    }

    function formClose() {

        if (!form.is(':visible')) return;

        form.slideUp();
        closeForm.fadeOut();
    }

    function appendComments(messages) {

        ul.empty();

        messages[0].forEach(function (message) {
            var nameIndex = message.indexOf(':');
            var name = message.substring(0, nameIndex);
            var messageContent = message.substring(nameIndex + 1, 250);

            ul.append('<li class="shoutbox-whole-comment">' +
                '<span class="shoutbox-username text-info bold">' + name + '</span>' +
                '<p class="shoutbox-comment">' + messageContent + '</p>' +
                '<div class="shoutbox-comment-details"><span class="shoutbox-comment-reply text-primary" data-name="' + name + '">REPLY</span>' +
            '</li>');
        });
    }

    function escapeHTML (unsafe_str) {
    return unsafe_str
      .replace(/&/g, '&amp;')
      .replace(/</g, '&lt;')
      .replace(/>/g, '&gt;')
      .replace(/\"/g, '&quot;')
      .replace(/\'/g, '&#39;');
}
});