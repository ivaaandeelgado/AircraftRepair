# AircraftRepair
Aplicación que gestiona reparaciones de aviones, desarrollada con:
· **Backend** : .NET 8 
· **Frontend** : Angular 21
· **BD** : SQL Server
Creada por Olga Segura Gayete e Ivan Delgado Galvan

*************Setup*************

Para probar la aplicación se han de seguir los siguientes pasos:
- Clonar ambos repositorios (API + Angular)
- 
- **Generar base de datos:**
    · Ejecutar en Admin Console Manager "Update-Database" (Nos construye la BD en SQL Server)
  
- **Crear usuarios de prueba:**
    · Ejecutamos API
    · En el swagger ejecutamos el endpoint **"/api/Dev/generate-hash"** (esto nos devuelve el hash de la password para               introducir el primer user en la BD, el del admin)
    · Copiamos el hash que nos da en el response
    · Abrimos el SQL Server Managment y en la BD que nos ha creado insertamos en la tabla de AppUsers el usuario admin con el         comando **"(INSERT INTO AppUsers(UserName, PasswordHash, IdPermission) Values ('Admin','AQAAAAIAAYagAAAAEKXEQSJtk6ewIJWhMDhRCJP2dE843QJA9GNxlMUPv8ym499AaDI5QV71NFbQf4JvHA==',1))"**
    · Una vez insertado el usuario admin volver al swagger al apartado **"/api/Auth/login"**
    · Hacer login con el usuario "Admin" y contraseña "Admin123!"
    · Copiamos el token que nos devuelve en el response y lo pegamos en el apartado **Authorize** de la parte superior del           swagger, en el label "Value", después autoriza
    · Abre el endpoint de POST **"/api/Users"** y introduce el nombre y contraseña de usuarios junto al rol 2 para que sean         usuarios normales

- **Abrir web**
    - Ejecutar el comando **ng serve** o pulsar F5 para desplegar la web
    - Loguearse con Admin o User
 

*************Funcionalidades de la app*************

- **Funcionalidades Admin**
  · Crear tarea
  · Eliminar tarea
  · Modificar tarea (Asignaciones o estado)
  · Visualizar todas las tareas

- **Funcionalidades de usuario**
  · Visualizar tareas assignadas
  · Modificar estado tarea
      

- **Funcionlidades Usuarios**
- Visualizar tareas que tienen asignadasa
- Modificar estado tarea
