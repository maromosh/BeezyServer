using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BeezyServer.Models;

public partial class BeezyDbContext : DbContext
{
    public BeezyDbContext()
    {
    }

    public BeezyDbContext(DbContextOptions<BeezyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beekeeper> Beekeepers { get; set; }

    public virtual DbSet<ChatBeekeeper> ChatBeekeepers { get; set; }

    public virtual DbSet<ChatPic> ChatPics { get; set; }

    public virtual DbSet<ChatQuestionsAswer> ChatQuestionsAswers { get; set; }

    public virtual DbSet<Plant> Plants { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<ReportPicture> ReportPictures { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Workshop> Workshops { get; set; }

    public virtual DbSet<WorkshopPic> WorkshopPics { get; set; }

    public virtual DbSet<WorkshopRegister> WorkshopRegisters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Initial Catalog=BeezyDB;User ID=BeezyAdminLogin;Password=thePassword;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beekeeper>(entity =>
        {
            entity.HasKey(e => e.BeeKeeperId).HasName("PK__Beekeepe__4758570DE37693B5");

            entity.Property(e => e.BeeKeeperId).ValueGeneratedNever();
            entity.Property(e => e.BeekeeperIsActive).HasDefaultValue(true);

            entity.HasOne(d => d.BeeKeeper).WithOne(p => p.Beekeeper)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Beekeeper__BeeKe__29572725");
        });

        modelBuilder.Entity<ChatBeekeeper>(entity =>
        {
            entity.HasKey(e => e.ChatBeekeepers).HasName("PK__ChatBeek__82274B9543664A78");

            entity.HasOne(d => d.BeeKeeper).WithMany(p => p.ChatBeekeepers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChatBeeke__BeeKe__403A8C7D");
        });

        modelBuilder.Entity<ChatPic>(entity =>
        {
            entity.HasKey(e => e.ChatPicId).HasName("PK__ChatPic__80E0A51B2C6861D5");

            entity.HasOne(d => d.ChatQuestionsAswers).WithMany(p => p.ChatPics).HasConstraintName("FK__ChatPic__ChatQue__45F365D3");
        });

        modelBuilder.Entity<ChatQuestionsAswer>(entity =>
        {
            entity.HasKey(e => e.ChatQuestionsAswersId).HasName("PK__ChatQues__45C0FA9039F4D7AA");

            entity.HasOne(d => d.User).WithMany(p => p.ChatQuestionsAswers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChatQuest__UserI__4316F928");
        });

        modelBuilder.Entity<Plant>(entity =>
        {
            entity.HasKey(e => e.PlantId).HasName("PK__Plants__98FE395CDE3AAA45");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Report__D5BD4805692E7461");

            entity.HasOne(d => d.BeeKeeper).WithMany(p => p.Reports).HasConstraintName("FK__Report__BeeKeepe__2E1BDC42");

            entity.HasOne(d => d.User).WithMany(p => p.Reports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Report__UserId__2D27B809");
        });

        modelBuilder.Entity<ReportPicture>(entity =>
        {
            entity.HasKey(e => e.PicId).HasName("PK__ReportPi__B04A93C14BAB9C9F");

            entity.HasOne(d => d.Report).WithMany(p => p.ReportPictures).HasConstraintName("FK__ReportPic__Repor__30F848ED");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C606B05DE");
        });

        modelBuilder.Entity<Workshop>(entity =>
        {
            entity.HasKey(e => e.WorkshopId).HasName("PK__Workshop__7A008C0ADB19DEE7");

            entity.HasOne(d => d.BeeKeeper).WithMany(p => p.Workshops)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Workshop__BeeKee__35BCFE0A");
        });

        modelBuilder.Entity<WorkshopPic>(entity =>
        {
            entity.HasKey(e => e.WorkshopPicId).HasName("PK__Workshop__D72386FF36ADC070");

            entity.HasOne(d => d.Workshop).WithMany(p => p.WorkshopPics).HasConstraintName("FK__WorkshopP__Works__38996AB5");
        });

        modelBuilder.Entity<WorkshopRegister>(entity =>
        {
            entity.HasKey(e => e.WorkshopRegisters).HasName("PK__Workshop__ACD6E10390000A2A");

            entity.HasOne(d => d.User).WithMany(p => p.WorkshopRegisters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkshopR__UserI__3C69FB99");

            entity.HasOne(d => d.Workshop).WithMany(p => p.WorkshopRegisters).HasConstraintName("FK__WorkshopR__Works__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
