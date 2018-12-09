Param(
    [Parameter(Mandatory, Position = 0)]
    [string]$APIKey
)

dotnet build -c Release
if ($LASTEXITCODE -ne 0) {
    Write-Error "Build failed"
    exit 1
}

$count = 0
$packages = (Get-ChildItem src/*/bin/Release/*.nupkg)
foreach ($package in $packages) {
    Write-Progress "Pushing packages" -CurrentOperation "Pushing $($package.Name)" -PercentComplete (($count * 100.0) / $packages.Length)

    dotnet nuget push -k $APIKey -s https://api.nuget.org/v3/index.json $package.FullName

    if ($LASTEXITCODE -ne 0) {
        Write-Error "Unable to push $($package.Name)"
        exit 1
    }

    $count++
}

Write-Progress "Done" -Completed
Write-Output "Successfully pushed $count packages"
