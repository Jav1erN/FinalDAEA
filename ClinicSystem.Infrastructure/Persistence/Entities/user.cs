using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class user
{
    public Guid user_id { get; set; }

    public Guid role_id { get; set; }

    public string username { get; set; } = null!;

    public string email { get; set; } = null!;

    public string password_hash { get; set; } = null!;

    public string first_name { get; set; } = null!;

    public string last_name { get; set; } = null!;

    public string? phone { get; set; }

    public bool? is_active { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public DateTime? deleted_at { get; set; }

    public Guid? created_by { get; set; }

    public Guid? updated_by { get; set; }

    public virtual ICollection<user> Inversecreated_byNavigation { get; set; } = new List<user>();

    public virtual ICollection<user> Inverseupdated_byNavigation { get; set; } = new List<user>();

    public virtual ICollection<appointment> appointmentcreated_byNavigations { get; set; } = new List<appointment>();

    public virtual ICollection<appointment> appointmentupdated_byNavigations { get; set; } = new List<appointment>();

    public virtual ICollection<audit_log> audit_logs { get; set; } = new List<audit_log>();

    public virtual ICollection<billing> billingcreated_byNavigations { get; set; } = new List<billing>();

    public virtual ICollection<billing> billingupdated_byNavigations { get; set; } = new List<billing>();

    public virtual user? created_byNavigation { get; set; }

    public virtual ICollection<doctor> doctorcreated_byNavigations { get; set; } = new List<doctor>();

    public virtual ICollection<doctor> doctorupdated_byNavigations { get; set; } = new List<doctor>();

    public virtual doctor? doctoruser { get; set; }

    public virtual ICollection<insurance_policy> insurance_policycreated_byNavigations { get; set; } = new List<insurance_policy>();

    public virtual ICollection<insurance_policy> insurance_policyupdated_byNavigations { get; set; } = new List<insurance_policy>();

    public virtual ICollection<laboratory_test> laboratory_testcreated_byNavigations { get; set; } = new List<laboratory_test>();

    public virtual ICollection<laboratory_test> laboratory_testupdated_byNavigations { get; set; } = new List<laboratory_test>();

    public virtual ICollection<medical_record> medical_recordcreated_byNavigations { get; set; } = new List<medical_record>();

    public virtual ICollection<medical_record> medical_recordupdated_byNavigations { get; set; } = new List<medical_record>();

    public virtual ICollection<medication> medicationcreated_byNavigations { get; set; } = new List<medication>();

    public virtual ICollection<medication> medicationupdated_byNavigations { get; set; } = new List<medication>();

    public virtual ICollection<notification> notifications { get; set; } = new List<notification>();

    public virtual ICollection<patient> patientcreated_byNavigations { get; set; } = new List<patient>();

    public virtual ICollection<patient> patientupdated_byNavigations { get; set; } = new List<patient>();

    public virtual patient? patientuser { get; set; }

    public virtual ICollection<payment> payments { get; set; } = new List<payment>();

    public virtual ICollection<prescription> prescriptioncreated_byNavigations { get; set; } = new List<prescription>();

    public virtual ICollection<prescription> prescriptiondispensed_byNavigations { get; set; } = new List<prescription>();

    public virtual ICollection<prescription> prescriptionupdated_byNavigations { get; set; } = new List<prescription>();

    public virtual ICollection<refresh_token> refresh_tokens { get; set; } = new List<refresh_token>();

    public virtual role role { get; set; } = null!;

    public virtual ICollection<stock_movement> stock_movements { get; set; } = new List<stock_movement>();

    public virtual user? updated_byNavigation { get; set; }

    public virtual ICollection<vital_sign> vital_signs { get; set; } = new List<vital_sign>();
}
