using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Models
{
    public class AniListNotification : AniListObject
    {
        public string Type { get; set; }
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
            var parsedType = AniListEnum.GetEnum<NotificationType>(Type);
            var notificationText = "Error occurred while parsing notification.";

            if (parsedType.Equals(NotificationType.ActivityMessage))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> sent you a message.";
            }
            else if (parsedType.Equals(NotificationType.ActivityReply))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> replied to your activity.";
            }
            else if (parsedType.Equals(NotificationType.Following))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> started following you.";
            }
            else if (parsedType.Equals(NotificationType.ActivityMention))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> mentioned you in their activity.";
            }
            else if (parsedType.Equals(NotificationType.ThreadCommentMention))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> mentioned you, in the forum thread <b><font color='{accentColor}'>{Thread?.Title}</font></b>.";
            }
            else if (parsedType.Equals(NotificationType.ThreadSubscribed))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> commented in your subscribed forum thread <b><font color='{accentColor}'>{Thread?.Title}</font></b>.";
            }
            else if (parsedType.Equals(NotificationType.ThreadCommentReply))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> replied to your comment, in the forum thread <b><font color='{accentColor}'>{Thread?.Title}</font></b>.";
            }
            else if (parsedType.Equals(NotificationType.Airing))
            {
                notificationText = $"Episode <b><font color='{accentColor}'>{Episode}</font></b> of <b><font color='{accentColor}'>{Media?.Title?.UserPreferred}</font></b> aired.";
            }
            else if (parsedType.Equals(NotificationType.ActivityLike))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> liked your activity.";
            }
            else if (parsedType.Equals(NotificationType.ActivityReplyLike))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> liked your activity reply.";
            }
            else if (parsedType.Equals(NotificationType.ThreadLike))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> liked your forum thread, <b><font color='{accentColor}'>{Thread?.Title}</font></b>.";
            }
            else if (parsedType.Equals(NotificationType.ThreadCommentLike))
            {
                notificationText = $"<b><font color='{accentColor}'>{User?.Name}</font></b> liked your comment, in the forum thread <b><font color='{accentColor}'>{Thread?.Title}</font></b>.";
            }

            return notificationText;
        }

        public string GetImageUri()
        {
            var parsedType = AniListEnum.GetEnum<NotificationType>(Type);
            string imageUrl = User?.Avatar?.Large;

            if (parsedType.Equals(NotificationType.Airing))
            {
                imageUrl = Media?.CoverImage?.Large;
            }

            return imageUrl;
        }

        public NotificationActionType GetNotificationActionType()
        {
            var parsedType = AniListEnum.GetEnum<NotificationType>(Type);
            NotificationActionType returnType = null;

            if (parsedType.Equals(NotificationType.ActivityMessage) ||
                parsedType.Equals(NotificationType.ActivityReply) ||
                parsedType.Equals(NotificationType.ActivityMention) ||
                parsedType.Equals(NotificationType.ActivityLike) ||
                parsedType.Equals(NotificationType.ActivityReplyLike))
            {
                returnType = NotificationActionType.Activity;
            }
            else if (parsedType.Equals(NotificationType.Following))
            {
                returnType = NotificationActionType.User;
            }
            else if (parsedType.Equals(NotificationType.ThreadCommentMention) ||
                parsedType.Equals(NotificationType.ThreadSubscribed) ||
                parsedType.Equals(NotificationType.ThreadCommentReply) ||
                parsedType.Equals(NotificationType.ThreadLike) ||
                parsedType.Equals(NotificationType.ThreadCommentLike))
            {
                returnType = NotificationActionType.Thread;
            }
            else if (parsedType.Equals(NotificationType.Airing))
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

            public static NotificationType ActivityMessage => new NotificationType("ACTIVITY_MESSAGE", "Activity Message", 0);
            public static NotificationType ActivityReply => new NotificationType("ACTIVITY_REPLY", "Activity Reply", 1);
            public static NotificationType Following => new NotificationType("FOLLOWING", "Following", 2);
            public static NotificationType ActivityMention => new NotificationType("ACTIVITY_MENTION", "Activity Mention", 3);
            public static NotificationType ThreadCommentMention => new NotificationType("THREAD_COMMENT_MENTION", "Thread Comment Mention", 4);
            public static NotificationType ThreadSubscribed => new NotificationType("THREAD_SUBSCRIBED", "Thread Subscribed", 5);
            public static NotificationType ThreadCommentReply => new NotificationType("THREAD_COMMENT_REPLY", "Thread Comment Reply", 6);
            public static NotificationType Airing => new NotificationType("AIRING", "Airing", 7);
            public static NotificationType ActivityLike => new NotificationType("ACTIVITY_LIKE", "ActivityLike", 8);
            public static NotificationType ActivityReplyLike => new NotificationType("ACTIVITY_REPLY_LIKE", "Activity Reply Like", 9);
            public static NotificationType ThreadLike => new NotificationType("THREAD_LIKE", "Thread Like", 10);
            public static NotificationType ThreadCommentLike => new NotificationType("THREAD_COMMENT_LIKE", "Thread Comment Like", 11);
        }

        public sealed class NotificationActionType : AniListEnum
        {
            private NotificationActionType(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static NotificationActionType Media => new NotificationActionType("MEDIA", "Media", 0);
            public static NotificationActionType User => new NotificationActionType("USER", "User", 1);
            public static NotificationActionType Thread => new NotificationActionType("THREAD", "Thread", 2);
            public static NotificationActionType Comment => new NotificationActionType("COMMENT", "Comment", 3);
            public static NotificationActionType Activity => new NotificationActionType("ACTIVITY", "Activity", 4);
        }

        #endregion
    }
}
