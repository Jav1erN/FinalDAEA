param(
    [string]$ApiBaseUrl = $env:API_BASE_URL
)

$ErrorActionPreference = "Stop"

if ([string]::IsNullOrWhiteSpace($ApiBaseUrl)) {
    $ApiBaseUrl = "https://finaldaea.onrender.com"
}

$ApiBaseUrl = $ApiBaseUrl.TrimEnd("/")
$Suffix = (Get-Date -Format "yyyyMMddHHmmss")
$Results = New-Object System.Collections.Generic.List[object]
$Ids = @{}

function Add-Result {
    param(
        [string]$Name,
        [string]$Method,
        [string]$Path,
        [int]$Expected,
        [int]$Actual,
        [string]$Status
    )

    $Results.Add([pscustomobject]@{
        Name = $Name
        Method = $Method
        Path = $Path
        Expected = $Expected
        Actual = $Actual
        Status = $Status
    }) | Out-Null
}

function Invoke-ClinicApi {
    param(
        [string]$Name,
        [string]$Method,
        [string]$Path,
        [object]$Body = $null,
        [int[]]$ExpectedStatus = @(200)
    )

    $uri = "$ApiBaseUrl$Path"
    $headers = @{ Accept = "application/json" }

    try {
        if ($null -eq $Body) {
            $response = Invoke-WebRequest -Uri $uri -Method $Method -Headers $headers -UseBasicParsing -TimeoutSec 60
        }
        else {
            $json = $Body | ConvertTo-Json -Depth 20
            $response = Invoke-WebRequest -Uri $uri -Method $Method -Headers $headers -ContentType "application/json" -Body $json -UseBasicParsing -TimeoutSec 60
        }

        if ($ExpectedStatus -notcontains [int]$response.StatusCode) {
            throw "Expected HTTP $($ExpectedStatus -join ',') but got HTTP $($response.StatusCode). Body: $($response.Content)"
        }

        Add-Result -Name $Name -Method $Method -Path $Path -Expected ($ExpectedStatus[0]) -Actual ([int]$response.StatusCode) -Status "PASS"

        if ([string]::IsNullOrWhiteSpace($response.Content)) {
            return $null
        }

        try {
            return $response.Content | ConvertFrom-Json
        }
        catch {
            return $response.Content
        }
    }
    catch {
        $statusCode = 0
        $body = ""

        if ($_.Exception.Response) {
            $statusCode = [int]$_.Exception.Response.StatusCode
            try {
                $reader = New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream())
                $body = $reader.ReadToEnd()
            }
            catch {
                $body = ""
            }
        }

        Add-Result -Name $Name -Method $Method -Path $Path -Expected ($ExpectedStatus[0]) -Actual $statusCode -Status "FAIL"
        throw "$Name failed. $Method $Path. $($_.Exception.Message) $body"
    }
}

function Require-Id {
    param(
        [object]$Response,
        [string]$PropertyName
    )

    if ($null -eq $Response -or $null -eq $Response.$PropertyName -or [string]::IsNullOrWhiteSpace([string]$Response.$PropertyName)) {
        throw "Response does not contain required id property '$PropertyName'. Response: $($Response | ConvertTo-Json -Depth 10)"
    }

    return [string]$Response.$PropertyName
}

function New-DbTimestamp {
    return (Get-Date).ToString("yyyy-MM-ddTHH:mm:ss")
}

function New-DbTimestampAfterMinutes {
    param([int]$Minutes)
    return (Get-Date).AddMinutes($Minutes).ToString("yyyy-MM-ddTHH:mm:ss")
}

function New-DbTimestampAfterDays {
    param([int]$Days)
    return (Get-Date).AddDays($Days).ToString("yyyy-MM-ddTHH:mm:ss")
}

Write-Host "ClinicSystem Render validation"
Write-Host "Base URL: $ApiBaseUrl"
Write-Host "Run suffix: $Suffix"

Invoke-ClinicApi -Name "Health" -Method "GET" -Path "/health" | Out-Null
$swagger = Invoke-WebRequest -Uri "$ApiBaseUrl/swagger/index.html" -Method GET -UseBasicParsing -TimeoutSec 60
if ($swagger.StatusCode -ne 200 -or -not $swagger.Content.Contains("Swagger")) {
    throw "Swagger UI is not available at $ApiBaseUrl/swagger/index.html"
}
Add-Result -Name "Swagger UI" -Method "GET" -Path "/swagger/index.html" -Expected 200 -Actual ([int]$swagger.StatusCode) -Status "PASS"

