namespace WebRetoTecnico.Models
{
    public class ComplejosDeportivos
    {
        public int i_ComplejoId { get; set; }
        public SedesOlimpicas? refSedeOlimpica { get; set; }
        public string? v_TipoComplejo{ get; set; }
        public string? v_Localizacion{ get; set; }
        public string? v_JefeOrganizacion { get; set; }
        public string? v_AreaTotalOcupada { get; set; }

    }
}
