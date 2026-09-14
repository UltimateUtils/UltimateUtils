FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UltimateFlags/Examples/UltimateFlags.Api.v9/UltimateFlags.Api.v9.csproj", "UltimateFlags/Examples/UltimateFlags.Api.v9/"]
RUN dotnet restore "UltimateFlags/Examples/UltimateFlags.Api.v9/UltimateFlags.Api.v9.csproj"
COPY . .
WORKDIR "/src/UltimateFlags/Examples/UltimateFlags.Api.v9"
RUN dotnet build "./UltimateFlags.Api.v9.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UltimateFlags.Api.v9.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UltimateFlags.Api.v9.dll"]
