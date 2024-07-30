# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ARG CONNECTION_DB
ENV ConnectionDB=$CONNECTION_DB
ENV testEVN "SiTest"

WORKDIR /app
COPY /FondoBackPub .
ENTRYPOINT ["dotnet", "BTGIn-back.dll"]