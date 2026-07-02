DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'clinical') THEN
        CREATE SCHEMA clinical;
    END IF;
END $EF$;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'auth') THEN
        CREATE SCHEMA auth;
    END IF;
END $EF$;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'billing') THEN
        CREATE SCHEMA billing;
    END IF;
END $EF$;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'laboratory') THEN
        CREATE SCHEMA laboratory;
    END IF;
END $EF$;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'pharmacy') THEN
        CREATE SCHEMA pharmacy;
    END IF;
END $EF$;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'notifications') THEN
        CREATE SCHEMA notifications;
    END IF;
END $EF$;


CREATE EXTENSION IF NOT EXISTS pgcrypto;


CREATE TABLE clinical.appointment_statuses (
    status_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    name character varying(30) NOT NULL,
    description text,
    CONSTRAINT appointment_statuses_pkey PRIMARY KEY (status_id)
);


CREATE TABLE clinical.departments (
    department_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    name character varying(100) NOT NULL,
    description text,
    is_active boolean DEFAULT TRUE,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    updated_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT departments_pkey PRIMARY KEY (department_id)
);


CREATE TABLE billing.insurance_companies (
    insurance_company_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    name character varying(150) NOT NULL,
    phone character varying(20),
    email character varying(150),
    address text,
    contact_name character varying(100),
    is_active boolean DEFAULT TRUE,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    updated_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT insurance_companies_pkey PRIMARY KEY (insurance_company_id)
);


CREATE TABLE notifications.notification_types (
    type_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    code character varying(50) NOT NULL,
    name character varying(100) NOT NULL,
    template_subject text,
    template_body text,
    is_active boolean DEFAULT TRUE,
    CONSTRAINT notification_types_pkey PRIMARY KEY (type_id)
);


CREATE TABLE auth.permissions (
    permission_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    resource character varying(100) NOT NULL,
    action character varying(50) NOT NULL,
    description text,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT permissions_pkey PRIMARY KEY (permission_id)
);


CREATE TABLE auth.roles (
    role_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    name character varying(50) NOT NULL,
    description text,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    updated_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT roles_pkey PRIMARY KEY (role_id)
);


CREATE TABLE clinical.specialties (
    specialty_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    department_id uuid NOT NULL,
    name character varying(100) NOT NULL,
    description text,
    is_active boolean DEFAULT TRUE,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    updated_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT specialties_pkey PRIMARY KEY (specialty_id),
    CONSTRAINT specialties_department_id_fkey FOREIGN KEY (department_id) REFERENCES clinical.departments (department_id)
);


CREATE TABLE auth.role_permissions (
    role_id uuid NOT NULL,
    permission_id uuid NOT NULL,
    CONSTRAINT role_permissions_pkey PRIMARY KEY (role_id, permission_id),
    CONSTRAINT role_permissions_permission_id_fkey FOREIGN KEY (permission_id) REFERENCES auth.permissions (permission_id) ON DELETE CASCADE,
    CONSTRAINT role_permissions_role_id_fkey FOREIGN KEY (role_id) REFERENCES auth.roles (role_id) ON DELETE CASCADE
);


CREATE TABLE auth.users (
    user_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    role_id uuid NOT NULL,
    username character varying(100) NOT NULL,
    email character varying(150) NOT NULL,
    password_hash text NOT NULL,
    first_name character varying(100) NOT NULL,
    last_name character varying(100) NOT NULL,
    phone character varying(20),
    is_active boolean DEFAULT TRUE,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    updated_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    deleted_at timestamp without time zone,
    created_by uuid,
    updated_by uuid,
    CONSTRAINT users_pkey PRIMARY KEY (user_id),
    CONSTRAINT users_created_by_fkey FOREIGN KEY (created_by) REFERENCES auth.users (user_id),
    CONSTRAINT users_role_id_fkey FOREIGN KEY (role_id) REFERENCES auth.roles (role_id),
    CONSTRAINT users_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES auth.users (user_id)
);


