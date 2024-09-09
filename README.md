1 - 
descargue el código de el siguiente enlace
https://github.com/ccmmasi2/EmployeesAndBeneficiaries.git

2-
Cuando lo descargue encontrará dos carpetas iniciales:
beneficiariesweb
	proyecto en angular
	Angular CLI: 16.2.15
	Node: 18.20.1
	Package Manager: npm 10.5.0
	
Beneficiaries.Solution
	proyecto de visual studio 2022
	NET Core 6

3 - 
Dentro de Beneficiaries.Solution ejecute el archivo Beneficiaries.Solution.sln 
El proyecto fue creado sobre visual studio 2022
NET Core 7

4 - 
Ejecute la aplicación

5 - 
La aplicación creará la base de datos automáticamente
	tablas
	procedimientos almacenados
Se insertarán datos predeterminados en una tabla pais

6 - 
Al ejecutar la aplicación de visual studio se espera que tenga SqlServer Instalado y en lo posible el Management Studio

7 -
Esta predeterminado para crear la bd en localhost de SqlServer 

8 - 
Si tiene una configuración diferente de Sql por favor valide la cadena de conexión en la api en el archivo appsettings.Development.json y busque BeneficiariesConectionDB
IMPORTANTE
Esta cadena de conexión se encuentra en DOS ARCHIVOS
	a - En el API en el archivo appsettings.json/appsettings.Development.json
			Esto para manejar las transacciones a la bd
	b - En la libreria de clases en el archivo appsettings.json
			Se agregó este archivo aca para trabajar con migrations. 
				Migrations en .Net requiere un archivo de configuración en el mismo proyecto para realizar los cambios en la bd. 

9 - 
Despliegue la aplicación sobre Visual Studio 2022, si todo se genera correctamente debería ver el swagger, la base de datos creada y datos insertados

10 - 
Abra el proyecto beneficiariesweb de angular abriendo ventana de comandos sobre la ruta y pulsando "code ."


11 - 
Una vez abierto abra una ventana de comandos

12 -
Instale los node_modules con "npm install"

13 -
Tome nota del puerto en que se ejecuta el visual studio. Valide que el Swagger se ejecute. 
Valide el puerto en el archivo enviroment/enviroment.ts, ajuste la variable baseUrl si es necesario

14 -
Ejecute la aplicación con "ng serve -o"





Herramientas, tecnologías y características usadas:
-----------------------------------------------------------------
-----------------------------------------------------------------
-----------------------------------------------------------------


Patron repository
-----------------------------------------------------------------
Inyección de dependencias
	Se creó la interface en la librería de clases, su implementación en la API, se obtiene la cadena de conexión de forma segura desde la biblioteca de clases para crear la bd. 
-----------------------------------------------------------------
Herencia
	En la carpeta models se pueden encontrar diferentes clases que estan relacionadas por medio de herencia, lo cual permite reutilizar código. 
-----------------------------------------------------------------
Uso de migrations
	Para generar la bd se usó migrations, uno para crear los procedimientos almacenados y otro para crear tablas. 
		El código dentro de la migratión para generar tablas fué creado mediante comandos de EntityFrameworkCore
-----------------------------------------------------------------
		El código para crear los procedimientos almacenados fue creado a mano. 
Uso de seedData
	Para el listado de paises, se hace uso de EntityFramework para validar si hay datos que insertar a la tabla y si es así se insertan
-----------------------------------------------------------------
La intensión del proyecto es crear la bd, por ese motivo se hizo necesario añadir los archivos appsettings tanto en la raiz de la API como en la raiz de la 	libreria de clases. 
	Adicionalmente a esto se creó el contexto AppDbContextFactory para completar esta tarea de generar la bd. 
-----------------------------------------------------------------
Se generó una clase genérica PagedList para definir la funcionalidad de paginación
-----------------------------------------------------------------
Tenemos la capa Object Repository como la capa más cercana a la bd
	En esta capa usamos dos maneras para conectarnos a la bd, usando procedimientos almacenados en ambas 
		1 - Usando conexión mediante Entity Framework Core, este permite realizar consultas de manera mas simple
		2 - Usando ADO .NET, lo que permite llamar a procedimientos almacenados con un control mas fino de lo que se hará con ella. 
-----------------------------------------------------------------
Tenemos la capa BusinessLogic como la capa más cercana a la API 
-----------------------------------------------------------------

API cors
-----------------------------------------------------------------
En la API habilitamos los cors para permitir acceso a cualquier origen
-----------------------------------------------------------------
manejamos la inyección de dependencias para los servicios de transacción de tipo Scoped, lo cual me permite usar la conexión al servicio por cada request realizado, se usa singleton para la conexión a la bd en los servicios ADO para mantener vivo este servicio durante todo el ciclo de vida de la aplicación. 
-----------------------------------------------------------------
En la parte inferior del program.cs podemos encontrar el llamado a crear la bd al ejecutar la aplicación. 
-----------------------------------------------------------------


Proyecto web en Angular 
-----------------------------------------------------------------
Uso de directiva para validar campo numerico entre 1 y 100 
Uso de directiva para validar campo numerico de maximo 10 caracteres
-----------------------------------------------------------------
uso de RXJS para permitir cambios reactivos en la aplicación, haciendo uso de observables para mantener información actualizada
-----------------------------------------------------------------
Uso de angular material para mostrar información
-----------------------------------------------------------------
