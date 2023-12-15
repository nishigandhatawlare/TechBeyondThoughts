#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TechBeyondThoughts.Services.EmailAPI/TechBeyondThoughts.Services.EmailAPI.csproj", "TechBeyondThoughts.Services.EmailAPI/"]
RUN dotnet restore "TechBeyondThoughts.Services.EmailAPI/TechBeyondThoughts.Services.EmailAPI.csproj"
COPY . .
WORKDIR "/src/TechBeyondThoughts.Services.EmailAPI"
RUN dotnet build "TechBeyondThoughts.Services.EmailAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TechBeyondThoughts.Services.EmailAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechBeyondThoughts.Services.EmailAPI.dll"]