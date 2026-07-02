-- Seed minimo idempotente para validar ClinicSystem en Neon/Render.
-- No contiene credenciales ni datos sensibles.
-- Puede ejecutarse varias veces sin duplicar catalogos.

BEGIN;

CREATE EXTENSION IF NOT EXISTS pgcrypto;

-- Roles base
INSERT INTO auth.roles (role_id, name, description)
VALUES
    ('11111111-1111-1111-1111-111111111111', 'Admin', 'Rol administrativo para validacion y operacion del sistema'),
    ('11111111-1111-1111-1111-111111111112', 'Doctor', 'Rol medico para atencion clinica'),
    ('11111111-1111-1111-1111-111111111113', 'Patient', 'Rol de paciente'),
    ('11111111-1111-1111-1111-111111111114', 'Receptionist', 'Rol operativo para agenda y admision')
ON CONFLICT (name) DO UPDATE
SET description = EXCLUDED.description,
    updated_at = CURRENT_TIMESTAMP;

-- Permisos base
INSERT INTO auth.permissions (permission_id, resource, action, description)
VALUES
    ('22222222-2222-2222-2222-222222222201', 'patients', 'read', 'Consultar pacientes'),
    ('22222222-2222-2222-2222-222222222202', 'patients', 'write', 'Crear y actualizar pacientes'),
    ('22222222-2222-2222-2222-222222222203', 'appointments', 'read', 'Consultar citas'),
    ('22222222-2222-2222-2222-222222222204', 'appointments', 'write', 'Crear y actualizar citas'),
    ('22222222-2222-2222-2222-222222222205', 'medical-records', 'read', 'Consultar historias clinicas'),
    ('22222222-2222-2222-2222-222222222206', 'medical-records', 'write', 'Crear y actualizar historias clinicas'),
    ('22222222-2222-2222-2222-222222222207', 'billing', 'read', 'Consultar facturacion'),
    ('22222222-2222-2222-2222-222222222208', 'billing', 'write', 'Crear y actualizar facturacion'),
    ('22222222-2222-2222-2222-222222222209', 'catalogs', 'read', 'Consultar catalogos'),
    ('22222222-2222-2222-2222-222222222210', 'catalogs', 'write', 'Crear y actualizar catalogos')
ON CONFLICT (resource, action) DO UPDATE
SET description = EXCLUDED.description;

-- Permisos para Admin
INSERT INTO auth.role_permissions (role_id, permission_id)
SELECT r.role_id, p.permission_id
FROM auth.roles r
CROSS JOIN auth.permissions p
WHERE r.name = 'Admin'
ON CONFLICT (role_id, permission_id) DO NOTHING;

-- Estados de cita
INSERT INTO clinical.appointment_statuses (status_id, name, description)
VALUES
    ('33333333-3333-3333-3333-333333333301', 'Scheduled', 'Cita programada'),
    ('33333333-3333-3333-3333-333333333302', 'Confirmed', 'Cita confirmada'),
    ('33333333-3333-3333-3333-333333333303', 'Completed', 'Cita completada'),
    ('33333333-3333-3333-3333-333333333304', 'Cancelled', 'Cita cancelada')
ON CONFLICT (name) DO UPDATE
SET description = EXCLUDED.description;

-- Departamentos clinicos
INSERT INTO clinical.departments (department_id, name, description, is_active)
VALUES
    ('44444444-4444-4444-4444-444444444401', 'Medicina General', 'Departamento de atencion primaria', TRUE),
    ('44444444-4444-4444-4444-444444444402', 'Cardiologia', 'Departamento de cardiologia', TRUE),
    ('44444444-4444-4444-4444-444444444403', 'Pediatria', 'Departamento de pediatria', TRUE),
    ('44444444-4444-4444-4444-444444444404', 'Laboratorio Clinico', 'Departamento de apoyo diagnostico', TRUE)
ON CONFLICT (name) DO UPDATE
SET description = EXCLUDED.description,
    is_active = EXCLUDED.is_active,
    updated_at = CURRENT_TIMESTAMP;

-- Especialidades
INSERT INTO clinical.specialties (specialty_id, department_id, name, description, is_active)
VALUES
    ('55555555-5555-5555-5555-555555555501', '44444444-4444-4444-4444-444444444401', 'Medicina Familiar', 'Especialidad de medicina familiar', TRUE),
    ('55555555-5555-5555-5555-555555555502', '44444444-4444-4444-4444-444444444402', 'Cardiologia General', 'Especialidad de cardiologia general', TRUE),
    ('55555555-5555-5555-5555-555555555503', '44444444-4444-4444-4444-444444444403', 'Pediatria General', 'Especialidad de pediatria general', TRUE),
    ('55555555-5555-5555-5555-555555555504', '44444444-4444-4444-4444-444444444404', 'Patologia Clinica', 'Especialidad de laboratorio clinico', TRUE)
ON CONFLICT (name) DO UPDATE
SET department_id = EXCLUDED.department_id,
    description = EXCLUDED.description,
    is_active = EXCLUDED.is_active,
    updated_at = CURRENT_TIMESTAMP;

-- Tipos de notificacion
INSERT INTO notifications.notification_types (type_id, code, name, template_subject, template_body, is_active)
VALUES
    ('66666666-6666-6666-6666-666666666601', 'APPOINTMENT_REMINDER', 'Recordatorio de cita', 'Recordatorio de cita medica', 'Tiene una cita medica programada.', TRUE),
    ('66666666-6666-6666-6666-666666666602', 'LAB_RESULT_READY', 'Resultado de laboratorio listo', 'Resultado disponible', 'Su resultado de laboratorio esta disponible.', TRUE),
    ('66666666-6666-6666-6666-666666666603', 'BILLING_NOTICE', 'Aviso de facturacion', 'Aviso de facturacion', 'Tiene una actualizacion de facturacion.', TRUE)
ON CONFLICT (code) DO UPDATE
SET name = EXCLUDED.name,
    template_subject = EXCLUDED.template_subject,
    template_body = EXCLUDED.template_body,
    is_active = EXCLUDED.is_active;

-- Catalogos de apoyo para pruebas manuales
INSERT INTO billing.insurance_companies (insurance_company_id, name, phone, email, address, contact_name, is_active)
VALUES
    ('77777777-7777-7777-7777-777777777701', 'Seguro Demo ClinicSystem', '+5110000000', 'contacto@segurodemo.local', 'Direccion demo', 'Contacto Demo', TRUE)
ON CONFLICT (name) DO UPDATE
SET phone = EXCLUDED.phone,
    email = EXCLUDED.email,
    address = EXCLUDED.address,
    contact_name = EXCLUDED.contact_name,
    is_active = EXCLUDED.is_active,
    updated_at = CURRENT_TIMESTAMP;

INSERT INTO pharmacy.medications (
    medication_id,
    name,
    generic_name,
    presentation,
    concentration,
    laboratory,
    requires_prescription,
    stock,
    unit_price,
    is_active
)
SELECT
    '88888888-8888-8888-8888-888888888801',
    'Paracetamol Demo 500mg',
    'Paracetamol',
    'Tableta',
    '500mg',
    'Laboratorio Demo',
    FALSE,
    100,
    2.50,
    TRUE
WHERE NOT EXISTS (
    SELECT 1
    FROM pharmacy.medications
    WHERE medication_id = '88888888-8888-8888-8888-888888888801'
       OR name = 'Paracetamol Demo 500mg'
);

COMMIT;
