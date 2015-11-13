var validator = (function () {
    function validUserNamePassword(input) {
        var len = input.length,
            charCode,
            i;
        if (len) {
            if (!(6 <= len && len <= 20)) {
                return false;
            }
            for (i = 0; i < len; i += 1) {
                charCode = input.charCodeAt(i);
                if (!((charCode >= 48 && charCode <= 57) || // 0-9
                (charCode >= 65 && charCode <= 122) || // a-z,A-Z
                (charCode == 45 || charCode == 46 || charCode == 95) /* - . _*/)) {
                    return false;
                }
            }
            return true;
        }

        return false;
    }

    function validName(input) {
        var len = input.length,
            charCode,
            i;

        if (len) {
            if (!(2 <= len && len <= 20)) {
                return false;
            }
        }

        for (i = 0; i < len; i += 1) {
            charCode = input.charCodeAt(i);

            if (!(charCode >= 65 && charCode <= 122)) {
                return false;
            }
        }

        return true;
    }

    function validEmail(input) {
        var pattern = new RegExp(/\b[A-Z0-9._%+-]+@(?:[A-Z0-9-]{2,}\.)+[A-Z]{2,4}\b/ig);
        return pattern.test(input);
    }

    function validURL(input) {
        var pattern = new RegExp(/^(?:(?:https?|ftp):\/\/)(?:\S+(?::\S*)?@)?(?:(?!(?:10|127)(?:\.\d{1,3}){3})(?!(?:169\.254|192\.168)(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,}))\.?)(?::\d{2,5})?(?:[\/?#]\S*)?$/i);
        return pattern.test(input);
    }

    return {
        validUserNamePassword: validUserNamePassword,
        validName: validName,
        validEmail: validEmail,
        validURL: validURL
    };
})();