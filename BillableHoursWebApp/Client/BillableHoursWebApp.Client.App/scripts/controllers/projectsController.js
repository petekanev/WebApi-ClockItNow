var projectsController = function() {
    function add(context) {
        templates.get('createProjectModal')
            .then(function(template) {
                context.$element().html(template());
                $('#btn-addinput').on('click', function() {
                    // append another div of inputs

                    var attachmentContainer = $('<div class="well well-sm attachment-container">')
                      .append('<input type="text" class="form-control attachment-name" value="" placeholder="Attachment Name" />')
                      .append('<input type="text" class="form-control attachment-url" value="" placeholder="example.com/attachment1.zip" />');

                    $('#input-attachments').append(attachmentContainer);

                    return false;
                });

                $('#btn-post').on('click', function() {
                    var project = {};
                    project.Name = $('#input-title').val();
                    project.Description = $('#input-description').val();
                    project.CategoryId = 2; // GET FROM DATA ATTRIBUTE
                    project.DueDate = $('#input-duedate').val() || null;
                    project.IsComplete = false;
                    project.PricePerHour = $('#input-payment');

                    var attachments = $('#input-attachments').children();

                    //foreach attachments and append to project.Attachments

                    return false;
                });
            });
    }

    function all(context) {
        
    }

    return {
        add: add,
        all: all
    }
}();
