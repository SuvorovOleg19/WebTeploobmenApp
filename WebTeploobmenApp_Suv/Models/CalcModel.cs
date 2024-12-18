namespace WebTeploobmenApp_Suv.Models
{
    public class CalculationResult
    {
        public double Y_coordinate { get; set; } // Координата по высоте слоя
        public double TempMaterial { get; set; } // Температура материала
        public double TempGas { get; set; } // Температура газа
        public double TempDiff { get; set; } // Разница температур
    }

    public class CalcModel
    {
        // Свойства исходных данных
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

        // Вычисляемая площадь аппарата
        public double Square => Math.PI * Math.Pow(Diameter, 2) / 4;

        // Соотношение теплоёмкостей
        public double Ratio_of_heat_capacities() =>
            (Heat_capacity_of_pellets * Consumption_of_pellets) /
            (Speed_air * Square * Avg_heat_capacity);

        // Относительная высота слоя
        public double Relative_height_of_layer(double y) =>
            (Volumetric_heat_transfer_coefficient * y) /
            (Speed_air * Avg_heat_capacity * 1000);

        // Расчёт Upsilon
        private double CalculateUpsilon(double y, double ratio, double relativeHeight) =>
            1 - Math.Exp(((ratio - 1) * relativeHeight) / ratio);

        // Расчёт Theta
        private double CalculateTheta(double y, double ratio, double relativeHeight) =>
            1 - ratio * Math.Exp(((ratio - 1) * relativeHeight) / ratio);

        // Основной метод расчётов
        public List<CalculationResult> CalcResult()
        {
            var result = new List<CalculationResult>();
            var ratio = Ratio_of_heat_capacities();
            var maxRelativeHeight = Relative_height_of_layer(Height);
            var maxMexp = CalculateTheta(Height, ratio, maxRelativeHeight);

            for (double y = 0; y <= Height; y += 0.5)
            {
                var relativeHeight = Relative_height_of_layer(y);
                var upsilon = CalculateUpsilon(y, ratio, relativeHeight) / maxMexp;
                var theta = CalculateTheta(y, ratio, relativeHeight) / maxMexp;

                // Температуры
                var t = Temp_pellets + (Temp_air - Temp_pellets) * upsilon; // Температура материала
                var T = Temp_pellets + (Temp_air - Temp_pellets) * theta;   // Температура газа

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
