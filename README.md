<<<<<<< HEAD
# SALT-Challenge
=======
# Getting Started
### 1. Clone Repository:

```
git clone https://github.com/your-username/your-repo.git
cd your-repo
```
### 2. Install Dependencies:

```
dotnet restore
```

### 3. Configure Database:

- Open appsettings.json and update the database connection string. (This project is using mongodb for db)
- Run EF Core migration to create the database:
```
dotnet ef database update
```
- Build and Run
```
dotnet build
dotnet run
```
- The application will be accessible at `https://localhost:7065` or `http://localhost:5124` by default.

# API Endpoints
```
Get Max Grades:

Endpoint: /api/linq-query
Method: GET
Public endpoint without authentication.
Returns JSON data containing the MAX GRADE for each MATA KULIAH.
User Authentication: -
Response Example: [
	{
		"id": 1,
		"mhsId": 2,
		"mataKuliah": "DBMS",
		"grade": 80
	},
	....
]
```
```
Endpoint: /api/auth/login
Method: POST
Returns a JWT token for authentication.
Record Transaction:
Body: {
	"email": "admin@admin12.com",
	"password": "Admin"
}

Response Example:
{
	"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImFkbWluQGFkbWluMTIuY29tIiwibmJmIjoxNzAyNDU0NTI5LCJleHAiOjE3MDUwNDY1MjksImlhdCI6MTcwMjQ1NDUyOX0.qwiRjiduFtXi2JLlG2RBXQbLPTmsw-h3ki1ap8ZjUmI",
	"user": {
		"id": null,
		"email": "admin@admin12.com",
		"password": "Admin"
	}
}
```

```
Endpoint: /api/auth/register
Method: POST
Create a new users
Record Transaction:
Body: {
	"email": "admin@admin10.com",
	"password": "Admin"
}

Response Example:
{
	"id": "6579b14c4343d2bb771e4b49",
	"email": "admin@admin10.com",
	"password": "Admin"
}
```
```
Endpoint: /api/transaction/header/
Method: POST
Authentication Middleware
The application includes middleware to ensure that certain routes are accessible only to authenticated users. This is achieved using JWT tokens obtained from the /login endpoint.
Records a transaction header.
Body: {
	"CustomerName": "Budi3",
	"TransactionDate": "2023-12-12",
	"TotalAmount": 100
}
Response Example: {
	"id": "6579b2144343d2bb771e4b4a",
	"customerName": "Budi3",
	"transactionDate": "2023-12-12",
	"totalAmount": 100
}
```
```
Endpoint: api/transaction/header/:id
Method: PUT
Requires authentication using JWT token.
Returns a updated of recorded transactions.
Authentication Middleware
The application includes middleware to ensure that certain routes are accessible only to authenticated users. This is achieved using JWT tokens obtained from the /login endpoint.
Body: {
  "CustomerName": "Budi4",
	"TransactionDate": "2023-12-12",
	"TotalAmount": 100
}
Response Example: {
  "id": "6579b2144343d2bb771e4b4a",
	"customerName": "Budi4",
	"transactionDate": "2023-12-12",
	"totalAmount": 100
}
```

```
Endpoint: api/transaction/header
Method: GET
Requires authentication using JWT token.
Returns a list of recorded transactions.
Authentication Middleware
The application includes middleware to ensure that certain routes are accessible only to authenticated users. This is achieved using JWT tokens obtained from the /login endpoint.
Body: -
Response Example:[
	{
		"id": "657922bbb51daabb9aa68b2e",
		"customerName": "Budi",
		"transactionDate": "2023-12-12",
		"totalAmount": 100
	},
	{
		"id": "657957073c5ed5af0377097c",
		"customerName": "Budi2",
		"transactionDate": "2023-12-12",
		"totalAmount": 100
	},
 ...
]
```

```
Endpoint: api/transaction/header/:id
Method: GET
Requires authentication using JWT token.
Returns a list of recorded transaction by id.
Authentication Middleware
The application includes middleware to ensure that certain routes are accessible only to authenticated users. This is achieved using JWT tokens obtained from the /login endpoint.
Body: -
Response Example:{
	"id": "657922bbb51daabb9aa68b2e",
	"customerName": "Budi",
	"transactionDate": "2023-12-12",
	"totalAmount": 100
}
```
```
Endpoint: /api/transaction/detail/
Method: POST
Authentication Middleware
The application includes middleware to ensure that certain routes are accessible only to authenticated users. This is achieved using JWT tokens obtained from the /login endpoint.
Records a transaction detail.
Body: {
	"ProductName": "Indomilk",
	"Quantity": 5,
	"Price": 1000,
	"TransactionHeaderId": "65795987cc58e769b2561fd9"
}
Response Example: {
	"id": "6579b3f44343d2bb771e4b4b",
	"productName": "Indomilk",
	"quantity": 5,
	"price": 1000,
	"transactionHeaderId": "65795987cc58e769b2561fd9"
}
```
```
Endpoint: api/transaction/detail/:id
Method: PUT
Requires authentication using JWT token.
update transactions.
Authentication Middleware
The application includes middleware to ensure that certain routes are accessible only to authenticated users. This is achieved using JWT tokens obtained from the /login endpoint.
Body: 
	"ProductName": "Indomilk2",
	"Quantity": 5,
	"Price": 1000,
	"TransactionHeaderId": "65795987cc58e769b2561fd9"
}
Response Example: {
  "id": "6579b3f44343d2bb771e4b4b",
	"ProductName": "Indomilk2",
	"Quantity": 5,
	"Price": 1000,
	"TransactionHeaderId": "65795987cc58e769b2561fd9"
}
```

```
Endpoint: api/transaction/detail
Method: GET
Requires authentication using JWT token.
Returns a list of recorded detail transactions.
Authentication Middleware
The application includes middleware to ensure that certain routes are accessible only to authenticated users. This is achieved using JWT tokens obtained from the /login endpoint.
Body: -
Response Example:[
	{
		"id": "65795a85241450db38a9d001",
		"productName": "Indomilk",
		"quantity": 5,
		"price": 1000,
		"transactionHeaderId": "65795987cc58e769b2561fd9"
	},
	{
		"id": "6579b3f44343d2bb771e4b4b",
		"productName": "Indomilk",
		"quantity": 5,
		"price": 1000,
		"transactionHeaderId": "65795987cc58e769b2561fd9"
	},
...
]
```

```
Endpoint: api/transaction/detail/:id
Method: GET
Requires authentication using JWT token.
Returns a list of recorded transaction detail by id.
Authentication Middleware
The application includes middleware to ensure that certain routes are accessible only to authenticated users. This is achieved using JWT tokens obtained from the /login endpoint.
Body: -
Response Example:{
	"id": "65795a85241450db38a9d001",
	"productName": "Indomilk",
	"quantity": 5,
	"price": 1000,
	"transactionHeaderId": "65795987cc58e769b2561fd9"
}
```



# Database Migration
Ensure that you have configured the database connection string in appsettings.json. Run the following command to apply EF Core migrations and create the database:

```
dotnet ef database update
```

# Database
This application uses Entity Framework Core, which supports various database providers. This API using MongoDB as a DB. If you want to use a different provider, update the connection string and install the corresponding EF Core package.


# Contributing
1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Commit your changes and push to your fork.
4. Submit a pull request.

# License
This project is licensed under the MIT License.
>>>>>>> c47d1a1d599274c7a32e83a6a1784a69d2a1186c
