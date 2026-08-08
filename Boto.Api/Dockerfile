FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Boto.Api/Boto.Api.csproj", "Boto.Api/"]
COPY ["Boto.Application/Boto.Application.csproj", "Boto.Application/"]
COPY ["Boto.Domain/Boto.Domain.csproj", "Boto.Domain/"]
COPY ["Boto.Infrastructure/Boto.Infrastructure.csproj", "Boto.Infrastructure/"]
RUN dotnet restore "Boto.Api/Boto.Api.csproj"

COPY . .
WORKDIR "/src/Boto.Api"
RUN dotnet build "Boto.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Boto.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Boto.Api.dll"]
