namespace Kingdoms
{
    using System;

    public class AeriaEventArgs
    {
        public string token;
        public string userguid;

        public AeriaEventArgs(string u, string t)
        {
            this.userguid = u;
            this.token = t;
        }
    }
}

