# Employee Management API

Esta es una API RESTful construida con ASP.NET Core que permite gestionar una base de datos de empleados. La API soporta operaciones CRUD básicas y utiliza procedimientos almacenados para ciertas consultas.

## Características

- Crear un nuevo empleado.
- Obtener la lista de empleados.
- Obtener un empleado por ID.
- Actualizar un empleado.
- Eliminar un empleado.
- Obtener empleados contratados después de una fecha específica mediante un procedimiento almacenado.

## Requisitos

- .NET 7 SDK o superior
- SQL Server
- Visual Studio 2022 o superior (opcional, pero recomendado)

## Instalación

### 1. Clonar el repositorio

Clona el repositorio en tu máquina local:

bash
git clone https://github.com/sergioCamargoCamargo/EmployeeManagementAPI.git


### 2. Configurar la cadena de conexión
Abre el archivo appsettings.json y configura la cadena de conexión a tu base de datos SQL Server, reemplaza your_server_name con el nombre de tu servidor SQL:

-	"ConnectionStrings": {
		"DefaultConnection": "Server=[your_server_name];Database=DBEMPLOYEE;Trusted_Connection=True;TrustServerCertificate=True;"
	}
	

### 3. Crear la Base de Datos

- Abre SQL Server Management Studio (SSMS).
- Ejecuta el script que vas a encontrar en la carpeta llamada "DBScript" para crear la base de datos, la tabla de empleados y los procedimientos almacenados de forma automatica.


### 4. Ejecutar la Aplicación
- Abre el proyecto en Visual Studio.
- Presiona F5 para compilar y ejecutar la aplicación


## Uso de la API
### Endpoints Principales.
	-POST /api/employees: Crear un nuevo empleado.
	-GET /api/employees: Obtener la lista de empleados.
	-GET /api/employees/{id}: Obtener un empleado por ID.
	-PUT /api/employees/{id}: Actualizar un empleado.
	-DELETE /api/employees/{id}: Eliminar un empleado.
	-GET /api/employees/hiredAfter/{hireDate}: Obtener empleados contratados después de una fecha específica.

### Ejemplo de Petición.
	-Puedes utilizar herramientas como Postman para interactuar con la API. Ejemplo de una petición GET para obtener empleados contratados después de una fecha específica: "https://localhost:5001/api/employees/hiredAfter/2023-01-01"

##Pruebas

- Este proyecto utiliza xUnit para las pruebas unitarias. Para ejecutar las pruebas, puedes usar Visual Studio.

- Ejecutar Pruebas desde Visual Studio:
	- Abre el explorador de pruebas(Test Explorer) (Ctrl + E, T).
	- Ejecuta todas las pruebas para asegurarte de que pasan correctamente.
	
Licencia
Este proyecto está bajo la Licencia MIT.
