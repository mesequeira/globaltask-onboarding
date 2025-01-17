# Objetivo
Como último objetivo de este onboarding técnico, está la necesidad de tener un dashboard donde se pueda interactuar con los endpoints que fueron generadores anteriormente. Para esto, se creará un sitio web utilizando Blazor Server 8.

## Arquitectura y estructura de carpetas
Al igual que en la Web API, queremos mantener la estructura basada en Clean Architecture. La diferencia principal es que, en este caso, tendremos un proyecto Blazor Server que servirá como punto de entrada (similar a la capa WebApi en el ejemplo anterior).

Para la parte del front-end en Blazor, dividiremos el proyecto de la siguiente manera:

```
📂 src
├── 📂 Core
│   ├── 📂 Users.Application
│   │   ├── 📂 Users
|   │   │   ├── 📂 Validators
|   │   │   ├── 📂 Actions
|   │   │   ├── 📂 Effects
|   │   │   ├── 📂 Reducers
|   │   │   └── 📂 States
|   |
│   ├── 📂 Users.Domain
│   │   ├── 📂 Users
|   │   │   ├── 📂 DTOs
|   │   │   ├── 📂 Services
|   │   │   └── 📂 Extensions
│
├── 📂 Infrastructure
│   ├── 📂 Users.Infrastructure
│   │   ├── 📂 Users
|   │   │   ├── 📂 Services
│
├── 📂 Web
│   ├── 📂 Users.Blazor
│   │   ├── 📂 Pages
│   │   ├── 📂 Components
│   │   ├── 📂 Shared
│   │   ├── 📄 Program.cs
│   │   └── 📄 App.razor
|
📂 tests
```

* **Core**: Contiene la lógica principal y las entidades (`Domain`) además de los casos de uso (`Application`), similar al ejemplo de la Web API.
* **Infrastructure**: Se responsabiliza de las implementaciones de detalle (en este caso, las llamadas HTTP a la Web API, usando servicios).

* **Web**: Es nuestra capa “de presentación” en **Blazor**. Aquí estarán las `Pages`, `Components`, la configuración de **MudBlazor** y cualquier otra configuración específica de la aplicación (como la inyección de dependencias).

## MudBlazor como librería de estilos
Para quienes hayan trabajado con **MVC** sabrá que lo común en ese framework era trabajar con **Bootstrap** 4 o 5. Al salir los frameworks componetizados como **React**, **Vue** y **Blazor** aparecieron librerías un poco más ricas y complejas para la experiencia del desarrollador. Por el lado de React la más conocida es `Material UI`, la equivalencia de esta para **Blazor** es `MudBlazor` la cual sigue la misma teoría de componentes reutilizables, escalables y customizables.

