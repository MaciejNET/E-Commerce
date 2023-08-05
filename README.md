# Ecommerce Web Api

![example workflow](https://github.com/MaciejNET/E-Commerce/actions/workflows/dotnet.yml/badge.svg)

Project created to study and practice modular monolith and DDD concepts.

[Here](https://miro.com/app/board/uXjVMy3rVUc=/?share_link_id=104061673349) you can find miro board with Event Storming session.

## Architecture
![image](EcommerceArchitecture.jpg)

## Modules
|Work progrss|Done|
|-------------|----|
|Users|❌|
|Discounts|⏳|
|Products|✅|
|Orders|❌|
|Returns|❌|
|Reviews|✅|

## Technologies
.NET 7, Docker, Sql Server(Azure SQL Edge), xUnit, Github Actions

## How to run
```bash
docker-compose up -d
cd src/Bootstrapper/ECommerce.Bootstrapper/
dotnet run
```