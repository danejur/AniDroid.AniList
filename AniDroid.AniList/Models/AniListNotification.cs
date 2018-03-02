using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

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

        [JsonConverter(typeof(AniListEnumConverter<NotificationType>))]
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

        [JsonConverter(typeof(AniListEnumConverter<NotificationActionType>))]
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
