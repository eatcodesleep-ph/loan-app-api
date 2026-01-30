# Loan App API (.NET 8)
An api use for loan app web for processing loan quotation and application

## Prerequisites
- .NET 8 SDK

## Configure
Add User Secrets:
- Manage User Secrets in Visual Studio
- Copy and Paste the secret config sent on the email (Filename: LoanAppApi-Secrets.txt)

# Database Migration
1. Install ef tool version 8 via terminal, Run Command: dotnet tool install --global dotnet-ef --version 8.*
2. Run command to change directory: cd "src/LoanApp.Infrastructure"
3. Run command to initiate migrations: dotnet ef migrations add InitialCreate -s ..\LoanApp.Api\LoanApp.Api.csproj
4. Run Command to update the database: dotnet ef database update -s ..\LoanApp.Api\LoanApp.Api.csproj

# Steps on running the application
1. Open the solution in Visual Studio 2022 or later
2. Set the Startup Project to LoanApp.Api
3. Run the application (F5)
4. The API will be accessible at https://localhost:{port}/swagger
5. You can test the API endpoints using Swagger UI
6. To get bearer token, use the /authentication/v1/ endpoint with valid credentials (credentials will be found on LoanAppApi-secrets.txt)
7. Copy the token from the response and use it to authorize requests in Swagger UI
8. To iniate loan application, use the /loanApplication/v1/ endpoint with the required payload
9. The endpoint will process the loan application and return the result with redirect URL if successful
10. Use the redirect URL to proceed with the loan application process on the Loan App Web (make sure the loan-app-web is running)
11. Follow the prompts on the Loan App Web to complete the application