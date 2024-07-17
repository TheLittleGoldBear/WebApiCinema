This project is Web API in .NET ASP. There is used .NET 8, EF Core, Mapper.
To work with this project th sql needs to be initialized

**1. Create and connect data base**
Add migration and update datatbase. Take a look a the file appsettings.json if the server and database names works for your setup.

**2. Seed data**
There is also a file with seed data. After the sql data base is sucessfully created the data can be seed.
In the terminal you should open a folder with the seed.cs script and insert into termnal **dotnet run seeddata**
