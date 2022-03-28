# CalculatorService
Coding Challenge: Calculator Service - Evicertia

Application realized in my practices in Evicertia.

It is a calculator in which the user chooses from a list of operations and, after entering the data, receives an answer.

It also gives the possibility of save and read the history of the operations you have realized, saving them associated with your user.
It also allows you to change users.

When starting the program, the server and the client will be started at the same time.

The application consists of a client, a server and a library with the models used.
The client is a console application with which the user will interact and connect to the server to send and receive a response.
The server is a web application which will receive the data passed to it by the client and with them it will perform the necessary functions and return a response.
Models are classes that are used by both the client and the server to use the data. The data they send and receive is in JSON format, in order to work with them, these models are used.

The application is made in Microsoft Visual Studio Community 2019, in C#, using .NET Core 3.1.
