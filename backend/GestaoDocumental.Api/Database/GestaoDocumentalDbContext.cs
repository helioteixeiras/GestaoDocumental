using System;
using System.Collections.Generic;
using GestaoDocumental.Api.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaoDocumental.Api.Database;

public partial class GestaoDocumentalDbContext : DbContext
{
    public GestaoDocumentalDbContext()
    {
    }

    public GestaoDocumentalDbContext(DbContextOptions<GestaoDocumentalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClassificacaoDocumento> ClassificacaoDocumentos { get; set; }

    public virtual DbSet<Colaborador> Colaboradors { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Direcao> Direcaos { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<DocumentoAnexo> DocumentoAnexos { get; set; }

    public virtual DbSet<DocumentoHistorico> DocumentoHistoricos { get; set; }

    public virtual DbSet<EstadoColaborador> EstadoColaboradors { get; set; }

    public virtual DbSet<EstadoDocumento> EstadoDocumentos { get; set; }

    public virtual DbSet<EstadoLogin> EstadoLogins { get; set; }

    public virtual DbSet<Fornecedor> Fornecedors { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Pais> Pais { get; set; }

    public virtual DbSet<Perfil> Perfils { get; set; }

    public virtual DbSet<PostoTrabalho> PostoTrabalhos { get; set; }

    public virtual DbSet<Provincia> Provincia { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<TipoDocumentoColaborador> TipoDocumentoColaboradors { get; set; }

    public virtual DbSet<TramitacaoDocumento> TramitacaoDocumentos { get; set; }

    public virtual DbSet<UsuarioSistema> UsuarioSistemas { get; set; }

 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClassificacaoDocumento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Classifi__3214EC07029D907A");

            entity.ToTable("ClassificacaoDocumento");

            entity.Property(e => e.Nome).HasMaxLength(80);
        });

        modelBuilder.Entity<Colaborador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Colabora__3214EC07DDF45E41");

            entity.ToTable("Colaborador");

            entity.HasIndex(e => e.NumDocumento, "UQ__Colabora__11150A80B24E655E").IsUnique();

            entity.Property(e => e.Cargo).HasMaxLength(150);
            entity.Property(e => e.DataNascimento).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Endereco).HasMaxLength(250);
            entity.Property(e => e.Nome).HasMaxLength(150);
            entity.Property(e => e.NumDocumento).HasMaxLength(50);
            entity.Property(e => e.NumMecanografo).HasMaxLength(50);

            entity.HasOne(d => d.Estado).WithMany(p => p.Colaboradors)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Colaborad__Estad__5AEE82B9");

            entity.HasOne(d => d.Genero).WithMany(p => p.Colaboradors)
                .HasForeignKey(d => d.GeneroId)
                .HasConstraintName("FK__Colaborad__Gener__59FA5E80");

            entity.HasOne(d => d.Nacionalidade).WithMany(p => p.Colaboradors)
                .HasForeignKey(d => d.NacionalidadeId)
                .HasConstraintName("FK__Colaborad__Nacio__5812160E");

            entity.HasOne(d => d.Perfil).WithMany(p => p.Colaboradors)
                .HasForeignKey(d => d.PerfilId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Colaborad__Perfi__5DCAEF64");

            entity.HasOne(d => d.PostoTrabalho).WithMany(p => p.Colaboradors)
                .HasForeignKey(d => d.PostoTrabalhoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Colaborad__Posto__59063A47");

            entity.HasOne(d => d.TipoDocumentoColaborador).WithMany(p => p.Colaboradors)
                .HasForeignKey(d => d.TipoDocumentoColaboradorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Colaborad__TipoD__571DF1D5");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departam__3214EC077DA0D7C6");

            entity.ToTable("Departamento");

            entity.Property(e => e.Nome).HasMaxLength(100);
            entity.Property(e => e.Sigla).HasMaxLength(20);

            entity.HasOne(d => d.Direcao).WithMany(p => p.Departamentos)
                .HasForeignKey(d => d.DirecaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Departame__Direc__5441852A");
        });

        modelBuilder.Entity<Direcao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Direcao__3214EC0720D379A2");

            entity.ToTable("Direcao");

            entity.Property(e => e.Nome).HasMaxLength(100);
            entity.Property(e => e.Sigla).HasMaxLength(20);
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC07B81173F0");

            entity.ToTable("Documento");

            entity.HasIndex(e => e.NumeroDocumento, "UQ__Document__A4202588CF4E8E6F").IsUnique();

            entity.Property(e => e.CodigoArquivo).HasMaxLength(100);
            entity.Property(e => e.DataAtualizacao).HasColumnType("datetime");
            entity.Property(e => e.DataCriacao).HasColumnType("datetime");
            entity.Property(e => e.DataDocumento).HasColumnType("datetime");
            entity.Property(e => e.DataRecepcao).HasColumnType("datetime");
            entity.Property(e => e.LocalizacaoFisica).HasMaxLength(250);
            entity.Property(e => e.NumeroDocumento).HasMaxLength(100);
            entity.Property(e => e.PalavrasChave).HasMaxLength(250);
            entity.Property(e => e.PrazoResposta).HasColumnType("datetime");
            entity.Property(e => e.ReferenciaExterna).HasMaxLength(150);
            entity.Property(e => e.Titulo).HasMaxLength(250);
            entity.Property(e => e.VersaoAtual).HasDefaultValue(1);

            entity.HasOne(d => d.Classificacao).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.ClassificacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__Class__5FB337D6");

            entity.HasOne(d => d.ColaboradorCriador).WithMany(p => p.DocumentoColaboradorCriadors)
                .HasForeignKey(d => d.ColaboradorCriadorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__Colab__628FA481");

            entity.HasOne(d => d.DirecaoOrigem).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.DirecaoOrigemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__Direc__619B8048");

            entity.HasOne(d => d.EstadoDocumento).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.EstadoDocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__Estad__60A75C0F");

            entity.HasOne(d => d.Fornecedor).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.FornecedorId)
                .HasConstraintName("FK__Documento__Forne__6383C8BA");

            entity.HasOne(d => d.TipoDocumento).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.TipoDocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__TipoD__5EBF139D");

            entity.HasOne(d => d.UtilizadorAtualizacao).WithMany(p => p.DocumentoUtilizadorAtualizacaos)
                .HasForeignKey(d => d.UtilizadorAtualizacaoId)
                .HasConstraintName("FK__Documento__Utili__6477ECF3");
        });

        modelBuilder.Entity<DocumentoAnexo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC07B7B11007");

            entity.ToTable("DocumentoAnexo");

            entity.Property(e => e.Caminho).HasMaxLength(500);
            entity.Property(e => e.DataUpload).HasColumnType("datetime");
            entity.Property(e => e.Extensao).HasMaxLength(20);
            entity.Property(e => e.HashSha256)
                .HasMaxLength(500)
                .HasColumnName("HashSHA256");
            entity.Property(e => e.NomeFisico).HasMaxLength(250);
            entity.Property(e => e.NomeOriginal).HasMaxLength(250);

            entity.HasOne(d => d.Documento).WithMany(p => p.DocumentoAnexos)
                .HasForeignKey(d => d.DocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__Docum__656C112C");
        });

