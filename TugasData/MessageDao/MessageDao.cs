using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TugasData.MessageDao
{
    /// <summary>
    /// Represents a conversation session containing a sequence of messages.
    /// </summary>
    public class Messages
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Ordered list of messages within this conversation.
        /// </summary>
        public List<MessageDetail> messages { get; set; } = new();

        // Convenience alias for C# conventions
        [NotMapped]
        public Guid Id
        {
            get => id;
            set => id = value;
        }

        [NotMapped]
        public List<MessageDetail> Details
        {
            get => messages;
            set => messages = value;
        }
    }

    /// <summary>
    /// Represents an individual chat message (e.g. system, user, assistant).
    /// </summary>
    public class MessageDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        /// <summary>
        /// Role of the message sender (e.g., "user", "assistant", "system").
        /// </summary>
        public string role { get; set; } = string.Empty;

        /// <summary>
        /// Content of the message.
        /// </summary>
        public string content { get; set; } = string.Empty;

        // Convenience aliases for C# conventions
        [NotMapped]
        public int Id
        {
            get => id;
            set => id = value;
        }

        [NotMapped]
        public string Role
        {
            get => role;
            set => role = value;
        }

        [NotMapped]
        public string Content
        {
            get => content;
            set => content = value;
        }

        public MessageDetail() { }

        public MessageDetail(string role, string content)
        {
            this.role = role;
            this.content = content;
        }
    }
}
