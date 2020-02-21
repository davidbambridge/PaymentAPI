Implementation of a payment gateway, and mock acquiring bank.  

Payment API implements the following routes:

Post - /api/Merchant
For creating new merchants returns a guid

Post - /api/Payment/{id}
Payment processing route, id represents the merchant id.  Takes JSON object as shown below containing the details of the payment and the customer making the payment.

{
    "paymentCustomer": {
        "title": "Mr",
        "firstName": "David",
        "familyName": "Bambridge",
        "address": {
            "addressLine1": "7 A Stree",
            "addressLine2": null,
            "townOrCity": "Westcliff-on-Sea",
            "postcode": "SS0 5TE",
            "country": "United Kingdom"
        }
    },
    "paymentCard": {
        "cardNumber": "0000000000000000",
        "nameOnCard": "Mr D P Bambridge",
        "cvv": 111,
        "month": 10,
        "year": 2022
    },
    "paymentAmount": 3.5,
    "paymentCurrency": "GBP"
}

Return a PaymentResult object containing the payment Id and the status of the payment i.e. Approved, Declined etc.

Get - /api/Payment/{id}
Gets all the payments made to a merchant, id represents the merchant Id

Get - ​/api​/Payment​/{id}​/{paymentId}
Gets a single payment made to a merchant, id represents the merchant Id and paymentId the id of the payment.

Mock Bank API implement the following route

Post - /api/payment
Returns a payment result object containing a paymentId and bool value indicating success (or otherwise).  

To Do / Improve
Unit test coverage could be increased, payments at the moment are treating each customer as a new customer, should be able to reuse details once created (same applied to customer address).  Need to implement encryption for both storing customer card details in the database and for sending to the bank api. 