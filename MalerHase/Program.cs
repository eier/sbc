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
                XcoQueue<Ei> unbemalt = space.Get<XcoQueue<Ei>>("UnbemalteEier", new Uri("xco://127.0.0.1:8000"));
                XcoQueue<Ei> bemalt = space.Get<XcoQueue<Ei>>("BemalteEier", new Uri("xco://127.0.0.1:8000"));

                while (true)
                {
                    Ei e = unbemalt.Dequeue(true);
                    e.Maler = args[1];
                    e.Farbe = args[2];
                    bemalt.Enqueue(e);
                }
            }
        }
    }
}
