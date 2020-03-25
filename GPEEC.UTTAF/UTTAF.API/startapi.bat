@echo off

color 8

echo ======================  INICIANDO API  ========================
echo ======================  FAZENDO BUILD  ========================
dotnet build

echo ======================  RODANDO API  ========================
cd ..
cd UTTAF.API
dotnet run
pause

echo ======================  RODANDO TESTES  ========================
cd ..
cd UTTAF.API.Tests
dotnet test

