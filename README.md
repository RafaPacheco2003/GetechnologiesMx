# GetechnologiesMx

Backend API desarrollado con .NET 6 implementando Arquitectura Hexagonal (Ports & Adapters), SQLite y Docker.

## 📐 Arquitectura

### Arquitectura Hexagonal (Ports & Adapters)

El proyecto implementa una arquitectura hexagonal limpia que separa las responsabilidades en capas bien definidas:

```
GetechnologiesMx/
├── Domain/                      # Capa de Dominio (Núcleo)
│   ├── Models/                  # Entidades del dominio
│   │   └── Persona.cs
│   ├── Exceptions/              # Excepciones del dominio
│   │   ├── NotFoundException.cs
│   │   └── AlreadyExistsException.cs
│   └── Port/                    # Puertos (Interfaces)
│       ├── In/                  # Puertos de entrada (Use Cases)
│       │   ├── StorePersonaUseCase.cs
│       │   ├── RetrievePersonaUseCase.cs
│       │   ├── UpdatePersonaUseCase.cs
│       │   └── DeletePersonaUseCase.cs
│       └── Out/                 # Puertos de salida (Repositorios)
│           └── PersonaRepositoryPort.cs
│
├── Application/                 # Capa de Aplicación
│   ├── UseCases/                # Implementación de casos de uso
│   │   ├── StorePersonaUseCaseImpl.cs
│   │   ├── RetrievePersonaUseCaseImpl.cs
│   │   ├── UpdatePersonaUseCaseImpl.cs
│   │   └── DeletePersonaUseCaseImpl.cs
│   └── Services/                # Servicios de aplicación
│       └── DirectorioService.cs
│
├── Infrastructure/              # Capa de Infraestructura (Adaptadores)
│   ├── Adapters/
│   │   └── PersonaRepositoryAdapter.cs  # Implementación del puerto de repositorio
│   ├── Persistence/
│   │   ├── AppDbContext.cs              # DbContext de EF Core
│   │   └── Entities/
│   │       └── PersonaEntity.cs         # Entidad de base de datos
│   ├── DTOs/
│   │   ├── Request/
│   │   │   ├── CreatePersonaRequest.cs
│   │   │   └── UpdatePersonaRequest.cs
│   │   └── Response/
│   │       └── PersonaResponse.cs
│   ├── Mappers/
│   │   └── PersonaMapper.cs             # Extension methods para mapeo
│   └── Controllers/
│       └── DirectorioRestService.cs         # API REST Controller
│
└── Tests/                       # Proyecto de pruebas
    ├── Unit/
    │   └── Infrastructure/
    │       └── Mappers/
    │           └── PersonaMapperTests.cs
    └── GetechnologiesMx.Tests.csproj

```

### Principios de Arquitectura Hexagonal

**1. Independencia del Dominio**
- El núcleo del negocio (`Domain`) no depende de frameworks externos
- Las reglas de negocio están aisladas en entidades y casos de uso
- Los puertos definen contratos sin implementación

**2. Inversión de Dependencias**
- Las capas externas dependen de las internas, nunca al revés
- Los adaptadores implementan los puertos definidos en el dominio
- Inyección de dependencias en `Program.cs`

**3. Separación de Concerns**
- **Domain:** Lógica de negocio pura
- **Application:** Orquestación de casos de uso
- **Infrastructure:** Detalles técnicos (DB, HTTP, etc.)

### Flujo de Datos

```
HTTP Request
    ↓
DirectorioRestService (Infrastructure)
    ↓
DirectorioService (Application)
    ↓
StorePersonaUseCase (Domain Port In)
    ↓
StorePersonaUseCaseImpl (Application)
    ↓
PersonaRepositoryPort (Domain Port Out)
    ↓
PersonaRepositoryAdapter (Infrastructure)
    ↓
AppDbContext → SQLite
```

### Stack Tecnológico
- **Framework:** ASP.NET Core 6.0
- **Arquitectura:** Hexagonal (Ports & Adapters)
- **Base de datos:** SQLite 
- **ORM:** Entity Framework Core 6.0.36
- **Testing:** xUnit 2.4.1 + Moq 4.20.72
- **Containerización:** Docker + Docker Compose
- **CI/CD:** GitHub Actions
- **Documentación API:** Swagger/OpenAPI

### Patrones de Diseño Implementados

