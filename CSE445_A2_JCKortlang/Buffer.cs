using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CSE445_A2_JCKortlang
{
    /**
     * Authors: Jan Christian Chavez-Kortlang
     * For: CSE445 HW2 - Dr. Janaka Balasooriya
     **/
    public class Buffer
    {
        public Buffer(int size)
        {
            this.size = size;
            writeIndex = 0;
            readIndex = 0;
            contentCount = 0;
            cells = new String[size];
        }
        public void write(string encryptedOrder)
        {
            if(Monitor.TryEnter(this))
            {
                Console.WriteLine("\n{0} ENTERED WRITE", Thread.CurrentThread.Name);
                try
                {
                    while (this.contentCount == this.size)
                    {
                        Console.WriteLine("WAIT: {0}", Thread.CurrentThread.Name);
                        Monitor.Wait(this);
                    }

                    cells[writeIndex] = encryptedOrder;
                    this.contentCount++;
                    Console.WriteLine("\n{0} WROTE: {1}, TO: {2}", Thread.CurrentThread.Name, encryptedOrder, writeIndex);
                    this.writeIndex = (writeIndex + 1) % size;
                }
                catch (Exception error)
                {
                    throw error;
                }
                finally
                {
                    Monitor.Pulse(this);
                    Monitor.Exit(this);
                    Console.WriteLine("\n{0} LEFT WRITE", Thread.CurrentThread.Name);
                }
            }
            else
            {
                Console.WriteLine("\n{0} ENTER WRITE REJECTED", Thread.CurrentThread.Name);
                Thread.Sleep(150);
                write(encryptedOrder);
            }
        }

        public string read()
        {
            if(Monitor.TryEnter(this))
            {
                Console.WriteLine("\n{0} ENTERED READ", Thread.CurrentThread.Name);
                string encryptedString;
                try
                {
                    while (this.contentCount == 0)
                    {
                        Console.WriteLine("WAIT: {0}", Thread.CurrentThread.Name);
                        Monitor.Wait(this);
                    }

                    encryptedString = this.cells[readIndex];
                    this.contentCount--;
                    Console.WriteLine("\n{0} READ: {1}, FROM: {2}", Thread.CurrentThread.Name, encryptedString, this.readIndex);
                    this.readIndex = (this.readIndex + 1) % this.size;
                    return encryptedString;
                }
                catch (Exception error)
                {
                    throw error;
                }
                finally
                {
                    Console.WriteLine("\n{0} LEFT READ", Thread.CurrentThread.Name);
                    Monitor.Pulse(this);
                    Monitor.Exit(this);
                }
            }
            else
            {
                Console.WriteLine("\n{0} ENTER READ REJECTED", Thread.CurrentThread.Name);
                Thread.Sleep(100);
                return read();
            }

        }

        private int size;
        private int writeIndex;
        private int readIndex;
        private int contentCount;
        private String[] cells;

    }
}
