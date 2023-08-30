FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["yado-backend/yado-backend.csproj", "yado-backend/"]
RUN dotnet restore "yado-backend/yado-backend.csproj"

COPY . .
WORKDIR "/src/yado-backend"
RUN dotnet build "yado-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "yado-backend.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "yado-backend.dll"]
