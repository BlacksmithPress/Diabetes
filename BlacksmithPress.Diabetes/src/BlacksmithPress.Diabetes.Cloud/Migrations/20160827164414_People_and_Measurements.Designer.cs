using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BlacksmithPress.Diabetes.Cloud.Models;

namespace BlacksmithPress.Diabetes.Cloud.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20160827164414_People_and_Measurements")]
    partial class People_and_Measurements
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlacksmithPress.Diabetes.Cloud.Models.MeasuredAttributes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DefaultUnitOfMeasureId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DefaultUnitOfMeasureId");

                    b.ToTable("MeasuredAttributes");
                });

            modelBuilder.Entity("BlacksmithPress.Diabetes.Cloud.Models.Measurements", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<int?>("AttributeId");

                    b.Property<Guid?>("SubjectId");

                    b.Property<DateTimeOffset>("Timestamp");

                    b.Property<int?>("UnitOfMeasureId");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("UnitOfMeasureId");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("BlacksmithPress.Diabetes.Cloud.Models.People", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("BlacksmithPress.Diabetes.Cloud.Models.UnitsOfMeasure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("UnitsOfMeasure");
                });

            modelBuilder.Entity("BlacksmithPress.Diabetes.Cloud.Models.MeasuredAttributes", b =>
                {
                    b.HasOne("BlacksmithPress.Diabetes.Cloud.Models.UnitsOfMeasure", "DefaultUnitOfMeasure")
                        .WithMany()
                        .HasForeignKey("DefaultUnitOfMeasureId");
                });

            modelBuilder.Entity("BlacksmithPress.Diabetes.Cloud.Models.Measurements", b =>
                {
                    b.HasOne("BlacksmithPress.Diabetes.Cloud.Models.MeasuredAttributes", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId");

                    b.HasOne("BlacksmithPress.Diabetes.Cloud.Models.People", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId");

                    b.HasOne("BlacksmithPress.Diabetes.Cloud.Models.UnitsOfMeasure", "UnitsOfMeasure")
                        .WithMany()
                        .HasForeignKey("UnitOfMeasureId");
                });
        }
    }
}
