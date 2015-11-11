var projectsController = function () {
    function add(context) {
        // get categories from DB and send them as a parameter to the template() call

        templates.get('createProjectModal')
            .then(function (template) {
                context.$element().html(template());
                $('#btn-addinput').on('click', function () {
                    // append another div of inputs

                    var attachmentContainer = $('<div class="well well-sm attachment-container">')
                      .append('<input type="text" class="form-control attachment-name" value="" placeholder="Attachment Name" />')
                      .append('<input type="text" class="form-control attachment-url" value="" placeholder="example.com/attachment1.zip" />');

                    $('#input-attachments').append(attachmentContainer);

                    return false;
                });

                $('#btn-post').on('click', function () {
                    var project = {};
                    project.Name = $('#input-title').val();
                    project.Description = $('#input-description').val();
                    project.CategoryId = 1; // GET FROM DATA ATTRIBUTE
                    // project.DueDate = Date.parse($('#input-duedate').val()) || null;
                    project.DueDate = null;
                    project.IsComplete = false;
                    project.PricePerHour = Number($('#input-payment').val());
                    project.Attachments = [];

                    var attachments = $('#input-attachments').children();

                    //foreach attachments and append to project.Attachments
                    for (var i = 0; i < attachments.length; i++) {
                        var attachment = {};
                        attachment.Name = attachments[i].firstElementChild.value;
                        attachment.Url = attachments[i].lastElementChild.value;

                        project.Attachments.push(attachment);
                    }

                    // validate data

                    data.projects.create(project)
                        .then(function (res) {
                            toastr.info('Successfully posted a project!');
                            context.redirect('#/projects');
                            console.log(res);
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

    function all(context) {

    }

    return {
        add: add,
        all: all
    }
}();
