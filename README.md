#Testro - Test Management System

###Requirements
- .NET SDK 5, you can download it from:
https://dotnet.microsoft.com/download/dotnet/5.0
- Docker, you can download it from:
https://www.docker.com/products/docker-desktop

###How to setup project

1. Clone repository from master branch

2. In terminal

```bash
docker run -p 3306:3306  --name maria_testro -e MYSQL_ROOT_PASSWORD=mypassword mariadb:10.5
```

3. Open terminal and go to path with project `../../test-manager/Testro.TestingManagement.WebApi`

4. Enter commands

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update
```

5. After all those steps enter command

```bash
dotnet run
```

Your default browser should open new tab with swagger if project runs but tab with swagger was not opened go to
https://localhost:5001/swagger/index.html in your browser.
