using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Major
    {
        private string code;
        private string title;
        public Major() { }
        public Major(string code, string title)
        {
            this.code = code;
            this.title = title;
        }
        public string Code { get => code; set => code = value; }
        public string Title { get => title; set => title = value; }
    }
}
