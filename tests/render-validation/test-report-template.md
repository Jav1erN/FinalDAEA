# Reporte de Validacion Render - ClinicSystem

Fecha:

Responsable:

API base:

Base de datos:

## Resumen

| Total endpoints | Exitosos | Fallidos | Bloqueados |
| --- | ---: | ---: | ---: |
|  |  |  |  |

## Detalle por endpoint

| Endpoint probado | Metodo | Payload | Respuesta esperada | Respuesta obtenida | Estado final |
| --- | --- | --- | --- | --- | --- |
| `/health` | GET | N/A | HTTP 200 |  |  |
| `/swagger/index.html` | GET | N/A | HTTP 200 |  |  |
| `/api/roles` | GET/POST/PUT | Ver script | HTTP 200 |  |  |
| `/api/permissions` | GET/POST | Ver script | HTTP 200 |  |  |
| `/api/departments` | GET/POST/PUT | Ver script | HTTP 200 |  |  |
| `/api/specialtys` | GET/POST | Ver script | HTTP 200 |  |  |
| `/api/appointment-statuses` | GET/POST | Ver script | HTTP 200 |  |  |
| `/api/notification-types` | GET/POST | Ver script | HTTP 200 |  |  |
| `/api/users` | GET/POST | Ver script | HTTP 200 |  |  |
| `/api/patients` | GET/POST/PUT | Ver script | HTTP 200 |  |  |
| `/api/doctors` | GET/POST | Ver script | HTTP 200 |  |  |
| `/api/appointments` | GET/POST | Ver script | HTTP 200 |  |  |
| `/api/medical-records` | GET/POST | Ver script | HTTP 200 |  |  |
| `/api/diagnoses` | POST | Ver script | HTTP 200 |  |  |
| `/api/treatments` | POST | Ver script | HTTP 200 |  |  |
| `/api/vital-signs` | POST | Ver script | HTTP 200 |  |  |
| `/api/laboratory-tests` | POST | Ver script | HTTP 200 |  |  |
| `/api/laboratory-results` | POST | Ver script | HTTP 200 |  |  |
| `/api/medications` | GET/POST | Ver script | HTTP 200 |  |  |
| `/api/stock-movements` | POST | Ver script | HTTP 200 |  |  |
| `/api/prescriptions` | POST | Ver script | HTTP 200 |  |  |
| `/api/prescription-details` | POST | Ver script | HTTP 200 |  |  |
| `/api/insurance-companys` | POST | Ver script | HTTP 200 |  |  |
| `/api/insurance-policys` | POST | Ver script | HTTP 200 |  |  |
| `/api/billings` | GET/POST/PUT | Ver script | HTTP 200 |  |  |
| `/api/billing-details` | POST | Ver script | HTTP 200 |  |  |
| `/api/payments` | POST | Ver script | HTTP 200 |  |  |
| `/api/notifications` | POST | Ver script | HTTP 200 |  |  |
| `/api/refresh-tokens` | POST | Ver script | HTTP 200 |  |  |

## Endpoints bloqueados o no probados

| Endpoint | Motivo exacto | Accion recomendada |
| --- | --- | --- |
| DELETE endpoints | No se ejecutan porque hacen eliminacion fisica con `DbSet.Remove`. | Validar en una base temporal o cambiar a soft delete si corresponde. |
| `/api/audit-logs` POST/PUT | No incluido en la prueba automatizada para evitar ambiguedad con columnas `jsonb` y payloads de auditoria. | Probar manualmente con JSON valido si se requiere auditoria. |

## Evidencia

Pegar salida de consola de `run-tests.ps1`:

```text

```

## Resultado final

Estado:

Observaciones:
