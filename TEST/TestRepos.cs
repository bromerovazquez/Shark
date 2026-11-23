
using Drim.Interfaces.MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.WorkerServices
{
    public class TestRepos : ITestRepos
    {
        

        public TestRepos()
        {
            
        }


        //public Test() 
        //{
        
        //}

        public string AdiosMundo(string despido)
        {
            return despido;
        }
    }
}
