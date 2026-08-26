$ErrorActionPreference='Stop'
& "$PSScriptRoot\build-windows-x64.ps1"
$iscc=(Get-Command ISCC.exe -ErrorAction SilentlyContinue).Source
if(-not $iscc){$iscc='C:\Program Files (x86)\Inno Setup 6\ISCC.exe'}
if(-not (Test-Path $iscc)){throw 'Inno Setup 6 introuvable. Installez-le puis relancez.'}
& $iscc "$PSScriptRoot\installer\EthernetLinkTester.iss"
