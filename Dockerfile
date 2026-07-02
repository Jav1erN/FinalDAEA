FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ClinicSystem.sln ./
COPY ClinicSystem.Domain/ClinicSystem.Domain.csproj ClinicSystem.Domain/
COPY ClinicSystem.Application/ClinicSystem.Application.csproj ClinicSystem.Application/
COPY ClinicSystem.Infrastructure/ClinicSystem.Infrastructure.csproj ClinicSystem.Infrastructure/
COPY ClinicSystem.API/ClinicSystem.API.csproj ClinicSystem.API/
COPY ClinicSystem.Tests/ClinicSystem.Tests.csproj ClinicSystem.Tests/

RUN dotnet restore ClinicSystem.sln

COPY . .
RUN dotnet publish ClinicSystem.API/ClinicSystem.API.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 10000

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ClinicSystem.API.dll"]
