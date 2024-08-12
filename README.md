# Temporal Workflow Microservice aspnetcore Sample

## Prerequistes

1. Install Docker
2. Install dotnet sdk 8.0

## Steps to run

1. Start Temporal Services 
```bash
docker compose -f docker-compose.infrastructure.yml up
```

2. Start Microservices
```bash
docker compose -f docker-compose.microservices.yml up
```

3. Start Temporal Worker
```bash
dotnet run --project Worker
```

4. Access Company and Workflow API [Employee-API](http://localhost:5002/swagger/index.html)

5. Access Temporal Workflow Dashboard [Temporal-Workflows](http://localhost:8080/namespaces/default/workflows)
