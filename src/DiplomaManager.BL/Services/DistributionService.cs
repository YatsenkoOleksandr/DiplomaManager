using DiplomaManager.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaManager.BLL.Extensions.DistributionService;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.BLL.Configuration;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Utils;
using AutoMapper;
using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.DTOs.RequestDTOs;
using DiplomaManager.DAL.Entities.RequestEntities;

namespace DiplomaManager.BLL.Services
{
    public class DistributionService : IDistributionService
    {
        private IDiplomaManagerUnitOfWork Database { get; }
        private ILocaleConfiguration CultureConfiguration { get; }
        private IEmailService EmailService { get; }

        public DistributionService(IDiplomaManagerUnitOfWork uow, ILocaleConfiguration configuration, IEmailService emailService)
        {
            Database = uow;
            CultureConfiguration = configuration;
            EmailService = emailService;
        }

        public FormedProjects Distribute(int degreeId, int graduationYear)
        {
            FormedProjects formedProjects = new FormedProjects();

            List<CapacityDTO> capacities;

            IEnumerable<StudentDTO> students = this.GetStudents(degreeId, graduationYear);
            IEnumerable<TeacherDTO> teachers = this.GetTeachersAndCapacities(degreeId, graduationYear, out capacities);
            IEnumerable<ProjectDTO> projects = this.GetProjects(degreeId, graduationYear); 

            formedProjects = this.DistributeStudents(degreeId, graduationYear, students, teachers, projects, capacities);
            formedProjects.ExistedUnchangedProjects = this.GetAcceptedProjects(degreeId, graduationYear).ToList();


            return formedProjects;
        }

        private IEnumerable<StudentDTO> GetStudents(int degreeId, int graduationYear)
        {
            List<IncludeExpression<Student>> studentPaths = new List<IncludeExpression<Student>>();
            studentPaths.Add(new IncludeExpression<Student>(pr => pr.PeopleNames));
            studentPaths.Add(new IncludeExpression<Student>(pr => pr.Group));

            IEnumerable<Student> students = Database.Students.Get(
                new FilterExpression<Student>[] { new FilterExpression<Student>(
                    st => st.Group.DegreeId == degreeId && st.Group.GraduationYear == graduationYear) },
                studentPaths.ToArray()
                );

            return Mapper.Map<IEnumerable<Student>, IEnumerable<StudentDTO>>(students);
        }


        private IEnumerable<TeacherDTO> GetTeachersAndCapacities(int degreeId, int graduationYear, out List<CapacityDTO> capacities)
        {
            capacities = new List<CapacityDTO>();

            List<IncludeExpression<Teacher>> teacherPaths = new List<IncludeExpression<Teacher>>();
            teacherPaths.Add(new IncludeExpression<Teacher>(pr => pr.PeopleNames));
            teacherPaths.Add(new IncludeExpression<Teacher>(pr => pr.Capacities));

            IEnumerable<Teacher> teachers = Database.Teachers.Get(
                new FilterExpression<Teacher>(
                    t => t.Capacities.Any(
                        c => c.StudyingYear.Year == graduationYear && c.DegreeId == degreeId)),
                teacherPaths.ToArray());

            // Get capacities
            foreach (Teacher t in teachers)
            {
                capacities.Add(
                    Mapper.Map<Capacity, CapacityDTO>(
                        t.Capacities
                        .Where(c => c.DegreeId == degreeId && c.StudyingYear.Year == graduationYear)
                        .FirstOrDefault()));
            }

            return Mapper.Map<IEnumerable<Teacher>, IEnumerable<TeacherDTO>>(teachers);
        }


        public IEnumerable<ProjectDTO> GetAcceptedProjects(int degreeId, int graduationYear)
        {
            List<IncludeExpression<Project>> includeProjectPaths = new List<IncludeExpression<Project>>();
            includeProjectPaths.Add(new IncludeExpression<Project>(pr => pr.Student));
            includeProjectPaths.Add(new IncludeExpression<Project>(pr => pr.Student.PeopleNames));
            includeProjectPaths.Add(new IncludeExpression<Project>(pr => pr.Student.Group));
            includeProjectPaths.Add(new IncludeExpression<Project>(pr => pr.Teacher));
            includeProjectPaths.Add(new IncludeExpression<Project>(pr => pr.Teacher.PeopleNames));
           
            IEnumerable<Project> existedPairs = Database.Projects.Get(
                new FilterExpression<Project>(
                    pr => pr.Accepted == true &&
                        pr.Student.Group.DegreeId == degreeId &&
                        pr.Student.Group.GraduationYear == graduationYear),
                includeProjectPaths.ToArray()
                );

            return Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDTO>>(existedPairs);
        }


