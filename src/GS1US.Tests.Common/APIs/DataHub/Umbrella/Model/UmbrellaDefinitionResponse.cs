/* 
 * Data Hub Umbrella API
 *
 * Data Hub umbrella management API
 *
 * OpenAPI spec version: 1.0
 * 
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = GS1US.Tests.Common.APIs.DataHub.Umbrella.Client.OpenAPIDateConverter;

namespace GS1US.Tests.Common.APIs.DataHub.Umbrella.Model
{
    /// <summary>
    /// UmbrellaDefinitionResponse
    /// </summary>
    [DataContract]
    public partial class UmbrellaDefinitionResponse :  IEquatable<UmbrellaDefinitionResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UmbrellaDefinitionResponse" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected UmbrellaDefinitionResponse() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="UmbrellaDefinitionResponse" /> class.
        /// </summary>
        /// <param name="parentAccountID">parentAccountID (required).</param>
        /// <param name="children">children (required).</param>
        public UmbrellaDefinitionResponse(string parentAccountID = default(string), List<UmbrellaChild> children = default(List<UmbrellaChild>))
        {
            // to ensure "parentAccountID" is required (not null)
            if (parentAccountID == null)
            {
                throw new InvalidDataException("parentAccountID is a required property for UmbrellaDefinitionResponse and cannot be null");
            }
            else
            {
                this.ParentAccountID = parentAccountID;
            }

            // to ensure "children" is required (not null)
            if (children == null)
            {
                throw new InvalidDataException("children is a required property for UmbrellaDefinitionResponse and cannot be null");
            }
            else
            {
                this.Children = children;
            }

        }
        
        /// <summary>
        /// Gets or Sets ParentAccountID
        /// </summary>
        [DataMember(Name="ParentAccountID", EmitDefaultValue=false)]
        public string ParentAccountID { get; set; }

        /// <summary>
        /// Gets or Sets Children
        /// </summary>
        [DataMember(Name="Children", EmitDefaultValue=false)]
        public List<UmbrellaChild> Children { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UmbrellaDefinitionResponse {\n");
            sb.Append("  ParentAccountID: ").Append(ParentAccountID).Append("\n");
            sb.Append("  Children: ").Append(Children).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as UmbrellaDefinitionResponse);
        }

        /// <summary>
        /// Returns true if UmbrellaDefinitionResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of UmbrellaDefinitionResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UmbrellaDefinitionResponse input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.ParentAccountID == input.ParentAccountID ||
                    (this.ParentAccountID != null &&
                    this.ParentAccountID.Equals(input.ParentAccountID))
                ) && 
                (
                    this.Children == input.Children ||
                    this.Children != null &&
                    this.Children.SequenceEqual(input.Children)
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
                int hashCode = 41;
                if (this.ParentAccountID != null)
                    hashCode = hashCode * 59 + this.ParentAccountID.GetHashCode();
                if (this.Children != null)
                    hashCode = hashCode * 59 + this.Children.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            // ParentAccountID (string) pattern
            Regex regexParentAccountID = new Regex(@"[0-9]{8}", RegexOptions.CultureInvariant);
            if (false == regexParentAccountID.Match(this.ParentAccountID).Success)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for ParentAccountID, must match a pattern of " + regexParentAccountID, new [] { "ParentAccountID" });
            }

            yield break;
        }
    }

}
