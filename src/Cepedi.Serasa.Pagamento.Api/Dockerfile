FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Cepedi.Serasa.Pagamento.Api/Cepedi.Serasa.Pagamento.Api.csproj", "src/Cepedi.Serasa.Pagamento.Api/"]
COPY ["src/Cepedi.Serasa.Pagamento.IoC/Cepedi.Serasa.Pagamento.IoC.csproj", "src/Cepedi.Serasa.Pagamento.IoC/"]
COPY ["src/Cepedi.Serasa.Pagamento.Dados/Cepedi.Serasa.Pagamento.Dados.csproj", "src/Cepedi.Serasa.Pagamento.Dados/"]
COPY ["src/Cepedi.Serasa.Pagamento.Dominio/Cepedi.Serasa.Pagamento.Dominio.csproj", "src/Cepedi.Serasa.Pagamento.Dominio/"]
COPY ["src/Cepedi.Serasa.Pagamento.Compartilhado/Cepedi.Serasa.Pagamento.Compartilhado.csproj", "src/Cepedi.Serasa.Pagamento.Compartilhado/"]
RUN dotnet restore "src/Cepedi.Serasa.Pagamento.Api/Cepedi.Serasa.Pagamento.Api.csproj"
COPY . .
WORKDIR "/src/src/Cepedi.Serasa.Pagamento.Api"
RUN dotnet build "./Cepedi.Serasa.Pagamento.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cepedi.Serasa.Pagamento.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cepedi.Serasa.Pagamento.Api.dll"]
