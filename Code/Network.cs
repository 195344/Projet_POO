using System;
using WireNode;
using Prod;
using Cons;
using System.Collections.Generic;

namespace Net
{
    public class Network{
        private List<Consumer> consumers;
        private List<Producer> producers;
        private List<DistribNode> distribNodes;
        private List<ConcentNode> concentNodes;
        private List<Wire> wires;
        private double totalProduction;
        private double totalEmission;
        private double totalCost;
        private double totalConsumption;
        private double differenceProductionConsumption;
        private List<string> messages = new List<string>();

        public Network(List<Consumer> consumers, List<Producer> producers, List<DistribNode> distribNodes, List<ConcentNode> concentNodes, List<Wire> wires){
            this.consumers = consumers;
            this.producers = producers;
            this.distribNodes = distribNodes;
            this.concentNodes = concentNodes;
            this.wires = wires; 
        }

        public void update(){
            totalProduction = computeTotalProduction();
            totalEmission = computeTotalEmission();
            totalCost = computeTotalCost();
            totalConsumption = computeTotalConsumption();

            differenceProductionConsumption = computeDifferenceProductionConsumption();
            
            updateLine();
            writeMessages();
        }

        public void updateLine(){
            //ligne adjacente aux centrales
            foreach (Producer prod in producers){
                prod.getWire().setActivCurrent(prod.getProduction());//met la valeur de la production de la centrale dans le courrant actif du fil
                if(prod.getWire().getActivCurrent() > prod.getWire().getMaxCurrent()){
                    messages.Add("Attention : ligne W" + prod.getWire().getWireNumber() + " surchargee");
                }
            }
            //ligne connectee a un distribNode
            foreach (DistribNode node in distribNodes){
                for (int i = 0; i < node.getConnetions().Count ; i++) {
                    node.getConnetions()[i].setActivCurrent(node.getDistribution()[i] * node.getWire().getActivCurrent());//met la variable activeCurrent des connections à la bonne valeur en fonction du fil d'entree et de la distribution
                    if(node.getConnetions()[i].getActivCurrent() > node.getConnetions()[i].getMaxCurrent()){
                        messages.Add("Attention : ligne W" + node.getConnetions()[i].getWireNumber() + " surchargee");
                    }   
                }   

            }
            //ligne connectee a un concentNode
            foreach (ConcentNode node in concentNodes){
                double total = 0;
                for (int i = 0; i < node.getConnetions().Count ; i++) {
                    total += node.getConnetions()[i].getActivCurrent();//met la variable activeCurrent des connections à la bonne valeur en fonction du fil d'entree et de la distribution   
                }
                node.getWire().setActivCurrent(total);
                if(node.getWire().getActivCurrent() > node.getWire().getMaxCurrent()){
                        messages.Add("Attention : ligne W" + node.getWire().getWireNumber() + " surchargee");
                }

            }
            
        }

        private double computeTotalProduction(){
            double total = 0;
            foreach (Producer prod in producers){
                total += prod.getProduction();
            }
            return total;
        }

        private double computeTotalEmission(){
            double total = 0;
            foreach (Producer prod in producers){
                total += prod.getEmission();
            }
            return total;
        }

        private double computeTotalConsumption(){
            double total = 0;
            foreach (Consumer cons in consumers){
                total += cons.getConsumption();
            }
            return total;
        }

        private double computeTotalCost(){
            double total = 0;
            foreach (Producer prod in producers){
                total += prod.getCost();
            }
            return total;
        }

        private double computeDifferenceProductionConsumption(){
            return totalProduction - totalConsumption;
        }

        private void writeMessages(){
            if(differenceProductionConsumption > 0){
                messages.Add("Attention : surproduction de " + differenceProductionConsumption);
            }
            if(differenceProductionConsumption < 0){
                messages.Add("Attention : sousproduction de " + Math.Abs(differenceProductionConsumption));
            }
            if(totalProduction == 0){
                messages.Add("Attention : BLACKOUT");
            }
            //Erreur de sous et sur alimentation d'un consomateur
            foreach (Consumer cons in consumers){
                if(cons.getConsumption() > cons.getWire().getActivCurrent()){
                    if(!(cons is Dissipaters||cons is SellElectricity)){
                        messages.Add("Attention : sousalimentation du consomateur " + cons.getNumber());
                    }
                }
                if(cons.getWire().getActivCurrent() > cons.getConsumption()){
                    if(!(cons is Dissipaters||cons is SellElectricity)){
                        messages.Add("Attention : suralimentation du consomateur " + cons.getNumber());
                    }
                }
            }
        }

        public void displayMessages(){
            Console.WriteLine("");
            Console.WriteLine("Messages : ");
            Console.WriteLine("");
            foreach (string str in messages){
                Console.WriteLine(str);
            }
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("");

            messages.Clear();
        }

        public void displayStats(){
            Console.WriteLine("");
            Console.WriteLine("Stats : ");
            Console.WriteLine("");
            Console.WriteLine("Production total :" + getTotalProduction());
            Console.WriteLine("Cout total :" + getTotalCost());
            Console.WriteLine("Emission total :" + getTotalEmission());
            Console.WriteLine("Consomation total :" + getTotalConsumption());
        }

        public double getTotalProduction(){
            return totalProduction;
        }

        public double getTotalEmission(){
            return totalEmission;
        }

        public double getTotalCost(){
            return totalCost;
        }

        public double getTotalConsumption(){
            return totalConsumption;
        }

        public double getDifferenceProductionConsumption(){
            return differenceProductionConsumption;
        }

        public List<string> getMessages(){
            messages.Clear();
            return messages;
        }

        public List<Consumer> getConsumerList(){
            return consumers;
        }

        public List<Producer> getProducerList(){
            return producers;
        }

        public List<DistribNode> getDistribNodeList(){
            return distribNodes;
        }

    }
}