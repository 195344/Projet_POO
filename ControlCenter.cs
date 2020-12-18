using System;
using WireNode;
using Prod;
using Cons;
using Net;
using System.Collections.Generic;

namespace ControlCenter_file
{
    public class ControlCenter{

        Network network;

        public ControlCenter(Network network){
            this.network = network;
        }

        public void modDistribution(int x, double[] distribution){
            List<Double> distributionList = new List<Double>();  
            distributionList.AddRange(distribution);
            for (int i = 0; i < network.getDistribNodeList().Count ; i++) {
                if(network.getDistribNodeList()[i].getNumber()==x){
                    network.getDistribNodeList()[i].setDistribution(distributionList);
                }
            }
        }

        public void switchOff(int x){
            for (int i = 0; i < network.getProducerList().Count ; i++) {
                if(network.getProducerList()[i].getNumber()==x){
                    var nuclearPlant = (NuclearPlant)network.getProducerList()[i];
                    nuclearPlant.SwitchOff();
                }
            }
        }

        public void switchOn(int x){
            for (int i = 0; i < network.getProducerList().Count ; i++) {
                if(network.getProducerList()[i].getNumber()==x){
                    var nuclearPlant = (NuclearPlant)network.getProducerList()[i];
                    nuclearPlant.SwitchOn();
                }
            }
        }

        public void setProduction(int x, double production){
            for (int i = 0; i < network.getProducerList().Count ; i++) {
                if(network.getProducerList()[i].getNumber()==x){
                    if(network.getProducerList()[i] is GasPlant){
                        var gasPlant = (GasPlant)network.getProducerList()[i];
                        gasPlant.setProduction(production);
                    }
                    if(network.getProducerList()[i] is BuyElectricity){
                        var buyElectricity = (BuyElectricity)network.getProducerList()[i];
                        buyElectricity.setProduction(production);
                    }
                }
            }
        }

        public void setConsumption(int x, double consumption){
            for (int i = 0; i < network.getConsumerList().Count ; i++) {
                if(network.getConsumerList()[i].getNumber()==x){
                    network.getConsumerList()[i].setConsumption(consumption);
                }
            }
        }

        

    }

}