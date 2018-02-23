@echo off

set target_dir=publish_win
mkdir %target_dir%

rem Audio references other projects so they are automatically builder and copied into target_dir
dotnet publish -c release -o %CD%\%target_dir% ..\..\src\Audio\sfml-audio_core.csproj 