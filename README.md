# HTTP-Server

### About

The program implements the functions of the HTTP-server version 1.1. 

The following **types of requests** are implemented: 
* GET
* POST
* HEAD

the following **status codes** are supported:
* 200 OK
* 201 Created
* 400 Bad Request
* 404 Not Found
* 500 Internal Server Error

Static class Configuration (*Configuration.cs*) contains information about the server configuartion.
By default the server is running on port 8081.

The server is configured to a specific directory where the HTML files are located. 
By default it is a *web* subdirectory.

In the main window of the server there is a field in which the entire protocol of communication
of the HTML client with the HTML server is displayed.

### Contact

Karalina Dubitskaya
karalinadubitskaya@gmail.com
