using System;
using System.Collections.Generic;
using System.Threading;
using XcoSpaces;
using XcoSpaces.Collections;
using XcoSpaces.Exceptions;
using Eier;

namespace _MalerHase
{
    
    class Program
    {

        static void Main(string[] args)
        {
            MalerHase mh = new MalerHase(args[0], new Uri("xco://127.0.0.1:8000"), args[1]);
            mh.work();
        }
    }
}
