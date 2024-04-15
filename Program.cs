using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos
{
    internal class Program
    {
        public class Veiculo
        {
            [Key]
            public int Id { get; set; }
            public string? Modelo { get; set; }
            public int Ano { get; set; }
            public string? Placa { get; set; }
            public string? Status { get; set; } // Disponível, Reservado, Em Manutenção
        }

        public class Cliente
        {
            [Key]
            public int Id { get; set; }
            public string? Nome { get; set; }
            public string? CPF { get; set; }
            public string? Endereco { get; set; }
        }

        public class Reserva
        {
            [Key]
            public int Id { get; set; }
            public int ClienteId { get; set; }
            [ForeignKey("ClienteId")]
            public Cliente? Cliente { get; set; }
            public int VeiculoId { get; set; }
            [ForeignKey("VeiculoId")]
            public Veiculo? Veiculo { get; set; }
            public DateTime DataInicio { get; set; }
            public DateTime DataFim { get; set; }
            public decimal Custo { get; set; }
        }

        public class ApplicationContext : DbContext
        {
            public DbSet<Veiculo> Veiculos { get; set; }
            public DbSet<Cliente> Clientes { get; set; }
            public DbSet<Reserva> Reservas { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                 optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS01;Database=LocadoraVeiculos;Trusted_Connection=True;TrustServerCertificate=true");
            }
        }

        static void Main(string[] args)
        {
            using (var context = new ApplicationContext())
            {
                // Adicionar um cliente
                var cliente = new Cliente { Nome = "Patricia Ramos", CPF = "123.456.789-01", Endereco = "Rua Marte, 555" };
                context.Clientes.Add(cliente);
                context.SaveChanges();

                // Adicionar um veículo
                var veiculo = new Veiculo { Modelo = "Fiat Palio", Ano = 2016, Placa = "ABC-1235", Status = "Reservado" };
                context.Veiculos.Add(veiculo);
                context.SaveChanges();

                // Criar uma reserva
                var reserva = new Reserva { ClienteId = cliente.Id, VeiculoId = veiculo.Id, DataInicio = DateTime.Now, DataFim = DateTime.Now.AddDays(2), Custo = 350.00M };
                context.Reservas.Add(reserva);
                context.SaveChanges();
            }
        }


    }

}