CREATE TABLE auth.audit_logs (
    audit_log_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    user_id uuid,
    action character varying(100) NOT NULL,
    entity_name character varying(100) NOT NULL,
    entity_id uuid,
    old_values jsonb,
    new_values jsonb,
    ip_address inet,
    user_agent text,
    correlation_id uuid,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT audit_logs_pkey PRIMARY KEY (audit_log_id),
    CONSTRAINT audit_logs_user_id_fkey FOREIGN KEY (user_id) REFERENCES auth.users (user_id) ON DELETE SET NULL
);


CREATE TABLE clinical.doctors (
    doctor_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    user_id uuid NOT NULL,
    specialty_id uuid NOT NULL,
    license_number character varying(50) NOT NULL,
    years_experience integer,
    consultation_fee numeric(10,2),
    office character varying(100),
    is_active boolean DEFAULT TRUE,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    updated_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    deleted_at timestamp without time zone,
    created_by uuid,
    updated_by uuid,
    CONSTRAINT doctors_pkey PRIMARY KEY (doctor_id),
    CONSTRAINT doctors_created_by_fkey FOREIGN KEY (created_by) REFERENCES auth.users (user_id),
    CONSTRAINT doctors_specialty_id_fkey FOREIGN KEY (specialty_id) REFERENCES clinical.specialties (specialty_id),
    CONSTRAINT doctors_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES auth.users (user_id),
    CONSTRAINT doctors_user_id_fkey FOREIGN KEY (user_id) REFERENCES auth.users (user_id) ON DELETE RESTRICT
);


CREATE TABLE pharmacy.medications (
    medication_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    name character varying(150) NOT NULL,
    generic_name character varying(150),
    presentation character varying(100),
    concentration character varying(50),
    laboratory character varying(100),
    requires_prescription boolean DEFAULT TRUE,
    stock integer DEFAULT 0,
    unit_price numeric(10,2),
    is_active boolean DEFAULT TRUE,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    updated_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    deleted_at timestamp without time zone,
    created_by uuid,
    updated_by uuid,
    CONSTRAINT medications_pkey PRIMARY KEY (medication_id),
    CONSTRAINT medications_created_by_fkey FOREIGN KEY (created_by) REFERENCES auth.users (user_id),
    CONSTRAINT medications_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES auth.users (user_id)
);


CREATE TABLE notifications.notifications (
    notification_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    user_id uuid NOT NULL,
    type_id uuid NOT NULL,
    channel character varying(20) NOT NULL,
    status character varying(20) DEFAULT ('pending'::character varying),
    entity_type character varying(50),
    entity_id uuid,
    subject text,
    body text,
    scheduled_at timestamp without time zone,
    sent_at timestamp without time zone,
    read_at timestamp without time zone,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT notifications_pkey PRIMARY KEY (notification_id),
    CONSTRAINT notifications_type_id_fkey FOREIGN KEY (type_id) REFERENCES notifications.notification_types (type_id),
    CONSTRAINT notifications_user_id_fkey FOREIGN KEY (user_id) REFERENCES auth.users (user_id) ON DELETE CASCADE
);


CREATE TABLE clinical.patients (
    patient_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    user_id uuid,
    document_number character varying(20) NOT NULL,
    first_name character varying(100) NOT NULL,
    last_name character varying(100) NOT NULL,
    birth_date date,
    gender character varying(20),
    blood_type character varying(5),
    phone character varying(20),
    email character varying(150),
    address text,
    emergency_contact_name character varying(100),
    emergency_contact_phone character varying(20),
    is_active boolean DEFAULT TRUE,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    updated_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    deleted_at timestamp without time zone,
    created_by uuid,
    updated_by uuid,
    CONSTRAINT patients_pkey PRIMARY KEY (patient_id),
    CONSTRAINT patients_created_by_fkey FOREIGN KEY (created_by) REFERENCES auth.users (user_id),
    CONSTRAINT patients_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES auth.users (user_id),
    CONSTRAINT patients_user_id_fkey FOREIGN KEY (user_id) REFERENCES auth.users (user_id) ON DELETE SET NULL
);


CREATE TABLE auth.refresh_tokens (
    refresh_token_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    user_id uuid NOT NULL,
    token_hash text NOT NULL,
    expires_at timestamp without time zone NOT NULL,
    revoked_at timestamp without time zone,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT refresh_tokens_pkey PRIMARY KEY (refresh_token_id),
    CONSTRAINT refresh_tokens_user_id_fkey FOREIGN KEY (user_id) REFERENCES auth.users (user_id) ON DELETE CASCADE
);


