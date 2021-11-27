namespace ApiWebToken.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Cancion")]
    public partial class Cancion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cancion()
        {
            this.Favoritoes = new HashSet<Favorito>();
        }
    
        public int cancionID { get; set; }
        public int artistaID { get; set; }
        public string nombre { get; set; }
    
        public virtual Artista Artista { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorito> Favoritoes { get; set; }
    }
}
