
using Drim.Interfaces.MVC.Data;
using Drim.Interfaces.MVC.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.WorkerServices
{
    public class Test : ITest
    {
        public readonly IDrimRepository _DrimRepository;

        public Test(IDrimRepository DrimRepository)
        {
            _DrimRepository = DrimRepository;
        }


        //public Test() 
        //{
        
        //}

        public string holaMundo(string saludo)
        {
            return saludo;
        }
    }
}
