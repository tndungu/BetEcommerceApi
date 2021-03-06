# BetEcommerceApi

Ecommerce Backend API using .Net Core. 
Its the backend middleware for a complete Ecommerce Application that is built using React frontend, .Net Core API and MS SQL Server database. 
The front end is available at [React Front-end Repository](https://github.com/tndungu/betecommerceapp.git).

## Tools
The middleware is built using .NET Core 6.0 Framework. Successful build has been tested using Visual Studio 2022 Community edition. Also utilizes 
dependencies like NUnit Testing Framework, EntityFramework Core among others.

## Setup
The following steps are for setting up locally
1. Clone the repository using the following command:
  `git clone https://github.com/tndungu/BetEcommerceApi.git`
2. In `appsettings.json` file in BetEcommerce.Api project, update the following:
  ConnectionStrings - Update the properties Server, User ID and Password to correspond to your local database environment.
  MailSettings - Update the SMTP details to correspond to the SMTP Mail provider that you will be using. The project has been tested using Gmail SMTP, 
  other mail providers might   have different requirements like SMTP DeliveryMethods which have not been taken into account in this project. 
  If you decide to go with Google SMTP, please ensure under your Google Account you Enable less Secure Apps under Google Account -> Security -> Enable less secure Apps
3. Create Database named **BetEcommerce**
4. Run the Script in the folder `\SQL Scripts\SqlScripts.sql` against the created database.
5. Build and run the application. The project should open in the URL https://localhost:7066/swagger/index.html which will show you all available endpoints. 
  Should you require to    change the port number for instance it might not be available on your local environment, remember to update it on the front end 
  React app under `..\Src\api\config.js` file.

