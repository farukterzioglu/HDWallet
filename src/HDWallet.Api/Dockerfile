FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY src/HDWallet.Tron/HDWallet.Tron.csproj src/HDWallet.Tron/
COPY src/HDWallet.Core/HDWallet.Core.csproj src/HDWallet.Core/
COPY src/HDWallet.Secp256k1/HDWallet.Secp256k1.csproj src/HDWallet.Secp256k1/
COPY src/HDWallet.Tron/HDWallet.Tron.csproj src/HDWallet.Tron/
COPY src/HDWallet.Api/HDWallet.Api.csproj src/HDWallet.Api/

WORKDIR /source/src/HDWallet.Api
RUN dotnet restore

WORKDIR /source
COPY src src 

FROM build AS publish
WORKDIR /source/src/HDWallet.Api
RUN dotnet publish -c Release -o /app --no-restore 

FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HDWallet.Api.dll"]