- **Repository Pattern:** `PersonaRepositoryPort` + `PersonaRepositoryAdapter`
- **Use Case Pattern:** Casos de uso como clases independientes
- **DTO Pattern:** Separación entre entidades de dominio y DTOs de API
- **Mapper Pattern:** Extension methods para transformaciones
- **Dependency Injection:** Configuración en `Program.cs`

## 🧪 Testing

### Estrategia de Testing

**Framework:** xUnit 2.4.1  
**Mocking:** Moq 4.20.72  
**Cobertura actual:** Mappers (100%)

### Estructura de Tests

```
Tests/
├── GetechnologiesMx.Tests.csproj
└── Unit/
    └── Infrastructure/
        └── Mappers/
            └── PersonaMapperTests.cs (7 tests ✓)
```

### Tests Implementados

#### PersonaMapperTests (7/7 ✓)

**Mapeo Request → Model:**
```csharp
[Fact]
public void ToModel_FromRequest_ShouldMapCorrectly()
{
    // Arrange
    CreatePersonaRequest request = new CreatePersonaRequest(
        Nombre: "Juan",
        ApellidoPaterno: "Pérez",
        ApellidoMaterno: "García",
        Identificacion: "RFC123456"
    );

    // Act
    Persona persona = request.ToModel();

    // Assert
    Assert.NotNull(persona);
    Assert.Equal("Juan", persona.Nombre);
    Assert.Equal("RFC123456", persona.Identificacion);
}
```

**Tests cubiertos:**
1. ✅ `ToModel_FromRequest_ShouldMapCorrectly` - Request → Model
2. ✅ `ToEntity_FromModel_ShouldMapCorrectly` - Model → Entity
3. ✅ `ToModel_FromEntity_ShouldMapCorrectly` - Entity → Model
4. ✅ `ToResponse_FromEntity_ShouldMapCorrectly` - Entity → Response
5. ✅ `ToResponse_FromModel_ShouldMapCorrectly` - Model → Response
6. ✅ `ToResponseList_FromEntityList_ShouldMapCorrectly` - List Entity → List Response
7. ✅ `ToModelList_FromEntityList_ShouldMapCorrectly` - List Entity → List Model

### Configuración del Proyecto de Tests

**GetechnologiesMx.Tests.csproj:**
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <IsPackable>false</IsPackable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.1.0" />
    <PackageReference Include="xunit" Version="2.4.1" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.3" />
    <PackageReference Include="coverlet.collector" Version="3.1.2" />
    <PackageReference Include="Moq" Version="4.20.72" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\GetechnologiesMx.csproj" />
  </ItemGroup>
</Project>
```

### Ejecutar Tests

```bash
# Ejecutar todos los tests
dotnet test

# Con detalle verbose
dotnet test --verbosity normal

# Solo el proyecto de tests
dotnet test Tests/GetechnologiesMx.Tests.csproj

# Con cobertura de código
dotnet test /p:CollectCoverage=true
```

### Resultado de Tests

```
Passed!  - Failed:     0, Passed:     7, Skipped:     0, Total:     7, Duration: 9 ms
```

### Patrones de Testing Utilizados

**AAA Pattern (Arrange-Act-Assert):**
```csharp
// Arrange - Preparar datos de prueba
var request = new CreatePersonaRequest(...);

// Act - Ejecutar el método bajo prueba
var result = request.ToModel();

// Assert - Verificar el resultado
Assert.NotNull(result);
Assert.Equal("Expected", result.Property);
```

**Mocking con Moq:**
```csharp
// Crear mock del repositorio
var mockRepository = new Mock<PersonaRepositoryPort>();

// Configurar comportamiento esperado
mockRepository
    .Setup(repo => repo.FindById(1))
    .ReturnsAsync(new Persona { Id = 1, Nombre = "Test" });

