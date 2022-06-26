using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ejercico1_4TomarFoto.Models
{
    public class Foto
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public Byte [] foto { get; set; }
    }
}
