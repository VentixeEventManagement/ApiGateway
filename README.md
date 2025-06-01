ApiGateway
ApiGateway is a .NET 8 project that serves as an API Gateway using Ocelot, with integrated JWT authentication and Swagger documentation aggregation. The project is designed to route and secure API requests, providing a unified entry point for downstream services. It leverages the MMLib.SwaggerForOcelot library to aggregate Swagger endpoints, making it easier to explore and test APIs behind the gateway.
Features
•	Built with ASP.NET Core and Ocelot for API Gateway functionality.
•	JWT-based authentication and authorization for secure API access.
•	Aggregated Swagger UI for all downstream services using SwaggerForOcelot.
•	Customizable routing and endpoint management via ocelot.json.
•	Extensible architecture with support for custom interceptors and repositories.
Getting Started
1.	Clone the repository to your local machine.
2.	Configure environment variables and update appsettings.json and ocelot.json as needed for your environment and downstream services.
3.	Restore NuGet packages and build the solution using Visual Studio 2022 or the .NET CLI.
4.	Run the project. By default, the gateway is accessible at /gateway.
5.	Access Swagger UI at /gateway/swagger to explore and test the aggregated APIs.
Project Structure
•	Startup.cs: Configures services, authentication, Swagger, and Ocelot middleware.
•	Program.cs: Entry point for the application.
•	Repository/: Contains interfaces and implementations for managing Swagger endpoint data.
•	Security/: Contains JWT authentication services.
•	Controllers/: API controllers for gateway operations.
•	ocelot.json: Ocelot routing and downstream service configuration.
•	appsettings.json: Application configuration.
Testing
•	Integration and unit tests are located in the ApiGateway.Tests project.
•	Use your preferred test runner to execute tests and ensure gateway functionality.
Requirements
•	.NET 8 SDK
•	Visual Studio 2022 (recommended)
•	Downstream services with Swagger/OpenAPI documentation
Acknowledgments
•	Based on the MMLib.SwaggerForOcelot demo repository.
•	Utilizes Ocelot for API Gateway capabilities.
License
This project is provided as-is for demonstration and educational purposes. Please review and update the license as appropriate for your use case.
