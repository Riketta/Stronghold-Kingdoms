namespace Kingdoms
{
    using System;

    public interface IMailUserInterface
    {
        void addRecipient(string recipient);
        void popupClosed(bool ok);
    }
}

