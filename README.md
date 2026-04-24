https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-10.0&tabs=visual-studio-code


CRUD (DONE)
   ↓
DTOs (next)
   ↓
Password Hashing
   ↓
Login System
   ↓
JWT Authentication
   ↓
Authorization (roles)
   ↓
Production architecture

var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });


    📁 src/
├── 📁 MyBlog.Domain/          (Entities, Interfaces, Enums)
│   ├── 📁 Entities/           # Post.cs, Comment.cs, User.cs
│   ├── 📁 Interfaces/         # IPostRepository, IAuthService
│   └── 📁 Common/             # BaseEntity.cs
│
├── 📁 MyBlog.Application/     (Business Logic, DTOs, Mappings)
│   ├── 📁 DTOs/               # PostDto.cs, LoginRequest.cs
│   ├── 📁 Services/           # PostService.cs, CommentService.cs
│   ├── 📁 Validators/         # FluentValidation klasları
│   └── 📁 Mappings/           # AutoMapper profilləri
│
├── 📁 MyBlog.Infrastructure/  (Data, Identity, External Services)
│   ├── 📁 Data/               # AppDbContext.cs
│   ├── 📁 Repositories/       # PostRepository.cs
│   ├── 📁 Identity/           # JwtTokenGenerator.cs
│   └── 📁 Migrations/         # EF Core migrations
│
└── 📁 MyBlog.API/             (Controllers, Middlewares, Config)
    ├── 📁 Controllers/        # PostsController.cs, AuthController.cs
    ├── 📁 Middlewares/        # ErrorHandlerMiddleware.cs
    └── 📄 Program.cs          # Dependency Injection & Pipeline
