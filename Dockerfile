# Use the standard microsoft .net core image
FROM microsoft/aspnetcore-build:latest

# copy web API contents
WORKDIR /app
COPY . /app

RUN dotnet restore
EXPOSE 5000
	
# Build and run the dotnet application from within container
ENTRYPOINT ["dotnet", "run"]



