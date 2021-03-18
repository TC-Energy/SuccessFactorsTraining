using System.Collections;
using System.Collections.Generic;
using SuccessFactors.Models;

//This ViewModel class was created to load different model data in the same view (.cshtml)
public class ViewModel
{
    public IEnumerable<CourseInfo> MyAssignedCourses { get; set; }

    // Creates an Iterable object to load the instructor's details from CourseInfo model
    public IEnumerable<CourseInfo> AllInstructors { get; set; }

    // Creates an Iterable object to load the assigned courses' details from CourseInfo model
    public IEnumerable<CourseInfo> AllInstructorAssignCourses { get; set; }

    // Creates an Iterable object to load the taught courses' details from CourseInfo model
    public IEnumerable<CourseInfo> AllInstructorTaughtCourses { get; set; }


    public IEnumerable<SessionInfo> InstructorSessions { get; set; }

    public IEnumerable<InstructorInfo> InstructorDetails { get; set; }

    public IEnumerable<StudentInfo> AttendanceList { get; set; }

    public IEnumerable<StudentInfo> CompleteAttendanceList { get; set; }

}