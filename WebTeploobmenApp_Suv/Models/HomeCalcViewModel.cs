using Microsoft.CodeAnalysis.Options;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace WebTeploobmenApp_Suv.Models
{
    public class HomeCalcViewModel
    {
        public List<CalculationResult> Result { get; set; }
        public double? Height { get; set; }
        public double? Diameter_of_pellets { get; set; }
        public double? Temp_pellets { get; set; }
        public double? Temp_air { get; set; }
        public double? Speed_air { get; set; }
        public double? Avg_heat_capacity { get; set; }
        public double? Consumption_of_pellets { get; set; }
        public double? Heat_capacity_of_pellets { get; set; }
        public double? Diameter { get; set; }
        public double? Volumetric_heat_transfer_coefficient { get; set; }

        public List<double> TempMaterial { get; set; } = [ 260, 200, 150, 120, 100 ];
        public List<double> TempGas { get; set; } = [ 100, 120, 150, 190, 250 ];

    }
}
