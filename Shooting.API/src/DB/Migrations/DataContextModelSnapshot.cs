// <auto-generated />
using System;
using DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DB.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DB.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DB.Entities.Competition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("DB.Entities.Competitor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("BirthYear")
                        .HasColumnType("integer");

                    b.Property<Guid>("CompetitionId")
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float?>("Result")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.ToTable("Competitor");
                });

            modelBuilder.Entity("DB.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageLabel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("DB.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DB.Entities.Comment", b =>
                {
                    b.HasOne("DB.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Entities.Post", null)
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("DB.Entities.Competitor", b =>
                {
                    b.HasOne("DB.Entities.Competition", "Competition")
                        .WithMany("Competitors")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competition");
                });

            modelBuilder.Entity("DB.Entities.Post", b =>
                {
                    b.HasOne("DB.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("DB.Entities.Competition", b =>
                {
                    b.Navigation("Competitors");
                });

            modelBuilder.Entity("DB.Entities.Post", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
