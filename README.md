```bash
├── demo-soap-api
│ ├── Controllers
│ ├── Data
│ │ ├── Contexts
│ │ └── Repositories
│ ├── DTOs
│ ├── Entities
│ ├── Interfaces
│ │ ├── IControllers
│ │ ├── IRepositories
│ │ └── IServices
│ ├── Mapping
│ ├── Services
```

## API Folder structure

Controllers/: Contains API endpoints  
Data/: Houses database contexts and repository implementations  
DTOs/: Data Transfer Objects for API requests/responses  
Entities/: Domain entities (database models)  
Interfaces/: Defines interfaces, including for repositories, services,controllers  
Mapping/: Contains AutoMapper profiles  
Services/: Business logic services
