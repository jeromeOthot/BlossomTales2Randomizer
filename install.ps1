$source = Join-Path $PSScriptRoot "MonoMod"
$destination = Split-Path $PSScriptRoot -Parent

# 1. Copier le contenu de MonoMod vers BlossomTales2
robocopy $source $destination /E

# 2. Copier les DLL vers BlossomTales2
Copy-Item "$PSScriptRoot\BlossomTales2.Randomizer.mm.dll" $destination -Force
Copy-Item "$PSScriptRoot\Newtonsoft.Json.dll" $destination -Force
Copy-Item "$PSScriptRoot\settings.json" $destination -Force

# 3. Copier masterTileset.xnb avec sauvegarde
$sourceXnb = Join-Path $PSScriptRoot "masterTileset.xnb"
$destinationSprite = Join-Path $destination "Content\Sprites"
$destinationXnb = Join-Path $destinationSprite "masterTileset.xnb"

# Vérifier que le dossier destination existe
if (!(Test-Path $destinationSprite)) {
    New-Item -ItemType Directory -Path $destinationSprite -Force | Out-Null
}

# Sauvegarder le fichier existant
if (Test-Path $destinationXnb) {
    $backup = "$destinationXnb.bak"
    Copy-Item $destinationXnb $backup -Force

    Write-Host "Sauvegarde créée : $backup" -ForegroundColor Yellow
}

# Copier le nouveau fichier
Copy-Item $sourceXnb $destinationXnb -Force

# 4. Exécuter MonoMod.exe depuis le dossier parent
$monoModExe = Join-Path $destination "MonoMod.exe"
$gameExe = Join-Path $destination "BlossomTales2.exe"

if ((Test-Path $monoModExe) -and (Test-Path $gameExe)) {
    Start-Process -FilePath $monoModExe -ArgumentList "`"$gameExe`"" -WorkingDirectory $destination -Wait
}
else {
    Write-Host "MonoMod.exe ou BlossomTales2.exe introuvable !" -ForegroundColor Red
}

# 5. Renommer le fichier généré par MonoMod
$monoModdedExe = Join-Path $destination "MONOMODDED_BlossomTales2.exe"
$randomizerExe = Join-Path $destination "BlossomTales2Randomizer.exe"

if (Test-Path $monoModdedExe) {
    Rename-Item -Path $monoModdedExe -NewName "BlossomTales2Randomizer.exe" -Force

    Write-Host "Fichier renommé : BlossomTales2Randomizer.exe" -ForegroundColor Green
}
else {
    Write-Host "Fichier MONOMODDED_BlossomTales2.exe introuvable !" -ForegroundColor Red
}

# 6. Créer le dossier de configuration dans AppData/Roaming
$appDataFolder = Join-Path $env:APPDATA "Blossom Tales 2"

if (!(Test-Path $appDataFolder)) {
    New-Item -ItemType Directory -Path $appDataFolder -Force | Out-Null

    Write-Host "Dossier créé : $appDataFolder" -ForegroundColor Green
}
else {
    Write-Host "Le dossier Blossom Tales 2 existe déjà." -ForegroundColor Yellow
}
