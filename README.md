# PaymentGatewayApi
The project shows Payment Gateway Api implementation in .NET Core 3.1 with JWT Token Authentication. It allows logged merchant to process the payment and retrieve details of previous payments that he made.

# How to start
1. Make sure SQL Express Server is installed
2. Make sure Soap UI 5.6.0 is installed
3. Download the repository
4. Go to the PaymentGatewayApi project and import the file: Bank-Mock-soapui-project.xml into Soap UI
5. Run Bank Mock service - right-hand click on RestBankMockService and click Start Minimized
6. Go back to the application and open Package Manager Console
7. Navigate to ./PaymentGatewayApi and execute command: dotnet ef database update
8. Application is ready to run. The start page is set to OpenAPI documentation

# Assumptions that were made
1. Bank Mock is Rest Service made in Soap UI. It has fixed balance of 5000 euros. It returns status and unique identifier generated from randoms numbers.
Success or failure of payment is based only on difference between payment amount and account balance.
2. The currency can be set by user but is not used for payment process.
3. Details of payment can be retrieved only by the user that pricessed the payment.
4. Basic card number validation and card validation is done.

# What could be improved
1. There should be more advanced card data validation
2. Data is database should be encrypted same as data sent from PaymentGatewayAPI to Bank Service
3. Application logging should be added
4. JWT Token Authentication should be switched to OAuth2 to provide better maintainability when system grows
5. Role-Based Authorization should be added
6. API versioning should be added