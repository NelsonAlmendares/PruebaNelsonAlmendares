# Administración de nóminas

Documentación general y técnologías empleadas.

## Técnologías

#### Sql Server Management Studio 2019 (SSMS 19) como gestor de base de datos
#### Microsoft Visual Studio 2022 (IDE)
#### Framework: ASP -> Aplicacoion Web (.Net 8.0 Core)
#### Arquitectura de proyecto: MVC

## Usage
```bash
- Decargar la carpeta contenedore y ejecutar la solución (archivo exe aún no creado)
- Usar el script de base de datos Generated dentro de la carpeta Administracion
- Ejectutar después de haber creado la base de datos
```

## Configuración 
#### Crear usuario en Sql Server Management Studio y editar: `appsettings.json`, en los campos de User & Password colocar tus credenciales de sesión de Sql. 
#### Debe quedarte una configuración como: `Data Source = (local); Initial Catalog = Empleados; User ID = tu_user; Password = tu_contraseña`

En el caso de que no cuentes con el sistema de auntenticación de Sql Server, debes colocar la configuacion: 
#### `Data Source=(local); Initial Catalog=Empleados; Integrated Security=true`

# Errores durante desarrollo:
No se utilizo Entity dado a la brevedad del tiempo, se crearon clases con POO y métodos con retornos

## Por terminar: 

```bash
- Reportes con parámetros (incompleto) 
- Cargar empleados por medio de métodos de POO, crear un Foreach para el Modelo de datos
- Exportar documento de excel
```
