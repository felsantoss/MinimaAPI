using Microsoft.OpenApi.Models;
using PizzaStore.DB;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<PizzaDb>(options => options.UseInMemoryDatabase("items"));
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
    
app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.ToListAsync()); // retorna um lista de items

/* app.MapGet("/pizzas", () => PizzaDB.GetPizzas());
app.MapPost("/pizzas", (Pizza pizza) => PizzaDB.CreatePizza(pizza));
app.MapPut("/pizzas", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));
app.MapDelete("/pizzas/{id}", (int id) => PizzaDB.RemovePizza(id)); */

app.Run();
