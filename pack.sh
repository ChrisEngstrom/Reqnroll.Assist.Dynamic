#!/bin/bash

rm -rf ./SpecFlow.Assist.Dynamic/bin/Release

dotnet clean
dotnet test
dotnet build -c Release

dotnet pack ./SpecFlow.Assist.Dynamic/SpecFlow.Assist.Dynamic.csproj -p:NuspecFile=./SpecFlow.Assist.Dynamic.nuspec -c Release

open ./bin/Release