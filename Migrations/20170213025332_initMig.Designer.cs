using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SSAAttenderAPI.Data;

namespace SSAAttenderAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170213025332_initMig")]
    partial class initMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SSAAttenderAPI.Data.DAL.Model.AttendanceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("dayAttended");

                    b.Property<bool>("dayDidAttend");

                    b.Property<string>("trainingAttended");

                    b.Property<string>("userName");

                    b.HasKey("Id");

                    b.ToTable("Attendance");
                });

            modelBuilder.Entity("SSAAttenderAPI.Data.DAL.Model.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("userName");

                    b.HasKey("Id");

                    b.ToTable("UserAttending");
                });
        }
    }
}
