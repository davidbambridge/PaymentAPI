using System;
using System.Diagnostics.CodeAnalysis;

namespace Services.Common.DTO
{
    /// <summary>
    /// Class representing unique items. 
    /// </summary>
    public class UniqueItem : IComparable<UniqueItem>, IEquatable<UniqueItem>
    {
        private string _id = Guid.Empty.ToString();

        public string UUID
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// Initializes a new instance of the UniqueItem class.
        /// </summary>
        public UniqueItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the UniqueItem class.
        /// </summary>
        /// <param name="other">The other.</param>
        public UniqueItem(UniqueItem other)
        {
            // Ensure new instance
            UUID = other.UUID;
        }

        /// <summary>
        /// To be implemented: Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo([AllowNull] UniqueItem other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// To be implemented: Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals([AllowNull] UniqueItem other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns if a unique Id has been changed from its default value
        /// </summary>
        /// <returns>bool</returns>
        public bool IsValid()
        {
            return (uint)string.CompareOrdinal(this._id, Guid.Empty.ToString()) > 0U;
        }
    }
}
