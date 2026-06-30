FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HelloUltimateUtils/HelloUltimateUtils.csproj", "HelloUltimateUtils/"]
RUN dotnet restore "HelloUltimateUtils/HelloUltimateUtils.csproj"
COPY . .
WORKDIR "/src/HelloUltimateUtils"
RUN dotnet build "./HelloUltimateUtils.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HelloUltimateUtils.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloUltimateUtils.dll"]
