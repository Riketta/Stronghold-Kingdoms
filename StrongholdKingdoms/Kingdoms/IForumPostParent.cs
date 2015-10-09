namespace Kingdoms
{
    using System;

    public interface IForumPostParent
    {
        void newTopic(long forumID, string heading, string body);
    }
}

