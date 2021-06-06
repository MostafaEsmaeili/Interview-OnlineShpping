
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Order/Order.WebApi/Order.WebApi.csproj", "Order/Order.WebApi/"]
RUN dotnet restore "Order/Order.WebApi/Order.WebApi.csproj"
COPY . .
WORKDIR "/src/Order/Order.WebApi"
RUN dotnet build "Order.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.WebApi.dll"]