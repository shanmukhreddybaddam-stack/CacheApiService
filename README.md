# CacheApiService

## Overview
CacheApiService is a .NET-based service designed to provide efficient caching mechanisms for applications. It helps improve performance by reducing redundant data fetching and optimizing resource usage.

## Features
- In-memory caching for fast data retrieval.
- Configurable expiration policies.
- Easy integration with existing .NET applications.
- Background service to periodically fetch and update cached data from an external API.
- API endpoints for accessing cached data.
- Comprehensive logging for monitoring and debugging.

## Project Structure
The project is organized into the following key directories and files:

- **Configurations/**: Contains configuration classes for the application.
  - `CacheSettings.cs`: Defines settings for cache expiration and other cache-related configurations.
  - `ExternalApiOptions.cs`: Defines settings for interacting with the external API, such as base URL and API key.

- **Controllers/**: Contains API controllers that define endpoints for accessing data.
  - `DataController.cs`: Provides endpoints for accessing cached data.

- **IService/**: Contains interface definitions for services used in the application.
  - `IBackgroundDataLoader.cs`: Interface for the background data loader service.
  - `ICacheService.cs`: Interface for the caching service.
  - `IExternalApiService.cs`: Interface for the external API service.

- **Models/**: Contains data models used in the application.
  - `Post.cs`, `PostResponse.cs`: Models for post data.
  - `Product.cs`, `ProductResponse.cs`: Models for product data.
  - `User.cs`, `UserResponse.cs`: Models for user data.

- **Service/**: Contains service implementations for the application.
  - `BackgroundDataLoader.cs`: A background service that periodically fetches data from an external API and updates the cache.
  - `CacheService.cs`: Implements caching logic for storing and retrieving data.
  - `ExternalApiService.cs`: Handles communication with the external API.

- **Properties/**: Contains project properties and settings.
  - `launchSettings.json`: Defines launch settings for the application.

- **Program.cs**: The entry point of the application, where services are configured and the application is started.
- **appsettings.json**: Contains application-wide configuration settings.
- **appsettings.Development.json**: Contains development-specific configuration settings.

## How the Application Works
1. **Startup**:
   - The application starts by configuring services in `Program.cs`.
   - Services such as `CacheService`, `ExternalApiService`, and `BackgroundDataLoader` are registered in the dependency injection container.

2. **Configuration**:
   - Configuration settings are loaded from `appsettings.json` and `appsettings.Development.json`.
   - These settings include cache expiration time and external API details.

3. **Background Data Loading**:
   - The `BackgroundDataLoader` service runs in the background, periodically fetching data from the external API.
   - The fetched data is stored in the cache using the `CacheService`.

4. **API Endpoints**:
   - The `DataController` provides endpoints for accessing cached data.
   - Clients can retrieve data from the cache without directly interacting with the external API, improving performance and reducing API calls.

5. **Caching**:
   - The `CacheService` handles storing and retrieving data from the cache.
   - Cached data is refreshed periodically by the `BackgroundDataLoader` to ensure it remains up-to-date.

6. **Logging**:
   - The application uses the built-in .NET logging framework to log important events, errors, and debug information.
   - Logging is configured in `Program.cs` and can be customized to log to the console, files, or external systems.
   - The `ILogger` interface is used throughout the application to log messages, making it easier to monitor the application's behavior and troubleshoot issues.
   - Example log messages include:
     - Successful data fetches from the external API.
     - Cache updates and expiration events.
     - Errors encountered during API calls or background tasks.

## Prerequisites
- .NET SDK installed on your system.
- Basic knowledge of .NET and C#.

## Installation
1. Clone the repository:
    ```bash
    git clone https://github.com/shanmukhreddybaddam-stack/CacheApiService.git
    ```
2. Navigate to the project directory:
    ```bash
    cd CacheApiService
    ```
3. Restore dependencies:
    ```bash
    dotnet restore
    ```

## Usage
1. Build the project:
    ```bash
    dotnet build
    ```
2. Run the service:
    ```bash
    dotnet run
    ```
3. Access the API at `http://localhost:5137`.

## Configuration
- Modify the `appsettings.json` file to configure caching options, such as expiration time and external API settings.
- Example configuration in `appsettings.json`:
    ```json
    {
      "CacheSettings": {
        "ExpirationTimeInMinutes": 10
      },
      "ExternalApiOptions": {
        "BaseUrl": "https://api.example.com",
        "ApiKey": "your-api-key"
      }
    }
    ```

## Contributing
Contributions are welcome! Please fork the repository and submit a pull request.

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.
