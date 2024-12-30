    INSTRUKTIONER!!!

    En teammedlem ska kunna sätta upp databasen lokalt genom att:
    Skapa en tom databas
    Justera anslutningssträngen.
    Köra dotnet ef add [name]
    Köra dotnet ef database update för att applicera migrations.
    Alla migrations och inställningar ska fungera utan problem i en ny miljö.

    OBS!
    Varje gruppmedlem ansvarar för att Constrains för sina modeller(User.cs) implementeras i EcommerceContext.cs/OnModelCreating()
