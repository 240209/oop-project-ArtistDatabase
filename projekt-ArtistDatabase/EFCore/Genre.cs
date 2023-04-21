using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_ArtistDatabase.EFCore
{
    public class Genre
    {
        [Key]
        public Guid Id { get; private set; }

        [Required]
        public string Name { get; set; }

        public virtual IList<Artist> Artists { get; set; }

        public Genre()
        {
            Artists = new List<Artist>();
        }
    }
}
