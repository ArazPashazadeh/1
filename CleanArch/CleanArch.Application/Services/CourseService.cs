using System;
using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;

namespace CleanArch.Application.Services
{
    public class CourseService : ICourseService
    {
        private ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public CourseViewModel GetCourses()
        {
            return new CourseViewModel { Courses = _courseRepository.GetCourses() };
        }

        public Course GetCourseById(Guid courseId)
        {
            Course course = _courseRepository.GetCourseById(courseId);
            return course;
        }
    }
}
