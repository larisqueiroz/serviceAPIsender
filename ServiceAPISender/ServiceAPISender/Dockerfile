FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ServiceAPISender.csproj", "ServiceAPISender/"]
RUN dotnet restore "ServiceAPISender/ServiceAPISender.csproj"
COPY . .
WORKDIR "/src" 
RUN dotnet build "ServiceAPISender.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceAPISender.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceAPISender.dll"]
