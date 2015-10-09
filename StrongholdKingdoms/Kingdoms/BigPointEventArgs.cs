namespace Kingdoms
{
    using System;

    public class BigPointEventArgs
    {
        public string token;
        public string userguid;

        public BigPointEventArgs(string u, string t)
        {
            this.userguid = u;
            this.token = t;
        }
    }
}