CREATE TABLE pharmacy.stock_movements (
    movement_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    medication_id uuid NOT NULL,
    movement_type character varying(30) NOT NULL,
    quantity integer NOT NULL,
    reference_id uuid,
    notes text,
    performed_by uuid,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT stock_movements_pkey PRIMARY KEY (movement_id),
    CONSTRAINT stock_movements_medication_id_fkey FOREIGN KEY (medication_id) REFERENCES pharmacy.medications (medication_id),
    CONSTRAINT stock_movements_performed_by_fkey FOREIGN KEY (performed_by) REFERENCES auth.users (user_id)
);


CREATE TABLE clinical.appointments (
    appointment_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    patient_id uuid NOT NULL,
    doctor_id uuid NOT NULL,
    status_id uuid NOT NULL,
    appointment_date timestamp without time zone NOT NULL,
    duration_minutes integer DEFAULT 30,
    reason text,
    notes text,
    cancellation_reason text,
    rescheduled_from uuid,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    updated_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    deleted_at timestamp without time zone,
    created_by uuid,
    updated_by uuid,
    CONSTRAINT appointments_pkey PRIMARY KEY (appointment_id),
    CONSTRAINT appointments_created_by_fkey FOREIGN KEY (created_by) REFERENCES auth.users (user_id),
    CONSTRAINT appointments_doctor_id_fkey FOREIGN KEY (doctor_id) REFERENCES clinical.doctors (doctor_id),
    CONSTRAINT appointments_patient_id_fkey FOREIGN KEY (patient_id) REFERENCES clinical.patients (patient_id),
    CONSTRAINT appointments_rescheduled_from_fkey FOREIGN KEY (rescheduled_from) REFERENCES clinical.appointments (appointment_id) ON DELETE SET NULL,
    CONSTRAINT appointments_status_id_fkey FOREIGN KEY (status_id) REFERENCES clinical.appointment_statuses (status_id),
    CONSTRAINT appointments_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES auth.users (user_id)
);


CREATE TABLE billing.insurance_policies (
    insurance_policy_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    patient_id uuid NOT NULL,
    insurance_company_id uuid NOT NULL,
    policy_number character varying(100) NOT NULL,
    coverage_percentage numeric(5,2),
    max_coverage_amount numeric(12,2),
    start_date date NOT NULL,
    end_date date,
    is_active boolean DEFAULT TRUE,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    updated_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    deleted_at timestamp without time zone,
    created_by uuid,
    updated_by uuid,
    CONSTRAINT insurance_policies_pkey PRIMARY KEY (insurance_policy_id),
    CONSTRAINT insurance_policies_created_by_fkey FOREIGN KEY (created_by) REFERENCES auth.users (user_id),
    CONSTRAINT insurance_policies_insurance_company_id_fkey FOREIGN KEY (insurance_company_id) REFERENCES billing.insurance_companies (insurance_company_id),
    CONSTRAINT insurance_policies_patient_id_fkey FOREIGN KEY (patient_id) REFERENCES clinical.patients (patient_id),
    CONSTRAINT insurance_policies_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES auth.users (user_id)
);


CREATE TABLE clinical.medical_records (
    medical_record_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    patient_id uuid NOT NULL,
    doctor_id uuid NOT NULL,
    appointment_id uuid,
    chief_complaint text,
    diagnosis text,
    treatment text,
    observations text,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    updated_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    deleted_at timestamp without time zone,
    created_by uuid,
    updated_by uuid,
    CONSTRAINT medical_records_pkey PRIMARY KEY (medical_record_id),
    CONSTRAINT medical_records_appointment_id_fkey FOREIGN KEY (appointment_id) REFERENCES clinical.appointments (appointment_id) ON DELETE SET NULL,
    CONSTRAINT medical_records_created_by_fkey FOREIGN KEY (created_by) REFERENCES auth.users (user_id),
    CONSTRAINT medical_records_doctor_id_fkey FOREIGN KEY (doctor_id) REFERENCES clinical.doctors (doctor_id),
    CONSTRAINT medical_records_patient_id_fkey FOREIGN KEY (patient_id) REFERENCES clinical.patients (patient_id),
    CONSTRAINT medical_records_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES auth.users (user_id)
);


