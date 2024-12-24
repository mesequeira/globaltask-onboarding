# Objetivo
La idea principal de este onboarding es que los nuevos integrantes del equipo puedan tener una idea general de cómo se construyen los distintos proyectos en nuestra celula de desarrollo.

El proyecto contemplará varios escenarios donde se apliquen los patrones y arquitecturas más utilizadas en nuestros servicios.

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

## Dockerización de proyectos

Dentro de nuestras soluciones, todos nuestros proyectos están creados sobre contenedores de Docker. Esto trae ciertos beneficios a la hora de desarrollar con múltiples compañeros y desplegar proyectos.

* **Consistencia**: Empaquetar la aplicación con todas sus dependencias elimina los problemas de “funciona en mi máquina”.
* **Estandarización**: Mantienes un mismo entorno de ejecución en todas las etapas (desarrollo, pruebas, producción).
* **Despliegue rápido**: Los contenedores se inician o detienen casi instantáneamente, facilitando escalabilidad y actualizaciones.

**Tarea 3:** Levantar el proyecto de la WebApi con un Dockerfile.

**Tarea 4:** Levantar la instancia de base de datos con un Dockerfile.

> Para más información al respecto, se puede visitar el siguiente artículo: [Microsoft SQL Server - Ubuntu based images](https://hub.docker.com/r/microsoft/mssql-server)

**Tarea 5:** Generar un `docker-compose.yaml` como orquestador para manejar los dockerfiles generados anteriormente.

**Tarea 6:** Investigar e implementar la manera de manejar los secretos, como el `ConnectionString`, mediante variables de entorno creando un archivo `.env` en conjunto con el `docker-compose.yaml`.

> Para más información al respecto, se puede visitar el siguiente artículo: [Set environment variables within your container's environment](https://docs.docker.com/compose/how-tos/environment-variables/set-environment-variables/)

## Implementar patrón Result

En el groso de nuestros proyectos, las respuestas al cliente, las manejamos mediante el patrón `Result`. 

* El Result Pattern permite encapsular el resultado de una operación junto con su estado (éxito o error), lo que facilita entender el flujo de datos en el programa.

* En lugar de utilizar excepciones para controlar el flujo, se pueden retornar objetos que representen claramente el éxito o fallo de una operación, mejorando la legibilidad

* Se evita el abuso de excepciones para manejar casos comunes de fallo, utilizando un enfoque explícito. Esto permite un mejor control sobre los errores esperados y una mayor eficiencia.

**Tarea 7:** Investigar cómo implementar el patrón Result dentro de nuestro proyecto para implementarlo en los endpoints a crear.

> Para más información al respecto, se puede visitar el siguiente artículo: [Get Rid of Exceptions in Your Code With the Result Pattern](https://youtu.be/WCCkEe_Hy2Y)

## Implementar Entity Framework, patrón repositorio y unit of work

Como ORM dentro de nuestros proyectos se utiliza Entity Framework aplicando un patrón repistorio con unit of work. La idea de estos patrones es:

* Abstraer el acceso de datos ocultando los detalles de la implementación del acceso a datos detrás de una interfaz.
* Centraliza la lógica de la persistencia en un mismo lugar, evitando que cada Handler repita las consultas.
* Dado que el acceso a datos está abstraído mediante interfaces, es más fácil generar mocks para aislar la lógica de negocio durante los unit tests.
* Aplica el principio de responsabilidad única, ya que el repositorio se encarga exclusivamente de la lógica de la persistencia.

**Tarea 8:** Investigar e implementar Entity Framework dentro del proyecto, junto a los patrones repositorio y unit of work para la entidad `User.cs`. Todo lo relacionado al DbContext, Repositories y Configurations de los campos de la tabla Users, debe generarse en la capa de `Persistence`.

**Tarea 9:** Implementar migrations que se encarguen de generar el schema de base de datos.

> Para más información al respecto, se puede visitar el siguiente vídeo: [Repository Pattern With Entity Framework Core | Clean Architecture, .NET 6](https://youtu.be/h4KIngWVpfU)

**Tarea 10:** Ya configurados `MediatR` y `CQRS` crear la estructura de carpetas y generar las operaciones para la entidad user. Para saber cómo deben definir los endpoints, utilicen el archivo [openapi.yaml](./openapi.yaml), el cual pueden visualizar de forma interactiva yendo a [Swagger Editor](https://editor.swagger.io/).

> Para más información sobre cómo trabajar con paginado en .NET, se puede visitar el siguiente vídeo: [Adding Filtering, Sorting And Pagination To a REST API | .NET 7](https://youtu.be/X8zRvXbirMU)

## Crear validaciones de modelos con FluentValidation

`FluentValidation` nos permite poder manejar las reglas de negocio de una manera más limpia y ordenada. Gracias a su versatilidad permite, no solo, realizar validaciones de formato sino validaciones con peticiones a bases de datos en tiempo real.

**Tarea 11**: Investigar e implementar `FluentValidation` en el proyecto, para luego incluir reglas de negocio en los endpoints que consumirá el cliente.

> Para más información al respecto, se puede visitar el siguiente vídeo: [Make Your Business Rules Cleaner With Fluent Validation](https://youtu.be/AYrmu9_RFnM)

## Implementar pruebas unitarias con xUnit

Las pruebas unitarias garantizan la calidad y robustez de nuestra solución, evitando cualquier caso border que no se esté contemplando en un flujo normal.

* **Confianza en el código:** Permiten verificar que cada pieza de la aplicación (Handlers, Repositorios, Servicios, etc.) funcione correctamente de manera aislada.
* **Documentación viviente:** Facilitan la comprensión de las reglas de negocio al leer los casos de prueba.
* **Prevención de regresiones:** Aseguran que nuevos cambios no rompan la funcionalidad existente.
* **Mejor diseño de código:** Fomentan la aplicación de principios SOLID, ya que los componentes deben ser testeables de manera independiente.

> Para más información al respecto, se puede visitar el siguiente vídeo: [C# Unit Testing Best Practices for Great Code Coverage](https://youtu.be/o_KVvUjwxIw)

**Tarea 12**: Crear un proyecto de pruebas con xUnit en la carpeta `tests` e implementar pruebas unitarias sobre la capa de `Application` para probar: casos de error y éxito en `Handlers` y validaciones con `FluentValidation` sobre, por lo menos, las operaciones de tipo `PUT` y `POST`.

**Tarea 13**: Crear un proyecto de pruebas unitarias con `xUnit` para revisar que las referencias de proyectos estén correctamente configuradas y no violen los principios de Clean Architecture. El proyecto puede llamarse `Users.Architecture.Tests`.



## Consideraciones generales:

* Las propiedades de la entidad `User.cs` es a gusto del desarrollador, pero por lo menos debe contar con ciertas propiedades mínimas, las cuales deberán tener validaciones de formato, obligatoriedad, entre otros. 
    * **PhoneNumber**: debe tener el formato válido de un número de teléfono (sin letras, con una longitud máxima y mínima, etc.)
    * **Email**: debe respetar el formato de correo@dominio.com.
    * **Birthday**: el usuario debe ser mayor de 18 años.

* **Todas las entidades que representen tablas de bases de datos**, deberán heredar de una clase base, la cual contenga atributos genéricos que sirvan de trazabilidad: **Id**, **ModifiedAt**, **CreatedAt**.

* Las operaciones realizadas deben contar con los siguientes requerimientos:
    * Contar con sus `Command` o `Query`.
    * Tener un `Handler` encargado de manejar ese Command o Query.
    * Tener un `endpoint` que se encargue de ejecutar ese Handler.
 
* Hacer uso de objetos **DTOs** para los endpoints de tipo GET, no devolver las entidades de base de datos directamente al cliente. Para realizar la conversión se puede optar por [AutoMapper](https://automapper.org/) o [Mapster](https://github.com/MapsterMapper/Mapster).

* Todos los endpoints que se generen deben responder siguiendo la estructura del `Result Pattern`.

## Glosario de tareas

* [ ]  Configurar las referencias entre proyectos, siguiendo las reglas de la Clean Architecture y responsabilidad de las capas.
* [ ]  Investigar e implementar la configuración necesaria para utilizar `MediatR` y `CQRS` dentro del proyecto.
* [ ]  Levantar el proyecto de la WebApi con un Dockerfile.
* [ ]  Levantar la instancia de base de datos con un Dockerfile.
* [ ]  Generar un `docker-compose.yaml` como orquestador para manejar los dockerfiles generados anteriormente.
* [ ]  Investigar e implementar la manera de manejar los secretos, como el `ConnectionString`, mediante variables de entorno creando un archivo `.env` en conjunto con el `docker-compose.yaml`.
* [ ]  Investigar cómo implementar el patrón Result dentro de nuestro proyecto para implementarlo en los endpoints a crear.
* [ ]  Investigar e implementar Entity Framework dentro del proyecto, junto a los patrones repositorio y unit of work para la entidad `User.cs`. Todo lo relacionado al DbContext, Repositories y Configurations de los campos de la tabla Users, debe generarse en la capa de `Persistence`.
* [ ]  Implementar migrations que se encarguen de generar el schema de base de datos.
* [ ]  Ya configurados `MediatR` y `CQRS` crear la estructura de carpetas y generar las operaciones para la entidad user. Para saber cómo deben definir los endpoints, utilicen el archivo [openapi.yaml](./openapi.yaml), el cual pueden visualizar de forma interactiva yendo a [Swagger Editor](https://editor.swagger.io/).
* [ ]  Investigar e implementar `FluentValidation` en el proyecto, para luego incluir reglas de negocio en los endpoints que consumirá el cliente.
* [ ]  Crear un proyecto de pruebas con xUnit en la carpeta `tests` e implementar pruebas unitarias sobre la capa de `Application` para probar: casos de error y éxito en `Handlers` y validaciones con `FluentValidation` sobre, por lo menos, las operaciones de tipo `PUT` y `POST`.
* [ ]  Crear un proyecto de pruebas unitarias con `xUnit` para revisar que las referencias de proyectos estén correctamente configuradas y no violen los principios de Clean Architecture. El proyecto puede llamarse `Users.Architecture.Tests`.