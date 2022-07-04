using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Student
    {
        private int id;
        private string name;
        private bool sex;
        private DateTime dob;
        private string major;
        private bool active;
        private float scholarship;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public bool Sex { get => sex; set => sex = value; }
        public DateTime Dob { get => dob; set => dob = value; }
        public string Major { get => major; set => major = value; }
        public bool Active { get => active; set => active = value; }
        public float Scholarship { get => scholarship; set => scholarship = value; }

        public Student() { }

        public Student(int ID, string NAME, bool SEX, DateTime DOB, string MAJOR, bool ACTIVE, float SCHOLAR)
        {
            this.id = ID;
            this.name = NAME;
            this.sex = SEX;
            this.dob = DOB;
            this.major = MAJOR;
            this.active = ACTIVE;
            this.scholarship = SCHOLAR;
        }
    }
}
