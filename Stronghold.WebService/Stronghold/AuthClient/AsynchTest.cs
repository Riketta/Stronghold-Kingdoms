namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class AsynchTest
    {
        public TargetMethod callbackMethod;
        public string OutputString;
        public Control theControl;
        public IATestProxy theProxy = XmlRpcProxyGen.Create<IATestProxy>();

        public void doPing(TargetMethod method, Control ctrl)
        {
            this.theControl = ctrl;
            this.callbackMethod = method;
            this.theProxy.Beginping(new AsyncCallback(this.GetResponse), null);
        }

        public void GetResponse(IAsyncResult asr)
        {
            XmlRpcAsyncResult result = (XmlRpcAsyncResult) asr;
            this.OutputString = ((IATestProxy) result.ClientProtocol).Endping(asr);
            this.theControl.Invoke(new TargetMethod(this.callbackMethod.Invoke), new object[] { this, this.OutputString });
        }

        public void HandleCallback(IAsyncResult asr)
        {
        }

        public override string ToString()
        {
            return this.OutputString;
        }

        public delegate void HandleCallbackDelegate(IAsyncResult asr);

        public delegate void TargetMethod(object sender, object args);
    }
}

