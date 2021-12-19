#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PucMinas.TCC.Authentication/PucMinas.TCC.Authentication.csproj", "PucMinas.TCC.Authentication/"]
COPY ["PucMinas.TCC.Domain/PucMinas.TCC.Domain.csproj", "PucMinas.TCC.Domain/"]
COPY ["PucMinas.TCC.Persistence.SqlServer/PucMinas.TCC.Persistence.SqlServer.csproj", "PucMinas.TCC.Persistence.SqlServer/"]
COPY ["PucMinas.TCC.Persistence/PucMinas.TCC.Persistence.csproj", "PucMinas.TCC.Persistence/"]
COPY ["PucMinas.TCC.Utility/PucMinas.TCC.Utility.csproj", "PucMinas.TCC.Utility/"]
RUN dotnet restore "PucMinas.TCC.Authentication/PucMinas.TCC.Authentication.csproj"
COPY . .
WORKDIR "/src/PucMinas.TCC.Authentication"
RUN dotnet build "PucMinas.TCC.Authentication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PucMinas.TCC.Authentication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PucMinas.TCC.Authentication.dll"]