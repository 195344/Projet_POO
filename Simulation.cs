namespace Simulation
{
    public class WeatherSimulation
    {   
        private double x;
        private double y;
        public WeatherSimulation(double x, double y)
        {
            this.x=y;
            this.y=y;
        }

        public double Sunshine()
        {
            //indicate here the code to compute sunshine at a certain point
            return 50;
        }

        public double Wind()
        {
            //indicate here the code to compute wind force at a certain point
            return 400;
        }

        public double Temperature()
        {
            //indicate here the code to compute temperature at a certain point
            return 20;
        }
    }

    public class MarketSimulation
    {   
        public double ElectricityBuyPrice()
        {
            //indicate here the code to compute the electricity buy price
            return 0.20;
        }

        public double ElectricitySellPrice()
        {
            //indicate here the code to compute the electricity sell price
            return 0.15;
        }

        public double UraniumPrice()
        {
            //indicate here code to compute uranium price
            return 60;
        }

        public double GasPrice()
        {
            //indicate here code to compute gas price
            return 5;
        }
    }
}