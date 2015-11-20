DISCLAIMER: This project was developed as part of a project assignment in the Web and Cloud Services @Telerik Academy and was written entirely for learning purposes

# Billable Hours - Clock It Now

`Project description`
* Clock It Now is a platform similar to freelancer.com but limited in functionality where clients can make enquiries in the form of posts for any job that they need to see done. Payment is on hourly basis depending on logged sessions by the employee who has undertaken the project. Invoices are handed out after a project is properly completed.

`User Roles`
 * **Not Registered users**
 * * can **view** all projects and recent activity
 * **Registered clients**
 *  *   can add new projects
 *  *   can preview invoices of their completed projects
 *  *  live chat
 * **Registered employees**
 *  *   can work on projects
 *  *   can start work session on a project they are working on
 *  *   can finalize project and send invoice to client
 *  *  live chat

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
|GET (public) | api/categories | Returns all categories in the system |
|GET (public) | api/categories/{id} | Returns the category with the provided Id |
|POST | api/categories | An authenticated user can create a category |
|PUT | api/categories | An authenticated user can edit a category name |
