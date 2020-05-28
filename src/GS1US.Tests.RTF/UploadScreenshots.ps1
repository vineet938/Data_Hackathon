$x = Get-ChildItem  -Path bin/Debug/Reports/*.png
foreach ($y in $x) {
    Write-Host $y
}