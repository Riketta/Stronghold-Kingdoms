namespace Kingdoms
{
    using System;

    public interface IForumReplyParent
    {
        void newPost(long threadID, string body);
    }
}

