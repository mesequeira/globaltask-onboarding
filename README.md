# Objetivo
La idea principal de este onboarding es que los nuevos integrantes del equipo puedan tener una idea general de cómo se construyen los distintos proyectos en nuestra celula de desarrollo.

El proyecto contemplará varios escenarios donde se apliquen los patrones y arquitecturas más utilizadas en nuestros servicios. Primero se iniciará con un proyecto WebApi, para luego continuar con un Worker

# Proyecto WebApi

La estructura inicial de este proyecto se puede encontrar en la carpeta [webapi](./webapi/).

Este servicio contará con operaciones básicas sobre una entidad Usuario, las cuales deberán desarrollarse bajo distintos conceptos de arquitecturas y patrones de diseño, ejecutándolo con contenedores de Docker y manejando el acceso a datos con Entity Framework y SQL Server.

# Proyecto Worker

La estructura inicial de este proyecto se puede encontrar en la carpeta [worker](./worker/).

Este servicio se encargará de procesar tareas asíncronicas con RabbitMQ integrándose con la librería MassTransit, deberá enviar notificaciones vía correo electrónico por lo que se configurará un SMTP de Gmail para poder lograrlo.