CREATE TABLE billing.billing (
    billing_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    patient_id uuid NOT NULL,
    appointment_id uuid,
    insurance_policy_id uuid,
    issue_date timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    subtotal numeric(12,2) NOT NULL,
    discount numeric(12,2) DEFAULT (0),
    insurance_coverage numeric(12,2) DEFAULT (0),
    total_amount numeric(12,2) GENERATED ALWAYS AS (((subtotal - discount) - insurance_coverage)) STORED,
    status character varying(30) NOT NULL,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    updated_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    deleted_at timestamp without time zone,
    created_by uuid,
    updated_by uuid,
    CONSTRAINT billing_pkey PRIMARY KEY (billing_id),
    CONSTRAINT billing_appointment_id_fkey FOREIGN KEY (appointment_id) REFERENCES clinical.appointments (appointment_id) ON DELETE SET NULL,
    CONSTRAINT billing_created_by_fkey FOREIGN KEY (created_by) REFERENCES auth.users (user_id),
    CONSTRAINT billing_insurance_policy_id_fkey FOREIGN KEY (insurance_policy_id) REFERENCES billing.insurance_policies (insurance_policy_id),
    CONSTRAINT billing_patient_id_fkey FOREIGN KEY (patient_id) REFERENCES clinical.patients (patient_id),
    CONSTRAINT billing_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES auth.users (user_id)
);


CREATE TABLE clinical.diagnoses (
    diagnosis_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    medical_record_id uuid NOT NULL,
    cie10_code character varying(10) NOT NULL,
    description text,
    is_primary boolean DEFAULT FALSE,
    noted_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT diagnoses_pkey PRIMARY KEY (diagnosis_id),
    CONSTRAINT diagnoses_medical_record_id_fkey FOREIGN KEY (medical_record_id) REFERENCES clinical.medical_records (medical_record_id) ON DELETE CASCADE
);


CREATE TABLE laboratory.laboratory_tests (
    laboratory_test_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    patient_id uuid NOT NULL,
    doctor_id uuid NOT NULL,
    medical_record_id uuid,
    test_name character varying(150) NOT NULL,
    status character varying(30) NOT NULL,
    requested_date timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    sample_taken_date timestamp without time zone,
    completed_date timestamp without time zone,
    observations text,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    updated_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    deleted_at timestamp without time zone,
    created_by uuid,
    updated_by uuid,
    CONSTRAINT laboratory_tests_pkey PRIMARY KEY (laboratory_test_id),
    CONSTRAINT laboratory_tests_created_by_fkey FOREIGN KEY (created_by) REFERENCES auth.users (user_id),
    CONSTRAINT laboratory_tests_doctor_id_fkey FOREIGN KEY (doctor_id) REFERENCES clinical.doctors (doctor_id),
    CONSTRAINT laboratory_tests_medical_record_id_fkey FOREIGN KEY (medical_record_id) REFERENCES clinical.medical_records (medical_record_id) ON DELETE SET NULL,
    CONSTRAINT laboratory_tests_patient_id_fkey FOREIGN KEY (patient_id) REFERENCES clinical.patients (patient_id),
    CONSTRAINT laboratory_tests_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES auth.users (user_id)
);


CREATE TABLE pharmacy.prescriptions (
    prescription_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    medical_record_id uuid NOT NULL,
    doctor_id uuid NOT NULL,
    patient_id uuid NOT NULL,
    valid_until date,
    dispensed_at timestamp without time zone,
    dispensed_by uuid,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    updated_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    deleted_at timestamp without time zone,
    created_by uuid,
    updated_by uuid,
    CONSTRAINT prescriptions_pkey PRIMARY KEY (prescription_id),
    CONSTRAINT prescriptions_created_by_fkey FOREIGN KEY (created_by) REFERENCES auth.users (user_id),
    CONSTRAINT prescriptions_dispensed_by_fkey FOREIGN KEY (dispensed_by) REFERENCES auth.users (user_id),
    CONSTRAINT prescriptions_doctor_id_fkey FOREIGN KEY (doctor_id) REFERENCES clinical.doctors (doctor_id),
    CONSTRAINT prescriptions_medical_record_id_fkey FOREIGN KEY (medical_record_id) REFERENCES clinical.medical_records (medical_record_id) ON DELETE CASCADE,
    CONSTRAINT prescriptions_patient_id_fkey FOREIGN KEY (patient_id) REFERENCES clinical.patients (patient_id),
    CONSTRAINT prescriptions_updated_by_fkey FOREIGN KEY (updated_by) REFERENCES auth.users (user_id)
);


