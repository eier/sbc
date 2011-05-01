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
            NestHase nh = new NestHase(args[0], new Uri("xco://127.0.0.1:8000"));
            nh.work();
        }
    }
}
