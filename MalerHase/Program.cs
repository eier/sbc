using System;
using System.Collections.Generic;
using System.Threading;
using XcoSpaces;
using XcoSpaces.Collections;
using XcoSpaces.Exceptions;
using Eier;

namespace MalerHase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (XcoSpace space = new XcoSpace(0))
            {
                try
                {
                    XcoQueue<Ei> queue = space.Get<XcoQueue<Ei>>("UnbemalteEier", new Uri("xco://10.0.0.104:8000"));
                    queue.Enqueue(new Ei("test"));
                }
                catch (XcoException e)
                {
                    Console.WriteLine("Unable to reach queue! Stopping..." + e.ToString());
                }
            }
        }
    }
}
