var projectsController = function () {
    function add(context) {
        if (localStorage.getItem(constants.localStorage.LOCAL_STORAGE_ROLE) !== "2") {
            context.redirect('/#');
            toastr.info('You cannot add a project if you are not registered as a Client!');
        }

        var categories;

        data.categories.all()
            .then(function (res) {
                categories = res;

                return templates.get('createProjectModal');
            },
            function (err) {
                toastr.error('An error occurred while trying to fetch information from the server...');
                context.redirect('/#');
            })
            .then(function (template) {
                context.$element().html(template(categories));

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
                    project.CategoryId = Number($('#input-category').find(":selected").attr('data-catid'));
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

                        if (attachment.Name.length <= 0 || attachment.Url.length <= 1) {
                            continue;
                        }

                        project.Attachments.push(attachment);
                    }

                    // validate data
                    if (project.Name.length < 1) {
                        toastr.error('Project name is required')
                        return;
                    }

                    if (project.Description.length < 1) {
                        toastr.error('Project description is required')
                        return;
                    }

                    if (isNaN(project.PricePerHour)) {
                        toastr.error('Pay/Hour must be valid number');
                        return;
                    }

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

                $('#btn-clear').on('click', function () {
                    $('#input-attachments').empty().append($('<div class="well well-sm attachment-container">')
                      .append('<input type="text" class="form-control attachment-name" value="" placeholder="Attachment Name" />')
                      .append('<input type="text" class="form-control attachment-url" value="" placeholder="example.com/attachment1.zip" />'));
                    $('#input-payment').val('');
                    $('#input-title').val('');
                    $('#input-description').val('');
                    $('#input-title').val('');

                    return false;
                });
            });
    }

    function all(context) {
        var categories;

        data.categories.all()
            .then(function (res) {
                categories = res;

                return templates.get('categories');
            })
        .then(function (template) {
            context.$element().html(template(categories));
        }, function (err) {
            toastr.error('An error occurred while trying to fetch information from the server...');
            context.redirect('/#');
        });
    }

    function getById(context) {
        var id = context.params['id'];

        var project;

        data.projects.get(id)
            .then(function (res) {
                project = res;
                return templates.get('project');
            },
                function (err) {
                    toastr.error('Something hapened...');
                    console.log(err);
                    return false;
                })
            .then(function (template) {
                context.$element().html(template(project));
            }, function (err) {
                toastr.error('An error occurred while trying to fetch information from the server...');
                context.redirect('/#');
                console.log(err);
            });
    }

    function getByCategory(context) {
        var id = context.params['id'];

        var projects;

        data.projects.getByCategory(id)
            .then(function (res) {
                projects = res;
                return templates.get('projects');
            },
                function (err) {
                    toastr.error('Something hapened...');
                    console.log(err);
                    return false;
                })
            .then(function (template) {
                var container = context.$element().find('#projects-list');
                if (container.length !== 1) {
                    context.$element().html(template(projects));
                } else {
                    container.html(template(projects));
                }
            }, function (err) {
                toastr.error('An error occurred while trying to fetch information from the server...');
                context.redirect('/#');
                console.log(err);
            });
    }

    return {
        add: add,
        all: all,
        getById: getById,
        getByCategory: getByCategory
    }
}();
