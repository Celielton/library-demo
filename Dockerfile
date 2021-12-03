ARG SDK=5.0
ARG RUNTIME=5.0

FROM mcr.microsoft.com/dotnet/aspnet:$RUNTIME AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:$SDK AS build
WORKDIR /sln

# Copy the main source project files
COPY ./*.sln ./
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj);do echo ${file%.*} && mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done
RUN dotnet restore

COPY ./src ./src
RUN dotnet publish "./src/Presentation/library-api/library-api.csproj" -c Release -o "./dist" --no-restore
RUN rm ./dist/*.pdb
RUN rm ./dist/*deps.json

# Build
COPY . .
RUN dotnet publish -c Release -o out

# Build image
FROM base AS final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Local
COPY --from=build /sln/dist .
ENTRYPOINT ["dotnet", "library-api.dll"]