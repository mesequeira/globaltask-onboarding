# Objetivo
La idea principal de este onboarding es que los nuevos integrantes del equipo puedan tener una idea general de cómo se construyen los distintos proyectos en nuestra celula de desarrollo.

El proyecto contemplará varios escenarios donde se apliquen los patrones y arquitecturas más utilizadas en nuestros servicios.

# Requisitos previos

* Tener instalado la [SDK](https://dotnet-microsoft-com.translate.goog/en-us/download/dotnet/9.0?_x_tr_sl=en&_x_tr_tl=es&_x_tr_hl=es&_x_tr_pto=tc) de .NET 9.
* Visual Studio 2022, Visual Studio Code o JetBrains.
* Clonar y crear un fork sobre el repositorio de onboarding técnico en [GitHub](https://github.com/mesequeira/globaltask-onboarding) o [Azure DevOps](https://dev.azure.com/BroxelExt/Globaltask/_git/gt.onboarding).
* Haber finalizado con la creación del proyecto de [WebApi](../webapi/).

## ¿Qué es un worker service?

Un Worker Service es una aplicación de consola diseñada para ejecutar procesos de manera continua o programada, sin necesidad de exponer una interfaz gráfica o un endpoint HTTP como sucede con las Web Apis. 

En lugar de escuchar peticiones de un cliente externo, un  Worker Service se centra en tareas de procesamiento interno, ya sea para ejecutar trabajos en segundo plano, gestionar colas de mensajería o realizar operaciones de mantenimiento.

Al igual que en la [WebApi](../webapi/), en un Worker Service se aplican los principios de `Clean Architecture` para mantener la lógica de negocio independiente de los detalles de infraestructura. Esto implica que la estructura del proyecto siga el enfoque de capas y el estilo de `Vertical Slice`, permitiendo una mayor escalabilidad y mantenibilidad con el paso del tiempo.

## Operaciones principales sobre usuarios

Una vez abierta la solución, encontrarán una estructura básica con las capas necesarias para comenzar el desarrollo. Es común encontrar proyectos de tipo worker que sigan esta arquitectura dentro de nuestros equipos de trabajo, los mismos siguen una Clean Architecture. 

```plaintext
📂 src
├── 📂 Core
│   ├── 📂 Users.Worker.Application
│   ├── 📂 Users.Worker.Domain
├── 📂 Infrastructure
│   ├── 📂 Users.Worker.Infrastructure
│   ├── 📂 Users.Worker.Persistence
├── 📂 Worker
│   ├── 📂 Users.Worker
│   │   ├── 📄 appsettings.json
│   │   ├── 📄 appsettings.Development.json
│   │   └── 📄 Program.cs
│   │   └── 📄 Worker.cs
📂 tests
```

Siguiendo lo trabajado en el proyecto de [WebApi](../webapi/), para nuestro Worker deberemos crear distintas notificaciones para los usuarios en distintos escenarios:

* Cuando un usuario se crea, enviar un mensaje de bienvenida.
* Cuando un usuario sufre modificaciones, enviar un mensaje notificando los cambios.
* Cuando un usuario es eliminado, enviar un mensaje notificándolo junto a un motivo.

## Estructura de carpetas y referencias de proyectos

Se deberá continuar con la misma lógica de referencias de proyectos y estructura de carpetas del proyecto de [WebApi](../webapi/) ya que el proyecto del Worker sigue la misma estructura de la Clean Architecture.

> Para mayor facilidad, se puede optar por copiar y pegar las clases ya creadas dentro del proyecto de [WebApi](../webapi/). 

```plaintext
📂 src
├── 📂 Core
│   ├── 📂 Users.Worker.Application
│   │   ├── 📂 Users
|   │   │   ├── 📂 Consumers
|   │   │   ├── 📂 Profiles
│   ├── 📂 Users.Worker.Domain
│   │   ├── 📂 Users
|   │   │   ├── 📂 Models
|   │   │   ├── 📂 Events
├── 📂 Infrastructure
│   ├── 📂 Users.Worker.Infrastructure
│   │   ├── 📂 Emails
|   │   │   ├── 📂 Services
|   |   |   │   │   ├── 📄 IEmailService.cs
|   |   |   │   │   ├── 📄 EmailService.cs
│   ├── 📂 Users.Worker.Persistence
│   │   ├── 📂 Users
|   │   │   ├── 📂 Repositories
|   │   │   ├── 📂 Configurations
├── 📂 Worker
│   ├── 📂 Users.Worker
│   │   ├── 📄 appsettings.json
│   │   ├── 📄 appsettings.Development.json
│   │   └── 📄 Program.cs
📂 tests
```

**Tarea 1:** Configurar las referencias entre proyectos, siguiendo las reglas de la Clean Architecture y responsabilidad de las capas.

**Tarea 2:** Dockerizar el proyecto de `Worker` y crear un `docker-compose.yml` encargado de levantar el servicio de Worker y `RabbitMQ`.

**Tarea 3:** Copiar y pegar todo lo relacionado a la persistencia de la entidad `User.cs` generado en el proyecto de `WebApi`, respetando el posicionamiento en las mismas capas.

**Tarea 4:** Para la base de datos, utilizar la misma generada en el proyecto de `WebApi` investigando cómo conectar dos contenedores de Docker distintos mediante `networks`.

**Tarea 5:** Investigar e implementar MassTransit junto a RabbitMQ en el proyecto. Contemplar que RabbitMQ y MassTransit pueden ser considerados servicios externos, por lo que su configuración e inyección debe estar en los proyectos correctos.

> Para más información al respecto, se puede visitar el siguiente vídeo: [Getting Started With MassTransit (Beginner Friendly)](https://youtu.be/CTKWFMZVIWA)

## Primer evento y consumers

RabbitMQ funciona como un broker de mensajería en el que exista un `Publisher` y un `Consumer`. Para este caso práctico, podemos interpretar a la WebApi como el Publisher y al Worker como el Consumer.

* **Publisher**: Quién publica los mensajes en la cola de mensajería.
* **Consumer**: Aquel que está a la escucha de mensajes publicados en la cola de mensajería, los atrapa y ejecuta.

Por lo que, será necesario configurar nuestra WebApi como un publicador de mensajes.

**Tarea 6:** Configurar nuestra [WebApi](../webapi/) con RabbitMQ y MassTransit para que pueda publicar eventos. Contemplar que RabbitMQ y MassTransit pueden ser considerados servicios externos, por lo que su configuración e inyección debe estar en los proyectos correctos.

**Tarea 7:** Crear un primer evento en el proyecto `Domain` de nuestro Worker y WebApi llamado `UserRegisteredEvent.cs`. Luego, hacer que nuestro WebApi publique este evento a la hora de crear un usuario y crear un `Consumer` llamado `UserRegisteredEventConsumer.cs` en nuestro Worker encargado de manejar y consumir este evento.

## Enviar notificaciones mediante correo electrónico

Dentro de nuestro servicios manejamos distintas notificaciones: push notifications, correo electrónicos, entre otros. Para esto, normalmente se utiliza un servicio de SMTP propio que las compañías contratan, pero Gmail nos ofrece una versión gratuita de esto que podemos implementar. 

**Tarea 8:** Investigar e implementar un servicio que permita enviar correos electrónicos con Gmail. Pueden crear una cuenta de gmail aparte para no utilizar una personal.

> Para más información al respecto, se puede visitar el siguiente vídeo: [Easy Email Verification in .NET: FluentEmail + Papercut](https://youtu.be/KtCjH-1iCIk)

**Tarea 9:** Implementar dentro del consumer `UserRegisteredEventConsumer.cs` una lógica que consuma el servicio de envio de correo electrónico y envíe una notificación de bienvenida.

**Tarea 10:** Crear dos eventos nuevos en el proyecto `Domain` de nuestra WebApi y Worker `UserUpdatedEvent.cs` y `UserDeletedEvent.cs` junto a los 2 consumers en el worker `UserUpdatedEventConsumer.cs` y `UserDeletedEventConsumer.cs`.

**Tarea 11:** En el `UserUpdatedEventConsumer.cs` enviar una notificación que permita saber qué campos se modificaron del usuario, mostrando los valores viejos y los nuevos.

**Tarea 12:** En el `UserDeletedEventConsumer.cs` enviar una notificación que avise sobre la eliminación del usuario junto a un motivo de por qué fue borrado. Para esto, habrá que agregar un campo en el endpoint de la WebApi, que se encarga de eliminar el usuario, para que se pueda cargar un motivo.

**Tarea 13:** Crear unit tests que validen los casos de uso generados con MassTransit y RabbitMQ. Además, reutilizar los units tests de arquitectura utilizados en la WebApi.

> Para más información al respecto, se puede visitar el siguiente vídeo: [MassTransit Testing with Web Application Factory](https://youtu.be/Uzme7vInDz0)

# Glosario de tareas

* [ ] Configurar las referencias entre proyectos, siguiendo las reglas de la Clean Architecture y responsabilidad de las capas.
* [ ]  Dockerizar el proyecto de `Worker` y crear un `docker-compose.yml` encargado de levantar el servicio de Worker y `RabbitMQ`.
* [ ]  Copiar y pegar todo lo relacionado a la persistencia de la entidad `User.cs` generado en el proyecto de `WebApi`, respetando el posicionamiento en las mismas capas.
* [ ]  Para la base de datos, utilizar la misma generada en el proyecto de `WebApi` investigando cómo conectar dos contenedores de Docker distintos mediante `networks`.
* [ ]  Investigar e implementar MassTransit junto a RabbitMQ en el proyecto. Contemplar que RabbitMQ y MassTransit pueden ser considerados servicios externos, por lo que su configuración e inyección debe estar en los proyectos correctos.
* [ ]  Configurar nuestra [WebApi](../webapi/) con RabbitMQ y MassTransit para que pueda publicar eventos. Contemplar que RabbitMQ y MassTransit pueden ser considerados servicios externos, por lo que su configuración e inyección debe estar en los proyectos correctos.
* [ ]  Crear un primer evento en el proyecto `Domain` de nuestro Worker y WebApi llamado `UserRegisteredEvent.cs`. Luego, hacer que nuestro WebApi publique este evento a la hora de crear un usuario y crear un `Consumer` llamado `UserRegisteredEventConsumer.cs` en nuestro Worker encargado de manejar y consumir este evento.
* [ ]  Investigar e implementar un servicio que permita enviar correos electrónicos con Gmail. Pueden crear una cuenta de gmail aparte para no utilizar una personal.
* [ ]  Implementar dentro del consumer `UserRegisteredEventConsumer.cs` una lógica que consuma el servicio de envio de correo electrónico y envíe una notificación de bienvenida.
* [ ]  Crear dos eventos nuevos en el proyecto `Domain` de nuestra WebApi y Worker `UserUpdatedEvent.cs` y `UserDeletedEvent.cs` junto a los 2 consumers en el worker `UserUpdatedEventConsumer.cs` y `UserDeletedEventConsumer.cs`.
* [ ]  En el `UserUpdatedEventConsumer.cs` enviar una notificación que permita saber qué campos se modificaron del usuario, mostrando los valores viejos y los nuevos.
* [ ]  En el `UserDeletedEventConsumer.cs` enviar una notificación que avise sobre la eliminación del usuario junto a un motivo de por qué fue borrado. Para esto, habrá que agregar un campo en el endpoint de la WebApi, que se encarga de eliminar el usuario, para que se pueda cargar un motivo.