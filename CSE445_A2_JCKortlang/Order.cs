using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE445_A2_JCKortlang
{
    /**
     * Authors: Jan Christian Chavez-Kortlang
     * For: CSE445 HW2 - Dr. Janaka Balasooriya
     **/
    class Order
    {
        public Order(int requesterID, int orderSize, int creditCardNumber)
        {
            this.requesterID = requesterID;
            this.receiverID = -1;
            this.orderSize = orderSize;
            this.creditCardNumber = creditCardNumber;
        }

        // This constructor is used to instantiate an order from a string that was created using Order.toDecryptedString()
        public Order(string orderString)
        {
            int length = orderString.Length;
            int tokenCount = 0;
            string token = "";
            for (int i = 0; i < length; i++)
            {
                // We must accound for the end of string.
                if (orderString[i] != ',' && i != length-1)
                {
                    token += orderString[i];
                }
                else
                {
                    tokenCount++;
                    switch (tokenCount)
                    {
                        // requesterID
                        case 1:
                            int.TryParse(token, out this.requesterID);
                            break;
                        // receiverID
                        case 2:
                            int.TryParse(token, out this.receiverID);
                            break;
                        // orderSize
                        case 3:
                            int.TryParse(token, out this.orderSize);
                            break;
                        // creditCardNumber
                        case 4:
                            int.TryParse(token, out this.creditCardNumber);
                            break;
                        case 5:
                            token += orderString[i];
                            this.requestedOn = token;
                            break;
                        default:
                            break;
                    }

                    //Reset the token
                    token = "";
                }
            }
        }

        public void setReceiverID(int receiverID)
        {
            this.receiverID = receiverID;
        }

        public void requested()
        {
            this.requestedOn = DateTime.Now.ToLongTimeString();
        }

        public void processed()
        {
            this.processedOn = DateTime.Now.ToLongTimeString();
        }

        public int getRequesterID()
        {
            return this.requesterID;
        }

        public int getReceiverID()
        {
            return this.receiverID;
        }

        public int getOrderSize()
        {
            return this.orderSize;
        }

        public int getCreditCardNumber()
        {
            return this.creditCardNumber;
        }

        public string getRequestedOn()
        {
            return this.requestedOn;
        }

        public string getProcessedOn()
        {
            return this.processedOn;
        }

        //For debugging
        public bool Equals(Order otherOrder)
        {
            return this.requesterID == otherOrder.requesterID &&
                this.orderSize == otherOrder.orderSize &&
                this.creditCardNumber == otherOrder.creditCardNumber;
        }

        public string toDecryptedString()
        {
            return this.requesterID
                + "," + this.receiverID
                + "," + this.orderSize
                + "," + this.creditCardNumber
                + "," + this.requestedOn.ToString();
        }

        public override string ToString()
        {
            return "\nRequesterID: " + this.requesterID
                + "\nReceiverID: " + this.receiverID
                + "\nOrderSize: " + this.orderSize
                + "\nCreditCard: " + this.creditCardNumber
                + "\nRequestedOn: " + this.requestedOn
                + "\nProcessedOn: " + this.processedOn;
        }
        
        private int requesterID;
        private int receiverID;
        private int orderSize;
        private int creditCardNumber;
        private string requestedOn;
        private string processedOn;
    }
}
