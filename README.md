# Drim.Interfaces.Shark

Aplicación MVC desarrollada sobre **ASP.NET Core .NET 8** para la gestión y ejecución de interfaces de negocio entre sistemas internos de Drim.

El proyecto gestiona principalmente:

- **Traspasos** entre almacenes y tiendas.
- **Inventarios** y procesos asociados.
- **Albaranes** y devoluciones.
- Configuración y seguimiento de procesos mediante archivos, pasos, parámetros y logs.

> Shark es la nueva versión migrada desde `Drim.Interfaces.MVC`.

## 1. Propósito del sistema

Drim.Interfaces.Shark permite:

- Consultar traspasos por almacén origen/destino, estado, artículo y fecha.
- Detectar incidencias, tickets y descuadres en traspasos.
- Gestionar devoluciones de proveedor y traspasos especiales `Y` / `YY`.
- Consultar y gestionar albaranes asociados a proveedores y almacenes.
- Cargar interfaces configuradas en la aplicación.
- Encadenar pasos de ejecución como archivos, consultas SQL, comparadores, DataExchange, logs, traspasos, inventarios y albaranes.
- Registrar y visualizar el estado de cada ejecución.
- Integrar los datos con SQL Server, FTP, rutas UNC, correo SMTP, SMS y servicios SOAP.

## 2. Funcionalidades principales

### Gestión de traspasos

- Traspasos entre almacenes.
- Consulta por fecha, estado, origen, destino y artículo.
- Incidencias de traspasos.
- Modificación de traspasos.
- Gestión de devoluciones.
- Cuadre de tickets y devoluciones.

### Gestión de inventarios

- Ejecución de interfaces relacionadas con inventarios.
- Integración con procesos configurados por pasos.
- Parametrización mediante entradas y salidas de cada proceso.

### Gestión de albaranes

- Consulta de albaranes por proveedor, almacén, artículo, estado y fecha.
- Gestión de devoluciones y tickets asociados.
- Procesos especiales de traspasos relacionados con albaranes.

### Motor de interfaces

El motor permite configurar procesos compuestos por una secuencia de pasos. Los tipos admitidos incluyen:

- Lectura y carga de archivos locales o FTP.
- Consultas SQL parametrizadas.
- Servicios SOAP/DataExchange.
- Comparadores de datos.
- Procesos de traspaso.
- Procesos de inventario.
- Procesos de albarán/devolución.
- Registro de estados y logs.

### Administración

- Autenticación mediante ASP.NET Core Identity.
- Sesiones de usuario.
- Control de roles y menú por rol.
- Visualización de procesos e incidencias.
- Actualización de configuración de archivos y pasos.

## 3. Arquitectura técnica

### Tecnología

- .NET 8
- ASP.NET Core MVC
- Razor Views
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- SignalR
- Serilog
- FTP
- SOAP/WCF
- SMTP
- Bootstrap y JavaScript

### Patrón de organización

- `Controllers`: controladores MVC y acciones HTTP.
- `Models`: modelos de dominio y lógica específica.
- `ViewModels`: modelos preparados para las vistas.
- `Views`: vistas Razor.
- `Entities`: entidades de datos.
- `Data`: contextos Entity Framework.
- `Repositorys`: acceso a datos y repositorios.
- `DAL`: acceso directo a SQL Server.
- `Services`: motor de interfaces y ejecución de pasos.
- `Helpers`: utilidades de configuración, fechas, datos, correo y FTP.
- `HUB`: comunicación en tiempo real mediante SignalR.
- `WorkerInterfaces`: ejecución periódica de interfaces.
- `WorkerLogsInterfaces`: ejecución periódica de gestión de logs.
- `Connected Services`: cliente SOAP DataExchange.
- `wwwroot`: recursos web, scripts y estilos.

## 4. Configuración

El proyecto utiliza `appsettings.json`, variables de entorno y perfiles de lanzamiento de Visual Studio/VS Code.

No debe publicarse ningún secreto real en Git. Consulte `appsettings.Example.json` para conocer las secciones disponibles y use variables de entorno o secretos locales.

Las principales secciones son:

- `Database`: servidores, bases de datos, usuarios y cadenas de conexión.
- `ExternalServices`: SOAP, HTTP y SMTP.
- `EmailAccounts`: cuentas de correo y contraseñas.
- `Path`, `PathDev`: rutas locales, repositorio y URL virtual.
- `Path_Origen_*` y `Path_Destino_*`: orígenes y destinos FTP o UNC.
- `Serilog`: logging y archivo de salida.
- `SegundosCheckProcessInterfaces`: intervalo del worker de interfaces.
- `SegundosCheckProcessLogInterfaces`: intervalo del worker de logs.
- `Entorno`: entorno de ejecución, normalmente `pro` o `dev`.

