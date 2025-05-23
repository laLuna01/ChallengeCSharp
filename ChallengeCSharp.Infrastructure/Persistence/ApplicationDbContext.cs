using ChallengeCSharp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChallengeCSharp.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<Genero> Generos { get; set; }
    public DbSet<Pais> Paises { get; set; }
    public DbSet<Estado> Estados { get; set; }
    public DbSet<Cidade> Cidades { get; set; }
    public DbSet<Bairro> Bairros { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Dentista> Dentistas { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Consulta> Consultas { get; set; }
    public DbSet<Tratamento> Tratamentos { get; set; }
    public DbSet<Sinistro> Sinistros { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genero>().ToTable("CH_GENERO");
        modelBuilder.Entity<Genero>().HasKey(g => g.ID_GENERO);
        
        modelBuilder.Entity<Pais>().ToTable("CH_PAIS");
        modelBuilder.Entity<Pais>().HasKey(p => p.COD_PAIS);
        
        modelBuilder.Entity<Estado>().ToTable("CH_ESTADO");
        modelBuilder.Entity<Estado>().HasKey(e => e.COD_ESTADO);
        modelBuilder.Entity<Estado>()
            .HasOne(e => e.Pais)
            .WithMany(p => p.Estados)
            .HasForeignKey(e => e.COD_PAIS);
        
        modelBuilder.Entity<Cidade>().ToTable("CH_CIDADE");
        modelBuilder.Entity<Cidade>().HasKey(e => e.COD_CIDADE);
        modelBuilder.Entity<Cidade>()
            .HasOne(e => e.Estado)
            .WithMany(p => p.Cidades)
            .HasForeignKey(e => e.COD_ESTADO);
        
        modelBuilder.Entity<Bairro>().ToTable("CH_BAIRRO");
        modelBuilder.Entity<Bairro>().HasKey(e => e.COD_BAIRRO);
        modelBuilder.Entity<Bairro>()
            .HasOne(e => e.Cidade)
            .WithMany(p => p.Bairros)
            .HasForeignKey(e => e.COD_CIDADE);
        
        modelBuilder.Entity<Endereco>().ToTable("CH_ENDERECO");
        modelBuilder.Entity<Endereco>().HasKey(e => e.COD_ENDERECO);
        modelBuilder.Entity<Endereco>()
            .HasOne(e => e.Bairro)
            .WithMany(p => p.Enderecos)
            .HasForeignKey(e => e.COD_BAIRRO);
        
        modelBuilder.Entity<Dentista>().ToTable("CH_DENTISTA");
        modelBuilder.Entity<Dentista>().HasKey(e => e.ID_DENTISTA);
        modelBuilder.Entity<Dentista>()
            .HasOne(e => e.Endereco)
            .WithMany(p => p.Dentistas)
            .HasForeignKey(e => e.ENDERECO_ID_ENDERECO);
        modelBuilder.Entity<Dentista>()
            .HasOne(e => e.Genero)
            .WithMany(p => p.Dentistas)
            .HasForeignKey(e => e.GENERO_ID_GENERO);
        
        modelBuilder.Entity<Paciente>().ToTable("CH_PACIENTE");
        modelBuilder.Entity<Paciente>().HasKey(e => e.ID_PACIENTE);
        modelBuilder.Entity<Paciente>()
            .HasOne(e => e.Endereco)
            .WithMany(p => p.Pacientes)
            .HasForeignKey(e => e.ENDERECO_ID_ENDERECO);
        modelBuilder.Entity<Paciente>()
            .HasOne(e => e.Genero)
            .WithMany(p => p.Pacientes)
            .HasForeignKey(e => e.GENERO_ID_GENERO);
        
        modelBuilder.Entity<Consulta>().ToTable("CH_CONSULTA");
        modelBuilder.Entity<Consulta>().HasKey(e => e.ID_CONSULTA);
        modelBuilder.Entity<Consulta>()
            .HasOne(e => e.Paciente)
            .WithMany(p => p.Consultas)
            .HasForeignKey(e => e.PACIENTE_ID_PACIENTE);
        modelBuilder.Entity<Consulta>()
            .HasOne(e => e.Dentista)
            .WithMany(p => p.Consultas)
            .HasForeignKey(e => e.DENTISTA_ID_DENTISTA);
        
        modelBuilder.Entity<Tratamento>().ToTable("CH_TRATAMENTO");
        modelBuilder.Entity<Tratamento>().HasKey(e => e.ID_TRATAMENTO);
        modelBuilder.Entity<Tratamento>()
            .HasOne(e => e.Consulta)
            .WithMany(p => p.Tratamentos)
            .HasForeignKey(e => e.CONSULTA_ID_CONSULTA);
        
        modelBuilder.Entity<Sinistro>().ToTable("CH_SINISTRO");
        modelBuilder.Entity<Sinistro>().HasKey(e => e.ID_SINISTRO);
        modelBuilder.Entity<Sinistro>()
            .HasOne(e => e.Consulta)
            .WithMany(p => p.Sinistros)
            .HasForeignKey(e => e.CONSULTA_ID_CONSULTA);
    }
}