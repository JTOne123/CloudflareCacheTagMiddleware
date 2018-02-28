Push-Location .\src\CloudflareCacheTagMiddleware

Write-Host "Removing previous nuget packages"
Remove-Item .\bin\Release\netcoreapp2.0\*.nupkg > $null

Write-Host "Building and packaging"
msbuild /t:pack /p:Configuration=Release

$nugetPackage = Get-ChildItem .\bin\Release\*.nupkg | Select-Object -First 1

Write-Host "Publishing package:$nugetPackage"
nuget push $nugetPackage -Source https://api.nuget.org/v3/index.json

Pop-Location