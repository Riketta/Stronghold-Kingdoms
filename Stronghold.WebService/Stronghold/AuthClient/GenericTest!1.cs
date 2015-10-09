namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;
    using System.Runtime.CompilerServices;

    public class GenericTest<T>
    {
        public string OutputString;
        public T theProxy;

        public event CallbackCompletedHander CallbackCompleted;

        public GenericTest()
        {
            this.theProxy = XmlRpcProxyGen.Create<T>();
        }

        public void OnCallbackCompleted()
        {
            if (this.CallbackCompleted != null)
            {
                this.CallbackCompleted(this, new EventArgs());
            }
        }

        public void PingCallback(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            this.OutputString = ((IATestProxy) result.ClientProtocol).Endping(asr);
            this.OnCallbackCompleted();
        }
    }
}

