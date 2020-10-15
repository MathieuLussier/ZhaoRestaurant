# ZhaoRestaurant
Asp.Net Core Application for a fictitious restaurent.
 
[DEMO](https://zhao.mathieulussier.ca/)

## Installation

clone the project from github then copy over .env-exemple and fill the needed information.

```bash
cp .env-example .env
```

## Usage

use dotnet core to launch the app.

```bash
dotnet run
```

## Docker

You can use docker to launch the app too.

```bash
docker build -t <username>/zhao-app:0.1.0 .

docker run -p <port>:80 --name zhao-app -d <username>/zhao-app:0.1.0
```

# Todos

  - Adding database context.
  - Adding database connection to the database context.
  - Adding user authentification for dashboard.
  - Adding partial view for user to add an evaluation of the restaurant.
  - Rework the menu to be store and database and show in the view.
  - Adding the logic to apply discount.

### Technologies

* [NetCore] - Computer Software Framework
* [MVC] - Model–view–controller - Software Design Pattern
* [Razor] - Template Framework 

## License
[MIT](https://choosealicense.com/licenses/mit/)

