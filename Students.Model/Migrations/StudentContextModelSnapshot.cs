﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Students.Model;

namespace Students.Model.Migrations
{
    [DbContext(typeof(StudentContext))]
    partial class StudentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Students.Model.Entities.Course", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("NAME")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<long>("Number")
                        .HasColumnName("NUMBER")
                        .HasColumnType("bigint")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("COURSE");
                });

            modelBuilder.Entity("Students.Model.Entities.Enrollment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CourseId")
                        .HasColumnName("COURSE_ID")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("EnrollmentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ENROLLMENT_DATE")
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 10, 5, 21, 10, 5, 229, DateTimeKind.Local).AddTicks(8846));

                    b.Property<long>("StudentId")
                        .HasColumnName("STUDENT_ID")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("ENROLLMENT");
                });

            modelBuilder.Entity("Students.Model.Entities.Student", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnName("DATE_OF_BIRTH")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("FIRST_NAME")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("LAST_NAME")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("PersonalId")
                        .IsRequired()
                        .HasColumnName("PERSONAL_ID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PersonalId")
                        .IsUnique()
                        .HasName("UQ_STUDENT_PERSONAL_ID");

                    b.ToTable("STUDENT");
                });

            modelBuilder.Entity("Students.Model.Entities.Enrollment", b =>
                {
                    b.HasOne("Students.Model.Entities.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Students.Model.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
