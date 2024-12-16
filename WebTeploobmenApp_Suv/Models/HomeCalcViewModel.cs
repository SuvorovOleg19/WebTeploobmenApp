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

        //public string OperationTypeName
        //{
        //    get
        //    {
        //        if (OperationTypes.TryGetValue(OperationType, out string? value)) return OperationTypes[OperationType];

        //        return "";
        //    }
        //}
            
        //    => OperationType switch
        //{
        //    1 => "Сложение",
        //    2 => "Вычитание",
        //    3 => "Умножение",
        //    4 => "Деление",
        //    _ => "" 
        //};

        //public static Dictionary<int, string> OperationTypes = new()
        //{
        //    [1] = "Сложение",
        //    [2] = "Вычитание",
        //    [3] = "Умножение",
        //    [4] = "Деление"
        //};
    }
}
