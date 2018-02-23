#!/bin/bash

target_dir=publish_linux
mkdir $target_dir

# Audio references other projects so they are automatically builder and copied into target_dir
dotnet publish -c release -o $PWD/$target_dir ../../src/Audio/sfml-audio_core.csproj
