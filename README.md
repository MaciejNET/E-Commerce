# Ecommerce Web Api

![example workflow](https://github.com/MaciejNET/E-Commerce/actions/workflows/dotnet.yml/badge.svg)

This project is an Ecommerce Web API built using the modular monolith architecture. Each module is responsible for a specific business capability. This approach combines the simplicity of a monolith with the flexibility of microservices. Modules communicate with each other using messages via in-memory message broker. The Bootstrapper project is responsible for starting up the application. It loads all the modules, registers their services, and sets up the middleware pipeline. It's designed to study and practice Domain-Driven Design (DDD) concepts. The project is written in C# and uses .NET 7, Docker, SQL Server, xUnit, and GitHub Actions.

## Event Storming
[Here](https://miro.com/app/board/uXjVMy3rVUc=/?share_link_id=104061673349) you can find the Miro board with the Event Storming session.

## Architecture
![image](EcommerceArchitecture.jpg)

## Technologies
 - .NET 7
 - Docker
 - Sql Server
 - xUnit
 - Github Actions

## How to run
```bash
docker-compose up -d
cd src/Bootstrapper/ECommerce.Bootstrapper/
dotnet run
```