# TodoApi
A simple API that allows users to Create, Retrieve, Update and Delete (CRUD) to-do list items to a MongoDB instance.

## Pre-requisites
An instance of MongoDB - appsettings.Development.json is defined for a local instance (localhost:27017)

## API Endpoints
The Todo CRUD API provides the following endpoints:

GET: /api/Tasks - Get all tasks
GET: /api/Tasks/{id} -  Get a tasks by ID.
POST: /api/Tasks -  Create a new task.
PUT: /api/Tasks/{id}: Update an existing task.
DELETE: /api/Tasks/{id}: Delete a task by ID.

Utilise the Swagger page (localhost:7138/swagger/index.html) to call the API

Example of Task document:
    {
        "id": "663cbcac4021bcb6a2ee6421",
        "name": "Task item 1",
        "isComplete": true
    }
	
## To Do
Implementation of logging framework, to capture any errors/warnings
