using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MSTART_Hiring_Task.Models;

public partial class MstartContext : DbContext
{
    public MstartContext()
    {
    }

    public MstartContext(DbContextOptions<MstartContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=OSAMA-ODX\\SQLEXPRESS02; Database=Mstart;Trusted_Connection=True;Encrypt=False; MultipleActiveResultSets=True; ");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__3214EC277C5010B3");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("ID");
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Account_Number");
            entity.Property(e => e.Balance).HasColumnType("money");
            entity.Property(e => e.currency)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DateTimeUtc)
                .HasColumnType("datetime")
                .HasColumnName("DateTime_UTC");
            entity.Property(e => e.ServerDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Server_DateTime");
            entity.Property(e => e.UpdateDateTimeUtc)
                .HasColumnType("datetime")
                .HasColumnName("Update_DateTime_UTC");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Account__User_ID__398D8EEE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC27847379C6");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("datetime")
                .HasColumnName("Date_Of_Birth");
            entity.Property(e => e.DateTimeUtc)
                .HasColumnType("datetime")
                .HasColumnName("DateTime_UTC");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("Last_Name");
            entity.Property(e => e.ServerDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Server_DateTime");
            entity.Property(e => e.UpdateDateTimeUtc)
                .HasColumnType("datetime")
                .HasColumnName("Update_DateTime_UTC");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
