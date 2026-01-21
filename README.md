# GetechnologiesMx

Backend API desarrollado con .NET 6 y SQLite, dockerizado con volúmenes persistentes.

## Arquitectura

### Stack Tecnológico
- **Framework:** ASP.NET Core 6.0
- **Base de datos:** SQLite 
- **ORM:** Entity Framework Core 6.0.36
- **Containerización:** Docker + Docker Compose
- **CI/CD:** GitHub Actions
- **Documentación API:** Swagger/OpenAPI


## Docker

### Arquitectura de Contenedores

**Enfoque:** Single-container con volumen persistente para SQLite.

**Dockerfile (Multi-stage build):**
- **Stage 1 (build):** Imagen SDK .NET 6.0
  - Restaura dependencias
  - Compila proyecto en Release
  - Genera artefactos en `/app/publish`
  
- **Stage 2 (runtime):** Imagen ASP.NET Runtime 6.0
  - Copia artefactos compilados
  - Expone puerto 5000
  - Ejecuta aplicación optimizada

**Beneficios:**
- Imagen final ligera (solo runtime, sin SDK)
- Build reproducible
- Layer caching optimizado

### Docker Compose

```yaml
services:
  app:
    build: .
    image: getechnologiesmx_app:latest
    ports:
      - '5001:5000'
    volumes:
      - data:/app/Data
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__Default=Data Source=/app/Data/app.db

volumes:
  data:
    driver: local
```

**Características:**
- Volumen nombrado `data` para persistencia de SQLite
- Connection string configurable vía variable de entorno
- Port mapping flexible (host:container)
- Persistencia de datos entre reinicios

### Comandos Docker

```bash
# Build y levantar
docker compose up --build

# Modo detached
docker compose up -d

# Ver logs
docker compose logs -f app

# Verificar volumen
docker exec -it getechnologiesmx-app-1 ls -la /app/Data

# Inspeccionar volumen
docker volume inspect getechnologiesmx_data

# Parar y eliminar contenedores (volumen persiste)
docker compose down

# Eliminar volumen también
docker compose down -v
```

## GitHub Actions Pipeline

### Workflow: .NET CI/CD

**Archivo:** `.github/workflows/test_and_build.yaml`

**Triggers:**
- Push a branch `develop`
- Pull Request hacia `develop`

**Jobs:**

#### Build Job
Runner: `ubuntu-latest`

**Steps:**
1. **Checkout:** `actions/checkout@v4`
2. **Setup .NET:** `actions/setup-dotnet@v4` (versión 8.0.x para workflow)
3. **Cache NuGet:** `actions/cache@v4`
   - Path: `~/.nuget/packages`, `~/.dotnet/tools`
   - Key: `${{ runner.os }}-nuget-${{ hashFiles('**/packages.lock.json') }}`
4. **Restore:** `dotnet restore --verbosity minimal`
5. **Build:** `dotnet build --no-restore --configuration Release`
6. **Test:** `dotnet test --no-build --verbosity normal --configuration Release`

**Optimizaciones:**
- Caching de paquetes NuGet para builds más rápidos
- Build en configuración Release
- Flags `--no-restore` y `--no-build` para evitar pasos redundantes

## Estrategia de Commits

### Convenciones (Conventional Commits)

```
<type>(<scope>): <subject>

<body>

<footer>
```


### Estructura de Branches

```
main (producción)
  └── develop (desarrollo)
       ├── feature/user-authentication
       ├── feature/product-catalog
       ├── bugfix/sqlite-lock-issue
       └── hotfix/critical-security-patch
```

**Estrategia:**
- `main` → producción estable
- `develop` → integración continua
- `feature/*` → nuevas funcionalidades
- `bugfix/*` → correcciones no críticas
- `hotfix/*` → correcciones urgentes a producción

### Ejemplo de Historial de Commits

```bash
git log --oneline --graph

* 8ab23e4 ci(actions): add GitHub Actions workflow under .github/workflows
* a7f3d21 feat(db): configure SQLite with EF Core and volume persistence
* 5c9e8f0 build(docker): add multi-stage Dockerfile for .NET 6.0
* 2d4b1a9 feat(api): initialize ASP.NET Core Web API project
* 1a2b3c4 docs(readme): add project structure and conventions
* 9e8f7d6 chore: initial commit
```