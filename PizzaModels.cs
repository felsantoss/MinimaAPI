using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace PizzaStore.Models
{
    public class Pizza 
    {
        [Key]
        public int Id {get; set;}

        [MaxLength(50, ErrorMessage = "Este campo deve ter no máximo 60 caracteres")]
        [MinLength(5, ErrorMessage = "Este campo deve ter no minimo 5 caracteres")]
        public string? Name {get; set;}

        [MaxLength(50, ErrorMessage = "Este campo deve ter no máximo 60 caracteres")]
        [MinLength(5, ErrorMessage = "Este campo deve ter no minimo 5 caracteres")]
        public string? Description {get; set;}
    }

    class PizzaDb : DbContext // representa uma  conexão ou sessão para consultar ou salvar no banco de dados
    {
        public PizzaDb(DbContextOptions options) : base(options) {}
        public DbSet<Pizza> Pizzas { get; set; } = null!;
    }
}