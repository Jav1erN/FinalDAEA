# Validacion Render - ClinicSystem

## Objetivo

Validar el backend publicado en Render contra la base Neon sin eliminar datos existentes.

URL objetivo:

```text
https://finaldaea.onrender.com
```

## Archivos

- `database/clinic_system_seed.sql`: datos minimos idempotentes para catalogos.
- `tests/render-validation/run-tests.ps1`: validacion HTTP automatizada contra Render.
- `tests/render-validation/test-report-template.md`: plantilla de reporte manual.
- `tests/render-validation/clinic-system-render.http`: coleccion HTTP manual.

## Paso 1: ejecutar seed en Neon

No versionar credenciales ni `DATABASE_URL`.

En una terminal con `psql`:

```powershell
$env:DATABASE_URL="<URL_DE_NEON>"
psql $env:DATABASE_URL -f "database/clinic_system_seed.sql"
```

El seed es idempotente:

- Usa UUIDs fijos para catalogos base.
- Usa `ON CONFLICT` donde existen restricciones unicas.
- Usa `WHERE NOT EXISTS` para catalogos sin restriccion unica.
- Puede ejecutarse varias veces.

## Paso 2: configurar URL base

```powershell
$env:API_BASE_URL="https://finaldaea.onrender.com"
```

## Paso 3: ejecutar validacion automatizada

```powershell
.\tests\render-validation\run-tests.ps1
```

El script:

- Valida `GET /health`.
- Valida Swagger UI.
- Ejecuta `GET` simples sobre modulos principales.
- Crea registros nuevos con sufijo unico.
- Guarda IDs devueltos en memoria.
- Consulta registros creados con `GET /{id}`.
- Actualiza algunos registros con `PUT`.
- No ejecuta `DELETE`.

## Orden funcional probado

1. Catalogos base: roles, permisos, departamentos, especialidades, estados de cita y tipos de notificacion.
2. Datos operativos: usuario, paciente, doctor.
3. Flujo clinico: cita, historia clinica, diagnostico, tratamiento, signos vitales, laboratorio.
4. Flujo farmacia: medicamento, movimiento de stock, receta y detalle.
5. Flujo facturacion: aseguradora, poliza, factura, detalle y pago.
6. Flujo notificaciones: notificacion y refresh token.

## Dependencias relevantes

- `auth.users.role_id` depende de `auth.roles`.
- `clinical.specialties.department_id` depende de `clinical.departments`.
- `clinical.doctors.user_id` depende de `auth.users`.
- `clinical.doctors.specialty_id` depende de `clinical.specialties`.
- `clinical.patients.user_id` puede ser nulo.
- `clinical.appointments.patient_id` depende de `clinical.patients`.
- `clinical.appointments.doctor_id` depende de `clinical.doctors`.
- `clinical.appointments.status_id` depende de `clinical.appointment_statuses`.
- `clinical.medical_records.patient_id` depende de `clinical.patients`.
- `clinical.medical_records.doctor_id` depende de `clinical.doctors`.
- `clinical.medical_records.appointment_id` puede depender de `clinical.appointments`.
- Diagnosticos, tratamientos y signos vitales dependen de `clinical.medical_records`.
- Laboratorio depende de paciente, doctor y opcionalmente historia clinica.
- Facturacion depende de paciente, cita y opcionalmente poliza.
- Pagos y detalles dependen de factura.
- Notificaciones dependen de usuario y tipo de notificacion.

## DELETE

No se ejecutan deletes en la suite. La revision del codigo muestra que los handlers de `Delete*Command` llaman `repository.Remove(entity)` y el repositorio usa `DbSet.Remove(entity)`. Por tanto, los endpoints `DELETE` eliminan fisicamente, aunque varias tablas tengan columna `deleted_at`.

## Observaciones de rutas

El backend publicado conserva rutas con pluralizacion irregular generada por los controladores:

- `api/specialtys`
- `api/insurance-companys`
- `api/insurance-policys`

La suite usa esas rutas exactas.

## Controladores y rutas inspeccionadas

Todos los controladores siguen el patron REST basico `GET`, `GET /{id}`, `POST`, `PUT /{id}` y `DELETE /{id}`:

| Modulo | Ruta |
| --- | --- |
| Appointments | `/api/appointments` |
| AppointmentStatuses | `/api/appointment-statuses` |
| AuditLogs | `/api/audit-logs` |
| BillingDetails | `/api/billing-details` |
| Billings | `/api/billings` |
| Departments | `/api/departments` |
| Diagnoses | `/api/diagnoses` |
| Doctors | `/api/doctors` |
| InsuranceCompanies | `/api/insurance-companys` |
| InsurancePolicies | `/api/insurance-policys` |
| LaboratoryResults | `/api/laboratory-results` |
| LaboratoryTests | `/api/laboratory-tests` |
| MedicalRecords | `/api/medical-records` |
| Medications | `/api/medications` |
| Notifications | `/api/notifications` |
| NotificationTypes | `/api/notification-types` |
| Patients | `/api/patients` |
| Payments | `/api/payments` |
| Permissions | `/api/permissions` |
| PrescriptionDetails | `/api/prescription-details` |
| Prescriptions | `/api/prescriptions` |
| RefreshTokens | `/api/refresh-tokens` |
| Roles | `/api/roles` |
| Specialties | `/api/specialtys` |
| StockMovements | `/api/stock-movements` |
| Treatments | `/api/treatments` |
| Users | `/api/users` |
| VitalSigns | `/api/vital-signs` |

## Si falla una prueba

1. Confirmar que `/health` responde.
2. Confirmar que Swagger esta habilitado en Render.
3. Confirmar que `database/clinic_system_seed.sql` fue ejecutado en Neon.
4. Revisar si hay restricciones unicas por ejecuciones previas; el script usa sufijo por timestamp para minimizar colisiones.
5. Documentar el resultado en `test-report-template.md`.
