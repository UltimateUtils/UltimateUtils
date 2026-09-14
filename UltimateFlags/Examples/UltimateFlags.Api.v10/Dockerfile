FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UltimateFlags/Examples/UltimateFlags.Api.v10/UltimateFlags.Api.v10.csproj", "UltimateFlags/Examples/UltimateFlags.Api.v10/"]
RUN dotnet restore "UltimateFlags/Examples/UltimateFlags.Api.v10/UltimateFlags.Api.v10.csproj"
COPY . .
WORKDIR "/src/UltimateFlags/Examples/UltimateFlags.Api.v10"
RUN dotnet build "./UltimateFlags.Api.v10.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UltimateFlags.Api.v10.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UltimateFlags.Api.v10.dll"]
