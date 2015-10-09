namespace Stronghold.AuthClient
{
    using System;

    public interface IBPResponse
    {
        string Message { get; }

        int? SuccessCode { get; }

        string URL { get; }
    }
}

