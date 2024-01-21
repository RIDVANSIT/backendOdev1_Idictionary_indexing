using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.models
{
    public interface IStudent : IUser
    {
        int Absenteeism { get; set; }
        byte Marks { get; set; }
        int StdNumber { get; set; }
    }
    public class Student : User, IStudent
    {
        public int Absenteeism { get; set; }
        public byte Marks { get; set; }
        public int StdNumber { get; set; }
        public Student() { }
        public Student(string userName, string password, bool isActive, int absenteeism, byte marks) : base(userName, password, isActive)
        {
            Absenteeism = absenteeism;
            Marks = marks;
        }
    }
}
