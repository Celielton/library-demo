FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build-env
WORKDIR /app

#Show .NET version
RUN dotnet --version

# copy csproj then restore
COPY ./*.sln ./
COPY src/Presentation/*.csproj src/Presentation/
COPY src/Shared.Application/*.csproj src/Shared.Application/
COPY src/Shared.Infrastructure/*.csproj src/Shared.Infrastructure/
COPY src/Shared.Domain/*.csproj src/Shared.Domain/
COPY src/Shared.Core/*.csproj src/Shared.Core/
COPY tests/Library.Tests/*.csproj tests/Library.Tests/
RUN dotnet restore

# Build
COPY . .
RUN dotnet publish -c Release -o out

# Build image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "library-api.dll", "--environment=Development"]