#define MyAppName "Ethernet Link Tester Pro"
#define MyAppVersion "1.0"
#define MyAppExeName "EthernetLinkTester.exe"
[Setup]
AppId={{DA80D2C7-2662-44C6-BB1F-609CC3096D42}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
DefaultDirName={autopf}\EthernetLinkTester
DefaultGroupName={#MyAppName}
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
OutputDir=..\dist
OutputBaseFilename=EthernetLinkTester-Setup-Windows11-x64
Compression=lzma2
SolidCompression=yes
PrivilegesRequired=admin
WizardStyle=modern
[Files]
Source: "..\publish\win-x64\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
[Tasks]
Name: "desktopicon"; Description: "Créer un raccourci sur le Bureau"; GroupDescription: "Raccourcis"
Name: "firewall"; Description: "Autoriser TCP/UDP 5201 dans le pare-feu Windows"; GroupDescription: "Réseau"; Flags: checkedonce
[Run]
Filename: "netsh"; Parameters: "advfirewall firewall add rule name=\"Ethernet Link Tester TCP\" dir=in action=allow protocol=TCP localport=5201"; Flags: runhidden; Tasks: firewall
Filename: "netsh"; Parameters: "advfirewall firewall add rule name=\"Ethernet Link Tester UDP\" dir=in action=allow protocol=UDP localport=5201"; Flags: runhidden; Tasks: firewall
Filename: "{app}\{#MyAppExeName}"; Description: "Lancer {#MyAppName}"; Flags: nowait postinstall skipifsilent
[UninstallRun]
Filename: "netsh"; Parameters: "advfirewall firewall delete rule name=\"Ethernet Link Tester TCP\""; Flags: runhidden
Filename: "netsh"; Parameters: "advfirewall firewall delete rule name=\"Ethernet Link Tester UDP\""; Flags: runhidden
