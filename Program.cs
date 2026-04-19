// using Microsoft.EntityFrameworkCore;
// using TodoApi.Models;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.

// builder.Services.AddControllers();
// // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

// builder.Services.AddDbContext<TodoContext>(opt =>
//     opt.UseInMemoryDatabase("TodoList"));

// var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
//     app.UseSwaggerUi(options =>
//     {
//         options.DocumentPath = "/openapi/v1.json";
//     });
// }

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();




using System.Text;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt")
.Get<JwtSettings>() ?? throw new Exception("Jwt is missing");
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication("Bearer")
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,

        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("ToDoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAdminService, AdminServise>();
builder.Services.AddScoped<PasswordService>();
// builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("AllowAll");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();





if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.MapGet("/product", async (AppDbContext db) =>
await db.Product.ToListAsync()
);

app.MapGet("product/{id}", async (int id, AppDbContext db) =>
  await db.Product.FindAsync(id) is Product product ?
   Results.Ok(product)
   : Results.NotFound()
);

app.MapPost("/product", async (Product product, AppDbContext db) =>
{
    db.Product.Add(product);
    await db.SaveChangesAsync();

    return Results.Created($"/product/{product.Id}", product);
}

);

app.MapPut("/product/{id}", async (int id, AppDbContext db, Product inputProduct) =>
{
    var product = await db.Product.FindAsync(id);
    if (product is null) return Results.NotFound();

    product.Name = inputProduct.Name;
    product.Price = inputProduct.Price;
    product.Stock = inputProduct.Stock;

    db.SaveChanges();

    return Results.Ok(inputProduct);

});

app.MapDelete("/product/{id}", async (int id, AppDbContext db) =>
{
    if (await db.Product.FindAsync(id) is Product product)
    {
        db.Product.Remove(product);
        db.SaveChanges();
        return Results.NotFound();
    }

    return Results.NotFound();
}
);


app.MapGet("/experience", async (AppDbContext db) =>
await db.Experience.ToListAsync()

);

app.MapGet("/experience/{id}", async (int id, AppDbContext db) =>

    await db.Experience.FindAsync(id)
    is Experience experience
    ? Results.Ok(experience)
    : Results.NotFound()


);

app.MapPost("/experience", async (Experience experience, AppDbContext db) =>
{
    db.Experience.Add(experience);
    db.SaveChanges();

    return Results.Created("/experience/{experience.Id}", experience);
});


app.MapPut("/experience/{id}", async (int id, AppDbContext db, Experience inputExperience) =>
{

    var product = await db.Experience.FindAsync(id);
    if (product is null) return Results.NotFound();

    product.Experiences = inputExperience.Experiences;
    product.Year = inputExperience.Year;

    db.SaveChanges();

    inputExperience.Id = id;

    return Results.Ok(inputExperience);

});

app.MapDelete("/experience/{id}", async (int id, AppDbContext db) =>
{
    if (await db.Experience.FindAsync(id) is Experience experience)
    {
        db.Experience.Remove(experience);
        return Results.NotFound();
    }


    return Results.NotFound();
});

// Writing the user data endpoint
// Taking the user endpoint for this quest

// GET /todos
// GET /todos?userId=1
// POST /todos
// PATCH /todos/{id} (mark complete)
// DELETE /todos/{id}

// app.MapGet("/user", async (AppDbContext db) =>
//  await db.User.ToListAsync());

// app.MapGet("/user/{id}", async (int id, AppDbContext db) =>
// //  Get id
// // Checking the spesifict user
// // Check if it is exist, if not return 
// // and then return the data

// {
//     var item = await db.User.FindAsync(id);
//     if (item is null)
//         return Results.NotFound();

//     return Results.Ok(item);
// }

// );


// app.MapPost("/user", async (User user, AppDbContext db) =>

// // Getting user data
// // Adding it and saving
// // then return ok 
// {
//     db.User.Add(user);
//     await db.SaveChangesAsync();

//     return Results.Created("/user/{user.id}", user);
// }
// );

// // Get id, check data, if not return 
// // If founded, then change it with the new body data
// app.MapPut("/user/{id}", async (int id, User inputUser, AppDbContext db)=>
// {
//     var item = await db.User.FindAsync(id);
//     if(item is null) return Results.NotFound();

//     item.Email = inputUser.Email;
//     item.Name = inputUser.Name;

//     await db.SaveChangesAsync();

//     return Results.NoContent();

// }
// );



// // Get the id to find the data
// // Check the user and then change the name of the user
// // Save changes and return nothing

// app.MapPatch("/user/{id}", async(int id, AppDbContext db, User inputUser)=>
// {
//     var item = await db.User.FindAsync(id);

//     if(item is null) return Results.NotFound();

//     if(string.IsNullOrWhiteSpace(inputUser.Name))
//     return Results.BadRequest("Name cannot be found");

//     item.Name = inputUser.Name;

//     await db.SaveChangesAsync();

//     return Results.NoContent();
// }


// );



// // Special learning to check

// // POST /users/5?active=true
// // Authorization: Bearer token123
// // Content-Type: application/json

// // {
// //   "name": "John"
// // }

// app.MapPost("/user/{id}", async (int id, bool active, User user, HttpRequest request)=>
// {
//     var authHeader = request.Headers["Authorization"].ToString();

//     return Results.Ok(new
//     {
//       ResultId= id,
//       QueryActive= active,
//       HeadersAuth= authHeader,
//       BodyName= user.Name
//     }
//     );
// }

// );



app.Run();


// I created dotnet program
// I called api routes, used get, post, put
// I added ram database which is sendning on the ram each restart
// I created database chema and using that schemeI adding the data with dependency injection
// I then sending the data
// Used async for getting the id from the get to find the single data,
// using finding and listing data that gets from the method from the dotnet




// app.MapGet("/todoApi", async (ToDoDb db) =>
// await db.Todos.ToListAsync()
// );
// app.MapGet("/todoApi/{id}", async (int id, ToDoDb db) =>
//     await db.Todos.FindAsync(id) is Todo todo
//         ? Results.Ok(todo)
//         : Results.NotFound()

// );
// app.MapPost("/todoApi", async (Todo todo, ToDoDb db) =>
// {
//     db.Todos.Add(todo);
//     await db.SaveChangesAsync();

//     return Results.Created($"/todoitems/{todo.Id}", todo);
// });

// app.MapPut("/todoApi/{id}", async (int id, Todo inputTodo, ToDoDb db) =>
// {
//     // Get the data with id\
//     // the set the data to the id same data
//     // and show the changed data with return
//     var todo = await db.Todos.FindAsync(id);
//     if (todo is null) return Results.NotFound();
//     todo.Name = inputTodo.Name;
//     todo.IsComplete = inputTodo.IsComplete;

//     await db.SaveChangesAsync();

//     return Results.Ok(inputTodo);

// });

// app.MapDelete("/todoApi/{id}", async (int id, ToDoDb db) =>
// {
//     if (await db.Todos.FindAsync(id) is Todo todo)
//     {
//         db.Todos.Remove(todo);
//         db.SaveChanges();
//         return Results.NoContent();
//     }

//     return Results.NotFound();
// });
// app.MapGet("/", () => "Hello world");
// app.MapGet("/users", () => "Users are here!");
// app.MapPost("/", (User name) => { return name; });