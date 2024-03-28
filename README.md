# River Almond Monitor

# Tech Stack

The tech stack for this software is as follows:

## API

- [.Net Core](https://github.com/dotnet/core)
- [Entity Framework Core](https://github.com/aspnet/EntityFrameworkCore)
- [Sql Server Express](https://www.microsoft.com/en-gb/sql-server/)

## UI

- [Next](https://nextjs.org/docs)
---

# Local Development: API

## Building the API
```
dotnet build
```

## Running the API
```
dotnet run -p dissertation/raag-api/
```

You can then open a web browser and use Swagger to interact with the API at either:

- http://localhost:5000/swagger
- https://localhost:5000/swagger

  


## Applying Database Migrations to your Local SQL Express DB


This will also create the SQL Express DB specified in the options file: 

```
<details>
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=RiverDataDb;Trusted_Connection=True;Encrypt=false"
},
```
</details>
---

```
dotnet ef database update -p raag-api/Migrations -s raag-api/Migrations


# Local Development: RAAG Front End

## Prerequisites

You will need [Node.js](https://nodejs.org) version 8.0 or greater installed on your system

### Initial Setup

- `cd` into the `dissertation/raag-front-end` directory
- `npm install`

Running the app

npm run start
```

The app should now be up and running at http://localhost:3000
```


