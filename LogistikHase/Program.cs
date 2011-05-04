using System;
using System.Collections.Generic;
using System.Text;
using XcoSpaces;
using XcoSpaces.Collections;
using XcoSpaces.Exceptions;
using Eier;

namespace LogistikHase
{
    class Program
    {
        static void Main(string[] args)
        {
            LogistikHase lh = new LogistikHase(args[0], new Uri("xco://127.0.0.1:8000"));
            lh.work();
        }
    }
}
