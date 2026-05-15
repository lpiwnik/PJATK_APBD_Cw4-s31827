# Projekt: Computer Inventory API - APBD 2026 

## 🚀 Szybki Start (Docker)

Aplikacja jest w pełni skonteneryzowana. Nie musisz instalować środowiska .NET 8.0 na systemie hosta, aby uruchomić projekt – wszystko, co niezbędne (Runtime, biblioteki), znajduje się w obrazie Docker.

---

## 📦 Konfiguracja Docker Compose

Stwórz plik `docker-compose.yml` i wklej poniższą treść:

```yaml
services:
  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: sql-server-db
    environment:
      - ACCEPT_EULA=Y
      - MSSQL_SA_PASSWORD=Str0ng!Passw0rd
    ports:
      - "1433:1433"
    healthcheck:
      test: ["CMD", "/opt/mssql-tools18/bin/sqlcmd", "-S", "localhost", "-U", "sa", "-P", "Str0ng!Passw0rd", "-Q", "SELECT 1", "-C"]
      interval: 10s
      timeout: 5s
      retries: 5

  api:
    image: lpiwnik/computer-inventory-api:latest
    container_name: pc-inventory-api
    depends_on:
      db:
        condition: service_healthy
    environment:
      # WAŻNE: ASPNETCORE_ENVIRONMENT=Development jest wymagane do działania Swaggera.
      # W przypadku braku tego ustawienia (tryb Production), interfejs Swagger UI nie będzie dostępny.
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__DefaultConnection=Server=db;Database=ComputerInventoryDB;User Id=sa;Password=Str0ng!Passw0rd;TrustServerCertificate=True
    ports:
      - "5229:8080"
```

---

### Swagger UI (Dokumentacja)

```text
http://localhost:5229/swagger/index.html
```


---
## 🛠 Zarządzanie i czyszczenie środowiska

### 1. Uruchomienie usług

```bash
docker-compose up -d
```

### 2. Zatrzymanie usług

```bash
docker-compose down
```

### 3. Nuklearne sprzątanie

Usuwa:

- kontenery,
- wolumeny (bazę danych),
- pobrane obrazy Docker,
- nieużywane zasoby systemowe.

```bash
docker-compose down -v --rmi all && docker system prune -a -f
```

---

## 📋 Wymagania

Do uruchomienia projektu wymagany jest jedynie:

- Docker
- Docker Compose

Nie jest wymagane lokalne środowisko .NET SDK ani SQL Server.

---

## ✅ Status projektu

Projekt gotowy do uruchomienia w środowisku Docker.
Swagger UI aktywny w trybie Development.
