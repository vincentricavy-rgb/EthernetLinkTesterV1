# Exécuter en administrateur sur chaque PC de test.
$port=5201
New-NetFirewallRule -DisplayName 'Ethernet Link Tester TCP' -Direction Inbound -Action Allow -Protocol TCP -LocalPort $port -ErrorAction SilentlyContinue
New-NetFirewallRule -DisplayName 'Ethernet Link Tester UDP' -Direction Inbound -Action Allow -Protocol UDP -LocalPort $port -ErrorAction SilentlyContinue
Write-Host "Pare-feu ouvert sur TCP/UDP $port" -ForegroundColor Green
