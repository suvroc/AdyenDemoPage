# Adyen Demo Page

It uses the Web Component workflow (https://docs.adyen.com/checkout/components-web#step-5-additional-payment-details).

## Setup

There are only 2 interesting files:
- Index.cshtml - with frontend side
- CanaryController.cs with the backend side

Before you start the project on your own machine, replace the default key:
- `ApiKey` and `MerchantAccount` in `CanaryController`
- `originKey` in `Index.cshtml` file

You also need to add allowed IP addresses of your API in Adyen service.

## Keys to Adyen service
The keys to Adyen account is generated for completely artificial account. You can try to play with it, but still we have a IP restriction setup.

## Screen

![Demo screen](doc/adyen_screen.png)
