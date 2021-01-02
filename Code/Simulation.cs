namespace Simulation
{

    public abstract class WeatherSimulation{
        protected double x;
        protected double y;
        public WeatherSimulation(double x, double y)
        {
            this.x=y;
            this.y=y;
        }

        public abstract double Sunshine();
        public abstract double Wind();
        public abstract double Temperature();

    }

    public class SimpleWeatherSimulation : WeatherSimulation
    {   
        
        public SimpleWeatherSimulation(double x, double y) : base(x,y)
        {
            this.x=y;
            this.y=y;
        }
        public override double Sunshine()
        {
            return 50;
        }

        public override double Wind()
        {
            return 400;
        }

        public override double Temperature()
        {
            return 20;
        }
    }

    public abstract class MarketSimulation{
        
        public abstract double ElectricityBuyPrice();
        public abstract double ElectricitySellPrice();
        public abstract double UraniumPrice();
        public abstract double GasPrice();

    }

    public class SimpleMarketSimulation : MarketSimulation
    {   
        public override double ElectricityBuyPrice()
        {
            return 0.20;
        }

        public override double ElectricitySellPrice()
        {
            return 0.15;
        }

        public override double UraniumPrice()
        {
            return 60;
        }

        public override double GasPrice()
        {
            return 5;
        }
    }
}