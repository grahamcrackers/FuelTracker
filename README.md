<a href="#travis-badge">
    <img alt="travic-ci build" src="https://travis-ci.com/grahamcrackers/FuelTracker.svg?branch=master">
</a>
<a href="https://codecov.io/gh/grahamcrackers/FuelTracker">
  <img src="https://codecov.io/gh/grahamcrackers/FuelTracker/branch/master/graph/badge.svg" />
</a>

# Gas Tracker API

## Overview

The Gas Tracker API is an API for tracking gas milage and generating reports and data for your vehicles. The [GasTracker UI](https://github.com/grahamcrackers/gas-tracker-ui) sits on top of it to provide an up to date UI for easy interaction.

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

## Inspiration

- https://garywoodfine.com/generic-repository-pattern-net-core/
- https://fullstackmark.com/post/20/painless-integration-testing-with-aspnet-core-web-api
