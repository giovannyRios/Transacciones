FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5097 7213 80 443 57605

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Transacciones/Transacciones.csproj", "Transacciones/"]
COPY ["Transacciones.Dominio/Transacciones.Dominio.csproj", "Transacciones.Dominio/"]
COPY ["Transacciones.Negocio/Transacciones.Negocio.csproj", "Transacciones.Negocio/"]
RUN dotnet restore "Transacciones/Transacciones.csproj"
COPY . .
WORKDIR "/src/Transacciones"
RUN dotnet build "Transacciones.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Transacciones.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Transacciones.dll"]