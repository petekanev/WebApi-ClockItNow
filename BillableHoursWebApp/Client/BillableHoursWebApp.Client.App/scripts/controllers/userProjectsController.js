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