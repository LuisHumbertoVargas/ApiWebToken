namespace ApiWebToken.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Favorito")]
    public partial class Favorito
    {
        public int favoritoID { get; set; }
        public int perfilID { get; set; }
        public int cancionID { get; set; }
    
        [JsonIgnore]
        public virtual Cancion Cancion { get; set; }
        [JsonIgnore]
        public virtual Perfil Perfil { get; set; }
        
    }
        
}
