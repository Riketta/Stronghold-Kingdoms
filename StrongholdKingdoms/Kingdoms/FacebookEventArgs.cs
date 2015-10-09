namespace Kingdoms
{
    using System;

    public class FacebookEventArgs
    {
        public string token;
        public string userguid;

        public FacebookEventArgs(string u, string t)
        {
            this.userguid = u;
            this.token = t;
        }
    }
}