Ejemplo de configuración local mediante PowerShell:

```powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
$env:Entorno="dev"
$env:Database__ProductionServer="<servidor>"
$env:Database__ProductionUser="<usuario>"
$env:Database__ProductionPassword="<contraseña>"
dotnet run --project .\Drim.Interfaces.Shark.csproj
```

## 5. Compilación y ejecución

### Requisitos

- SDK .NET 8 o superior.
- Acceso a las bases de datos SQL Server configuradas.
- Acceso a rutas UNC/FTP según los procesos habilitados.
- Visual Studio o VS Code con el adaptador C# para depuración.

### Compilación

Desde la carpeta del proyecto:

```powershell
dotnet restore .\Drim.Interfaces.Shark.csproj
dotnet build .\Drim.Interfaces.Shark.csproj
dotnet run --project .\Drim.Interfaces.Shark.csproj
```

La aplicación se inicia por defecto en:

```text
http://localhost:9004/Account/Login
```

## 6. Depuración en VS Code

El workspace incluye `.vscode/launch.json` y `.vscode/tasks.json`.

Usar:

1. Abrir la vista **Run and Debug**.
2. Seleccionar **Drim.Interfaces.Shark (.NET 8)**.
3. Pulsar **Start Debugging**.

La tarea de compilación ejecuta:

```powershell
dotnet build .\Drim.Interfaces.Shark\Drim.Interfaces.Shark.csproj
```

## 7. Base de datos

La aplicación utiliza varios catálogos independientes:

- `APPDRIM`
- `DRIM`
- `G001`
- `PDV`
- `WEB_DATOS`
- `WEB_PEDIDOS`
- `WEB_PEDIDOS_HISTORICO`
- `WEB_ESTADISTICA`
- `A3LABORAL`
- `A3COMMON`
- `ELMUNDODEDRIM`

Las cadenas se generan a partir de `EntornoHelper` y la configuración de `Database`. Se recomienda revisar los catálogos utilizados por cada repositorio antes de publicar o ejecutar un entorno distinto.

## 8. Flujo general de procesamiento

1. Los procesos disponibles se leen desde el repositorio de interfaces.
2. Cada proceso contiene archivos, pasos y parámetros.
3. `WorkerInterfaces` coordina la ejecución periódica.
4. `InterfacesArchivosServices` ejecuta cada tipo de paso.
5. Los datos se leen desde SQL, archivos locales, FTP o DataExchange.
6. Los resultados se transforman y guardan en el destino correspondiente.
7. Los estados y errores se registran mediante logs y SignalR.

## 9. Logging y trazabilidad

Serilog escribe información de ejecución en consola y en el archivo configurado por `Serilog:WriteTo`.

Por defecto, la ruta relativa es:

```text
LogsFiles/log.txt
```

La aplicación expone además el hub SignalR `/logsinterfaces` para comunicación de estado con el interfaz web.

## 10. Seguridad y publicación

Antes de publicar el repositorio:

- No incluir contraseñas, usuarios privilegiados, cadenas completas de conexión ni claves privadas.
- Usar variables de entorno o el gestor de secretos del servidor.
- Crear un archivo `appsettings.Development.json` local y no versionarlo.
- Eliminar información de producción de `appsettings.json` o sustituirla por valores vacíos/placeholders.
- Restringir el acceso a las rutas UNC y FTP.
- Habilitar HTTPS en producción.
- Revisar los paquetes obsoletos y migrar System.Data.SqlClient a Microsoft.Data.SqlClient.
- Validar los permisos de usuario y los métodos protegidos antes de exponer la aplicación.

## 11. Estado de migración

El proyecto ha sido migrado inicialmente desde .NET 6 a .NET 8 y conserva el código y las dependencias originales en gran medida.

Pendientes recomendados:

- Actualizar las bibliotecas de EF Core a una versión compatible y uniforme.
- Reemplazar `System.Data.SqlClient` por `Microsoft.Data.SqlClient`.
- Sustituir referencias de Microsoft Visual Studio Web CodeGeneration que no sean necesarias en ejecución.
- Revisar APIs o paquetes obsoletos.
- Migrar dependencias WCF incompatibles si fuese necesario.
- Incorporar pruebas automatizadas.
- Centralizar totalmente la configuración mediante DI e `IConfiguration`.

## 12. Estructura simplificada

```text
Drim.Interfaces.Shark/
├── Controllers/
├── Data/
├── Entities/
├── Helpers/
├── HUB/
├── Models/
├── Repositorys/
├── Services/
├── ViewModels/
├── Views/
├── wwwroot/
├── appsettings.json
├── appsettings.Example.json
├── launchSettings.json
└── Drim.Interfaces.Shark.csproj
```

## 13. Licencia

Proyecto interno de Drim. Define una licencia privada antes de hacerlo público en GitHub.
