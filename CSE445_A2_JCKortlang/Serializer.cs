using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using CSE445_A2_JCKortlang.EncryptionService;

namespace CSE445_A2_JCKortlang
{
    /**
     * Authors: Jan Christian Chavez-Kortlang
     * For: CSE445 HW2 - Dr. Janaka Balasooriya
     **/
    class Serializer
    {
        public static string encrypt(Order order)
        {                
            IService proxy = new EncryptionService.ServiceClient();
            return proxy.Encrypt(order.toDecryptedString());
        }

        public static Order decrypt(string encryptedOrder)
        {            
            IService proxy = new EncryptionService.ServiceClient();
            string decryptedString = proxy.Decrypt(encryptedOrder);
            return new Order(decryptedString);
        }
    }
}
