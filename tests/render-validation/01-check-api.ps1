$ErrorActionPreference = "Stop"

$ApiBaseUrl = "https://finaldaea.onrender.com"

$Endpoints = @(
    @{ Name = "Health"; Path = "/health" },
    @{ Name = "Swagger JSON"; Path = "/swagger/v1/swagger.json" },
    @{ Name = "Roles"; Path = "/api/roles" },
    @{ Name = "Permissions"; Path = "/api/permissions" },
    @{ Name = "Departments"; Path = "/api/departments" },
    @{ Name = "Specialties"; Path = "/api/specialtys" },
    @{ Name = "Appointment Statuses"; Path = "/api/appointment-statuses" },
    @{ Name = "Notification Types"; Path = "/api/notification-types" },
    @{ Name = "Insurance Companies"; Path = "/api/insurance-companys" },
    @{ Name = "Medications"; Path = "/api/medications" },
    @{ Name = "Patients"; Path = "/api/patients" },
    @{ Name = "Doctors"; Path = "/api/doctors" },
    @{ Name = "Appointments"; Path = "/api/appointments" },
    @{ Name = "Medical Records"; Path = "/api/medical-records" }
)

$Results = @()

foreach ($Endpoint in $Endpoints) {
    $Url = "$ApiBaseUrl$($Endpoint.Path)"

    try {
        $Response = Invoke-WebRequest `
            -Uri $Url `
            -Method GET `
            -UseBasicParsing `
            -TimeoutSec 60

        $Results += [PSCustomObject]@{
            Name = $Endpoint.Name
            Path = $Endpoint.Path
            StatusCode = $Response.StatusCode
            Result = "PASS"
        }

        Write-Host "[PASS] $($Endpoint.Name) -> HTTP $($Response.StatusCode)" -ForegroundColor Green
    }
    catch {
        $StatusCode = "N/A"

        if ($_.Exception.Response -and $_.Exception.Response.StatusCode) {
            $StatusCode = [int]$_.Exception.Response.StatusCode
        }

        $Results += [PSCustomObject]@{
            Name = $Endpoint.Name
            Path = $Endpoint.Path
            StatusCode = $StatusCode
            Result = "FAIL"
        }

        Write-Host "[FAIL] $($Endpoint.Name) -> HTTP $StatusCode" -ForegroundColor Red
        Write-Host "       $($_.Exception.Message)" -ForegroundColor DarkYellow
    }
}

Write-Host ""
Write-Host "Resumen:"
$Results | Format-Table -AutoSize

$Failed = $Results | Where-Object { $_.Result -eq "FAIL" }

if ($Failed.Count -gt 0) {
    throw "$($Failed.Count) endpoint(s) fallaron. Revisa rutas o logs de Render."
}

Write-Host ""
Write-Host "Validacion GET completada correctamente." -ForegroundColor Green