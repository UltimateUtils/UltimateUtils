FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UltimateFlags/Examples/UltimateFlags.Example.EF.Host/UltimateFlags.Example.EF.Host.csproj", "UltimateFlags/Examples/UltimateFlags.Example.EF.Host/"]
RUN dotnet restore "UltimateFlags/Examples/UltimateFlags.Example.EF.Host/UltimateFlags.Example.EF.Host.csproj"
COPY . .
WORKDIR "/src/UltimateFlags/Examples/UltimateFlags.Example.EF.Host"
RUN dotnet build "./UltimateFlags.Example.EF.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UltimateFlags.Example.EF.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UltimateFlags.Example.EF.Host.dll"]
