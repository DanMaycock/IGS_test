FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY * ./

# Create and seed the DB
ENV PATH $PATH:~/.dotnet/tools
RUN dotnet tool install -g dotnet-ef && dotnet ef database update

RUN dotnet publish -c Release -o out

ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "out/igsapi.dll"]