$simpleGets = @(
    "/api/roles",
    "/api/permissions",
    "/api/departments",
    "/api/specialtys",
    "/api/appointment-statuses",
    "/api/notification-types",
    "/api/patients",
    "/api/doctors",
    "/api/appointments",
    "/api/medical-records",
    "/api/medications",
    "/api/billings"
)

foreach ($path in $simpleGets) {
    Invoke-ClinicApi -Name "GET $path" -Method "GET" -Path $path | Out-Null
}

$role = Invoke-ClinicApi -Name "Create role" -Method "POST" -Path "/api/roles" -Body @{
    name = "RenderValidatorRole-$Suffix"
    description = "Role created by Render validation $Suffix"
}
$Ids.RoleId = Require-Id $role "roleId"

$permission = Invoke-ClinicApi -Name "Create permission" -Method "POST" -Path "/api/permissions" -Body @{
    resource = "render-validation-$Suffix"
    action = "execute"
    description = "Permission created by Render validation $Suffix"
}
$Ids.PermissionId = Require-Id $permission "permissionId"

$department = Invoke-ClinicApi -Name "Create department" -Method "POST" -Path "/api/departments" -Body @{
    name = "Render Validation Dept $Suffix"
    description = "Department created by validation"
    isActive = $true
}
$Ids.DepartmentId = Require-Id $department "departmentId"

$specialty = Invoke-ClinicApi -Name "Create specialty" -Method "POST" -Path "/api/specialtys" -Body @{
    departmentId = $Ids.DepartmentId
    name = "Render Validation Specialty $Suffix"
    description = "Specialty created by validation"
    isActive = $true
}
$Ids.SpecialtyId = Require-Id $specialty "specialtyId"

$appointmentStatus = Invoke-ClinicApi -Name "Create appointment status" -Method "POST" -Path "/api/appointment-statuses" -Body @{
    name = "RenderStatus-$Suffix"
    description = "Status created by validation"
}
$Ids.StatusId = Require-Id $appointmentStatus "statusId"

$notificationType = Invoke-ClinicApi -Name "Create notification type" -Method "POST" -Path "/api/notification-types" -Body @{
    code = "RENDER_VALIDATION_$Suffix"
    name = "Render validation notification $Suffix"
    templateSubject = "Validation"
    templateBody = "Validation body"
    isActive = $true
}
$Ids.NotificationTypeId = Require-Id $notificationType "typeId"

$insuranceCompany = Invoke-ClinicApi -Name "Create insurance company" -Method "POST" -Path "/api/insurance-companys" -Body @{
    name = "Render Insurance $Suffix"
    phone = "+511$Suffix".Substring(0, 12)
    email = "insurance.$Suffix@example.com"
    address = "Render validation address"
    contactName = "Render Contact"
    isActive = $true
}
$Ids.InsuranceCompanyId = Require-Id $insuranceCompany "insuranceCompanyId"

$medication = Invoke-ClinicApi -Name "Create medication" -Method "POST" -Path "/api/medications" -Body @{
    name = "Render Medication $Suffix"
    genericName = "Generic $Suffix"
    presentation = "Tablet"
    concentration = "500mg"
    laboratory = "Render Lab"
    requiresPrescription = $false
    stock = 100
    unitPrice = 2.5
    isActive = $true
}
$Ids.MedicationId = Require-Id $medication "medicationId"

$user = Invoke-ClinicApi -Name "Create user" -Method "POST" -Path "/api/users" -Body @{
    roleId = $Ids.RoleId
    username = "render.user.$Suffix"
    email = "render.user.$Suffix@example.com"
    passwordHash = "validation-not-a-real-password-hash"
    firstName = "Render"
    lastName = "Validator"
    phone = "900000001"
    isActive = $true
}
$Ids.UserId = Require-Id $user "userId"

