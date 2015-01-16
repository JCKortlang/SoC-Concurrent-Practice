using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSE445_A2_JCKortlang
{
    class Agency
    {
        public Agency()
        {
            ;
        }

        public void priceCutEventHandler(double currentPrice, double newPrice)
        {
            Thread orderRequest = new Thread(() => priceCut(currentPrice, newPrice));
            orderRequest.Start();
        }

        //Event Handler
        private void priceCut(double currentPrice, double newPrice)
        {
            double discount = 100 - (newPrice / currentPrice)*100;
            Order order = new Order(1, this.calculateRoomCount(discount), this.generateCreditCardNumber());
            order.requested();
            string encryptedOrder = Serializer.encrypt(order);

            //MultiCellBuffer.setOneCell(encryptedOrder);
            Program.buffer.write(encryptedOrder);
        }

        private int generateCreditCardNumber()
        {
            return new Random((int)System.DateTime.UtcNow.Ticks).Next(1000000000) / 1000000;
        }

        private int calculateRoomCount(double discount)
        {
            return (int)discount;
        }
    }
}