        modelBuilder.Entity<DocumentoHistorico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC072769FB03");

            entity.ToTable("DocumentoHistorico");

            entity.Property(e => e.Acao).HasMaxLength(150);
            entity.Property(e => e.DataAcao).HasColumnType("datetime");

            entity.HasOne(d => d.Documento).WithMany(p => p.DocumentoHistoricos)
                .HasForeignKey(d => d.DocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__Docum__66603565");

            entity.HasOne(d => d.Utilizador).WithMany(p => p.DocumentoHistoricos)
                .HasForeignKey(d => d.UtilizadorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__Utili__6754599E");
        });

        modelBuilder.Entity<EstadoColaborador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EstadoCo__3214EC07BEA7D5A8");

            entity.ToTable("EstadoColaborador");

            entity.Property(e => e.Nome).HasMaxLength(50);
        });

        modelBuilder.Entity<EstadoDocumento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EstadoDo__3214EC0722AE2348");

            entity.ToTable("EstadoDocumento");

            entity.Property(e => e.Nome).HasMaxLength(50);
        });

        modelBuilder.Entity<EstadoLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EstadoLo__3214EC0740BD35AB");

            entity.ToTable("EstadoLogin");

            entity.Property(e => e.Nome).HasMaxLength(50);
        });

        modelBuilder.Entity<Fornecedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Forneced__3214EC07092DD836");

            entity.ToTable("Fornecedor");

            entity.Property(e => e.ContactoAlternativo).HasMaxLength(20);
            entity.Property(e => e.ContactoPrincipal).HasMaxLength(20);
            entity.Property(e => e.Email1).HasMaxLength(80);
            entity.Property(e => e.Email2).HasMaxLength(80);
            entity.Property(e => e.Endereco).HasMaxLength(250);
            entity.Property(e => e.Nif).HasMaxLength(50);
            entity.Property(e => e.Nome).HasMaxLength(150);
            entity.Property(e => e.PontoFocal).HasMaxLength(80);
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genero__3214EC074B76DBEE");

            entity.ToTable("Genero");

            entity.Property(e => e.Nome).HasMaxLength(50);
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Municipi__3214EC07607074F2");

            entity.ToTable("Municipio");

            entity.Property(e => e.Nome).HasMaxLength(80);

            entity.HasOne(d => d.Provincia).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.ProvinciaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Municipio__Provi__534D60F1");
        });

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pais__3214EC077903404E");

            entity.Property(e => e.Nome).HasMaxLength(50);
        });

        modelBuilder.Entity<Perfil>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Perfil__3214EC0748F25AA8");

            entity.ToTable("Perfil");

            entity.Property(e => e.Nome).HasMaxLength(80);
        });

        modelBuilder.Entity<PostoTrabalho>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostoTra__3214EC078C795F02");

            entity.ToTable("PostoTrabalho");

            entity.Property(e => e.Nome).HasMaxLength(100);
            entity.Property(e => e.Sigla).HasMaxLength(20);

            entity.HasOne(d => d.Departamento).WithMany(p => p.PostoTrabalhos)
                .HasForeignKey(d => d.DepartamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostoTrab__Depar__5535A963");

            entity.HasOne(d => d.Municipio).WithMany(p => p.PostoTrabalhos)
                .HasForeignKey(d => d.MunicipioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostoTrab__Munic__5629CD9C");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Provinci__3214EC0776531CA7");

            entity.Property(e => e.Nome).HasMaxLength(60);

            entity.HasOne(d => d.Pais).WithMany(p => p.Provincia)
                .HasForeignKey(d => d.PaisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Provincia__PaisI__52593CB8");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoDocu__3214EC07670A5546");

            entity.ToTable("TipoDocumento");

            entity.Property(e => e.Nome).HasMaxLength(100);
        });

        modelBuilder.Entity<TipoDocumentoColaborador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoDocu__3214EC075DFCDEC9");

            entity.ToTable("TipoDocumentoColaborador");

            entity.Property(e => e.Nome).HasMaxLength(100);
        });

        modelBuilder.Entity<TramitacaoDocumento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tramitac__3214EC0704B9FC07");

            entity.ToTable("TramitacaoDocumento");

            entity.Property(e => e.DataEnvio).HasColumnType("datetime");
            entity.Property(e => e.DataRececao).HasColumnType("datetime");
            entity.Property(e => e.Estado).HasMaxLength(100);

            entity.HasOne(d => d.ColaboradorDestino).WithMany(p => p.TramitacaoDocumentoColaboradorDestinos)
                .HasForeignKey(d => d.ColaboradorDestinoId)
                .HasConstraintName("FK__Tramitaca__Colab__6C190EBB");

            entity.HasOne(d => d.ColaboradorOrigem).WithMany(p => p.TramitacaoDocumentoColaboradorOrigems)
                .HasForeignKey(d => d.ColaboradorOrigemId)
                .HasConstraintName("FK__Tramitaca__Colab__6B24EA82");

            entity.HasOne(d => d.DirecaoDestino).WithMany(p => p.TramitacaoDocumentoDirecaoDestinos)
                .HasForeignKey(d => d.DirecaoDestinoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tramitaca__Direc__6A30C649");

            entity.HasOne(d => d.DirecaoOrigem).WithMany(p => p.TramitacaoDocumentoDirecaoOrigems)
                .HasForeignKey(d => d.DirecaoOrigemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tramitaca__Direc__693CA210");

            entity.HasOne(d => d.Documento).WithMany(p => p.TramitacaoDocumentos)
                .HasForeignKey(d => d.DocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tramitaca__Docum__68487DD7");
        });

        modelBuilder.Entity<UsuarioSistema>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsuarioS__3214EC07AC565DAA");

            entity.ToTable("UsuarioSistema");

            entity.HasIndex(e => e.ColaboradorId, "UQ__UsuarioS__28AA72205B486489").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__UsuarioS__536C85E4126F4AB7").IsUnique();

            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.Bloqueado).HasDefaultValue(false);
            entity.Property(e => e.DataCriacao).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(500);
            entity.Property(e => e.PasswordSalt).HasMaxLength(500);
            entity.Property(e => e.TentativasFalhadas).HasDefaultValue(0);
            entity.Property(e => e.UltimoLogin).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.Colaborador).WithOne(p => p.UsuarioSistema)
                .HasForeignKey<UsuarioSistema>(d => d.ColaboradorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioSi__Colab__5BE2A6F2");

            entity.HasOne(d => d.EstadoLogin).WithMany(p => p.UsuarioSistemas)
                .HasForeignKey(d => d.EstadoLoginId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioSi__Estad__5CD6CB2B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
