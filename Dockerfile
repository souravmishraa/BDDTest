# # FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# # WORKDIR /app

# # COPY *.sln .
# # COPY MySpecFlowTests/*.csproj ./MySpecFlowTests/
# # RUN dotnet restore

# # COPY . .
# # WORKDIR /app/MySpecFlowTests
# # RUN dotnet build --configuration Release

# # CMD ["dotnet", "test", "--no-build", "--configuration", "Release"]
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# WORKDIR /app

# COPY *.sln ./
# COPY *.csproj ./

# RUN dotnet restore

# COPY . .

# RUN dotnet build --configuration Release

# CMD ["dotnet", "test", "--no-build", "--configuration", "Release"]
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# WORKDIR /app

# COPY . .

# RUN dotnet restore
# RUN dotnet build --configuration Release
# RUN dotnet publish -c Release -o /out

# FROM mcr.microsoft.com/dotnet/runtime:8.0
# WORKDIR /app
# COPY --from=build /out .

# ENTRYPOINT ["dotnet", "MySpecFlowTests.dll"]

# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /app
# COPY . .
# RUN dotnet restore
# RUN dotnet build --configuration Release

# # Instead of publishing, we'll just run tests
# FROM mcr.microsoft.com/dotnet/sdk:8.0
# WORKDIR /app
# COPY --from=build /app .

# ENTRYPOINT ["dotnet", "test", "--no-build", "--logger:console;verbosity=detailed"]
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet build --configuration Release

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

# Install the missing native library here:
RUN apt-get update && apt-get install -y libglib2.0-0

COPY --from=build /app .

ENTRYPOINT ["dotnet", "test", "--no-build", "--logger:console;verbosity=detailed"]
