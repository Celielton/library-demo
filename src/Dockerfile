FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build-env
WORKDIR /app

#Show .NET version
RUN dotnet --version

# copy csproj then restore
COPY *.csproj ./
RUN dotnet restore


# Build
COPY . ./
RUN dotnet publish -c Release -o out

# Build image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "library-api.dll", "--environment=Development"]