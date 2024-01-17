using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);
// string para conexão com o banco de dados
var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data source=Pizzas.db"; 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlite<PizzaDb>(connectionString);
builder.Services.AddSwaggerGen(c => 
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "PizzaStore API", 
                                          Description = "Making the Pizzas you love", 
                                          Version = "v1" });
});
    
var app = builder.Build();
    
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});

// Retorna uma lista de items    
app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.ToListAsync());

// Cria um item no banco de dados
app.MapPost("/pizza", async (PizzaDb db, Pizza pizza) =>
{
   await db.Pizzas.AddAsync(pizza);
   await db.SaveChangesAsync();
   return Results.Created($"/pizza/{pizza.Id}", pizza);
});

// Retorna um único item especificando o ID
app.MapGet("/pizza/{id}", async (PizzaDb db, int id) => await db.Pizzas.FindAsync(id));

// Atualiza um item no banco de dados especificando um ID
app.MapPut("/pizza/{id}", async (PizzaDb db, Pizza updatepizza, int id) => 
{
   var pizza = await db.Pizzas.FindAsync(id);

   if (pizza is null)
   return Results.NotFound();

   pizza.Name = updatepizza.Name;
   pizza.Description = updatepizza.Description;

   await db.SaveChangesAsync();
   return Results.NoContent();
});

// Exclui um item no banco de dados especificando um ID
app.MapDelete("/pizza/{id}", async (PizzaDb db, int id) => 
{
   var pizza = await db.Pizzas.FindAsync(id);

   if (pizza is null)
   return Results.NotFound();

   db.Pizzas.Remove(pizza);
   await db.SaveChangesAsync();
   return Results.Ok();
});

app.Run();