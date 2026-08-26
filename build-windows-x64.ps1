$ErrorActionPreference='Stop'
$root=Split-Path -Parent $MyInvocation.MyCommand.Path
$proj=Join-Path $root 'src\EthernetLinkTester\EthernetLinkTester.csproj'
$out=Join-Path $root 'publish\win-x64'
dotnet restore $proj
dotnet publish $proj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o $out
Write-Host "OK: $out\EthernetLinkTester.exe" -ForegroundColor Green
