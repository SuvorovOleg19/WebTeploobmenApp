namespace WebTeploobmenApp_Suv.Models
{
    public class CalculationResult
    {
        public double Y_coordinate { get; set; }
        public double TempMaterial { get; set; }
        public double TempGas { get; set; }
        public double TempDiff { get; set; }
    }
    public class CalcModel
    {
        public double Height { get; set; }
        public double Diameter_of_pellets { get; set; }
        public double Temp_pellets { get; set; }
        public double Temp_air { get; set; }
        public double Speed_air { get; set; }
        public double Avg_heat_capacity { get; set; }
        public double Consumption_of_pellets { get; set; }
        public double Heat_capacity_of_pellets { get; set; }
        public double Diameter { get; set; }
        public double Volumetric_heat_transfer_coefficient { get; set; }

        public double Square =>
            Math.PI * Math.Pow(Diameter, 2) / 4;

        public double Ratio_of_heat_capacities() =>
            (Heat_capacity_of_pellets * Consumption_of_pellets) / (Speed_air * Square * Avg_heat_capacity);

        public double Relative_height_of_layer(double y) =>
            (Volumetric_heat_transfer_coefficient * y) / (Speed_air * Avg_heat_capacity * 1000);

        private double Exp1(double y) =>
            1 - Math.Exp(((Ratio_of_heat_capacities() - 1) * Relative_height_of_layer(y)) / Ratio_of_heat_capacities());

        private double Mexp1(double y) =>
            1 - Ratio_of_heat_capacities() * Math.Exp(((Ratio_of_heat_capacities() - 1) * Relative_height_of_layer(y)) / Ratio_of_heat_capacities());

        public List<CalculationResult> CalcResult()
        {
            var result = new List<CalculationResult>();
            for (double y = 0; y <= Height; y += 0.5)
            {
                var upsilon = Exp1(y) / (Mexp1(Height));
                var theta = Mexp1(y) / (Mexp1(Height));
                var t = Temp_pellets + (Temp_air - Temp_pellets) * upsilon;
                var T = Temp_pellets + (Temp_air - Temp_pellets) * theta;

                result.Add(new CalculationResult
                {
                    Y_coordinate = y,
                    TempMaterial = Math.Round(t),
                    TempGas = Math.Round(T),
                    TempDiff = Math.Round(t - T)
                });
            }
            return result;
        }
    }
}
