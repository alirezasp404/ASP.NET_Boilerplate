FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/MyProject.Web.Host/MyProject.Web.Host.csproj", "src/MyProject.Web.Host/"]
COPY ["src/MyProject.Web.Core/MyProject.Web.Core.csproj", "src/MyProject.Web.Core/"]
COPY ["src/MyProject.Application/MyProject.Application.csproj", "src/MyProject.Application/"]
COPY ["src/MyProject.Core/MyProject.Core.csproj", "src/MyProject.Core/"]
COPY ["src/MyProject.EntityFrameworkCore/MyProject.EntityFrameworkCore.csproj", "src/MyProject.EntityFrameworkCore/"]
WORKDIR "/src/src/MyProject.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/MyProject.Web.Host", "src/MyProject.Web.Host"]
COPY ["src/MyProject.Web.Core", "src/MyProject.Web.Core"]
COPY ["src/MyProject.Application", "src/MyProject.Application"]
COPY ["src/MyProject.Core", "src/MyProject.Core"]
COPY ["src/MyProject.EntityFrameworkCore", "src/MyProject.EntityFrameworkCore"]
WORKDIR "/src/src/MyProject.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "MyProject.Web.Host.dll"]
