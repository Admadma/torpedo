using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace torpedo.Models
{
    public class MainModel
    {
        public int a { get; set; }
        public MainModel(int number)
        {
            this.a = number;
        }

        public int returnNumber()
        {
            return a;
        }
    }
}
