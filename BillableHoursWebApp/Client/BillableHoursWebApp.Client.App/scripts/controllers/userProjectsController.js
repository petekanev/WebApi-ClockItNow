var userProjectsController = function () {
    function validateState(context) {
        if (localStorage.getItem(constants.localStorage.LOCAL_STORAGE_ROLE) !== "2"
            && localStorage.getItem(constants.localStorage.LOCAL_STORAGE_ROLE) !== "1"
            || !localStorage.getItem(constants.localStorage.LOCAL_STORAGE_USERNAME)) {
            context.redirect('/#/projects');
            toastr.info('You shouldn\'t be here!');
        }
    }

    function all(context) {
        validateState(context);

        var projects;

        data.projects.getUserProjects()
            .then(function (res) {
                projects = res;

                return templates.get('userProjects');
            },
            function (error) {
                toastr.error('An error occurred while trying to fetch information from the server...');
                toastr.error(error.statusText);
                console.log(error);
            })
            .then(function (template) {
                context.$element().html(template(projects));
            });
    }

    // this is the view where you would edit a project
    function getById(context) {
        var id = context.params['id'];

        var container;

        validateState(context);

        var project;

        data.projects.get(id)
            .then(function (res) {
                project = res;

                if (localStorage.getItem(constants.localStorage.LOCAL_STORAGE_ROLE) === "2") {
                    return templates.get('clientProject');
                } else {
                    return templates.get('employeeProject');
                }
            },
                function (error) {
                    toastr.error('An error occurred while trying to fetch information from the server...');
                    toastr.error(error.statusText);
                    console.log(error);
                })
            .then(function (template) {
                context.$element().find('#project-view').html(template(project));

                // clients view buttons
                $('#btn-project-invoice').on('click', function () {
                    var id = $(this).attr('data-projid');

                    data.projectsActions.getInvoice(id)
                        .then(function (res) {
                            console.log(res);
                        }, function (error) {
                            console.log(error);
                        });

                    return false;
                });

                if ($('#btn-worklog-end').length > 0) {
                    $('#btn-worklog-createForm').hide();
                }

                // employees view buttons
                $('#btn-worklog-createForm').on('click', function () {

                    container = $('#form-worklog-create');

                    var input = $('<input type="text" class="col-md-9" id="form-worklog-shortDesc" placeholder="Short Description">');
                    var btn = $('<button class="btn btn-sm btn-success col-md-3" id="btn-worklog-start">').text('Start Session');

                    container.append(input).append(btn);

                    $('#btn-worklog-createForm').hide();

                    $('#btn-worklog-start').on('click', function () {
                        // validate input field with id #form-worklog-shortDesc
                        var projectId = $('#project-title').attr('data-projid');
                        var worklog = {};
                        worklog.ShortDescription = $('#form-worklog-shortDesc').val();

                        console.log(projectId);

                        data.projectsActions.startSession(projectId, worklog)
                        .then(function (res) {
                            $('#project-view').hide();
                            toastr.info('Started a session.');
                        }, function (error) {
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

                    return false;
                });

                $('#btn-worklog-end').on('click', function () {
                    var worklogId = $(this.parentNode.parentNode).attr('data-logid');

                    data.projectsActions.finishSession(worklogId)
                        .then(function (res) {
                            $('#project-view').hide();
                            toastr.info('Ended a session');
                            context.redirect('#/users/projects');
                        }, function (error) {
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

                $('#btn-project-finalize').on('click', function () {
                    var projectId = $('#project-title').attr('data-projid');

                    data.projectsActions.finalize(id)
                        .then(function (res) {
                            toastr.info('Successfully completed the project! Payment is pending!');
                            context.redirect('#/users/projects');
                        }, function (error) {
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

    function beginProject(context) {
        var id = context.params['id'];

        if (localStorage.getItem(constants.localStorage.LOCAL_STORAGE_ROLE) !== "1") {
            context.redirect('/#');
            toastr.info('You must be registered as an Employee to work on projects!');
        }

        data.projectsActions.enroll(id)
            .then(function (res) {
                context.redirect('/#/users/projects');
                toastr.success('You added a project to your projects list!');
            },
                function (err) {
                    toastr.error(err.statusText);
                    console.log(err);
                    return false;
                });
    }

    return {
        all: all,
        getById: getById,
        begin: beginProject
    }
}();