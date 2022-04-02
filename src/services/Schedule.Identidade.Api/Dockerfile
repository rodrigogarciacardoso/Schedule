#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/services/Schedule.Identidade.Api/Schedule.Identidade.Api.csproj", "src/services/Schedule.Identidade.Api/"]
RUN dotnet restore "src/services/Schedule.Identidade.Api/Schedule.Identidade.Api.csproj"
COPY . .
WORKDIR "/src/src/services/Schedule.Identidade.Api"
RUN dotnet build "Schedule.Identidade.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Schedule.Identidade.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Schedule.Identidade.Api.dll"]