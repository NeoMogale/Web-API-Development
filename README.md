CMPG323-Project-2-35407972
NWU Tech Trends API Development

Overview
This project involves developing a RESTful API for NWU Tech Trends to track time and cost savings from automations. The API interfaces with a database that stores telemetry data, allowing stakeholders to retrieve, update, and manage this information. The database is hosted on Microsoft Azure.

About the Project
The purpose of this API is to monitor and report on the efficiency gains achieved by automations created by NWU Tech Trends. Each time an automation runs, telemetry data is recorded, including time saved and associated costs. This data is grouped by project and client, and is accessible through the API.

Database Structure
The NWUTechTrends database includes the following tables:

Client: Information about each client.
Project: Project details for each client.
Process: Details of the processes associated with each project.
Telemetry: Records the time and cost savings generated by automations.

Microsoft Azure Integration
add sql pic here.

Microsoft Azure Setup:
// add azure pic here
Created an Azure account.
Set up a monthly budget to manage resource credits.
Created a resource group named rgTechTrends.
Deployed a SQL server and database within this resource group.
API Creation:

Developed the API using Visual Studio.
Added controllers to handle CRUD operations for the database tables.
Implemented JWT-based authentication to secure API access.
Hosting the API:

The API was published via Visual Studio and deployed to Azure.
//add vs to azure pic here

Security
The API is secured using token-based authentication to ensure only authorized users can access it. Sensitive data such as passwords is excluded from the source code using a .gitignore file.

API Usage Guide
To use the API, stakeholders need to follow these steps:
1. Register an Account:
Visit the API registration endpoint to create an admin user account. This account will be used to access and manage the telemetry data.

2. Log In:
Use the registration credentials to log in. A successful login returns an authentication token, which is required for further API requests.

Using the API:
Endpoints

GET /api/clients: Retrieve a list of all clients.
GET /api/clients/{id}: Retrieve details of a specific client.
POST /api/clients: Create a new client entry.
PUT /api/clients/{id}: Update client details.
DELETE /api/clients/{id}: Delete a client.
Project Data

GET /api/projects: Retrieve a list of all projects.
GET /api/projects/{id}: Retrieve details of a specific project.
POST /api/projects: Create a new project entry.
PUT /api/projects/{id}: Update project details.
DELETE /api/projects/{id}: Delete a project.
Process Data

GET /api/processes: Retrieve a list of all processes.
GET /api/processes/{id}: Retrieve details of a specific process.
POST /api/processes: Create a new process entry.
PUT /api/processes/{id}: Update process details.
DELETE /api/processes/{id}: Delete a process.
Telemetry Data

GET /api/telemetry: Retrieve a list of all telemetry records.
GET /api/telemetry/{id}: Retrieve details of a specific telemetry record.
POST /api/telemetry: Create a new telemetry record.
PATCH /api/telemetry/{id}: Update telemetry details.
DELETE /api/telemetry/{id}: Delete a telemetry record.
Authentication
Include the authentication token received upon login in the Authorization header for each request.

Example:

makefile
Copy code
Authorization: Bearer <YourTokenHere>
Example Workflow
Register: Create an admin account using the registration endpoint.
Log In: Obtain an authentication token.
Access: Use the token to authenticate requests to the API.
Manage Data: Use the endpoints to create, read, update, or delete data in the database.
Security Notes
The API is secured using token-based authentication.
Only authenticated admin users can access the API endpoints.
Sensitive server details and passwords are protected and not included in the codebase.
Additional Aspects Implemented
Error Handling: The API includes custom error messages and status codes to provide clear feedback to users.
Swagger Integration: The API is documented with Swagger, providing an interactive interface for exploring the endpoints.
Resource Group
[Add Link to Your Azure Resource Group Here]

Link to API
[Add Link to Your Deployed API Here]

