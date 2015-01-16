using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSE445_A2_JCKortlang
{
    /**
     * Authors: Jan Christian Chavez-Kortlang
     * For: CSE445 HW2 - Dr. Janaka Balasooriya
     **/
    class TravelAgency
    {
        public TravelAgency(int id)
        {
            this.id = id;
            this.creditCardNumber = new Random((int)System.DateTime.UtcNow.Ticks).Next(1000000000)/ 1000000;
        }
        public void priceCut(double currentPrice, double newPrice)
        {
            double discount = 100 - (newPrice / currentPrice)*100;
            Order order = new Order(this.id, calculateRoomCount(discount), this.creditCardNumber);
            order.requested();
            string encryptedOrder = Serializer.encrypt(order);
            Thread.Sleep(1000);
            Program.buffer.write(encryptedOrder);
        }

        public bool verifyCreditCard(int creditCardNumber)
        {
            return creditCardNumber == this.creditCardNumber;
        }

        private int calculateRoomCount(double discount)
        {
            int rooms = (int)discount;
            return rooms > 0 ? rooms : 1;
        }

        private int id;
        private int creditCardNumber;
    }
}
