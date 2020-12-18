using Simulation;
using WireNode;

namespace Prod
{

    public abstract class Producer
    {
        protected int number;
        protected static int n = 1;
        protected Wire wire;
        public abstract double getProduction();
        public abstract double getCost();
        public abstract double getEmission();
        public void setWire(Wire w){
            this.wire = w;

        }
        public Wire getWire(){
            return this.wire;

        }

        public int getNumber()
        {
           return number ;
        }
    }

    public class GasPlant : Producer
    {
        private double emission;
        private double production;
        private MarketSimulation marketsimulation;
        public GasPlant(double production, double emission, MarketSimulation marketsimulation)
        {
            this.production=production;
            this.emission=emission;
            this.marketsimulation=marketsimulation;
            this.number = n;
            n++;
        }

        public override double getProduction()
        {
            return production;
        }

        public void setProduction(double value)
        {
            production=value;
        }
        
        public override double getCost()
        {
            return marketsimulation.GasPrice()*production;
        }
        
        public override double getEmission()
        {
            return emission*production;
        }

    }

    public class NuclearPlant : Producer
    {
        private double production;
        private double emission;
        private double production_real;
        private MarketSimulation marketsimulation;
        public NuclearPlant(double production, double emission, MarketSimulation marketsimulation)
        {
            this.production=production;
            this.production_real=production;
            this.emission=emission;
            this.marketsimulation=marketsimulation;
            this.number = n;
            n++;
        }

        public override double getProduction()
        {
            return production_real;
        }

        public void SwitchOff()
        {
            production_real=0;
        }

        public void SwitchOn()
        {
            production_real=production;
        }
        
        public override double getCost()
        {
            return marketsimulation.UraniumPrice()*production_real;
        }
        
        public override double getEmission()
        {
            return emission*production_real;
        }
    }

    public class WindPlant : Producer
    {
        private double emission;
        private double cost;
        private WeatherSimulation weathersimulation;
        public WindPlant(double emission, double cost, WeatherSimulation weathersimulation)
        {
            this.emission=emission;
            this.cost=cost;
            this.weathersimulation=weathersimulation;
            this.number = n;
            n++;
        }

        public override double getProduction()
        {
            return weathersimulation.Wind();
        }
        
        public override double getCost()
        {
            return cost*getProduction();
        }
        
        public override double getEmission()
        {
            return emission*getProduction();
        }

    }

    public class SolarPlant : Producer
    {
        private double emission;
        private double cost;
        private WeatherSimulation weathersimulation;
        public SolarPlant(double emission, double cost, WeatherSimulation weathersimulation)
        {
            this.emission=emission;
            this.cost=cost;
            this.weathersimulation=weathersimulation;
            this.number = n;
            n++;
        }

        public override double getProduction()
        {
            return weathersimulation.Sunshine();
        }
        
        public override double getCost()
        {
            return cost*getProduction();
        }
        
        public override double getEmission()
        {
            return emission*getProduction();
        }

    }

    public class BuyElectricity : Producer
    {
        private double production;
        private double emission;
        private MarketSimulation marketsimulation;
        public BuyElectricity(double production, double emission, MarketSimulation marketsimulation)
        {
            this.production=production;
            this.emission=emission;
            this.marketsimulation=marketsimulation;
            this.number = n;
            n++;
        }

        public override double getProduction()
        {
            return production;
        }

        public void setProduction(double value)
        {
            production=value;
        }
        
        public override double getCost()
        {
            return marketsimulation.ElectricityBuyPrice()*production;
        }
        
        public override double getEmission()
        {
            return emission*production;
        }

    }
}