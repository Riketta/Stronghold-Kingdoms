namespace Kingdoms
{
    using System;

    public class SHKMutexObject : MarshalByRefObject, SHKMutex
    {
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}

