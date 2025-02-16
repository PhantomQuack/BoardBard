FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BoardBard.Web/BoardBard.Web.Client/BoardBard.Web.Client.csproj", "BoardBard.Web/BoardBard.Web.Client/"]
RUN dotnet restore "BoardBard.Web/BoardBard.Web.Client/BoardBard.Web.Client.csproj"
COPY . .
WORKDIR "/src/BoardBard.Web/BoardBard.Web.Client"
RUN dotnet build "BoardBard.Web.Client.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BoardBard.Web.Client.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoardBard.Web.Client.dll"]
