# Objetivo
La idea principal de este onboarding es que cada los nuevos integrantes del equipo puedan tener una idea general de cómo se construyen los distintos proyectos en nuestra celula de desarrollo.

El proyecto iniciará como un CRUD básico para manejar operaciones sobre usuarios, pero, a medida que se vaya avanzando, se irán solicitando requerimientos un poco más complejos.

## Requisitos previos

* Tener instalado la [SDK](https://dotnet-microsoft-com.translate.goog/en-us/download/dotnet/9.0?_x_tr_sl=en&_x_tr_tl=es&_x_tr_hl=es&_x_tr_pto=tc) de .NET 9.
* Visual Studio 2022, Visual Studio Code o JetBrains.
* Clonar y crear un fork sobre el [repositorio de onboarding técnico](https://github.com/mesequeira/globaltask-onboarding) en GitHub.

## Operaciones principales sobre usuarios.

Una vez abierta la solución, encontrarán una estructura super básica con las capas necesarias para comenzar el desarrollo. Dentro de nuestro equipo seguimos los últimos estándares de diseño y arquitectura, por lo que notarán que el mismo está construido siguiendo una Clean Architecture.

## ¿Qué es clean architecture?

La arquitectura limpia es un enfoque donde se busca separar la lógica de negocio de los detalles de infraestructura. Se encarga de organizar el proyecto en capas fomentando la escabilidad y mantenibilidad permitiendo evolucionar el software sin que los cambios tecnológicos afecten la lógica central de la aplicación.

> Para más información al respecto, se puede visitar el siguiente vídeo: [The Beginner's Guide to Clean Architecture](https://youtu.be/TQdLgzVk2T8)

```plaintext
📂 src
├── 📂 Core
│   ├── 📂 Users.Application
│   ├── 📂 Users.Domain
├── 📂 Infrastructure
│   ├── 📂 Users.Infrastructure
│   ├── 📂 Users.Persistence
├── 📂 WebApi
│   ├── 📂 Users.WebApi
│   │   ├── 📂 Controllers
│   │   ├── 📄 appsettings.json
│   │   ├── 📄 appsettings.Development.json
│   │   └── 📄 Program.cs
📂 tests
```

## Estructura de carpetas

Para las capas de `Application`, `Domain`, `Infrastructure` y `Persistence` se deberá seguir una estructura de carpetas en particular, conocida como `Vertical Slice`. Este es un enfoque donde cada funcionalidad o caso de uso se implementa como una slice vertical independiente que abarca todas las operaciones necesarias.

Teniendo en cuenta que nuestro caso de uso será la de manejar operaciones sobre una entidad User, podemos organizar nuestro proyecto siguiendo este enfoque.

> Para más información al respecto, se puede visitar el siguiente vídeo: [Refactoring the Domain Layer Structure With Vertical Slices](https://youtu.be/R-XODYF2iBQ)

```plaintext
📂 src
├── 📂 Core
│   ├── 📂 Users.Application
│   │   ├── 📂 Users
|   │   │   ├── 📂 Commands
|   │   │   ├── 📂 Queries
|   │   │   ├── 📂 Profiles
|   │   │   ├── 📂 Events
│   ├── 📂 Users.Domain
│   │   ├── 📂 Users
|   │   │   ├── 📂 Models
|   │   │   ├── 📂 Services
├── 📂 Infrastructure
│   ├── 📂 Users.Infrastructure
│   ├── 📂 Users.Persistence
│   │   ├── 📂 Users
|   │   │   ├── 📂 Repositories
|   │   │   ├── 📂 Configurations
├── 📂 WebApi
│   ├── 📂 Users.WebApi
│   │   ├── 📂 Users
|   │   │   ├── 📂 Controllers
|   |   │   │   ├── 📄 UsersController.cs
│   │   ├── 📄 appsettings.json
│   │   ├── 📄 appsettings.Development.json
│   │   └── 📄 Program.cs
📂 tests
```

## Implementar referencias de proyectos, CQRS y MediatR

En Clean Architecture, cada capa (proyecto) debe contener únicamente aquello que le corresponda. Para permitir la comunicación entre capas, se establecen referencias entre proyectos desde afuera hacia adentro, pero nunca al revés. Esto significa:

* `WebApi` referencia a `Application` e `Infrastructure` (para poder inyectar servicios, repositorios, etc.).
* `Application` referencia a `Domain` (para utilizar entidades, interfaces de repositorio y contratos de dominio).
* `Infrastructure` y `Persistence` referencia a `Application` y `Domain` (para implementar las interfaces definidas en Domain o Application).
* `Domain` no referencia a ningún otro proyecto (es la capa más interna).

![alt text](https://miro.medium.com/v2/resize:fit:339/1*JWzL8VcHl13x0J5rDUZWzA.png)

**Tarea 1:** Configurar las referencias entre proyectos, siguiendo las reglas de la Clean Architecture y responsabilidad de las capas.

**Tarea 2:** Investigar e implementar la configuración necesaria para utilizar `MediatR` y `CQRS` dentro del proyecto.

> Para más información al respecto, se puede visitar el siguiente vídeo: [Clean Architecture With .NET 6 And CQRS - Project Setup](https://youtu.be/tLk4pZZtiDY)

## Implementar Entity Framework, patrón repositorio y unit of work

Como ORM dentro de nuestros proyectos se utiliza Entity Framework aplicando un patrón repistorio con unit of work. La idea de estos patrones es:

* Abstraer el acceso de datos ocultando los detalles de la implementación del acceso a datos detrás de una interfaz.
* Centraliza la lógica de la persistencia en un mismo lugar, evitando que cada Handler repita las consultas.
* Dado que el acceso a datos está abstraído mediante interfaces, es más fácil generar mocks para aislar la lógica de negocio durante los unit tests.
* Aplica el principio de responsabilidad única, ya que el repositorio se encarga exclusivamente de la lógica de la persistencia.

**Tarea 3:** Crear un servidor de base de datos SQL Server para poder conectarse desde el proyecto.

**Tarea 4:** Investigar e implementar Entity Framework dentro del proyecto, junto a los patrones repositorio y unit of work para la entidad `User.cs`. Todo lo relacionado al DbContext, Repositories y Configurations de los campos de la tabla Users, debe generarse en la capa de `Persistence`.

**Tarea 5:** Implementar migrations que se encarguen de generar el schema de base de datos.

> Para más información al respecto, se puede visitar el siguiente vídeo: [Repository Pattern With Entity Framework Core | Clean Architecture, .NET 6](https://youtu.be/h4KIngWVpfU)

**Tarea 6:** Ya configurados `MediatR` y `CQRS` crear la estructura de carpetas y generar las operaciones para la entidad user: crear, modificar, eliminar y obtener un usuario.

#### Consideraciones:

* Las propiedades de la entidad `User.cs` es a gusto del desarrollador, pero por lo menos debe contar con ciertas propiedades mínimas, las cuales deberán validar su formato con DataAnnotations o manualmente dentro de la lógica de negocio. Si las mismas no se cumplen, en las operaciones de tipo `UPDATE` o `CREATE`, se debe responder un mensaje de error al cliente.
    * **PhoneNumber**: debe tener el formato válido de un número de teléfono (sin letras, con una longitud máxima y mínima, etc.)
    * **Email**: debe respetar el formato de correo@dominio.com.
    * **Birthday**: el usuario debe ser mayor de 18 años.

* **Todas las entidades que representen tablas de bases de datos**, deberán heredar de una clase base, la cual contenga atributos genéricos que sirvan de trazabilidad: **Id**, **ModifiedAt**, **CreatedAt**.

* Las operaciones realizadas deben contar con los siguientes requerimientos:
    * Contar con sus `Command` o `Query`.
    * Tener un `Handler` encargado de manejar ese Command o Query.
    * Tener un `endpoint` que se encargue de ejecutar ese Handler.

## Dockerización de proyectos

Dentro de nuestras soluciones, todos nuestros proyectos están creados sobre contenedores de Docker. Esto trae ciertos beneficios a la hora de desarrollar con múltiples compañeros y desplegar proyectos.

* **Consistencia**: Empaquetar la aplicación con todas sus dependencias elimina los problemas de “funciona en mi máquina”.
* **Estandarización**: Mantienes un mismo entorno de ejecución en todas las etapas (desarrollo, pruebas, producción).
* **Despliegue rápido**: Los contenedores se inician o detienen casi instantáneamente, facilitando escalabilidad y actualizaciones.

**Tarea 7:** Levantar el proyecto de la WebApi con un Dockerfile.

**Tarea 8:** Levantar la instancia de base de datos con un Dockerfile.

> Para más información al respecto, se puede visitar el siguiente artículo: [Microsoft SQL Server - Ubuntu based images](https://hub.docker.com/r/microsoft/mssql-server)

**Tarea 9:** Generar un `docker-compose.yaml` que automáticamente inicialice ambos contenedores.

## Glosario de tareas

* [ ]  Configurar las referencias entre proyectos, siguiendo las reglas de la Clean Architecture y responsabilidad de las capas.
* [ ]  Investigar e implementar la configuración necesaria para utilizar MediatR y CQRS dentro del proyecto.
* [ ]  Crear un servidor de base de datos SQL Server para poder conectarse desde el proyecto.
* [ ]  Investigar e implementar Entity Framework dentro del proyecto, junto a los patrones repositorio y unit of work para la entidad `User.cs`. Todo lo relacionado al DbContext, Repositories y Configurations de los campos de la tabla Users, debe generarse en la capa de `Persistence`.
* [ ]  Implementar migrations que se encarguen de generar el schema de base de datos.
* [ ]  Ya configurados `MediatR` y `CQRS` crear la estructura de carpetas y generar las operaciones para la entidad user: crear, modificar, eliminar y obtener un usuario.
* [ ]  Levantar el proyecto de la WebApi con un Dockerfile.
* [ ]  Levantar la instancia de base de datos con un Dockerfile.
* [ ]  Generar un `docker-compose.yaml` que automáticamente inicialice ambos contenedores.