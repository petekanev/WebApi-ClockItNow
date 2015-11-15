var data = function () {
    function register(user) {
        var options = {};
        options.data = user;

        return ajaxRequester.post('/api/account/register', options);
    }

    function login(user) {
        var options = {};
        options.contentType = 'application/x-www-form-urlencoded';
        options.noStringify = true;
        // options.data = 'grant_type=password&username=' + user.username + '&password=' + user.password;
        options.data = user;

        return ajaxRequester.post('/token', options);
    }

    function getUserRole() {
        var options = {};
        options.headers = {};
        options.headers.Authorization = 'Bearer ' + localStorage.getItem(constants.localStorage.LOCAL_STORAGE_TOKEN);

        return ajaxRequester.get('/api/Account/UserInfo', options);
    }

    function allCategories() {
        return ajaxRequester.get('/api/categories');
    }

    function addCategory(category) {

    }

    function getCategory(id) {

    }

    function createProject(project) {
        var options = {};
        options.data = project;
        options.headers = {};
        options.headers.Authorization = 'Bearer ' + localStorage.getItem(constants.localStorage.LOCAL_STORAGE_TOKEN);

        return ajaxRequester.post('/api/projects', options);
    }

    function allProjects() {

    }

    function getProject(id) {
        var options = {};

        return ajaxRequester.get('/api/projects/' + id, options);
    }

    function getUserProjects() {
        var options = {};
        options.headers = {};
        options.headers.Authorization = 'Bearer ' + localStorage.getItem(constants.localStorage.LOCAL_STORAGE_TOKEN);

        return ajaxRequester.get('/api/users/projects', options);
    }

    function getByCategory(id) {
        var options = {};

        return ajaxRequester.get('/api/projects/category/' + id, options);
    }

    function updateProject(id, project) {

    }

    function deleteProject(id, project) {

    }

    function commentProject(id, comment) {

    }

    function addAttachmentToProject(id, attachment) {

    }

    function enrollInProject(id) {
        var options = {};
        options.headers = {};
        options.headers.Authorization = 'Bearer ' + localStorage.getItem(constants.localStorage.LOCAL_STORAGE_TOKEN);

        return ajaxRequester.put('/api/projects/' + id, options);
    }

    function startProjectSession(id, workLog) {
        var options = {};
        options.data = workLog;
        options.headers = {};
        options.headers.Authorization = 'Bearer ' + localStorage.getItem(constants.localStorage.LOCAL_STORAGE_TOKEN);

        return ajaxRequester.post('/api/projects/session/' + id, options);
    }

    function finishProjectSession(id) {
        var options = {};
        options.headers = {};
        options.headers.Authorization = 'Bearer ' + localStorage.getItem(constants.localStorage.LOCAL_STORAGE_TOKEN);

        return ajaxRequester.put('/api/projects/session/' + id, options);
    }

    function finalizeProject(id) {
        var options = {};
        options.headers = {};
        options.headers.Authorization = 'Bearer ' + localStorage.getItem(constants.localStorage.LOCAL_STORAGE_TOKEN);

        return ajaxRequester.put('/api/projects/complete/' + id, options);
    }

    function getProjectInvoice(id) {
        var options = {};
        options.headers = {};
        options.headers.Authorization = 'Bearer ' + localStorage.getItem(constants.localStorage.LOCAL_STORAGE_TOKEN);

        return ajaxRequester.get('/api/projects/complete/' + id, options);
    }

    return {
        users: {
            register: register,
            login: login,
            getRole: getUserRole
        },
        categories: {
            all: allCategories,
            add: addCategory,
            get: getCategory
        },
        projects: {
            all: allProjects,
            get: getProject,
            getUserProjects: getUserProjects,
            getByCategory: getByCategory,
            create: createProject,
            update: updateProject,
            delete: deleteProject
        },
        projectsActions: {
            addComment: commentProject,
            addAttachment: addAttachmentToProject,
            enroll: enrollInProject,
            startSession: startProjectSession,
            finishSession: finishProjectSession,
            finalize: finalizeProject,
            getInvoice: getProjectInvoice
        }
    }
}();
