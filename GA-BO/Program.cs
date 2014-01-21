using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA_BO.qap;

namespace GA_BO
{
    class Program
    {
        static void Main(string[] args)
        {
			QAPTester tester = new QAPTester();
			tester.allTest();
        }
    }
}
