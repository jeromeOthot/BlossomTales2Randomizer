$destination = Split-Path $PSScriptRoot -Parent

Write-Host "`nDesinstallation du Randomizer..." -ForegroundColor Cyan

# 1. Supprimer les fichiers provenant de MonoMod
$monoModSource = Join-Path $PSScriptRoot "MonoMod"

if (Test-Path $monoModSource) {

    $files = Get-ChildItem -LiteralPath $monoModSource -File -Recurse -Force

    foreach ($file in $files) {

        # Reconstituer le chemin relatif du fichier
        $relativePath = $file.FullName.Substring($monoModSource.Length).TrimStart([char[]]@('\','/'))

        $targetFile = Join-Path $destination $relativePath

        if (Test-Path -LiteralPath $targetFile -PathType Leaf) {
            Remove-Item -LiteralPath $targetFile -Force
            Write-Host "Supprime : $relativePath"
        }
    }
}
else {
    Write-Host "Dossier MonoMod introuvable. Suppression des dependances ignoree." -ForegroundColor Yellow
}

# 2. Supprimer les DLL du Randomizer
$dllFiles = @(
    "BlossomTales2.Randomizer.mm.dll",
    "Newtonsoft.Json.dll",
    "settings.json"
)

foreach ($file in $dllFiles) {

    $path = Join-Path $destination $file

    if (Test-Path -LiteralPath $path) {
        Remove-Item -LiteralPath $path -Force
        Write-Host "Supprime : $file"
    }
}

# 3. Supprimer l'executable modde
$randomizerExe = Join-Path $destination "BlossomTales2Randomizer.exe"

if (Test-Path $randomizerExe) {
    Remove-Item -LiteralPath $randomizerExe -Force
    Write-Host "Executable Randomizer supprime."
}

# 4. Restaurer masterTileset.xnb original
$destinationSprite = Join-Path $destination "Content\Sprites"

$destinationXnb = Join-Path $destinationSprite "masterTileset.xnb"
$backupXnb = "$destinationXnb.bak"

if (Test-Path $backupXnb) {

    Move-Item -LiteralPath $backupXnb -Destination $destinationXnb -Force

    Write-Host "masterTileset.xnb original restaure !" -ForegroundColor Green

}
else {
    Write-Host "ATTENTION : Sauvegarde masterTileset.xnb.bak introuvable !" -ForegroundColor Red
}

Write-Host "`nDesinstallation terminee !" -ForegroundColor Green