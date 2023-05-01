using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_ArtistDatabase.EFCore
{
    public class Album
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public Guid ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
        public Album()
        {
            Id = Guid.NewGuid();
        }
    }
}
