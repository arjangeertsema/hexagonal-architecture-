/*
 * Contact center service
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 0.0.1
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Reference.Adapters.Rest.Generated.Converters;

namespace Reference.Adapters.Rest.Generated.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class QuestionsModel : IEquatable<QuestionsModel>
    {
        /// <summary>
        /// Gets or Sets QuestionId
        /// </summary>
        [Required]
        [DataMember(Name="question_id", EmitDefaultValue=false)]
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Gets or Sets RecievedOn
        /// </summary>
        [Required]
        [DataMember(Name="recieved_on", EmitDefaultValue=false)]
        public DateTime RecievedOn { get; set; }

        /// <summary>
        /// Gets or Sets LastActivityOn
        /// </summary>
        [DataMember(Name="last_activity_on", EmitDefaultValue=false)]
        public DateTime LastActivityOn { get; set; }

        /// <summary>
        /// Gets or Sets Subject
        /// </summary>
        [Required]
        [DataMember(Name="subject", EmitDefaultValue=false)]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or Sets Sender
        /// </summary>
        [Required]
        [DataMember(Name="sender", EmitDefaultValue=false)]
        public string Sender { get; set; }


        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [TypeConverter(typeof(CustomEnumConverter<StatusEnum>))]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum ProcessStartedEnum for process_started
            /// </summary>
            [EnumMember(Value = "process_started")]
            ProcessStartedEnum = 1,
            
            /// <summary>
            /// Enum QuestionAnsweredEnum for question_answered
            /// </summary>
            [EnumMember(Value = "question_answered")]
            QuestionAnsweredEnum = 2,
            
            /// <summary>
            /// Enum AnswerRejectedEnum for answer_rejected
            /// </summary>
            [EnumMember(Value = "answer_rejected")]
            AnswerRejectedEnum = 3,
            
            /// <summary>
            /// Enum AnswerAcceptedEnum for answer_accepted
            /// </summary>
            [EnumMember(Value = "answer_accepted")]
            AnswerAcceptedEnum = 4,
            
            /// <summary>
            /// Enum AnswerModifiedEnum for answer_modified
            /// </summary>
            [EnumMember(Value = "answer_modified")]
            AnswerModifiedEnum = 5,
            
            /// <summary>
            /// Enum AnswerSendEnum for answer_send
            /// </summary>
            [EnumMember(Value = "answer_send")]
            AnswerSendEnum = 6
        }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [Required]
        [DataMember(Name="status", EmitDefaultValue=false)]
        public StatusEnum Status { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class QuestionsModel {\n");
            sb.Append("  QuestionId: ").Append(QuestionId).Append("\n");
            sb.Append("  RecievedOn: ").Append(RecievedOn).Append("\n");
            sb.Append("  LastActivityOn: ").Append(LastActivityOn).Append("\n");
            sb.Append("  Subject: ").Append(Subject).Append("\n");
            sb.Append("  Sender: ").Append(Sender).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((QuestionsModel)obj);
        }

        /// <summary>
        /// Returns true if QuestionsModel instances are equal
        /// </summary>
        /// <param name="other">Instance of QuestionsModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(QuestionsModel other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    QuestionId == other.QuestionId ||
                    QuestionId != null &&
                    QuestionId.Equals(other.QuestionId)
                ) && 
                (
                    RecievedOn == other.RecievedOn ||
                    RecievedOn != null &&
                    RecievedOn.Equals(other.RecievedOn)
                ) && 
                (
                    LastActivityOn == other.LastActivityOn ||
                    LastActivityOn != null &&
                    LastActivityOn.Equals(other.LastActivityOn)
                ) && 
                (
                    Subject == other.Subject ||
                    Subject != null &&
                    Subject.Equals(other.Subject)
                ) && 
                (
                    Sender == other.Sender ||
                    Sender != null &&
                    Sender.Equals(other.Sender)
                ) && 
                (
                    Status == other.Status ||
                    
                    Status.Equals(other.Status)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (QuestionId != null)
                    hashCode = hashCode * 59 + QuestionId.GetHashCode();
                    if (RecievedOn != null)
                    hashCode = hashCode * 59 + RecievedOn.GetHashCode();
                    if (LastActivityOn != null)
                    hashCode = hashCode * 59 + LastActivityOn.GetHashCode();
                    if (Subject != null)
                    hashCode = hashCode * 59 + Subject.GetHashCode();
                    if (Sender != null)
                    hashCode = hashCode * 59 + Sender.GetHashCode();
                    
                    hashCode = hashCode * 59 + Status.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(QuestionsModel left, QuestionsModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(QuestionsModel left, QuestionsModel right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
