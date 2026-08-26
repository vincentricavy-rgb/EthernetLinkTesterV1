$ErrorActionPreference='Stop'
& "$PSScriptRoot\build-windows-x64.ps1"
$out="$PSScriptRoot\dist"
New-Item -ItemType Directory -Force $out | Out-Null
$zip="$out\EthernetLinkTester-Portable-Windows11-x64.zip"
if(Test-Path $zip){Remove-Item $zip}
Compress-Archive -Path "$PSScriptRoot\publish\win-x64\*" -DestinationPath $zip
Write-Host "Portable: $zip" -ForegroundColor Green
