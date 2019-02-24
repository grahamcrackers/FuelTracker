# Gas Tracker API
## Overview

The Gas Tracker API is an API for tracking gas milage and generating reports and data for your vehicles. The [GasTracker UI]() sits on top of it to provide an up to date UI for easy interaction.

### Set up the Database - Entity Framework Code First
Currently I'm using `Sqlite` for quick development and storing the database locally.
```
dotnet ef migrations add <Migration> -o ./Data/Migrations/
dotnet ef database update
```

#### Seed the Database
```
cd ./Data
sqlite3 gastracker.db < seed.sql
```

### Repository Pattern
* https://garywoodfine.com/generic-repository-pattern-net-core/