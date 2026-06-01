<div align="center">

# ClinicalSystem

### Enterprise Hospital & Clinical Management Platform

.NET 9 • PostgreSQL • CQRS • MediatR • Hexagonal Architecture • Clean Architecture

---

Sistema integral para la gestión clínica, médica y administrativa de instituciones hospitalarias.

</div>

---

# 👥 Participantes

* Javier Neira
* Mireya Apaza

---

# Introducción del Proyecto

**ClinicalSystem** es una plataforma backend desarrollada en .NET 9 orientada a la administración integral de procesos clínicos y administrativos dentro de una institución de salud.

El sistema busca centralizar toda la información relacionada con pacientes, médicos, personal hospitalario, citas, historias clínicas, diagnósticos, laboratorio, farmacia, hospitalización, inventario, facturación, pagos y seguros, permitiendo mantener trazabilidad completa entre los procesos asistenciales y administrativos.

Actualmente muchas instituciones médicas manejan información distribuida en múltiples sistemas o procesos manuales, generando duplicidad de datos, dificultades de seguimiento y problemas de coordinación entre áreas. ClinicalSystem propone una solución unificada capaz de mejorar la organización de la información, optimizar la atención al paciente y facilitar la gestión hospitalaria.

El proyecto se desarrolla aplicando estándares modernos de ingeniería de software mediante Arquitectura Hexagonal (Ports & Adapters), CQRS, MediatR, Entity Framework Core y Vertical Slice Architecture, garantizando escalabilidad, mantenibilidad y bajo acoplamiento entre componentes.

---

# 🎯 Alcance

La versión inicial del sistema contempla el desarrollo de los siguientes módulos:

| Módulo              | Alcance                                  |
| ------------------- | ---------------------------------------- |
| Patients            | CRUD completo de pacientes               |
| Staff               | Gestión de personal hospitalario         |
| Medical Specialties | Administración de especialidades médicas |
| Doctors             | Gestión de médicos                       |
| Appointments        | Registro y programación de citas         |
| Medical Records     | Historias clínicas                       |
| Diagnoses           | Diagnósticos médicos                     |
| Prescriptions       | Recetas médicas                          |
| Laboratory Orders   | Solicitudes de laboratorio               |
| Laboratory Results  | Resultados clínicos                      |
| Imaging             | Estudios de imagenología                 |
| Rooms               | Administración de habitaciones           |
| Admissions          | Ingreso de pacientes                     |
| Medical Discharges  | Altas médicas                            |
| Surgeries           | Gestión de cirugías                      |
| Pharmacy            | Administración de medicamentos           |
| Inventory           | Control de inventario hospitalario       |
| Billing             | Facturación                              |
| Payments            | Registro de pagos                        |
| Insurance           | Gestión de seguros médicos               |

### Funcionalidades Incluidas

* Operaciones CRUD para todos los módulos.
* Arquitectura Hexagonal (Ports & Adapters).
* CQRS mediante MediatR.
* Entity Framework Core Database First.
* PostgreSQL.
* Repository Pattern.
* Unit Of Work.
* FluentValidation.
* Swagger/OpenAPI.
* Vertical Slice Architecture.
* Dependency Injection por capas.

### Funcionalidades No Incluidas

* Aplicación móvil.
* Integración con sistemas externos.
* Pasarelas de pago online.
* Inteligencia artificial clínica.
* Notificaciones por correo o SMS.
* Integraciones gubernamentales.

---

# Arquitectura

El sistema implementa:

* Clean Architecture
* Hexagonal Architecture
* CQRS
* MediatR
* Vertical Slice Architecture
* Repository Pattern
* Unit Of Work

---

# Objetivo General

Desarrollar una plataforma hospitalaria moderna capaz de centralizar la gestión clínica y administrativa de una institución médica mediante una arquitectura empresarial escalable basada en .NET 9, PostgreSQL, CQRS y Arquitectura Hexagonal.
