using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CSE445_A2_JCKortlang
{
    /**
     * Authors: Jan Christian Chavez-Kortlang
     * For: CSE445 HW2 - Dr. Janaka Balasooriya
     **/
    public delegate void PriceChanged(double currentPrice, double newPrice);

    class HotelSupplier
    {
        public static event PriceChanged priceCutEvent;

        public HotelSupplier(int id)
        {
            this.id = id;
            this.currentPrice = 500;
            this.priceCutCount = 0;
        }
        public void generatePrices()
        {
            for (int i = 0; i < LOOP_LIMIT && priceCutCount < PRICE_CUT_LIMIT; i++)
            {
                calculatePrice(i);
            }
        }

        public void processOrders()
        {
            for (int i = 0; i < 15;i++)
            {
                Thread processOrder = new Thread(() => this.processOrder());
                processOrder.Name = "OrderProcessor " + this.id;
                processOrder.Start();
            }
        }
        private void processOrder()
        {
            string encryptedOrder = Program.buffer.read();
            Order decryptedOrder = Serializer.decrypt(encryptedOrder);
            decryptedOrder.setReceiverID(this.id);

            if (Program.agencies[decryptedOrder.getRequesterID()].verifyCreditCard(decryptedOrder.getCreditCardNumber()))
            {
                decryptedOrder.processed();
                Console.WriteLine("\nOrder Processed @ Supplier {0}\n{1}", this.id, decryptedOrder);
            }
            else
            {
                Console.WriteLine("\nOrder Rejected @ Supplier {0}\n{1} - Invalid CreditCard", this.id, decryptedOrder);
            }    
        }

        private void calculatePrice(int i)
        {
            Random priceGenerator = new Random((int)System.DateTime.UtcNow.Ticks);
            double newPrice = priceGenerator.Next(500000000) / 1000000;

            if(this.currentPrice > newPrice)
            {
                this.priceCutCount++;
                Console.WriteLine("\nCurrent Price: {0:C}\nNew Price: {1:C}", currentPrice, newPrice);
                //Only call the event if it has been subscribed to.
                if(priceCutEvent != null)
                {
                    priceCutEvent(currentPrice, newPrice);
                }            
            }
            this.currentPrice = newPrice;
        }

        private int id;
        private int priceCutCount;
        private int PRICE_CUT_LIMIT = 3;
        private int LOOP_LIMIT = 1000;
        private double currentPrice;
    }
}
