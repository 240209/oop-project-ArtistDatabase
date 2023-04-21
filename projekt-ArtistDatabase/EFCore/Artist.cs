using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_ArtistDatabase.EFCore
{
    public class Artist
    {
        [Key]
        public Guid Id { get; private set; }

        [Required]
        public string Name { get; set; }

        public virtual IList<Genre> Genres { get; set; }
        public virtual IList<Album> Albums { get; set; }

        public Artist()
        {
            Genres = new List<Genre>();
            Albums = new List<Album>();
        }
    }
}
