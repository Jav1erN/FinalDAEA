# Schema PostgreSQL para Render

## Archivo principal

Usa este script para crear la estructura de la base de datos en Render PostgreSQL:

```text
database/clinic_system_schema.sql
```

El script fue generado desde `ClinicDbContext` con EF Core y contiene:

- Extension `pgcrypto`.
- Schemas:
  - `clinical`
  - `auth`
  - `billing`
  - `laboratory`
  - `pharmacy`
  - `notifications`
- 29 tablas.
- Primary keys.
- Foreign keys.
- Indices normales y unicos.
- Columnas calculadas y valores por defecto definidos en el modelo EF.

## Ejecucion en Render PostgreSQL

1. En Render, abre la base PostgreSQL creada para `clinic_system`.
2. Copia el `External Database URL` o usa la consola/shell que tengas disponible.
3. Ejecuta:

```bash
psql "<DATABASE_URL_DE_RENDER>" -f database/clinic_system_schema.sql
```

Si estas en Windows PowerShell:

```powershell
psql "<DATABASE_URL_DE_RENDER>" -f "database/clinic_system_schema.sql"
```

## Verificacion rapida

Despues de ejecutar el script, valida que existan los schemas:

```sql
SELECT schema_name
FROM information_schema.schemata
WHERE schema_name IN ('clinical', 'auth', 'billing', 'laboratory', 'pharmacy', 'notifications')
ORDER BY schema_name;
```

Valida tambien el total de tablas:

```sql
SELECT table_schema, COUNT(*) AS tables
FROM information_schema.tables
WHERE table_schema IN ('clinical', 'auth', 'billing', 'laboratory', 'pharmacy', 'notifications')
GROUP BY table_schema
ORDER BY table_schema;
```

## Nota importante

Este archivo crea la estructura, no inserta datos iniciales. Si tus endpoints dependen de catalogos como roles, permisos, estados de cita, tipos de notificacion, departamentos o especialidades, debes cargar tambien datos seed antes de probar los flujos completos por Swagger.
