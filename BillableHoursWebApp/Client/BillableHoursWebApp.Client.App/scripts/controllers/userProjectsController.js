var userProjectsController = function() {
    function all(context) {
        
    }

    // this is the view where you would edit a project
    function getById(context) {
        var id = context.params['id'];

    }

    return {
        all: all,
        getById: getById
    }
}();
