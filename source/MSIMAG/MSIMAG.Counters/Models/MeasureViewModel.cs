namespace MSIMAG.Counters.Models
{
    public enum CounterType
    {       
        heißes_Wasser=1,
        kaltes_Wasser=2,
        Heizung = 3,
        elektrischer =4,
        Gas=5
    }

    public enum Protocol
    {
         Übergabe=1,
         Abnahme=2,
         Einbau= 3,
         Ausbau = 4
    }

    public class MeasureViewModel
    {
        /// <summary>
        /// pcfsystemfield108
        /// </summary>
        public Guid Unit { get; set; }
        /// <summary>
        /// pcfsystemfield106
        /// </summary>
        public bool IsMainCounter { get; set; }
        /// <summary>
        /// pcfsystemfield102
        /// </summary>
        public string Position { get; set; }
        
        /// <summary>
        /// customobject1021
        /// </summary>
        public int CounterType { get; set; }

        /// <summary>
        /// pcfsystemfield107
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// pcfsystemfield103
        /// </summary>
        public string CounterNumber { get; set; }

        /// <summary>
        /// pcfsystemfield104
        /// </summary>
        public string Reading { get; set; }

        public IFormFile? FileUpload { get; set; }


    }
}
