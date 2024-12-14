using Microsoft.CodeAnalysis.Options;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace WebTeploobmenApp_Suv.Models
{
    public class HomeCalcViewModel
    {
        public int? Result { get; set; }
        public int? Num1 { get; set; }
        public int? Num2 { get; set; }
        public int OperationType { get; set; }

        public string OperationTypeName
        {
            get
            {
                if (OperationTypes.TryGetValue(OperationType, out string? value)) return OperationTypes[OperationType];

                return "";
            }
        }
            
        //    => OperationType switch
        //{
        //    1 => "Сложение",
        //    2 => "Вычитание",
        //    3 => "Умножение",
        //    4 => "Деление",
        //    _ => "" 
        //};

        public static Dictionary<int, string> OperationTypes = new()
        {
            [1] = "Сложение",
            [2] = "Вычитание",
            [3] = "Умножение",
            [4] = "Деление"
        };
    }
}