**Tarea 1:** Investigar e implementar MudBlazor como librería de estilos para el proyecto web. Toda la documentación referida a la librería se encuentra en el [siguiente enlace](https://mudblazor.com/getting-started/installation#manual-install-install-package).

**Tarea 2:** Crear una página que dentro contenga una tabla con la información general de los usuarios. Al final de cada fila deberá haber un botón que al presionarle muestre una `tooltip` con las opciones de **Eliminar** y **Editar** el registro. Fuera de la tabla, deberá haber un botón que corresponda a **Crear usuario**.

**Tarea 3:** Al hacer click sobre la opción **Editar** o el botón **Crear usuario** deberá dispararse un modal que contenga los campos requeridos para crear/modificar un usuario. El componente del modal deberá ser sólo uno y deberá servir para poder realizar las dos acciones.

**Tarea 4**: Al presionar eliminar, se deberá mostrar un modal de confirmación y luego de que el usuario acepte, enviar al backend la petición.

**Tarea 5**: Al guardar los cambios en el modal de alta o modificación, se deberá mostrar un modal de confirmación y luego que el usuario acepte, enviar al backend la petición.

**Tarea 6**: Dentro del formulario de alta o modificación, implementar las mismas validaciones del backend utilizando FluentValidator. Para más información visitar el [siguiente enlance](https://mudblazor.com/components/form).

Ejemplo visual de cómo debería verse el frontend:

### Página principal con tabla y botón para alta de usuarios
![alt text](./Vista%20lista%20usuarios.png)

### Vista de modal que aparecerá al crear o editar usuarios
![alt text](./Vista%20alta%20o%20edición%20usuarios.png)

## Uso de Fluxor para el manejo de estados globales
En lugar de manejar estados de forma local o usar patrones propios de Blazor (como @code con variables de estado), emplearemos `Fluxor` para centralizar los estados y las acciones de la aplicación. Esto nos permitirá:

* **Escalabilidad**: Facilita el crecimiento de la aplicación sin perder control de los estados.
* **Predictibilidad**: Los estados son inmutables y se modifican únicamente por medio de Actions y Reducers.
* **Organización**: Podremos definir lógicamente nuestros Reducers, Actions, Effects y States en carpetas relacionadas.

**Tarea 7**: Investigar e implementar Fluxor como manejador de estados globales. Toda la documentación referida a la librería se encuentra en el [siguiente enlace](https://github.com/mrpmorris/Fluxor).

> Para más información al respecto, se puede visitar el siguiente vídeo: [Blazor State Management with Fluxor.](https://youtu.be/yM9F8rxo8L8)

## Integración con la Web API

Todas las operaciones CRUD que se realicen en el sitio Blazor para la entidad `User` se harán a través de la Web API que construiste con Clean Architecture. Para ello:

* **Infrastructure**: Creamos servicios que encapsulen las llamadas HTTP (usando HttpClient, Refit u otra librería, si se prefiere).
Domain: Podrás reutilizar los modelos de dominio si así lo requieres o crear DTOs específicos para el front-end.

* **Application**: La capa Application en Blazor Server se puede alinear a la lógica de orquestación/transformación dentro de la app. Ya que el manejo de los reducers, states y effects está del lado de Application, deberás disparar desde tus Effects las llamadas a los servicios del Infrastructure, y luego los resultados se propagarán a través de los Reducers actualizando el State.

**Tarea 8:** Implementar los servicios y dtos para poder consumir los endpoints de la `WebApi` generada desde Infrastructure utilizando HttpClient, Refit u otra librería a elección.

**Tarea 9**: Implementar la lógica de los servicios a la página de alta, baja y modificación de los usuarios.

## Consideraciones generales

* El formulario de creación o modificación deberá contemplar las mismas validaciones que se contemplan en el backend. 

* Al realizar las llamadas a la webapi se deberá tener en cuenta que el patrón result puede responder un objeto o un array de errores, se deberá poder manejar bien estos casos.

* No se deberá mostrar ningún error en crudo al usuario en la vista (errores de internal server error, no se pudo conectar a la base de datos o no logro hacer la conexión con el backend).

* El diseño del sitio debe ser responsive.

## Glosario de tareas

* [ ]  Investigar e implementar MudBlazor como librería de estilos para el proyecto web. Toda la documentación referida a la librería se encuentra en el [siguiente enlace](https://mudblazor.com/getting-started/installation#manual-install-install-package).
* [ ] Crear una página que dentro contenga una tabla con la información general de los usuarios. Al final de cada fila deberá haber un botón que al presionarle muestre una `tooltip` con las opciones de **Eliminar** y **Editar** el registro. Fuera de la tabla, deberá haber un botón que corresponda a **Crear usuario**.
* [ ] Al hacer click sobre la opción **Editar** o el botón **Crear usuario** deberá dispararse un modal que contenga los campos requeridos para crear/modificar un usuario. El componente del modal deberá ser sólo uno y deberá servir para poder realizar las dos acciones.
* [ ] Al presionar eliminar, se deberá mostrar un modal de confirmación y luego de que el usuario acepte, enviar al backend la petición.
* [ ] Al guardar los cambios en el modal de alta o modificación, se deberá mostrar un modal de confirmación y luego que el usuario acepte, enviar al backend la petición.
* [ ] Dentro del formulario de alta o modificación, implementar las mismas validaciones del backend utilizando FluentValidator. Para más información visitar el [siguiente enlance](https://mudblazor.com/components/form).
* [ ] Investigar e implementar Fluxor como manejador de estados globales. Toda la documentación referida a la librería se encuentra en el [siguiente enlace](https://github.com/mrpmorris/Fluxor).
* [ ] Implementar los servicios y dtos para poder consumir los endpoints de la `WebApi` generada desde Infrastructure utilizando HttpClient, Refit u otra librería a elección.
* [ ] Implementar la lógica de los servicios a la página de alta, baja y modificación de los usuarios.