using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Models
{
    public class Pizza 
    {
        public int Id {get; set;}
        public string? Name {get; set;}
        public string? Description {get; set;}
    }

    class PizzaDb : DbContext // representa uma  conexão ou sessão para consultar ou salvar no banco de dados
    {
        public PizzaDb(DbContextOptions options) : base(options) {}
        public DbSet<Pizza> Pizzas { get; set; } = null!;
    }
}