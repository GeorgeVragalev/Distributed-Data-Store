FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Server1/Server1.csproj", "Server1/"]
RUN dotnet restore "Server1/Server1.csproj"
COPY . .
WORKDIR "/src/Server1"
RUN dotnet build "Server1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Server1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server1.dll"]
