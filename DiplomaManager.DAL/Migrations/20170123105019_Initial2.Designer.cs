using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DiplomaManager.DAL.EF;

namespace DiplomaManager.DAL.Migrations
{
    [DbContext(typeof(DiplomaManagerContext))]
    [Migration("20170123105019_Initial2")]
    partial class Initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DiplomaManager.DAL.Entities.EventsEntities.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CommitteeId");

                    b.Property<int>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("CommitteeId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.EventsEntities.Committee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("Id");

                    b.ToTable("Committees");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.EventsEntities.Defense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("Estimate");

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("Defenses");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.EventsEntities.UndergraduateDefense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CommitteeId");

                    b.Property<bool>("ControlSigned");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("EconomySigned");

                    b.Property<bool>("Passed");

                    b.Property<int>("PresentationReadiness");

                    b.Property<bool>("ReportExist");

                    b.Property<bool>("SafetySigned");

                    b.Property<int>("SoftwareReadiness");

                    b.Property<int>("StudentId");

                    b.Property<int>("WritingReadiness");

                    b.HasKey("Id");

                    b.HasIndex("CommitteeId");

                    b.HasIndex("StudentId");

                    b.ToTable("UndergraduateDefenses");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.ProjectEntities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Accepted");

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("DevelopmentAreaId");

                    b.Property<DateTime>("PracticeJournalPassed");

                    b.Property<int>("StudentId");

                    b.Property<int?>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("DevelopmentAreaId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.ProjectEntities.ProjectTitle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("LocaleId");

                    b.Property<int>("ProjectId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectTitles");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.RequestEntities.Capacity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AcceptedCount");

                    b.Property<int>("Count");

                    b.Property<int>("DegreeId");

                    b.Property<DateTime>("StudyingYear");

                    b.Property<int>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("DegreeId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Capacities");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.RequestEntities.DevelopmentArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("DevelopmentAreas");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.RequestEntities.Interest", b =>
                {
                    b.Property<int>("DevelopmentAreaId");

                    b.Property<int>("TeacherId");

                    b.HasKey("DevelopmentAreaId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Interests");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.StudentEntities.Degree", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Degrees");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.StudentEntities.DegreeName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DegreeId");

                    b.Property<int>("LocaleId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DegreeId");

                    b.ToTable("DegreeNames");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.StudentEntities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DegreeId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DegreeId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.TeacherEntities.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.TeacherEntities.PositionName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LocaleId");

                    b.Property<string>("Name");

                    b.Property<int>("PositionId");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.ToTable("PositionNames");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.UserEnitites.FirstName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("LocaleId");

                    b.Property<string>("Name");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("FirstNames");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.UserEnitites.LastName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("LocaleId");

                    b.Property<string>("Name");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LastNames");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.UserEnitites.Patronymic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("LocaleId");

                    b.Property<string>("Name");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Patronymics");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.UserEnitites.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.Property<string>("Status");

                    b.Property<DateTime>("StatusCreationDate");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.StudentEntities.Student", b =>
                {
                    b.HasBaseType("DiplomaManager.DAL.Entities.UserEnitites.User");

                    b.Property<int>("GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("Student");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.TeacherEntities.Teacher", b =>
                {
                    b.HasBaseType("DiplomaManager.DAL.Entities.UserEnitites.User");

                    b.Property<int>("PositionId");

                    b.HasIndex("PositionId");

                    b.ToTable("Teacher");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.EventsEntities.Appointment", b =>
                {
                    b.HasOne("DiplomaManager.DAL.Entities.EventsEntities.Committee", "Committee")
                        .WithMany("Appointments")
                        .HasForeignKey("CommitteeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DiplomaManager.DAL.Entities.TeacherEntities.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.EventsEntities.Defense", b =>
                {
                    b.HasOne("DiplomaManager.DAL.Entities.StudentEntities.Student", "Student")
                        .WithOne("Defense")
                        .HasForeignKey("DiplomaManager.DAL.Entities.EventsEntities.Defense", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.EventsEntities.UndergraduateDefense", b =>
                {
                    b.HasOne("DiplomaManager.DAL.Entities.EventsEntities.Committee", "Committee")
                        .WithMany("UndergraduateDefenses")
                        .HasForeignKey("CommitteeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DiplomaManager.DAL.Entities.StudentEntities.Student", "Student")
                        .WithMany("UndergraduateDefenses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.ProjectEntities.Project", b =>
                {
                    b.HasOne("DiplomaManager.DAL.Entities.RequestEntities.DevelopmentArea", "DevelopmentArea")
                        .WithMany()
                        .HasForeignKey("DevelopmentAreaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DiplomaManager.DAL.Entities.StudentEntities.Student", "Student")
                        .WithMany("Projects")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DiplomaManager.DAL.Entities.TeacherEntities.Teacher", "Teacher")
                        .WithMany("Projects")
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.ProjectEntities.ProjectTitle", b =>
                {
                    b.HasOne("DiplomaManager.DAL.Entities.ProjectEntities.Project", "Project")
                        .WithMany("ProjectsTitles")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.RequestEntities.Capacity", b =>
                {
                    b.HasOne("DiplomaManager.DAL.Entities.StudentEntities.Degree", "Degree")
                        .WithMany("Capacities")
                        .HasForeignKey("DegreeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DiplomaManager.DAL.Entities.TeacherEntities.Teacher", "Teacher")
                        .WithMany("Capacities")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.RequestEntities.Interest", b =>
                {
                    b.HasOne("DiplomaManager.DAL.Entities.RequestEntities.DevelopmentArea", "DevelopmentArea")
                        .WithMany("Interests")
                        .HasForeignKey("DevelopmentAreaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DiplomaManager.DAL.Entities.TeacherEntities.Teacher", "Teacher")
                        .WithMany("Interests")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.StudentEntities.DegreeName", b =>
                {
                    b.HasOne("DiplomaManager.DAL.Entities.StudentEntities.Degree", "Degree")
                        .WithMany("DegreeNames")
                        .HasForeignKey("DegreeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.StudentEntities.Group", b =>
                {
                    b.HasOne("DiplomaManager.DAL.Entities.StudentEntities.Degree", "Degree")
                        .WithMany("Groups")
                        .HasForeignKey("DegreeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.TeacherEntities.PositionName", b =>
                {
                    b.HasOne("DiplomaManager.DAL.Entities.TeacherEntities.Position", "Position")
                        .WithMany("PositionNames")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.UserEnitites.FirstName", b =>
                {
                    b.HasOne("DiplomaManager.DAL.Entities.UserEnitites.User", "User")
                        .WithMany("FirstNames")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.UserEnitites.LastName", b =>
                {
                    b.HasOne("DiplomaManager.DAL.Entities.UserEnitites.User", "User")
                        .WithMany("LastNames")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.UserEnitites.Patronymic", b =>
                {
                    b.HasOne("DiplomaManager.DAL.Entities.UserEnitites.User", "User")
                        .WithMany("Patronymics")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.StudentEntities.Student", b =>
                {
                    b.HasOne("DiplomaManager.DAL.Entities.StudentEntities.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiplomaManager.DAL.Entities.TeacherEntities.Teacher", b =>
                {
                    b.HasOne("DiplomaManager.DAL.Entities.TeacherEntities.Position", "Position")
                        .WithMany("Teachers")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
