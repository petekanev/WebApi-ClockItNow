var data = function() {
    function register(user) {
        
    }

    function login(user) {
        
    }

    function logout(user) {
        
    }

    function createProject(project) {
        
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
            logout: logout
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