CREATE TABLE clinical.treatments (
    treatment_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    medical_record_id uuid NOT NULL,
    description text NOT NULL,
    start_date date,
    end_date date,
    status character varying(30) DEFAULT ('active'::character varying),
    notes text,
    CONSTRAINT treatments_pkey PRIMARY KEY (treatment_id),
    CONSTRAINT treatments_medical_record_id_fkey FOREIGN KEY (medical_record_id) REFERENCES clinical.medical_records (medical_record_id) ON DELETE CASCADE
);


CREATE TABLE clinical.vital_signs (
    vital_sign_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    medical_record_id uuid NOT NULL,
    recorded_by uuid,
    systolic_bp integer,
    diastolic_bp integer,
    heart_rate integer,
    temperature numeric(4,1),
    respiratory_rate integer,
    weight_kg numeric(5,2),
    height_cm numeric(5,2),
    spo2 integer,
    recorded_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT vital_signs_pkey PRIMARY KEY (vital_sign_id),
    CONSTRAINT vital_signs_medical_record_id_fkey FOREIGN KEY (medical_record_id) REFERENCES clinical.medical_records (medical_record_id) ON DELETE CASCADE,
    CONSTRAINT vital_signs_recorded_by_fkey FOREIGN KEY (recorded_by) REFERENCES auth.users (user_id)
);


CREATE TABLE billing.billing_details (
    billing_detail_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    billing_id uuid NOT NULL,
    description text NOT NULL,
    quantity integer NOT NULL,
    unit_price numeric(12,2) NOT NULL,
    amount numeric(12,2) GENERATED ALWAYS AS (((quantity)::numeric * unit_price)) STORED,
    CONSTRAINT billing_details_pkey PRIMARY KEY (billing_detail_id),
    CONSTRAINT billing_details_billing_id_fkey FOREIGN KEY (billing_id) REFERENCES billing.billing (billing_id) ON DELETE CASCADE
);


CREATE TABLE billing.payments (
    payment_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    billing_id uuid NOT NULL,
    insurance_policy_id uuid,
    amount numeric(12,2) NOT NULL,
    payment_method character varying(50) NOT NULL,
    reference_number character varying(100),
    payment_date timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    status character varying(30) DEFAULT ('pending'::character varying),
    registered_by uuid,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT payments_pkey PRIMARY KEY (payment_id),
    CONSTRAINT payments_billing_id_fkey FOREIGN KEY (billing_id) REFERENCES billing.billing (billing_id),
    CONSTRAINT payments_insurance_policy_id_fkey FOREIGN KEY (insurance_policy_id) REFERENCES billing.insurance_policies (insurance_policy_id),
    CONSTRAINT payments_registered_by_fkey FOREIGN KEY (registered_by) REFERENCES auth.users (user_id)
);


CREATE TABLE laboratory.laboratory_results (
    result_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    laboratory_test_id uuid NOT NULL,
    parameter_name character varying(100) NOT NULL,
    result_value text,
    unit character varying(50),
    reference_range character varying(100),
    is_abnormal boolean DEFAULT FALSE,
    noted_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT laboratory_results_pkey PRIMARY KEY (result_id),
    CONSTRAINT laboratory_results_laboratory_test_id_fkey FOREIGN KEY (laboratory_test_id) REFERENCES laboratory.laboratory_tests (laboratory_test_id) ON DELETE CASCADE
);


