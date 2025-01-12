# 1. Aþama: Build iþlemi
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Projedeki dosyalarý kopyala ve baðýmlýlýklarý yükle
COPY . .
RUN dotnet restore

# Uygulamayý yayýnla (publish)
RUN dotnet publish -c Release -o out

# 2. Aþama: Runtime iþlemi
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Uygulamanýn çalýþacaðý port
EXPOSE 80

# Uygulamayý baþlat
ENTRYPOINT ["dotnet", "FootballApp.dll"]
