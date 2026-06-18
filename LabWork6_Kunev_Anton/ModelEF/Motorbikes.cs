namespace LabWork6_Kunev_Anton.ModelEF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Table_Motorbike")]
    public partial class Motorbikes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string Model { get; set; }

        [StringLength(50)]
        public string Brand { get; set; }

        public double? Price { get; set; }

        public int? Horsepower { get; set; }

        public double? Mileage { get; set; }

        public string Picture { get; set; }
    }
}


