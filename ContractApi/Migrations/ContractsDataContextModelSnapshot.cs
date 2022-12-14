// <auto-generated />
using ContractApi.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ContractApi.Migrations
{
    [DbContext(typeof(ContractsDataContext))]
    partial class ContractsDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            modelBuilder.Entity("ContractApi.Models.Contracts", b =>
                {
                    b.Property<int>("ContractsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("FirmName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.HasKey("ContractsId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("ContractApi.Models.ContractsInfo", b =>
                {
                    b.Property<int>("ContractsInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("ContractsId")
                        .HasColumnType("integer");

                    b.Property<int>("InfoType")
                        .HasColumnType("integer");

                    b.Property<string>("InfoValue")
                        .HasColumnType("text");

                    b.HasKey("ContractsInfoId");

                    b.HasIndex("ContractsId");

                    b.ToTable("ContractsInfo");
                });

            modelBuilder.Entity("ContractApi.Models.ContractsInfo", b =>
                {
                    b.HasOne("ContractApi.Models.Contracts", null)
                        .WithMany("ContractsInfo")
                        .HasForeignKey("ContractsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContractApi.Models.Contracts", b =>
                {
                    b.Navigation("ContractsInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
