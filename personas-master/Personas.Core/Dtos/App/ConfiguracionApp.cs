namespace Personas.Core.App
{
    public class ConfiguracionApp
    {

        public string guid { get; set; }
        public int codigoUsuarioRegistra { get; set; }

        private string _idioma;
        public string Idioma
        {
            get => _idioma.ToLower();
            set
            {
                _idioma = value;
            }
        }

        private string _modulo { get; set; }

        public string Modulo
        {
            get => _modulo.ToLower();
            set
            {
                _modulo = value;
            }
        }
        public int codigoAgencia { get; set; }

        public ConfiguracionApp() { }

    }
}
