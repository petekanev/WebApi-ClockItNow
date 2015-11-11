var usersController = function () {
    function register(context) {
        templates.get('register')
            .then(function (template) {
                context.$element().html(template());
                $('#btn-register').on('click', function () {
                    var user = {
                        Email: $('#reg-email').val(),
                        FirstName: $('#reg-fname').val(),
                        LastName: $('#reg-lname').val(),
                        Password: $('#reg-password').val(),
                        ConfirmPassword: $('#reg-confirmpassword').val()
                    };

                    if ($('#reg-type-client').is(':checked')) {
                        user.IsEmployee = false;
                    } else {
                        user.IsEmployee = true;
                    }

                    // validate input data here, use toastr for messages, return if validation fails

                    data.users.register(user)
                        .then(function (res) {
                            toastr.success('You registered successfully!');
                            context.redirect('#/users/login');
                        },
                            function (error) {
                                var response = error.responseJSON.ModelState;

                                if (!response) {
                                    toastr.error(error.statusText);
                                } else {
                                    for (var err in response) {
                                        if (response.hasOwnProperty(err)) {
                                            toastr.error(response[err][0]);
                                        }
                                    }
                                }
                            });

                    return false;
                });
            });
    }

    function login(context) {
        templates.get('login')
            .then(function (template) {
                context.$element().html(template());
                $('#btn-login').on('click', function () {
                    var user = {
                        grant_type: "password",
                        username: $('#login-username').val(),
                        password: $('#login-password').val()
                    };

                    // validate input

                    data.users.login(user)
                        .then(function (res) {
                            localStorage.setItem(constants.localStorage.LOCAL_STORAGE_TOKEN, res.access_token);
                            localStorage.setItem(constants.localStorage.LOCAL_STORAGE_USERNAME, res.userName);

                            toastr.success('Logged in successfully!');
                            context.redirect('#/');
                            // reload page
                            location.reload(true);
                        },
                            function (error) {
                                toastr.error(error.error_description);
                            });

                    return false;
                });
            });
    }

    function logout(context) {
        localStorage.removeItem(constants.localStorage.LOCAL_STORAGE_TOKEN);
        localStorage.removeItem(constants.localStorage.LOCAL_STORAGE_USERNAME);

        context.redirect('#/');
        location.reload(true);
    }

    return {
        register: register,
        login: login,
        logout: logout
    };
}();