$patient = Invoke-ClinicApi -Name "Create patient" -Method "POST" -Path "/api/patients" -Body @{
    userId = $null
    documentNumber = "RV$Suffix"
    firstName = "Patient"
    lastName = "Render$Suffix"
    birthDate = "1990-01-01"
    gender = "M"
    bloodType = "O+"
    phone = "900000002"
    email = "patient.$Suffix@example.com"
    address = "Render validation"
    emergencyContactName = "Emergency Contact"
    emergencyContactPhone = "900000003"
    isActive = $true
    createdBy = $Ids.UserId
    updatedBy = $Ids.UserId
}
$Ids.PatientId = Require-Id $patient "patientId"

$doctor = Invoke-ClinicApi -Name "Create doctor" -Method "POST" -Path "/api/doctors" -Body @{
    userId = $Ids.UserId
    specialtyId = $Ids.SpecialtyId
    licenseNumber = "LIC-$Suffix"
    yearsExperience = 5
    consultationFee = 120.00
    office = "A-101"
    isActive = $true
    createdBy = $Ids.UserId
    updatedBy = $Ids.UserId
}
$Ids.DoctorId = Require-Id $doctor "doctorId"

$appointmentDate = (Get-Date).AddDays(21).Date.AddHours(10).AddMinutes([int]((Get-Date).Second % 6) * 10).ToString("yyyy-MM-ddTHH:mm:ss")
$appointment = Invoke-ClinicApi -Name "Create appointment" -Method "POST" -Path "/api/appointments" -Body @{
    patientId = $Ids.PatientId
    doctorId = $Ids.DoctorId
    statusId = $Ids.StatusId
    appointmentDate = $appointmentDate
    durationMinutes = 30
    reason = "Render validation appointment"
    notes = "Created by render validation"
    createdBy = $Ids.UserId
    updatedBy = $Ids.UserId
}
$Ids.AppointmentId = Require-Id $appointment "appointmentId"

$medicalRecord = Invoke-ClinicApi -Name "Create medical record" -Method "POST" -Path "/api/medical-records" -Body @{
    patientId = $Ids.PatientId
    doctorId = $Ids.DoctorId
    appointmentId = $Ids.AppointmentId
    chiefComplaint = "Render validation chief complaint"
    diagnosis = "Validation diagnosis"
    treatment = "Validation treatment"
    observations = "Validation observations"
    createdBy = $Ids.UserId
    updatedBy = $Ids.UserId
}
$Ids.MedicalRecordId = Require-Id $medicalRecord "medicalRecordId"

$diagnosis = Invoke-ClinicApi -Name "Create diagnosis" -Method "POST" -Path "/api/diagnoses" -Body @{
    medicalRecordId = $Ids.MedicalRecordId
    cie10Code = "Z00.0"
    description = "General examination validation"
    isPrimary = $true
    notedAt = New-DbTimestamp
}
$Ids.DiagnosisId = Require-Id $diagnosis "diagnosisId"

$treatment = Invoke-ClinicApi -Name "Create treatment" -Method "POST" -Path "/api/treatments" -Body @{
    medicalRecordId = $Ids.MedicalRecordId
    description = "Render validation treatment"
    startDate = (Get-Date).ToString("yyyy-MM-dd")
    endDate = (Get-Date).AddDays(7).ToString("yyyy-MM-dd")
    status = "Active"
    notes = "Validation notes"
}
$Ids.TreatmentId = Require-Id $treatment "treatmentId"

$vitalSign = Invoke-ClinicApi -Name "Create vital sign" -Method "POST" -Path "/api/vital-signs" -Body @{
    medicalRecordId = $Ids.MedicalRecordId
    recordedBy = $Ids.UserId
    systolicBp = 120
    diastolicBp = 80
    heartRate = 72
    temperature = 36.7
    respiratoryRate = 16
    weightKg = 70.5
    heightCm = 175.0
    spo2 = 98
    recordedAt = New-DbTimestamp
}
$Ids.VitalSignId = Require-Id $vitalSign "vitalSignId"

$labTest = Invoke-ClinicApi -Name "Create laboratory test" -Method "POST" -Path "/api/laboratory-tests" -Body @{
    patientId = $Ids.PatientId
    doctorId = $Ids.DoctorId
    medicalRecordId = $Ids.MedicalRecordId
    testName = "Hemogram validation $Suffix"
    status = "Requested"
    requestedDate = New-DbTimestamp
    observations = "Validation lab test"
    createdBy = $Ids.UserId
    updatedBy = $Ids.UserId
}
$Ids.LaboratoryTestId = Require-Id $labTest "laboratoryTestId"

