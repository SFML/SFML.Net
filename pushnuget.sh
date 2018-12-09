#!/bin/sh

# Automatically exit on error
set -e

if [ -z "$1" ]; then
    echo "Specify the NuGet API Key as a parameter to this script"
    exit 1
fi

dotnet build -c Release

# Find every file that matches the glob ./src/*/bin/Release/*.nupkg
# and push it to nuget.org using the key specified as a parameter
# to this script

find \
    -type f \
    -regex "\./src/\w+/bin/Release/[^/]+\.nupkg" \
    -exec dotnet nuget push -k "$1" -s "https://api.nuget.org/v3/index.json" "{}" ";"

