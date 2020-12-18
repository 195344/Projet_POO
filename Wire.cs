
using Prod;
using Cons;
using System.Collections.Generic;


namespace WireNode{

    

    public abstract class Node
    {
        
        protected Wire lonelyWire;
        protected List<Wire> connetions; 

        public List<Wire> getConnetions ()
        {
            return connetions;
        }
        public void setWire(Wire w){
            this.lonelyWire = w;
        }

        public Wire getWire(){
            return lonelyWire;
        }

        public void addConnection(Wire w){
            this.connetions.Add(w);
        }
      


    }
    
    public class DistribNode: Node
    {
        //protected Wire input;
        protected List<double> distribution; 
        private int number;
        private static int n = 1;

        public DistribNode()//node or producer
        {
            this.connetions = new List<Wire>();
            this.distribution = new List<double>();
            this.number = n;
            n++;

        }

        public void setDistribution(List<double> distribution){
            this.distribution = distribution;
        }

        public List<double> getDistribution(){
            return distribution;
        }

        public int getNumber()
        {
           return number ;
        }

    }
    
    public class ConcentNode: Node
    {
        public ConcentNode()
        {
            this.connetions = new List<Wire>();

        }
    }




    public class Wire
    {
        private static int n = 1;
        private int number;
        private double maxCurrent;
        private double activCurrent;
        
        private object a;
        private object b;


        public Wire( Consumer b, ConcentNode a , double maxCurrent)
        {
            this.number = n;
            n++;
            this.a = a;
            this.b = b;
            this.maxCurrent = maxCurrent;
            a.setWire(this);
            b.setWire(this);
        }
        public Wire( Node a, Node b , double maxCurrent )
        {
            this.number = n;
            n++;
            this.a = a;
            this.b = b;
            this.maxCurrent = maxCurrent;
            a.addConnection(this);
            b.addConnection(this);

        }
        public Wire(Producer a, DistribNode b, double maxCurrent)
        {
            this.number = n;
            n++;
            this.a = a;
            this.b = b;
            this.maxCurrent = maxCurrent;
            a.setWire(this);
            b.setWire(this);

        }

        public int getWireNumber()
        {
           return number ;
        }

        public double getActivCurrent()
        {
           return activCurrent ;
        }

        public void setActivCurrent(double activCurrent)
        {
            this.activCurrent=activCurrent;
        }

        public double getMaxCurrent()
        {
           return maxCurrent;
        }

        public object getConnetionA()
        {
            return a;

        }
        public object getConnetionB()
        {
            return b; 

        }

    }
}