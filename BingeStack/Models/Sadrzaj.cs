using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BingeStack.Models
{
    public class Sadrzaj
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Vrsta { get; set; }

        public string Status { get; set; }

        public int Ocjena { get; set; }

        public string Osvrt { get; set; }

        public int UserId { get; set; }
    }
}
