    ## Instruktioner: Test av databas
    För att sätta upp en databas genom projektet och ansluta den lokalt behöver du:
    - Skapa en tom databas i psql (ecommerce/database=[name] i connectionString)
    - Justera anslutningssträngen efter lokala behov.
    - Gör potentiella ändringar
    - Kör dotnet ef add [name] för att commita ändringar
    - Kör dotnet ef database update för att applicera migrations.
    - Migrationen kan granskas via Migrations-foldern/filerna som skapas.
    - Alla migrations och inställningar ska fungera utan problem i en ny miljö.
    
    OBS!
    Varje gruppmedlem ansvarar för att Constrains för sina modeller(User.cs, ShoppingCart.cs, etc.) implementeras i EcommerceContext.cs/OnModelCreating()
