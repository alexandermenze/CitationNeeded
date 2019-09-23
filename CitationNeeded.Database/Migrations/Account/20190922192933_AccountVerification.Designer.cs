﻿// <auto-generated />
using CitationNeeded.Database.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CitationNeeded.Database.Migrations.Account
{
    [DbContext(typeof(AccountContext))]
    [Migration("20190922192933_AccountVerification")]
    partial class AccountVerification
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CitationNeeded.Domain.ValueTypes.Account", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("HashedPassword");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("CitationNeeded.Domain.ValueTypes.AccountVerification", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountId");

                    b.Property<bool>("IsVerified");

                    b.Property<string>("VerificationToken");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountVerifications");
                });

            modelBuilder.Entity("CitationNeeded.Domain.ValueTypes.AccountVerification", b =>
                {
                    b.HasOne("CitationNeeded.Domain.ValueTypes.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");
                });
#pragma warning restore 612, 618
        }
    }
}