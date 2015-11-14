var userProjectsController = function () {
    function all(context) {

    }

    // this is the view where you would edit a project
    function getById(context) {
        var id = context.params['id'];

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
                toastr.success('You added a project to your projects list!')
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
