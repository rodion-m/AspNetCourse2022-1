FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/GreatShop.WebApi/GreatShop.WebApi.csproj", "GreatShop.WebApi/"]
RUN dotnet restore "src/GreatShop.WebApi/GreatShop.WebApi.csproj"
COPY . .
WORKDIR "/src/GreatShop.WebApi"
RUN dotnet build "GreatShop.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GreatShop.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GreatShop.WebApi.dll"]
