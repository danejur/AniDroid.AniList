using System.Collections.Generic;

namespace AniDroid.AniList.Models
{
    public class AniListNotification : AniListObject
    {
        public NotificationType Type { get; set; }
        public string Context { get; set; }
        public List<string> Contexts { get; set; }
        public int CreatedAt { get; set; }
        public int Episode { get; set; }
        public int ActivityId { get; set; }
        public int CommentId { get; set; }
        public Media Media { get; set; }
        public User User { get; set; }
        public ForumThread Thread { get; set; }

        #region Display Methods

        public string GetNotificationHtml(string accentColor)
        {
            var notificationText = "Error occurred while parsing notification.";

            if (Type.Equals(NotificationType.ActivityMessage))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> sent you a message.";
            }
            else if (Type.Equals(NotificationType.ActivityReply))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> replied to your activity.";
            }
            else if (Type.Equals(NotificationType.Following))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> started following you.";
            }
            else if (Type.Equals(NotificationType.ActivityMention))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> mentioned you in their activity.";
            }
            else if (Type.Equals(NotificationType.ThreadCommentMention))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> mentioned you, in the forum thread <b><font color='{accentColor}'>{Thread?.Title}</font></b>.";
            }
            else if (Type.Equals(NotificationType.ThreadSubscribed))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> commented in your subscribed forum thread <b><font color='{accentColor}'>{Thread?.Title}</font></b>.";
            }
            else if (Type.Equals(NotificationType.ThreadCommentReply))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> replied to your comment, in the forum thread <b><font color='{accentColor}'>{Thread?.Title}</font></b>.";
            }
            else if (Type.Equals(NotificationType.Airing))
            {
                notificationText = $"Episode <b><font color='{accentColor}'>{Episode}</font></b> of <b><font color='{accentColor}'>{Media?.Title?.UserPreferred}</font></b> aired.";
            }
            else if (Type.Equals(NotificationType.ActivityLike))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> liked your activity.";
            }
            else if (Type.Equals(NotificationType.ActivityReplyLike))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> liked your activity reply.";
            }
            else if (Type.Equals(NotificationType.ThreadLike))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> liked your forum thread, <b><font color='{accentColor}'>{Thread?.Title}</font></b>.";
            }
            else if (Type.Equals(NotificationType.ThreadCommentLike))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> liked your comment, in the forum thread <b><font color='{accentColor}'>{Thread?.Title}</font></b>.";
            }

            return notificationText;
        }

        public string GetImageUri()
        {
            var imageUrl = User?.Avatar?.Large;

            if (Type.Equals(NotificationType.Airing))
            {
                imageUrl = Media?.CoverImage?.Large;
            }

            return imageUrl;
        }

        public NotificationActionType GetNotificationActionType()
        {
            NotificationActionType returnType = null;

            if (Type.Equals(NotificationType.ActivityMessage) ||
                Type.Equals(NotificationType.ActivityReply) ||
                Type.Equals(NotificationType.ActivityMention) ||
                Type.Equals(NotificationType.ActivityLike) ||
                Type.Equals(NotificationType.ActivityReplyLike))
            {
                returnType = NotificationActionType.Activity;
            }
            else if (Type.Equals(NotificationType.Following))
            {
                returnType = NotificationActionType.User;
            }
            else if (Type.Equals(NotificationType.ThreadCommentMention) ||
                     Type.Equals(NotificationType.ThreadSubscribed) ||
                     Type.Equals(NotificationType.ThreadCommentReply) ||
                     Type.Equals(NotificationType.ThreadLike) ||
                     Type.Equals(NotificationType.ThreadCommentLike))
            {
                returnType = NotificationActionType.Thread;
            }
            else if (Type.Equals(NotificationType.Airing))
            {
                returnType = NotificationActionType.Media;
            }

            return returnType;
        }

        #endregion

        #region Enum Classes

        public sealed class NotificationType : AniListEnum
        {
            private NotificationType(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static NotificationType ActivityMessage { get; } = new NotificationType("ACTIVITY_MESSAGE", "Activity Message", 0);
            public static NotificationType ActivityReply { get; } = new NotificationType("ACTIVITY_REPLY", "Activity Reply", 1);
            public static NotificationType Following { get; } = new NotificationType("FOLLOWING", "Following", 2);
            public static NotificationType ActivityMention { get; } = new NotificationType("ACTIVITY_MENTION", "Activity Mention", 3);
            public static NotificationType ThreadCommentMention { get; } = new NotificationType("THREAD_COMMENT_MENTION", "Thread Comment Mention", 4);
            public static NotificationType ThreadSubscribed { get; } = new NotificationType("THREAD_SUBSCRIBED", "Thread Subscribed", 5);
            public static NotificationType ThreadCommentReply { get; } = new NotificationType("THREAD_COMMENT_REPLY", "Thread Comment Reply", 6);
            public static NotificationType Airing { get; } = new NotificationType("AIRING", "Airing", 7);
            public static NotificationType ActivityLike { get; } = new NotificationType("ACTIVITY_LIKE", "ActivityLike", 8);
            public static NotificationType ActivityReplyLike { get; } = new NotificationType("ACTIVITY_REPLY_LIKE", "Activity Reply Like", 9);
            public static NotificationType ThreadLike { get; } = new NotificationType("THREAD_LIKE", "Thread Like", 10);
            public static NotificationType ThreadCommentLike { get; } = new NotificationType("THREAD_COMMENT_LIKE", "Thread Comment Like", 11);
        }

        public sealed class NotificationActionType : AniListEnum
        {
            private NotificationActionType(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static NotificationActionType Media { get; } = new NotificationActionType("MEDIA", "Media", 0);
            public static NotificationActionType User { get; } = new NotificationActionType("USER", "User", 1);
            public static NotificationActionType Thread { get; } = new NotificationActionType("THREAD", "Thread", 2);
            public static NotificationActionType Comment { get; } = new NotificationActionType("COMMENT", "Comment", 3);
            public static NotificationActionType Activity { get; } = new NotificationActionType("ACTIVITY", "Activity", 4);
        }

        #endregion
    }
}
