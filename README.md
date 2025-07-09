# Aplicación Web para la Gestión del Laboratorio Clínico "La Inmaculada"

Proyecto de desarrollo de una aplicación web para la gestión de pacientes, órdenes médicas, exámenes, resultados, cuentas por cobrar, convenios médicos y control de inventario de reactivos del laboratorio "La Inmaculada".

## Tecnologías utilizadas
- Blazor Server (.NET 8)
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server Express (desarrollo) y Azure SQL Database (producción)
- Azure App Service (despliegue)
- GitHub Actions (CI/CD)

## Estructura general
- **Frontend:** Blazor Server
- **Backend:** API REST en ASP.NET Core → [Ver repositorio API](https://github.com/cristhianchimbo50/apirest-lab-app.git)
- **Base de datos:** SQL Server local y Azure SQL Database
- **Despliegue:** Azure App Service + Dominio personalizado
- **Automatización:** GitHub Actions para CI/CD

## Funcionalidades principales
- Gestión de pacientes y médicos
- Gestión de órdenes médicas y exámenes
- Registro y visualización de resultados de laboratorio
- Gestión de cuentas por cobrar y pagos
- Control de inventario de reactivos
- Registro y control de convenios médicos
- Generación de pagos a médicos
- Sistema de autenticación y autorización por roles
- Generación de reportes PDF (órdenes y resultados)

---

## Planificación por Sprints

El proyecto está organizado en **10 sprints semanales**, del 24 de abril al 2 de julio.

Cada sprint incluye:
- Desarrollo incremental de funcionalidades.
- Revisión y ajustes al finalizar el sprint.
- Despliegues progresivos en ambiente de pruebas.

---

## Tabla de Sprint Planning

| ID | Tarea | Estimación (Horas) | Sprint | Fecha Fin |
|----|----------------------------------------------------------|--------------------|--------|------------|
| HT-001 | Configuración del entorno local | - | 1 | 30 abril |
| HT-002 | Configuración del repositorio | - | 1 | 30 abril |
| HT-003 | Instalación y configuración de SQL Server Express | - | 1 | 30 abril |
| HT-004 | Creación de proyecto API y acceso a base de datos | - | 2 | 7 mayo |
| HT-005 | Diseño e implementación de base de datos | - | 2 | 7 mayo |
| HT-006 | Separación de capas | - | 2 | 7 mayo |
| HT-007 | Configuración de consumo de API | - | 2 | 7 mayo |
| HT-008 | Primer servicio Blazor para API | - | 3 | 14 mayo |
| HT-009 | Integración de ASP.NET Identity | - | 3 | 14 mayo |
| HU-001 | Inicio de sesión (Login) | 5 | 3 | 14 mayo |
| HU-002 | Registro manual de nuevo paciente | 5 | 4 | 21 mayo |
| HU-003 | Preregistro de datos del paciente | 6 | 4 | 21 mayo |
| HU-022 | Ingreso de nuevo examen | 5 | 4 | 21 mayo |
| HU-023 | Edición de examen | 4 | 4 | 21 mayo |
| HU-024 | Visualización de exámenes registrados | 3 | 5 | 28 mayo |
| HU-004 | Selección de exámenes | 8 | 5 | 28 mayo |
| HU-005 | Registro de pago | 6 | 5 | 28 mayo |
| HU-006 | Confirmación y guardado de orden | 4 | 5 | 28 mayo |
| HU-007 | Impresión de orden médica | 5 | 6 | 4 junio |
| HU-008 | Visualización de órdenes | 3 | 6 | 4 junio |
| HU-009 | Detalle de orden médica | 4 | 6 | 4 junio |
| HU-025 | Ingreso de nuevo médico | 4 | 6 | 4 junio |
| HU-026 | Visualización de médicos registrados | 3 | 6 | 4 junio |
| HU-027 | Edición de datos de médico | 4 | 7 | 11 junio |
| HU-010 | Ingreso de resultados de exámenes | 8 | 7 | 11 junio |
| HU-011 | Impresión de resultados | 5 | 7 | 11 junio |
| HU-012 | Anulación de orden | 4 | 7 | 11 junio |
| HU-013 | Anulación de resultado | 4 | 7 | 11 junio |
| HU-014 | Visualización de cuentas por cobrar | 2 | 8 | 18 junio |
| HU-015 | Pago de saldo pendiente | 4 | 8 | 18 junio |
| HU-028 | Creación de reactivos | 3 | 8 | 18 junio |
| HU-029 | Visualización de reactivos | 2 | 8 | 18 junio |
| HU-030 | Edición de reactivos | 2 | 8 | 18 junio |
| HU-016 | Ingreso de stock de reactivos | 4 | 8 | 18 junio |
| HU-032 | Ingreso de asociación entre examen - reactivo | 3 | 8 | 18 junio |
| HU-033 | Visualización de asociación examen - reactivo | 2 | 8 | 18 junio |
| HU-017 | Egreso de reactivos por exámenes | 5 | 8 | 18 junio |
| HU-031 | Egreso de reactivos por otros factores | 3 | 8 | 18 junio |
| HU-018 | Visualización de convenios médicos | 2 | 9 | 18 junio |
| HU-019 | Ingreso de convenios con médicos | 4 | 9 | 25 junio |
| HU-020 | Cálculo de pago por orden médica | 2 | 9 | 25 junio |
| HU-021 | Generación de pagos a médicos | 6 | 9 | 25 junio |
| HT-014 | Mejora y modificación de la interfaz de usuario | - | 10 | 2 julio |
| HT-015 | Verificación interna de funcionalidades | - | 10 | 2 julio |
| HT-010 | Configuración de CI/CD con GitHub Actions | - | 10 | 2 julio |
| HT-011 | Despliegue completo en Azure (App + Dominio) | - | 10 | 2 julio |
| HT-012 | Azure SQL Database y migración de datos | - | 10 | 2 julio |
| HT-013 | Configuración de backup automático en Azure SQL Database | - | 10 | 2 julio |

---

## Notas finales
- Se sigue la metodología **Scrum**.
- Se realizan retrospectivas y revisiones al final de cada sprint.
- El código y la documentación se actualizan iterativamente.
- La solución está diseñada para escalarse y adaptarse a producción en Azure.

---

## 🎯 Objetivo final
**Tener una solución estable, segura y profesional desplegada en Azure, lista para producción, a más tardar el 2 de julio.**
