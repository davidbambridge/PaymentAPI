using System;
using System.Collections.Generic;
using System.Linq;
using Services.Common.Model;

using DTO = Services.Common.DTO;

namespace WebApi.Extensions
{
    /// <summary>
    /// Extension methods for converting between types
    /// </summary>
    public static class ModelExtensions
    {

        #region ToDTO

        /// <summary>
        /// Converts a customer object to DTO
        /// </summary>
        /// <param name="customer">Customer model</param>
        /// <returns>Customer DTO</returns>
        public static DTO.Customer ToDto(this Customer customer)
        {
            return customer is null
                ? null
                : new DTO.Customer()
                {
                    CustomerId = new DTO.CustomerId() {UUID = customer.CustomerId},
                    Title = (DTO.Title) customer.Title,
                    FamilyName = customer.FamilyName,
                    FirstName = customer.FistName,
                    Address = customer.Address.ToDto()
                };
        }

        /// <summary>
        /// Converts an address object to DTO
        /// </summary>
        /// <param name="address">Address model</param>
        /// <returns>Address DTO</returns>
        public static DTO.Address ToDto(this Address address)
        {
            return address is null
                ? null
                : new DTO.Address()
                {
                    AddressId = new DTO.AddressId() {UUID = address.AddressId},
                    AddressLine1 = address.AddressLine1,
                    AddressLine2 = address.AddressLine2,
                    Country = address.Country,
                    Postcode = address.Postcode,
                    TownOrCity = address.TownOrCity
                };
        }

        /// <summary>
        /// Converts payment item to DTO
        /// </summary>
        /// <param name="paymentItem">payment item model</param>
        /// <returns>Payment DTO</returns>
        public static DTO.PaymentItem ToDto(this PaymentItem paymentItem)
        {
            return paymentItem is null
                ? null
                : new DTO.PaymentItem()
                {
                    PaymentId = new DTO.PaymentId() {  UUID = paymentItem.PaymentId},
                    ExternalPaymentReference = paymentItem.ExternalPaymentReference,
                    PaymentAmount = paymentItem.PaymentAmount,
                    PaymentCard = paymentItem.PaymentCard.ToDto(),
                    PaymentCurrency = (DTO.PaymentCurrency)paymentItem.PaymentCurrency,
                    PaymentCustomer = paymentItem.PaymentCustomer.ToDto(),
                    PaymentMerchant = paymentItem.PaymentMerchant.ToDto(),
                    PaymentStatus = (DTO.PaymentStatus)paymentItem.PaymentStatus,
                    TransactionDateTime = paymentItem.TransactionDateTime
                };
        }

        /// <summary>
        /// Converts card to DTO
        /// </summary>
        /// <param name="card">card model</param>
        /// <returns>card dto</returns>
        public static DTO.Card ToDto(this Card card)
        {
            return card is null
                ? null
                : new DTO.Card()
                {
                    CardId = new DTO.CardId() { UUID = card.CardId },
                    CardNumber = card.CardNumber,
                    Cvv = card.Cvv,
                    Month = card.Month,
                    NameOnCard = card.NameOnCard,
                    Year = card.Year
                };
        }

        /// <summary>
        /// Converts merchant object to DTO
        /// </summary>
        /// <param name="merchant">Merchant model</param>
        /// <returns>Merchant DTO</returns>
        public static DTO.Merchant ToDto(this Merchant merchant)
        {
            return merchant is null
                ? null
                : new DTO.Merchant()
                {
                    MerchantId = new DTO.MerchantId() { UUID = merchant.MerchantId }
                };
        }

        /// <summary>
        /// Converts a list of payment items to DTO
        /// </summary>
        /// <param name="payments">List of payment model</param>
        /// <returns>List of payment DTO</returns>
        public static List<DTO.PaymentItem> ToDto(this List<PaymentItem> payments)
        {
            return (from paymentItem in payments where paymentItem != null select paymentItem.ToDto()).ToList();
        }

        #endregion

        #region ToDb

        /// <summary>
        /// Converts a customer DTO to model
        /// </summary>
        /// <param name="customer">Customer DTO</param>
        /// <returns>Customer Model</returns>
        public static Customer ToDb(this DTO.Customer customer)
        {
            return customer is null
                ? null
                : new Customer()
                {
                    CustomerId = customer.CustomerId.IsValid() ? customer.CustomerId.UUID : Guid.NewGuid().ToString(),
                    Title = (int) customer.Title,
                    FamilyName = customer.FamilyName,
                    FistName = customer.FirstName,
                    Address = customer.Address?.ToDb()
                };
        }

        /// <summary>
        /// Converts an address DTO to model
        /// </summary>
        /// <param name="address">Address DTO</param>
        /// <returns>Address model</returns>
        public static Address ToDb(this DTO.Address address)
        {
            return address is null
                ? null
                : new Address()
                {
                    AddressId = address.AddressId.IsValid() ? address.AddressId.UUID : Guid.NewGuid().ToString(),
                    AddressLine1 = address.AddressLine1,
                    AddressLine2 = address.AddressLine2,
                    Country = address.Country,
                    Postcode = address.Postcode,
                    TownOrCity = address.TownOrCity
                };
        }

        /// <summary>
        /// Converts PaymentItem DTO to model
        /// </summary>
        /// <param name="paymentItem">PaymentItem DTO</param>
        /// <returns>OPaymentItem model</returns>
        public static PaymentItem ToDb(this DTO.PaymentItem paymentItem)
        {

            return paymentItem is null
                ? null
                : new PaymentItem()
                {
                    PaymentId =
                        paymentItem.PaymentId.IsValid() ? paymentItem.PaymentId.UUID : Guid.NewGuid().ToString(),
                    ExternalPaymentReference = paymentItem.ExternalPaymentReference,
                    PaymentAmount = paymentItem.PaymentAmount,
                    PaymentCard = paymentItem.PaymentCard.ToDb(),
                    PaymentCurrency = (int) paymentItem.PaymentCurrency,
                    PaymentCustomer = paymentItem.PaymentCustomer.ToDb(),
                    PaymentMerchant = paymentItem.PaymentMerchant.ToDb(),
                    PaymentStatus = (int) paymentItem.PaymentStatus,
                    TransactionDateTime = paymentItem.TransactionDateTime
                };
        }

        /// <summary>
        /// Converts Card DTO to model
        /// </summary>
        /// <param name="card">Card DTO</param>
        /// <returns>Card model</returns>
        public static Card ToDb(this DTO.Card card)
        {
            return card is null
                ? null
                : new Card()
                {
                    CardId = card.CardId.IsValid() ? card.CardId.UUID : Guid.NewGuid().ToString(),
                    CardNumber = card.CardNumber,
                    Cvv = card.Cvv,
                    Month = card.Month,
                    NameOnCard = card.NameOnCard,
                    Year = card.Year
                };
        }

        /// <summary>
        /// Converts Merchant DTO to model
        /// </summary>
        /// <param name="merchant">Merchant DTO</param>
        /// <returns>Merchant model</returns>
        public static Merchant ToDb(this DTO.Merchant merchant)
        {
            return merchant is null
                ? null
                : new Merchant()
                {
                    MerchantId = merchant.MerchantId.UUID
                };
        }

        #endregion
    }
}
