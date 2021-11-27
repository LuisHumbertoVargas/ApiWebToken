namespace ApiWebToken.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Perfil")]
    public partial class Perfil
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Perfil()
        {
            this.Favoritoes = new HashSet<Favorito>();
        }

        public int perfilID { get; set; }
        public int usuarioID { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string edad { get; set; }
        public string sexo { get; set; }
        public string relacion { get; set; }
        public string religion { get; set; }
        public string ciudad { get; set; }
        public string pais { get; set; }
    
        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorito> Favoritoes { get; set; }
        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }
    }
}