CREATE TABLE pharmacy.prescription_details (
    prescription_detail_id uuid NOT NULL DEFAULT (gen_random_uuid()),
    prescription_id uuid NOT NULL,
    medication_id uuid NOT NULL,
    dosage character varying(100),
    frequency character varying(100),
    duration_days integer,
    quantity_prescribed integer NOT NULL,
    instructions text,
    is_substitutable boolean DEFAULT TRUE,
    CONSTRAINT prescription_details_pkey PRIMARY KEY (prescription_detail_id),
    CONSTRAINT prescription_details_medication_id_fkey FOREIGN KEY (medication_id) REFERENCES pharmacy.medications (medication_id),
    CONSTRAINT prescription_details_prescription_id_fkey FOREIGN KEY (prescription_id) REFERENCES pharmacy.prescriptions (prescription_id) ON DELETE CASCADE
);


CREATE UNIQUE INDEX appointment_statuses_name_key ON clinical.appointment_statuses (name);


CREATE INDEX idx_appointments_date ON clinical.appointments (appointment_date);


CREATE INDEX idx_appointments_doctor ON clinical.appointments (doctor_id);


CREATE INDEX idx_appointments_patient ON clinical.appointments (patient_id);


CREATE INDEX "IX_appointments_created_by" ON clinical.appointments (created_by);


CREATE INDEX "IX_appointments_rescheduled_from" ON clinical.appointments (rescheduled_from);


CREATE INDEX "IX_appointments_status_id" ON clinical.appointments (status_id);


CREATE INDEX "IX_appointments_updated_by" ON clinical.appointments (updated_by);


CREATE UNIQUE INDEX uix_appointment_slot ON clinical.appointments (doctor_id, appointment_date) WHERE (deleted_at IS NULL);


CREATE INDEX idx_audit_created ON auth.audit_logs (created_at);


CREATE INDEX idx_audit_entity ON auth.audit_logs (entity_name, entity_id);


CREATE INDEX idx_audit_user ON auth.audit_logs (user_id);


CREATE INDEX idx_billing_issue_date ON billing.billing (issue_date);


CREATE INDEX idx_billing_patient ON billing.billing (patient_id);


CREATE INDEX idx_billing_status ON billing.billing (status);


CREATE INDEX "IX_billing_appointment_id" ON billing.billing (appointment_id);


CREATE INDEX "IX_billing_created_by" ON billing.billing (created_by);


CREATE INDEX "IX_billing_insurance_policy_id" ON billing.billing (insurance_policy_id);


CREATE INDEX "IX_billing_updated_by" ON billing.billing (updated_by);


CREATE INDEX "IX_billing_details_billing_id" ON billing.billing_details (billing_id);


CREATE UNIQUE INDEX departments_name_key ON clinical.departments (name);


CREATE INDEX "IX_diagnoses_medical_record_id" ON clinical.diagnoses (medical_record_id);


CREATE UNIQUE INDEX doctors_license_number_key ON clinical.doctors (license_number);


CREATE UNIQUE INDEX doctors_user_id_key ON clinical.doctors (user_id);


CREATE INDEX "IX_doctors_created_by" ON clinical.doctors (created_by);


CREATE INDEX "IX_doctors_specialty_id" ON clinical.doctors (specialty_id);


CREATE INDEX "IX_doctors_updated_by" ON clinical.doctors (updated_by);


CREATE UNIQUE INDEX insurance_companies_name_key ON billing.insurance_companies (name);


CREATE UNIQUE INDEX insurance_policies_policy_number_key ON billing.insurance_policies (policy_number);


CREATE INDEX "IX_insurance_policies_created_by" ON billing.insurance_policies (created_by);


CREATE INDEX "IX_insurance_policies_insurance_company_id" ON billing.insurance_policies (insurance_company_id);


CREATE INDEX "IX_insurance_policies_patient_id" ON billing.insurance_policies (patient_id);


CREATE INDEX "IX_insurance_policies_updated_by" ON billing.insurance_policies (updated_by);


CREATE INDEX "IX_laboratory_results_laboratory_test_id" ON laboratory.laboratory_results (laboratory_test_id);


CREATE INDEX idx_lab_tests_doctor ON laboratory.laboratory_tests (doctor_id);


CREATE INDEX idx_lab_tests_patient ON laboratory.laboratory_tests (patient_id);


CREATE INDEX idx_lab_tests_status ON laboratory.laboratory_tests (status);


CREATE INDEX "IX_laboratory_tests_created_by" ON laboratory.laboratory_tests (created_by);


