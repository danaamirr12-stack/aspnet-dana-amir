# CoreFitness – ASP.NET Core MVC

Det här projektet var nog det tuffaste hittills. Mycket kod, många lager och massa saker som skulle hänga ihop – men jag kämpade mig igenom det och är faktiskt ganska nöjd med resultatet.

## Vad är det?

En gymportal byggd i ASP.NET Core MVC där man kan registrera konto, logga in, skaffa medlemskap, boka gymklasser och hantera sin profil. Admins kan lägga till och ta bort klasser.

## Arkitektur

Jag lade extra fokus på Clean Architecture eftersom det lyftes fram som viktigt. Projektet är uppdelat i fem lager – Domain, Application, Infrastructure, Web och Tests – där varje lager har ett tydligt ansvar.

## Kom igång

Klona repot, kontrollera connection string i `appsettings.json` och kör sedan:

```bash
dotnet ef database update --project CoreFitness.Infrastructure --startup-project CoreFitness.Web
dotnet run --project CoreFitness.Web
```

Vill du ha admin-behörighet, registrera dig med `admin@corefitness.se`.

## Tester

Fem enhetstester finns i `CoreFitness.Tests`, kör dem med `dotnet test`.
