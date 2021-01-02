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
            
            for(int i = 0; i < 3; i++){
                myNet.update();
                myNet.displayStats();
                myNet.displayMessages();
                if(myNet.getDifferenceProductionConsumption() > 0){//surproduction
                    myCC.setConsumption(3,myNet.getDifferenceProductionConsumption());
                }
                if(myNet.getDifferenceProductionConsumption() < 0){//sousproduction
                    myCC.modProduction(1,-myNet.getDifferenceProductionConsumption());
                }
                //delay de 1 sec
                System.Threading.Thread.Sleep(1000);
            }
            
        }

        public static Network exampleNet(){
            SimpleMarketSimulation france_market = new SimpleMarketSimulation();
            SimpleWeatherSimulation france_weather = new SimpleWeatherSimulation(0,0);
            GasPlant myGas = new GasPlant(1000,3,france_market);
            SolarPlant mySun = new SolarPlant(0,100,france_weather);
            BuyElectricity myBuy = new BuyElectricity(0,0,france_market);

            City myCity = new City(800);
            Company myCompany = new Company(400);
            Dissipaters myDisip = new Dissipaters(0);

            DistribNode myD1 = new DistribNode();
            Wire w1 = new Wire(myGas, myD1, 1000);
            DistribNode myD2 = new DistribNode();
            Wire w2 = new Wire(mySun, myD2, 1000);
            DistribNode myD3 = new DistribNode();
            Wire w3 = new Wire(myBuy, myD3, 1000);

            Double[] distribution = { 0.33333, 0.33333, 0.33333};
            List<Double> distributionList = new List<Double>();  
            distributionList.AddRange(distribution);
            myD1.setDistribution(distributionList);
            myD2.setDistribution(distributionList);
            myD3.setDistribution(distributionList);
            
            ConcentNode myC1 = new ConcentNode();
            Wire w4 = new Wire(myCity, myC1, 1000);
            ConcentNode myC2 = new ConcentNode();
            Wire w5 = new Wire(myCompany, myC2, 1000);
            ConcentNode myC3 = new ConcentNode();
            Wire w6 = new Wire(myDisip, myC3, 1000);

            Wire w7 = new Wire(myD1, myC1, 1000);
            Wire w8 = new Wire(myD1, myC2, 1000);
            Wire w9 = new Wire(myD1, myC3, 1000);
            Wire w10 = new Wire(myD2, myC1, 1000);
            Wire w11 = new Wire(myD2, myC2, 1000);
            Wire w12 = new Wire(myD2, myC3, 1000);
            Wire w13 = new Wire(myD3, myC1, 1000);
            Wire w14 = new Wire(myD3, myC2, 1000);
            Wire w15 = new Wire(myD3, myC3, 1000);

            Producer[] prod = { myGas, mySun, myBuy };
            List<Producer> prodList = new List<Producer>();  
            prodList.AddRange(prod); 

            Consumer[] cons = {myCity, myCompany, myDisip};
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
