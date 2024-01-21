using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.models
{
    public interface IJobber : IUser
    {
        string WorkArea { get; set; }
        public string CarNo { get; set; }
    }
    public class Jobber : User, IJobber
    {
        public string WorkArea { get; set; }
        public string CarNo { get; set; }
        public Jobber() { }
        public Jobber(string userName, string password, bool isActive, string workArea) : base(userName, password, isActive)
        {
            WorkArea = workArea;
        }
    }
}
