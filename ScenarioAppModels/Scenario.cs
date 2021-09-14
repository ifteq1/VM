using System;

namespace ScenarioAppModels
{
    public class Scenario
    {
        public int ScenarioID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Forename { get; set; }

        public string UserID { get; set; }

        public DateTime? SampleDate { get; set; }

        public string SampleDateDisplay => SampleDate.Value.ToString("dd/MM/yyyy HH:mm");

        public DateTime? CreationDate { get; set; }
        public string CreationDateDisplay => CreationDate.Value.ToString("dd/MM/yyyy HH:mm");
      
        public int NumMonths { get; set; }

        public int MarketID { get; set; }

        public int NetworkLayerID { get; set; }
    }
}
