# Belmont_Sales_Tax
Simple app that simulates adding items into a grocery cart and printing the results out onto a receipt.

I thought it was an enjoyable exercise; there's certain aspects of it that felt fundamentally wrong to me (tax exemption should absolutely be tied to an Item's "category" or something of that nature, not guesstimated from key words) but there's only so much you can do for a quick project such as this. The most challenging part was formatting the lines of the "receipt"; my code there can assuredly use some tidying, but it works.

<br/>I would like to keep adding features to the app, but I've hit my allotted 8 hours and should realistically stop there.
<br/>An overview of how far I got into my desired features would look something like:
- [x] Simple safety net checks during user input ("1 bag of coffee grounds at 6.29" works, but so will "2 bags of coffee grounds @ 6.29")
- [x] Formats Item container quantities appropriately (i.e., "2 boxes of tapioca" will be stored as 2 instances of the Item "box of tapioca")
- [x] Cross-checking Item names against a .csv file to determine tax exemption status
- [x] Receipt lines ordered alphabetically by Item name
- [x] Unit Tests (could still use some console output mock tests)
- [ ] GUI for interacting with the application

*Note: This application targets .NET Core 3.1 by default, and is not guaranteed to be compatible with any frameworks earlier than .NET Core 2.0.*
