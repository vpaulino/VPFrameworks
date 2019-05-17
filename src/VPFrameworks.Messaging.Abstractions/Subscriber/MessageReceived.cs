using System;
using System.Collections.Generic;
using System.Text;

namespace VPFrameworks.Messaging.Abstractions
{
    /// <summary>
    /// Represents the message that was received under some subscription
    /// </summary>
    public class MessageReceived 
    {
        /// <summary>
        /// Creates a new instance of a message
        /// </summary>
        /// <param name="id"> message id received from the channel. this usuallu is useful to signal the infrastructure that the message was received and processed</param>
        /// <param name="topicName">name of the channel from where the message came from </param>
        /// <param name="payload">the Payload of the message</param>
        public MessageReceived(string id, string topicName, byte[] payload)
        {
            this.Payload = payload;
            this.TopicName = topicName;
            this.Id = id;
            this.Created = DateTime.UtcNow;
        }
        /// <summary>
        /// Gets the payload of the message received
        /// </summary>
        public byte[] Payload { get; }

        /// <summary>
        /// Gets or sets the name of the topic from hwere the message came from 
        /// </summary>
        public string TopicName { get; }

        /// <summary>
        /// Message Id generated from the messaging infrastruture
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the data when this message was received
        /// </summary>
        public DateTime Created { get; }


    }
}
