# WebServices-Team-Nike

## RESTful API Overview
| HTTP Method | Web service endpoint | Description |
|:----------:|:-----------:|:-------------|
|POST (public) | api/Account/Register | Registers a new client/employee |
|POST (public) | api/Account/login | Logs in a user |
|GET (public) | api/projects | Returns all projects in the system |
|GET (public) | api/projects/{id} | Returns the project with the provided Id |
|GET (public) | api/projects/category/{id} | Returns all projects in a category |
|POST | api/projects | A user logged in as a client can post a project |
|PUT | api/projects | A user logged in as an employee can register as the one working on the project |
|DELETE |api/projects|Delete an existing project|
|POST | api/projects/session/{id} | A user logged in as an employee can start a work session on a project they are working on |
|PUT | api/projects/session/{id} | A user logged in as an employee can end a work session on a project |
|PUT | api/projects/complete/{id} | A user logged in as an employee can finalize a project and send invoice to client |
|GET | api/projects/complete/{id} | A user logged in as a client can review the final invoice of a project they posted |
