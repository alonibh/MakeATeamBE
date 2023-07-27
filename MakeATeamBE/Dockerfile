# Use the official .NET 6 SDK as the base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project file(s) to the working directory
COPY ./MakeATeamBE.csproj ./

# Restore the NuGet packages
RUN dotnet restore

# Copy the rest of the source code to the working directory
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Use a smaller runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the published output from the build image
COPY --from=build /app/out ./

# Expose the port your API is listening on (replace 80 with the actual port)
EXPOSE 80

# Start your API when the container starts
ENTRYPOINT ["dotnet", "MakeATeamBE.dll"]
