using System;
using System.ComponentModel.DataAnnotations;

namespace WaterLogger.Models
{
    public class DrinkingWaterModel
    {
        public int Id {  get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Value must be non negetive.")]
        public int Quantity { get; set; }
    }
}
