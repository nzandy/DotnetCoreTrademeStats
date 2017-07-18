# Use the standard microsoft .net core image
FROM microsoft/dotnet:latest

# copy web API contents
WORKDIR /app
COPY src/API /app/API
COPY src/ClassLib /app/ClassLib

WORKDIR /app/API
RUN ["dotnet", "restore"]

RUN ["dotnet", "build"]
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000/tcp

# Build and run the dotnet application from within container
ENTRYPOINT ["dotnet", "run", "--server.urls", "http://*:5000", "-e dbConnStr=$dbConnStr"]



