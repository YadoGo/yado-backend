# YADO - Backend

## Index

- [YADO - Backend](#yado---backend)
  - [Index](#index)
  - [Introduction](#introduction)
  - [Requirements](#requirements)
  - [Installation](#installation)
  - [Usage](#usage)
  - [Features](#features)
  - [Contribution](#contribution)
  - [License](#license)

## Introduction

[YADO - Backend](https://github.com/YadoGo/yado-backend) is the backend part of the YADO project, which serves as the API for user authentication, hotel data management, and more. It is developed using ASP .NET Core with an MVC (Model-View-Controller) architectural pattern. This backend is designed to work seamlessly with the [YADO - Frontend](https://github.com/YadoGo/yado-frontend) Angular application. The primary goal of YADO is to enhance the hotel search and management experience for travelers.

## Requirements

- [ASP .NET Core](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/get-started/overview/install)

## Installation

1. Clone this repository to your local machine.

```bash
git clone https://github.com/YadoGo/yado-backend.git
```

2. Navigate to the project directory.

```bash
cd yado-backend
```

3. Restore the required NuGet packages.

```bash
dotnet restore
```

4. Apply migrations to set up the database.

```bash
dotnet ef database update
```

## Usage

1. Start the development server.

```bash
dotnet run
```

2. The API will be available at `http://localhost:5000`.

## Features

- **Backend Technology**: ASP .NET Core with MVC architecture
- **Database**: Entity Framework Core
- **License**: [MIT License](LICENSE)

## Contribution

If you want to contribute to this project, follow these steps:

1. Fork the repository.
2. Create a branch for your new feature: `git checkout -b feature/awesome-feature`.
3. Make your changes and commit: `git commit -m 'Add an awesome feature'`.
4. Push your changes to your fork: `git push origin feature/awesome-feature`.
5. Open a pull request in the main repository.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
