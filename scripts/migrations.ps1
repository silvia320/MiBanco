<#
  Uso:  .\scripts\migrations.ps1 -Name AddAlgo
  Crea una migracion nueva en Infrastructure/Persistence/Migrations.
  Equivalente en Visual Studio (Package Manager Console, proyecto por defecto = MiBanco):
        Add-Migration AddAlgo -OutputDir Infrastructure/Persistence/Migrations
#>
param([Parameter(Mandatory = $true)][string]$Name)

$root = Split-Path $PSScriptRoot -Parent
$proj = Join-Path $root 'src\MiBanco\MiBanco.csproj'

dotnet ef migrations add $Name `
    --project $proj --startup-project $proj `
    --context AppDbContext `
    --output-dir Infrastructure/Persistence/Migrations