using System.ComponentModel.DataAnnotations;

namespace WebTeploobmenApp_Suv.Data
{
    public class Variant
    {
        [Key]
        public int Id { get; set; }

        public int Num1 { get; set; }
        public int Num2 { get; set; }
        public int OperationType { get; set; }

    }
}
