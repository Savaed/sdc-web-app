## SDCWebApp

[![License:MIT](https://img.shields.io/badge/license-MIT-brightgreen.svg)](https://github.com/Savaed/SDCWebApp/blob/master/LICENSE)
[![Tests:unit](https://img.shields.io/badge/tests-unit-brightgreen.svg)](https://github.com/Savaed/SDCWebApp/tree/master/UnitTests)

**SDC Web App** is a website where an unregistered user can check a lot of information about sightseeing, and also order tickets 
online to a fictional museum. It is also a kind of platform for registered users (administrators and moderators), whose main 
functions are managing user accounts and all available resources, such as ticket price lists, discount information or articles 
stored in the server database, etc., as well as generating dynamic statistics of visits. Statistics have not yet been implemented 
and will be added in future improvements.

The project uses the [API](https://documenter.getpostman.com/view/8564183/SVmvTyga?version=latest) to communicate with the server. 
The development of this API and its documentation was the **main goal** of the whole project and allowed me to learn many things 
related to working as a backend programmer.

[Here](https://sdc-app.azurewebsites.net) is a working project website.

> It is worth mentioning that this project was created only for **educational purposes** and cannot be considered as a real-world 
application. 

**Main technologies and tools:**

- **Backend:**
	- C#
	- ASP .NET Core Web Api 2.2
	- Entity Framework Core
	- Autofac
	- NUnit, Moq, FluentAssertions
	- FluentValidation
	- Microsoft Azure
	- Microsoft SQL Server
	- Visual Studio 2019

- **Frontend**:
	- Angular 8
	- JavaScript, Type Script
	- HTML, CSS
	- Visual Studio Code

## Main features

All features can be divided into two types:
- for unregistered users
- for registered users (administrators and moderators)

As an **unregistered user**, you can view all available resources, e.g. articles, ticket price lists, etc. Also, you can easily 
order any type of ticket and download it to your computer in pdf format. After a successful order, a confirmation will be sent to 
the e-mail address provided.

As a **registered user**, you can manage all types of resources, which means you can add, edit or delete resources. However, some 
resources have limited modification options (e.g. tickets cannot be manually added, edited or deleted). Some actions are limited 
to users with administrator privileges, who can, for example, create new accounts (for moderators or other administrators) and view 
all logs generated by the server. One of the future improvements of the system will concern statistics. This will enable administrators 
and moderators to generate dynamic statistics, which may include the number of tickets sold, the most frequently chosen ticket type, 
the average price of one order and more.

## Future improvements

Although the project currently works as planned, and my main goal was not to build a complete system but to learn and create a 
portfolio, this project will be further developed. It is planned to add the possibility of generating statistics, such as the number 
of customers in a given period, the most popular ticket price lists and so on. Also, the server logging system is limited but is 
planned to be improved. Extended resource management features will be added shortly.
