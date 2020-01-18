FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["notes-service.csproj", "notes-service/"]
RUN dotnet restore "notes-service/notes-service.csproj"
COPY . .
RUN dotnet publish "notes-service.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "notes-service.dll"]
