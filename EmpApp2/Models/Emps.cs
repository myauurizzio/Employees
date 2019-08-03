using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EmpApp2.Models
{
    public class Emps
    {
        [DisplayName("#")]
        public int ID { get; set; } // This is NOT a classical key. It's a rownum. 

        [DisplayName("Unique Key")]
        public string ROWID { get; set; }

        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [DisplayName("Отчество")]
        public string GivenName { get; set; }

        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        [DisplayName("Пол")]
        public string Gender { get; set; }

        [DisplayName("Должность")]
        public string Position { get; set; }

        [DisplayName("Дата рождения")]
        public DateTime BirthDate { get; set; }

        [DisplayName("Телефон")]
        public string Phone { get; set; }


    }
}
