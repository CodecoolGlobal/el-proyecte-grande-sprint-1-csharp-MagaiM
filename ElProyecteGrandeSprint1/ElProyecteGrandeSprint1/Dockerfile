#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ElProyecteGrandeSprint1/ElProyecteGrandeSprint1/ElProyecteGrandeSprint1.csproj", "ElProyecteGrandeSprint1/"]
RUN dotnet restore "ElProyecteGrandeSprint1/ElProyecteGrandeSprint1.csproj"
COPY . .
WORKDIR "/src/ElProyecteGrandeSprint1"
RUN dotnet build "ElProyecteGrandeSprint1/ElProyecteGrandeSprint1/ElProyecteGrandeSprint1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElProyecteGrandeSprint1/ElProyecteGrandeSprint1/ElProyecteGrandeSprint1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElProyecteGrandeSprint1.dll"]