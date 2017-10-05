using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace GrowerApp.Model
{
    [Table("News")]
    public class News
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Headline { get; set; }

        [NotNull]
        public string Image { get; set; }

        [NotNull]
        public string NewsDetails { get; set; }
    }
}
