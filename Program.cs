using System;
using Simulation;
using Prod;
using Cons;
using WireNode;
using System.Collections.Generic;
using Net;
using ControlCenter_file;

namespace projet_labo_cinfo
{
    class Program
    {
        static void Main(string[] args)
        {   
            
            Network myNet = exampleNet();
            ControlCenter myCC = new ControlCenter(myNet);
            //myCC.switchOff(1);
            //double[] distribution = {1,0,0};
            //myCC.modDistribution(1,distribution);
            //myCC.setProduction(1,1000);
            //myCC.setConsumption(1,1000);
            myNet.update();
            Console.WriteLine(myNet.getTotalProduction());
            Console.WriteLine(myNet.getTotalCost());
            Console.WriteLine(myNet.getTotalEmission());
            Console.WriteLine(myNet.getTotalConsumption());
            myNet.displayMessages();
            
        }

        public static Network exampleNet(){
            MarketSimulation france_market = new MarketSimulation();
            WeatherSimulation france_weather = new WeatherSimulation(0,0);
            //NuclearPlant myNuke = new NuclearPlant(1000,0,france_market);
            GasPlant myNuke = new GasPlant(1000,0,france_market);
            SolarPlant mySun = new SolarPlant(0,100,france_weather);
            BuyElectricity myBuy = new BuyElectricity(0,0,france_market);
            //Console.WriteLine(myprod.getProduction());
            //Console.WriteLine(myprod.getCost());
            //Console.WriteLine(myprod.getEmission());
            City myCity = new City(800);
            Company myCompany = new Company(400);
            Dissipaters myDisip = new Dissipaters(0);
            //
            DistribNode myD1 = new DistribNode();
            Wire w1 = new Wire(myNuke, myD1, 10000);
            DistribNode myD2 = new DistribNode();
            Wire w2 = new Wire(mySun, myD2, 10000);
            DistribNode myD3 = new DistribNode();
            Wire w3 = new Wire(myBuy, myD3, 10000);

            Double[] distribution = { 0.333, 0.333, 0.333};
            List<Double> distributionList = new List<Double>();  
            distributionList.AddRange(distribution);
            myD1.setDistribution(distributionList);
            myD2.setDistribution(distributionList);
            myD3.setDistribution(distributionList);
            
            ConcentNode myC1 = new ConcentNode();
            Wire w4 = new Wire(myCity, myC1, 10000);
            ConcentNode myC2 = new ConcentNode();
            Wire w5 = new Wire(myCompany, myC2, 10000);
            ConcentNode myC3 = new ConcentNode();
            Wire w6 = new Wire(myDisip, myC3, 10000);

            Wire w7 = new Wire(myD1, myC1, 10000);
            Wire w8 = new Wire(myD1, myC2, 10000);
            Wire w9 = new Wire(myD1, myC3, 10000);
            Wire w10 = new Wire(myD2, myC1, 10000);
            Wire w11 = new Wire(myD2, myC2, 10000);
            Wire w12 = new Wire(myD2, myC3, 10000);
            Wire w13 = new Wire(myD3, myC1, 10000);
            Wire w14 = new Wire(myD3, myC2, 10000);
            Wire w15 = new Wire(myD3, myC3, 10000);

            Producer[] prod = { myNuke, mySun, myBuy };
            List<Producer> prodList = new List<Producer>();  
            prodList.AddRange(prod); 

            Consumer[] cons = { myCompany, myCity, myDisip };
            List<Consumer> consList = new List<Consumer>();  
            consList.AddRange(cons); 

            DistribNode[] distrib = { myD1, myD2, myD3 };
            List<DistribNode> distribList = new List<DistribNode>();  
            distribList.AddRange(distrib); 

            ConcentNode[] concent = { myC1, myC2, myC3 };
            List<ConcentNode> concentList = new List<ConcentNode>();  
            concentList.AddRange(concent); 
        
            Wire[] wire = { w1, w2, w3, w4, w5, w6, w7, w8, w9, w10, w11, w12, w13, w14, w15 };
            List<Wire> wireList = new List<Wire>();  
            wireList.AddRange(wire); 

            Network myNet = new Network(consList, prodList, distribList, concentList, wireList);

            return myNet;

        }
        
    }
}
