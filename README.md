# Ethernet Link Tester Pro — Windows 11 x64

Application WPF .NET 8 unique : chaque PC peut être **Émetteur/Pilote** ou **Récepteur/Reflector**.

## Modules inclus

- Diagnostic ICMP : latence min/moy/max, gigue et pertes.
- Débit TCP et UDP entre les deux instances.
- Détection MTU IPv4 avec DF et indication jumbo.
- Scan de ports TCP et UDP (UDP : réponse / ICMP / indéterminé).
- Ethernet L2 avec Npcap : trames propriétaires EtherType 0x88B5.
- VLAN 802.1Q : VLAN ID 1–4094, scan de plage, contrôle du VID reçu, PCP 802.1p.
- QinQ / 802.1ad : S-VLAN + C-VLAN.
- RFC 2544 logiciel : throughput, latence, frame loss et estimation back-to-back par tailles de trames.
- ITU-T Y.1564 EtherSAM logiciel : profils CIR/EIR, FTD/FDV/FLR, phase Configuration et phase Performance.
- Historique et exports CSV/JSON.
- Installation et version portable Windows 11 x64.

## Important — conformité

Les campagnes RFC 2544 et Y.1564 sont implémentées comme **outil logiciel de qualification**. Windows, le scheduler, la carte réseau et Npcap introduisent de la gigue et peuvent limiter la génération de trafic à débit élevé. Il faut valider l'outil contre un générateur/analyseur de référence avant de présenter ses mesures comme des résultats certifiés ou métrologiques.

Le module VLAN/L2 nécessite **Npcap** installé sur les deux PC. L'installation de Npcap n'est pas embarquée pour éviter toute redistribution/licence implicite. Téléchargement officiel : https://npcap.com/

## Compilation

Prérequis sur un PC Windows 11 x64 :
1. .NET 8 SDK x64.
2. Internet lors du premier `dotnet restore` pour récupérer SharpPcap.
3. Npcap pour les fonctions Ethernet L2/VLAN.
4. Inno Setup 6 uniquement si vous voulez fabriquer le Setup.exe.

Dans PowerShell :

```powershell
Set-ExecutionPolicy -Scope Process Bypass
.\build-windows-x64.ps1
```

Résultat : `publish\win-x64\EthernetLinkTester.exe`

Pour la version portable :

```powershell
.\build-portable.ps1
```

Pour l'installateur :

```powershell
.\build-installer.ps1
```

Résultat : `dist\EthernetLinkTester-Setup-Windows11-x64.exe`

## Utilisation sur deux PC

1. Installer l'application et Npcap sur PC A et PC B.
2. Autoriser TCP/UDP 5201 dans le pare-feu (le Setup propose la règle automatiquement).
3. PC B : choisir **RÉCEPTEUR / REFLECTOR**. Pour L2/VLAN, sélectionner l'interface Npcap avant de passer Récepteur.
4. PC A : choisir **ÉMETTEUR / PILOTE**, saisir l'IP de B et lancer les tests.
5. Pour VLAN/L2, renseigner les vraies MAC des interfaces de test sur A et B.
6. Pour inverser le sens, changer simplement les rôles sur les deux PC.

## Notes VLAN

Les cartes/driver Windows peuvent retirer ou traiter les tags VLAN avant Npcap selon l'offload matériel. Pour un test opérateur sérieux, désactiver les VLAN/offloads qui perturbent la capture si nécessaire, utiliser une NIC validée, et comparer les résultats avec une capture externe.

## Build automatique GitHub Actions
Le dépôt contient `.github/workflows/build-windows-x64.yml`.
Après envoi du projet dans un dépôt GitHub, ouvrez **Actions > Build Windows x64 > Run workflow**.
Le job Windows génère et publie un artefact nommé `EthernetLinkTester-Windows11-x64` contenant :
- `EthernetLinkTester-Windows11-x64.exe`
- `EthernetLinkTester-Portable-Windows11-x64.zip`
- `EthernetLinkTester-Setup-Windows11-x64.exe`

Npcap reste requis sur la machine cible pour les fonctions Ethernet L2 / VLAN / PCP / QinQ.
