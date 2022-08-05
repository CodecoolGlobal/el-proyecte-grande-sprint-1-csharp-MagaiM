#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ElProyecteGrandeSprint1/ElProyecteGrandeSprint1/ElProyecteGrandeSprint1.csproj", "ElProyecteGrandeSprint1/"]
RUN dotnet restore "ElProyecteGrandeSprint1/ElProyecteGrandeSprint1.csproj"
COPY . .
WORKDIR "/src/ElProyecteGrandeSprint1"
RUN dotnet build "ElProyecteGrandeSprint1/ElProyecteGrandeSprint1.csproj" -c Release -o /app/build
RUN dotnet publish "ElProyecteGrandeSprint1/ElProyecteGrandeSprint1.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ElProyecteGrandeSprint1.dll"]