        private IEnumerable<ProjectDTO> GetProjects(int degreeId, int graduationYear)
        {
            List<IncludeExpression<Project>> projectPaths = new List<IncludeExpression<Project>>();
            projectPaths.Add(new IncludeExpression<Project>(pr => pr.Student));
            projectPaths.Add(new IncludeExpression<Project>(pr => pr.Student.PeopleNames));
            projectPaths.Add(new IncludeExpression<Project>(pr => pr.Student.Group));
            projectPaths.Add(new IncludeExpression<Project>(pr => pr.Teacher));
            projectPaths.Add(new IncludeExpression<Project>(pr => pr.Teacher.PeopleNames));
            projectPaths.Add(new IncludeExpression<Project>(pr => pr.Teacher.Capacities));

            IEnumerable<Project> projects = Database.Projects.Get(
                new FilterExpression<Project>(
                    pr => pr.Student.Group.DegreeId == degreeId && pr.Student.Group.GraduationYear == graduationYear),
                projectPaths.ToArray());

            return Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDTO>>(projects);
        }

        
        private FormedProjects DistributeStudents(
            int degreeId,
            int graduationYear,
            IEnumerable<StudentDTO> students,
            IEnumerable<TeacherDTO> teachers,
            IEnumerable<ProjectDTO> projects,
            IEnumerable<CapacityDTO> capacities)
        {
            Random rand = new Random();

            FormedProjects distributed = new FormedProjects();
            distributed.ExistedModifiedProjects = new List<ProjectDTO>();
            distributed.ConflictedProjects = new List<ProjectDTO>();
            distributed.NewProjects = new List<ProjectDTO>();


            // Accept active requests
            IEnumerable<ProjectDTO> activeProjects = projects
                .Where(pr => pr.Accepted == null)
                .OrderBy(pr => pr.CreationDate);
            
            foreach(ProjectDTO proj in activeProjects)
            {
                CapacityDTO cap = capacities.Where(c => c.TeacherId == proj.TeacherId).FirstOrDefault();

                if (cap.AcceptedCount < cap.Count)
                {
                    cap.AcceptedCount++;
                    // proj.Teacher.Capacities.Where(c => c.TeacherId == proj.TeacherId).FirstOrDefault().AcceptedCount++;
                    proj.Accepted = true;
                    distributed.ExistedModifiedProjects.Add(proj);
                }
                else
                {
                    proj.Accepted = false;
                    distributed.ExistedModifiedProjects.Add(proj);
                }
            }

            // Distribute students with denied requests

            IEnumerable<StudentDTO> deniedStudents = this.GetDeniedStudents(projects, students);
            
            List<TeacherDTO> freeTeachers = new List<TeacherDTO>();
            foreach(CapacityDTO cap in capacities)
            {
                if (cap.AcceptedCount < cap.Count)
                {
                    freeTeachers.Add(cap.Teacher);
                }
            }

            foreach(StudentDTO st in deniedStudents)
            {
                List<TeacherDTO> potentialTeachers =
                    GetPotentialTeachers(freeTeachers, projects, st.Id);
                int potentialCount = potentialTeachers.Count;

                if (potentialCount != 0)
                {
                    int teacherIndex = rand.Next(0, potentialCount);

                    TeacherDTO teacher = potentialTeachers[teacherIndex];

                    // Add to new projects
                    ProjectDTO newPr = new ProjectDTO()
                    {
                        StudentId = st.Id,
                        TeacherId = teacher.Id,
                        Student = st,
                        Teacher = teacher
                    };
                    distributed.NewProjects.Add(newPr);

                    // Subtract students in capacity
                    CapacityDTO cap = capacities.Where(c => c.TeacherId == teacher.Id).FirstOrDefault();
                    cap.AcceptedCount--;
                    if (cap.AcceptedCount == cap.Count)
                    {
                        freeTeachers.RemoveAt(teacherIndex);
                    }
                }
                else
                {
                    int teacherIndex = rand.Next(0, freeTeachers.Count);
                    TeacherDTO teacher = freeTeachers[teacherIndex];

                    ProjectDTO confProject = projects
                        .Where(p => p.StudentId == st.Id && p.TeacherId == teacher.Id)
                        .FirstOrDefault();
                    confProject.Accepted = true;
                    distributed.ConflictedProjects.Add(confProject);

                    if (!distributed.ExistedModifiedProjects.Any(p => p.StudentId == confProject.StudentId && 
                            p.TeacherId == confProject.TeacherId))
                    {
                        distributed.ExistedModifiedProjects.Add(confProject);
                    }

                    // Subtract students in capacity
                    CapacityDTO cap = capacities.Where(c => c.TeacherId == teacher.Id).FirstOrDefault();
                    cap.AcceptedCount--;
                    if (cap.AcceptedCount == cap.Count)
                    {
                        freeTeachers.RemoveAt(teacherIndex);
                    }
                }
            }

            // Distribute other students
            IEnumerable<StudentDTO> studentsWithoutRequests =
                GetStudentsWithoutRequests(students, projects);

            foreach(StudentDTO st in studentsWithoutRequests)
            {
                int teacherIndex = rand.Next(0, freeTeachers.Count);

                TeacherDTO teacher = freeTeachers[teacherIndex];

                // Add to new projects
                ProjectDTO newPr = new ProjectDTO()
                {
                    StudentId = st.Id,
                    TeacherId = teacher.Id,
                    Student = st,
                    Teacher = teacher
                };
                distributed.NewProjects.Add(newPr);

                // Subtract students in capacity
                CapacityDTO cap = capacities.Where(c => c.TeacherId == teacher.Id).FirstOrDefault();
                cap.AcceptedCount--;
                if (cap.AcceptedCount == cap.Count)
                {
                    freeTeachers.RemoveAt(teacherIndex);
                }
            }

            return distributed;
        } 

