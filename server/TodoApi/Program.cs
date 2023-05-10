using Microsoft.EntityFrameworkCore;
using TodoApi.models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoDbContext>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => options.SerializeAsV2 = true);
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

// GET all tasks
app.MapGet("/items", async (ToDoDbContext context) =>
{
    return await context.Items.ToListAsync();
});

// POST a new task
app.MapPost("/items", async (ToDoDbContext context, Item i) =>
{
    context.Items.Add(i);
    await context.SaveChangesAsync();
    return i;
});

// Update a task
app.MapPut("/items/{id}/{isco}", async (ToDoDbContext context, int id, bool isco) =>
{
    var item = await context.Items.FindAsync(id);
    item.IsComplete = isco;
    await context.SaveChangesAsync();
    return item;
});

// DELETE a task
app.MapDelete("/items/{id}", async (ToDoDbContext context, int id) =>
{
    var a = await context.Items.FindAsync(id);
    if (a is null)
        return Results.NotFound();
    context.Items.Remove(a);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();