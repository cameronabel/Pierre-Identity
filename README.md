# _Pierre's Treat Tracker_

#### By _Cameron Abel_

#### _Tool for treats and their flavors_

## Technologies Used

- _C#_
- _ASP.Net Core MVC_
- _Entity Framework Core_
- _MySQL_
- _Identity_

## Description

_Demonstrates many-to-many relationships, CRUD methods, and Identity using the ASP.NET MVC and MySQL._

## Setup/Installation Requirements

### Database Installation

A sample database `cameron_abel.sql` file is provided with this repository. If using MySQL Workbench, simply import this file as a new schema.

- Clone this repository to your local machine
- Navigate to `\Bakery`
- Create a new file called `appSettings.json`. Paste into this file the following code and replace `uid` and `pwd` fields with your own username and password for MySQL. If you changed the name of the database schema on import, update the `database` field to match:

```JSON
{
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=cameron_abel;uid=[NAME];pwd=[PASSWORD];"
  }
}
```

- Still within the `\Bakery` directory, execute `dotnet run`

## Known Bugs

Report bugs [here](mailto:cameronabel@gmail.com)

## License

_MIT_

Copyright (c) _2023_ _Cameron Abel_
