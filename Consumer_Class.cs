using WireNode;
namespace Cons
{


    public abstract class Consumer
    {
        private int number;
        private static int n = 1;
        protected double consumption;
        protected Wire wire;

        public Consumer(double consumption)
        {
            this.consumption=consumption;
            this.number = n;
            n++;
        }

        public double getConsumption()
        { 
            return consumption; 
        }

        public void setConsumption(double value)
        { 
            consumption = value; 
        }
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

    public class City : Consumer
    {
        public City(double consumption) : base(consumption){}
    }

    public class Company : Consumer
    {
        public Company(double consumption) : base(consumption){}
    }

    public class Dissipaters : Consumer
    {
        public Dissipaters(double consumption) : base(consumption){}
    }

}
