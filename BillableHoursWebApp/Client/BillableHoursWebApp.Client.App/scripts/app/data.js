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

    }

    function startProjectSession(id, workLog) {

    }

    function finishProjectSession(id, workLog) {

    }

    function finalizeProject(id) {

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
            finalize: finalizeProject
        }
    }
}();
