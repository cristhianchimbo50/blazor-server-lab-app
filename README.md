# Aplicación Web para la Gestión para Laboratorio Clínico "La Inmaculada"

Proyecto de desarrollo de una aplicación para la gestión de pacientes, órdenes médicas, exámenes, resultados, cuentas por cobrar, convenios médicos y control de inventario de reactivos.

## Tecnologías utilizadas
- Blazor Server (.NET 8)
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server Express y Azure SQL Database
- Azure App Service
- GitHub Actions (CI/CD)

## Estructura general
- **Frontend:** Blazor Server
- **Backend:** API REST en ASP.NET Core [Ver repositorio de la API](https://github.com/cristhianchimbo50/blazor-server-lab-api.git)
- **Base de datos:** SQL Server local (desarrollo) y Azure SQL Database (producción)
- **Despliegue:** Azure App Service + Dominio personalizado
- **Automatización:** GitHub Actions para CI/CD

## Funcionalidades principales
- Gestión de pacientes
- Gestión de órdenes médicas y exámenes
- Registro de resultados de laboratorio
- Cuentas por cobrar y control de pagos
- Control de inventario de reactivos
- Registro y gestión de convenios médicos
- Generación de reportes de pagos a médicos
- Sistema de autenticación y autorización por roles

## Planificación por Sprints

El desarrollo se organiza en **10 sprints** semanales, del 24 de abril al 2 de julio.

Al final de cada sprint se realizará:
- Revisión de funcionalidades terminadas
- Ajustes o correcciones si son necesarias
- Despliegues de avance en ambiente de pruebas

---

## Tabla de Sprint Planning

| ID | Tarea | Estimación (Horas) | Sprint | Fecha Fin |
|:---|:---|:---|:---|:---|
| HT-001 | Configuración del entorno local | - | 1 | 30 abril |
| HT-002 | Configuración del repositorio | - | 1 | 30 abril |
| HT-003 | Instalación y configuración de SQL Server Express | - | 1 | 30 abril |
| HT-004 | Creación de proyecto API y acceso a base de datos | - | 1 | 30 abril |
| HT-005 | Diseño e implementación de base de datos | - | 2 | 7 mayo |
| HT-006 | Separación de capas | - | 2 | 7 mayo |
| HT-007 | Configuración de consumo de API | - | 2 | 7 mayo |
| HT-008 | Primer servicio Blazor para API | - | 3 | 14 mayo |
| HT-009 | Integración de ASP.NET Identity | - | 3 | 14 mayo |
| HU-001 | Inicio de sesión (Login) | 5 | 3 | 14 mayo |
| HU-002 | Registro manual de nuevo paciente | 5 | 4 | 21 mayo |
| HU-003 | Preregistro de datos del paciente | 6 | 4 | 21 mayo |
| HU-004 | Selección de exámenes | 8 | 5 | 28 mayo |
| HU-005 | Registro de pago | 6 | 5 | 28 mayo |
| HU-006 | Confirmación y guardado de orden | 4 | 5 | 28 mayo |
| HU-007 | Impresión de orden médica | 5 | 6 | 4 junio |
| HU-008 | Visualización de órdenes | 5 | 6 | 4 junio |
| HU-009 | Detalle de orden médica | 4 | 6 | 4 junio |
| HU-010 | Ingreso de resultados de exámenes | 8 | 7 | 11 junio |
| HU-011 | Verificación de resultados completos | 4 | 7 | 11 junio |
| HU-012 | Impresión de resultados | 5 | 7 | 11 junio |
| HU-013 | Anulación de orden | 4 | 8 | 18 junio |
| HU-014 | Anulación de resultado | 4 | 8 | 18 junio |
| HU-015 | Visualización de cuentas por cobrar | 5 | 8 | 18 junio |
| HU-016 | Pago de saldo pendiente | 4 | 8 | 18 junio |
| HU-017 | Registro de ingreso de reactivos | 4 | 9 | 25 junio |
| HU-018 | Egreso de reactivos por exámenes | 7 | 9 | 25 junio |
| HU-019 | Registro de convenios con médicos | 5 | 9 | 25 junio |
| HU-020 | Cálculo de pago por orden médica | 6 | 9 | 25 junio |
| HU-021 | Listado de convenios médicos | 4 | 9 | 25 junio |
| HU-022 | Generación de pagos a médicos | 6 | 10 | 2 julio |
| HT-010 | Configuración de CI/CD con GitHub Actions | - | 10 | 2 julio |
| HT-011 | Despliegue de Blazor Server en Azure App Service | - | 10 | 2 julio |
| HT-012 | Creación de Azure SQL Database | - | 10 | 2 julio |
| HT-013 | Migración de Base de Datos Local a Azure SQL Database | - | 10 | 2 julio |
| HT-014 | Configuración de dominio personalizado | - | 10 | 2 julio |
| HT-015 | Configuración de appsettings y secretos | - | 10 | 2 julio |
| HT-016 | Configuración de Backup automático de Azure SQL Database | - | 10 | 2 julio |

---

## Notas finales
- Se seguirán prácticas ágiles (Scrum) durante todo el proyecto.
- Se realizarán retrospectivas breves al final de cada sprint.
- El código y la documentación se actualizarán progresivamente.

---

# Objetivo final
**Tener una solución estable, segura y profesional completamente operativa en Azure, lista para producción, a más tardar el 2 de julio.**