CREATE INDEX "IX_laboratory_tests_medical_record_id" ON laboratory.laboratory_tests (medical_record_id);


CREATE INDEX "IX_laboratory_tests_updated_by" ON laboratory.laboratory_tests (updated_by);


CREATE INDEX idx_medical_records_doctor ON clinical.medical_records (doctor_id);


CREATE INDEX idx_medical_records_patient ON clinical.medical_records (patient_id);


CREATE INDEX "IX_medical_records_appointment_id" ON clinical.medical_records (appointment_id);


CREATE INDEX "IX_medical_records_created_by" ON clinical.medical_records (created_by);


CREATE INDEX "IX_medical_records_updated_by" ON clinical.medical_records (updated_by);


CREATE INDEX "IX_medications_created_by" ON pharmacy.medications (created_by);


CREATE INDEX "IX_medications_updated_by" ON pharmacy.medications (updated_by);


CREATE UNIQUE INDEX notification_types_code_key ON notifications.notification_types (code);


CREATE INDEX idx_notifications_status ON notifications.notifications (status);


CREATE INDEX idx_notifications_user ON notifications.notifications (user_id);


CREATE INDEX "IX_notifications_type_id" ON notifications.notifications (type_id);


CREATE INDEX "IX_patients_created_by" ON clinical.patients (created_by);


CREATE INDEX "IX_patients_updated_by" ON clinical.patients (updated_by);


CREATE UNIQUE INDEX patients_document_number_key ON clinical.patients (document_number);


CREATE UNIQUE INDEX patients_user_id_key ON clinical.patients (user_id);


CREATE INDEX idx_payments_billing ON billing.payments (billing_id);


CREATE INDEX "IX_payments_insurance_policy_id" ON billing.payments (insurance_policy_id);


CREATE INDEX "IX_payments_registered_by" ON billing.payments (registered_by);


CREATE UNIQUE INDEX permissions_resource_action_key ON auth.permissions (resource, action);


CREATE INDEX "IX_prescription_details_medication_id" ON pharmacy.prescription_details (medication_id);


CREATE INDEX "IX_prescription_details_prescription_id" ON pharmacy.prescription_details (prescription_id);


CREATE INDEX idx_prescriptions_medical_record ON pharmacy.prescriptions (medical_record_id);


CREATE INDEX idx_prescriptions_patient ON pharmacy.prescriptions (patient_id);


CREATE INDEX "IX_prescriptions_created_by" ON pharmacy.prescriptions (created_by);


CREATE INDEX "IX_prescriptions_dispensed_by" ON pharmacy.prescriptions (dispensed_by);


CREATE INDEX "IX_prescriptions_doctor_id" ON pharmacy.prescriptions (doctor_id);


CREATE INDEX "IX_prescriptions_updated_by" ON pharmacy.prescriptions (updated_by);


CREATE INDEX "IX_refresh_tokens_user_id" ON auth.refresh_tokens (user_id);


CREATE INDEX "IX_role_permissions_permission_id" ON auth.role_permissions (permission_id);


CREATE UNIQUE INDEX roles_name_key ON auth.roles (name);


CREATE INDEX "IX_specialties_department_id" ON clinical.specialties (department_id);


CREATE UNIQUE INDEX specialties_name_key ON clinical.specialties (name);


CREATE INDEX idx_stock_movements_medication ON pharmacy.stock_movements (medication_id);


CREATE INDEX "IX_stock_movements_performed_by" ON pharmacy.stock_movements (performed_by);


CREATE INDEX "IX_treatments_medical_record_id" ON clinical.treatments (medical_record_id);


CREATE INDEX "IX_users_created_by" ON auth.users (created_by);


CREATE INDEX "IX_users_role_id" ON auth.users (role_id);


CREATE INDEX "IX_users_updated_by" ON auth.users (updated_by);


CREATE UNIQUE INDEX users_email_key ON auth.users (email);


CREATE UNIQUE INDEX users_username_key ON auth.users (username);


CREATE INDEX "IX_vital_signs_medical_record_id" ON clinical.vital_signs (medical_record_id);


CREATE INDEX "IX_vital_signs_recorded_by" ON clinical.vital_signs (recorded_by);