// Verificar que se llamó al método
mockRepository.Verify(repo => repo.FindById(1), Times.Once);
```

### Próximos Tests a Implementar

- [ ] Tests de Casos de Uso (StorePersonaUseCaseImpl, etc.)
- [ ] Tests de Servicios (DirectorioService)
- [ ] Tests de Controladores (DirectorioRestService)
- [ ] Tests de Integración con base de datos en memoria
- [ ] Tests de validaciones y excepciones


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
6. **Run Unit Tests:** `dotnet test Tests/GetechnologiesMx.Tests.csproj --no-build --verbosity normal --configuration Release --logger "trx;LogFileName=test-results.trx"`
7. **Publish Test Results:** `dorny/test-reporter@v1` - Publica resultados TRX en GitHub Actions

**Optimizaciones:**
- Caching de paquetes NuGet para builds más rápidos
- Build en configuración Release
- Flags `--no-restore` y `--no-build` para evitar pasos redundantes
- Reportes de tests visibles en la UI de GitHub Actions
- Tests se ejecutan siempre (`if: always()`) para ver resultados incluso si fallan

## 🔌 API Endpoints

### Base URL
```
http://localhost:5001/api
```

### Health Check

```bash
curl -X GET http://localhost:5001/api/health
```

### Personas

#### Crear Persona
```bash
curl -X POST http://localhost:5001/api/personas \
  -H "Content-Type: application/json" \
  -d '{
    "nombre": "Juan",
    "apellidoPaterno": "Pérez",
    "apellidoMaterno": "García",
    "identificacion": "RFC12345"
  }'
```

#### Crear Persona sin Apellido Materno (opcional)
```bash
curl -X POST http://localhost:5001/api/personas \
  -H "Content-Type: application/json" \
  -d '{
    "nombre": "María",
    "apellidoPaterno": "López",
    "apellidoMaterno": null,
    "identificacion": "RFC67890"
  }'
```

#### Listar Todas las Personas
```bash
curl -X GET http://localhost:5001/api/personas
```

#### Buscar Persona por Identificación
```bash
curl -X GET http://localhost:5001/api/personas/identificacion/RFC12345
```

#### Eliminar Persona por Identificación
```bash
curl -X DELETE http://localhost:5001/api/personas/identificacion/RFC12345
```

### Facturas

#### Crear Factura
```bash
curl -X POST http://localhost:5001/api/facturas \
  -H "Content-Type: application/json" \
  -d '{
    "numeroFactura": "FAC-001",
    "monto": 1500.50,
    "fecha": "2026-01-21T10:30:00",
    "personaId": 1
  }'
```

#### Crear Segunda Factura
```bash
curl -X POST http://localhost:5001/api/facturas \
  -H "Content-Type: application/json" \
  -d '{
    "numeroFactura": "FAC-002",
    "monto": 2500.75,
    "fecha": "2026-01-21T11:00:00",
    "personaId": 1
  }'
```

#### Listar Facturas de una Persona
```bash
curl -X GET http://localhost:5001/api/facturas/persona/1
```

### Validaciones

#### Intentar Crear Persona con Identificación Duplicada (debe fallar)
```bash
curl -X POST http://localhost:5001/api/personas \
  -H "Content-Type: application/json" \
  -d '{
    "nombre": "Pedro",
    "apellidoPaterno": "Gómez",
    "apellidoMaterno": "Ruiz",
    "identificacion": "RFC12345"
  }'
```

#### Intentar Crear Factura con Número Duplicado (debe fallar)
```bash
curl -X POST http://localhost:5001/api/facturas \
  -H "Content-Type: application/json" \
  -d '{
    "numeroFactura": "FAC-001",
    "monto": 3000.00,
    "fecha": "2026-01-21T12:00:00",
    "personaId": 1
  }'
```

### Flujo Completo de Prueba

```bash
# 1. Health check
curl -X GET http://localhost:5001/api/health

# 2. Crear persona
curl -X POST http://localhost:5001/api/personas \
  -H "Content-Type: application/json" \
  -d '{"nombre": "Juan", "apellidoPaterno": "Pérez", "apellidoMaterno": "García", "identificacion": "RFC12345"}'

# 3. Listar todas las personas
curl -X GET http://localhost:5001/api/personas

# 4. Buscar persona por identificación
curl -X GET http://localhost:5001/api/personas/identificacion/RFC12345

# 5. Crear factura para la persona
curl -X POST http://localhost:5001/api/facturas \
  -H "Content-Type: application/json" \
  -d '{"numeroFactura": "FAC-001", "monto": 1500.50, "fecha": "2026-01-21T10:30:00", "personaId": 1}'

# 6. Listar facturas de la persona
curl -X GET http://localhost:5001/api/facturas/persona/1

# 7. Eliminar persona
curl -X DELETE http://localhost:5001/api/personas/identificacion/RFC12345

# 8. Verificar eliminación
curl -X GET http://localhost:5001/api/personas
```

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