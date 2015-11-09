(function() {
  var sammyApp = Sammy('#content', function() {
    this.get('#/users/login', usersController.login);
    this.get('#/users/register', usersController.register);
    this.get('#/users/logout', usersController.logout);

    this.get('#/home', homeController.all);

    this.get('#/projects', projectsController.all);

    this.get('#/users/projects', userProjectsController)

    this.notFound = function() {
      location.assign('#/home');
    }
  });

  $(function() {
    sammyApp.run('#/');

    ////
    // check if a user is logged in, hide/show buttons depending on that
    ////
  });
}());
