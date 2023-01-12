# SkyScanner-Project
In the appsettings.json change the DefaultConnection to the connection string of your sql server.
The string must contain TrustServerCertificate=True;Trusted_Connection=True; commands.

After that run this commands in the NugetPackageManger console in the order they are presented here:

EntityFrameworkCore\Add-Migration UserDb -context UserDbContext

EntityFrameworkCore\update-database -context UserDbContext

EntityFrameworkCore\Add-Migration FlightDb -context FlightDbContext

EntityFrameworkCore\update-database -context FlightDbContext

You can Add Admin Users by changing the value of the Admin bool in the User DataBase

Link to the site: https://skyscannerweb.azurewebsites.net/

Link to Git: https://github.com/MarkOS5623/SkyScanner-Project.git