        private IEnumerable<StudentDTO> GetDeniedStudents(
            IEnumerable<ProjectDTO> projects, 
            IEnumerable<StudentDTO> students)
        {
            // Key - student id; Value - count of not accepted requests
            Dictionary<int, int> studentMap = new Dictionary<int, int>();
            foreach(StudentDTO st in students)
            {
                IEnumerable<ProjectDTO> studentProjects =
                    projects.Where(p => p.StudentId == st.Id);

                if (studentProjects.Count() != 0 && studentProjects.All(p => p.Accepted == false))
                {
                    studentMap.Add(st.Id, studentProjects.Count());
                }
            }

            IEnumerable<KeyValuePair<int, int>> pairs = studentMap.OrderBy(k => k.Value);

            List<StudentDTO> deniedStudents = new List<StudentDTO>();
            foreach(var key in pairs)
            {
                deniedStudents.Add(students.Where(s => s.Id == key.Key).FirstOrDefault());
            }

            return deniedStudents;
        }


        private List<TeacherDTO> GetPotentialTeachers(
            IEnumerable<TeacherDTO> freeTeachers, 
            IEnumerable<ProjectDTO> projects, 
            int studentId)
        {
            List<TeacherDTO> potentialTeachers = new List<TeacherDTO>();
            foreach(TeacherDTO t in freeTeachers)
            {
                var teacherProjects = projects.Where(p => p.TeacherId == t.Id);
                if (teacherProjects.All(p => p.StudentId != studentId))
                {
                    potentialTeachers.Add(t);
                }
            }
            return potentialTeachers;
        }

        private List<StudentDTO> GetStudentsWithoutRequests(
            IEnumerable<StudentDTO> students, 
            IEnumerable<ProjectDTO> projects)
        {
            List<StudentDTO> studentsWithoutRequests = new List<StudentDTO>();

            foreach(StudentDTO student in students)
            {
                var studentProjects = projects.Where(p => p.StudentId == student.Id);
                if (studentProjects == null || studentProjects.Count() == 0)
                {
                    studentsWithoutRequests.Add(student);
                }
            }

            return studentsWithoutRequests;
        }

        public void Save(int degreeId, int graduationYear, FormedProjects formedProjects)
        {
            foreach (ProjectDTO pr in formedProjects.NewProjects)
            {
                Database.Projects.Add(new Project()
                {
                    StudentId = pr.StudentId,
                    TeacherId = pr.TeacherId,
                });
            }

            foreach (ProjectDTO pr in formedProjects.ExistedModifiedProjects)
            {
                Project p = Database.Projects.Get(pr.Id);
                p.Accepted = pr.Accepted;
                p.StudentId = pr.StudentId;
                p.TeacherId = p.TeacherId;
                p.CreationDate = DateTime.Now;
                p.DevelopmentArea = Database.DevelopmentAreas.Get().FirstOrDefault();

            }

            Database.Save();

            // Change logic of changing capacity!!

            List<IncludeExpression<Teacher>> teacherPaths = new List<IncludeExpression<Teacher>>();
            teacherPaths.Add(new IncludeExpression<Teacher>(pr => pr.PeopleNames));
            teacherPaths.Add(new IncludeExpression<Teacher>(pr => pr.Capacities));

            IEnumerable<Teacher> teachers = Database.Teachers.Get(
                new FilterExpression<Teacher>(
                    t => t.Capacities.Any(
                        c => c.StudyingYear.Year == graduationYear && c.DegreeId == degreeId)),
                teacherPaths.ToArray());

            foreach(Teacher t in teachers)
            {
                Capacity cap =
                    t.Capacities
                    .Where(c => c.DegreeId == degreeId && c.StudyingYear.Year == graduationYear)
                    .FirstOrDefault();
                cap.AcceptedCount = t.Projects
                    .Where(p => p.Student.Group.DegreeId == degreeId &&
                        p.Student.Group.GraduationYear == graduationYear)
                    .Count();
            }

            Database.Save();
        }
    }
}