$labResult = Invoke-ClinicApi -Name "Create laboratory result" -Method "POST" -Path "/api/laboratory-results" -Body @{
    laboratoryTestId = $Ids.LaboratoryTestId
    parameterName = "Hemoglobin"
    resultValue = "14.2"
    unit = "g/dL"
    referenceRange = "13.0-17.0"
    isAbnormal = $false
    notedAt = New-DbTimestamp
}
$Ids.LaboratoryResultId = Require-Id $labResult "resultId"

$stockMovement = Invoke-ClinicApi -Name "Create stock movement" -Method "POST" -Path "/api/stock-movements" -Body @{
    medicationId = $Ids.MedicationId
    movementType = "Entry"
    quantity = 10
    referenceId = $null
    notes = "Render validation stock entry"
    performedBy = $Ids.UserId
}
$Ids.StockMovementId = Require-Id $stockMovement "movementId"

$prescription = Invoke-ClinicApi -Name "Create prescription" -Method "POST" -Path "/api/prescriptions" -Body @{
    medicalRecordId = $Ids.MedicalRecordId
    doctorId = $Ids.DoctorId
    patientId = $Ids.PatientId
    validUntil = (Get-Date).AddDays(30).ToString("yyyy-MM-dd")
    createdBy = $Ids.UserId
    updatedBy = $Ids.UserId
}
$Ids.PrescriptionId = Require-Id $prescription "prescriptionId"

$prescriptionDetail = Invoke-ClinicApi -Name "Create prescription detail" -Method "POST" -Path "/api/prescription-details" -Body @{
    prescriptionId = $Ids.PrescriptionId
    medicationId = $Ids.MedicationId
    dosage = "1 tablet"
    frequency = "Every 8 hours"
    durationDays = 3
    quantityPrescribed = 9
    instructions = "Take with water"
    isSubstitutable = $true
}
$Ids.PrescriptionDetailId = Require-Id $prescriptionDetail "prescriptionDetailId"

$insurancePolicy = Invoke-ClinicApi -Name "Create insurance policy" -Method "POST" -Path "/api/insurance-policys" -Body @{
    patientId = $Ids.PatientId
    insuranceCompanyId = $Ids.InsuranceCompanyId
    policyNumber = "POL-$Suffix"
    coveragePercentage = 50.0
    maxCoverageAmount = 1000.0
    startDate = (Get-Date).ToString("yyyy-MM-dd")
    endDate = (Get-Date).AddYears(1).ToString("yyyy-MM-dd")
    isActive = $true
    createdBy = $Ids.UserId
    updatedBy = $Ids.UserId
}
$Ids.InsurancePolicyId = Require-Id $insurancePolicy "insurancePolicyId"

$billing = Invoke-ClinicApi -Name "Create billing" -Method "POST" -Path "/api/billings" -Body @{
    patientId = $Ids.PatientId
    appointmentId = $Ids.AppointmentId
    insurancePolicyId = $Ids.InsurancePolicyId
    issueDate = New-DbTimestamp
    subtotal = 200.0
    discount = 10.0
    insuranceCoverage = 50.0
    status = "Pending"
    createdBy = $Ids.UserId
    updatedBy = $Ids.UserId
}
$Ids.BillingId = Require-Id $billing "billingId"

$billingDetail = Invoke-ClinicApi -Name "Create billing detail" -Method "POST" -Path "/api/billing-details" -Body @{
    billingId = $Ids.BillingId
    description = "Render validation consultation"
    quantity = 1
    unitPrice = 200.0
}
$Ids.BillingDetailId = Require-Id $billingDetail "billingDetailId"

$payment = Invoke-ClinicApi -Name "Create payment" -Method "POST" -Path "/api/payments" -Body @{
    billingId = $Ids.BillingId
    insurancePolicyId = $Ids.InsurancePolicyId
    paymentMethod = "Card"
    referenceNumber = "PAY-$Suffix"
    paymentDate = New-DbTimestamp
    status = "Registered"
    registeredBy = $Ids.UserId
}
$Ids.PaymentId = Require-Id $payment "paymentId"

