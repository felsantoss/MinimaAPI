namespace PizzaStore.Models;
using Microsoft.EntityFrameworkCore;
public class Pizza 
{
    public int Id {get; set;}
    public string ? Name {get; set;}
    public string ? Description {get; set;}

    class PizzaDb : DbContext // DbContext representa uma sessão ou conexão usada para consultar ou salvar info no banco de  dados
{
    public PizzaDb(DbContextOptions options) : base(options) { }
    public DbSet<Pizza> Pizzas {get; set;} = null!;
}
}