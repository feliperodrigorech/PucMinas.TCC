#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PucMinas.TCC.RegistrationInformation/PucMinas.TCC.RegistrationInformation.csproj", "PucMinas.TCC.RegistrationInformation/"]
COPY ["PucMinas.TCC.Domain/PucMinas.TCC.Domain.csproj", "PucMinas.TCC.Domain/"]
COPY ["PucMinas.TCC.Persistence.SqlServer/PucMinas.TCC.Persistence.SqlServer.csproj", "PucMinas.TCC.Persistence.SqlServer/"]
COPY ["PucMinas.TCC.Persistence/PucMinas.TCC.Persistence.csproj", "PucMinas.TCC.Persistence/"]
COPY ["PucMinas.TCC.Utility/PucMinas.TCC.Utility.csproj", "PucMinas.TCC.Utility/"]
RUN dotnet restore "PucMinas.TCC.RegistrationInformation/PucMinas.TCC.RegistrationInformation.csproj"
COPY . .
WORKDIR "/src/PucMinas.TCC.RegistrationInformation"
RUN dotnet build "PucMinas.TCC.RegistrationInformation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PucMinas.TCC.RegistrationInformation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PucMinas.TCC.RegistrationInformation.dll"]