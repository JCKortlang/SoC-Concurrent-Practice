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
    class Program
    {
        static int MAX_SUPPLIERS = 3;
        static int MAX_AGENCIES = 5;

        public static Buffer buffer = new Buffer(3);

        public static HotelSupplier[] suppliers = new HotelSupplier[MAX_SUPPLIERS];
        public static TravelAgency[] agencies = new TravelAgency[MAX_AGENCIES];
        private static Thread[] priceGenerators = new Thread[MAX_SUPPLIERS];
        private static Thread[] orderProcessors = new Thread[MAX_SUPPLIERS];

        static void Main(string[] args)
        {
            for (int i = 0; i < MAX_SUPPLIERS; i++ )
            {
                agencies[i] = new TravelAgency(i);
                suppliers[i] = new HotelSupplier(i);
                priceGenerators[i] = new Thread(suppliers[i].generatePrices);
                priceGenerators[i].Name = "PriceGenerator " + i;
                orderProcessors[i] = new Thread(suppliers[i].processOrders);
                orderProcessors[i].Name = "orderProcessor " + i;

                HotelSupplier.priceCutEvent += agencies[i].priceCut;
            }
            for (int i = 3; i < MAX_AGENCIES; i++)
            {
                agencies[i] = new TravelAgency(i);
                HotelSupplier.priceCutEvent += agencies[i].priceCut;
            }

            for (int i = 0; i < MAX_SUPPLIERS; i++)
            {
                priceGenerators[i].Start();
                orderProcessors[i].Start();
            }

            orderProcessors[0].Join();
            orderProcessors[1].Join();
            orderProcessors[2].Join();

            Console.ReadKey();

        }
    }
}