$notification = Invoke-ClinicApi -Name "Create notification" -Method "POST" -Path "/api/notifications" -Body @{
    userId = $Ids.UserId
    typeId = $Ids.NotificationTypeId
    channel = "Email"
    status = "Pending"
    entityType = "Appointment"
    entityId = $Ids.AppointmentId
    subject = "Render validation notification"
    body = "Notification created by render validation"
    scheduledAt = New-DbTimestampAfterMinutes 10
}
$Ids.NotificationId = Require-Id $notification "notificationId"

$refreshToken = Invoke-ClinicApi -Name "Create refresh token" -Method "POST" -Path "/api/refresh-tokens" -Body @{
    userId = $Ids.UserId
    tokenHash = "render-validation-token-$Suffix"
    expiresAt = New-DbTimestampAfterDays 7
}
$Ids.RefreshTokenId = Require-Id $refreshToken "refreshTokenId"

$getByIdChecks = @(
    @{ Name = "Get role by id"; Path = "/api/roles/$($Ids.RoleId)" },
    @{ Name = "Get permission by id"; Path = "/api/permissions/$($Ids.PermissionId)" },
    @{ Name = "Get department by id"; Path = "/api/departments/$($Ids.DepartmentId)" },
    @{ Name = "Get specialty by id"; Path = "/api/specialtys/$($Ids.SpecialtyId)" },
    @{ Name = "Get user by id"; Path = "/api/users/$($Ids.UserId)" },
    @{ Name = "Get patient by id"; Path = "/api/patients/$($Ids.PatientId)" },
    @{ Name = "Get doctor by id"; Path = "/api/doctors/$($Ids.DoctorId)" },
    @{ Name = "Get appointment by id"; Path = "/api/appointments/$($Ids.AppointmentId)" },
    @{ Name = "Get medical record by id"; Path = "/api/medical-records/$($Ids.MedicalRecordId)" },
    @{ Name = "Get billing by id"; Path = "/api/billings/$($Ids.BillingId)" },
    @{ Name = "Get medication by id"; Path = "/api/medications/$($Ids.MedicationId)" }
)

foreach ($check in $getByIdChecks) {
    Invoke-ClinicApi -Name $check.Name -Method "GET" -Path $check.Path | Out-Null
}

Invoke-ClinicApi -Name "Update department" -Method "PUT" -Path "/api/departments/$($Ids.DepartmentId)" -Body @{
    departmentId = $Ids.DepartmentId
    name = "Render Validation Dept $Suffix Updated"
    description = "Department updated by validation"
    isActive = $true
} | Out-Null

Invoke-ClinicApi -Name "Update patient" -Method "PUT" -Path "/api/patients/$($Ids.PatientId)" -Body @{
    patientId = $Ids.PatientId
    userId = $null
    documentNumber = "RV$Suffix"
    firstName = "PatientUpdated"
    lastName = "Render$Suffix"
    birthDate = "1990-01-01"
    gender = "M"
    bloodType = "O+"
    phone = "900000004"
    email = "patient.updated.$Suffix@example.com"
    address = "Render validation updated"
    emergencyContactName = "Emergency Contact"
    emergencyContactPhone = "900000003"
    isActive = $true
    updatedBy = $Ids.UserId
} | Out-Null

Invoke-ClinicApi -Name "Update billing" -Method "PUT" -Path "/api/billings/$($Ids.BillingId)" -Body @{
    billingId = $Ids.BillingId
    patientId = $Ids.PatientId
    appointmentId = $Ids.AppointmentId
    insurancePolicyId = $Ids.InsurancePolicyId
    issueDate = New-DbTimestamp
    subtotal = 220.0
    discount = 20.0
    insuranceCoverage = 50.0
    status = "Updated"
    updatedBy = $Ids.UserId
} | Out-Null

Write-Host ""
Write-Host "Created ids:"
$Ids.GetEnumerator() | Sort-Object Name | Format-Table -AutoSize

Write-Host ""
Write-Host "Validation summary:"
$Results | Format-Table -AutoSize

$failed = $Results | Where-Object { $_.Status -ne "PASS" }
if ($failed.Count -gt 0) {
    throw "$($failed.Count) validation step(s) failed."
}

Write-Host "All validation steps passed."
