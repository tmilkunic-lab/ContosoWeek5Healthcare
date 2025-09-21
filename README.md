Structure included
Controller
  SupplyItemsController.cs
Data
  SchoolContext.cs
  SchoolContextFactory.cs
Models
  SupplyItem.cs
ViewModels
  SupplyItemEditVm.cs
Views
  SupplyItems
    Index.cshtml
    Create.cshtml
    Edit.cshtml
appsettings.json
Program.cs

When creating, the following information:
-item number
description
unit of measure
lead time of day
PAR level/ROP/Onhand

In the Edit, there was validation and cross field rule

In the index:
search by item # or description
sort by PAR, description, or item #

After system created used examples such as nitrile gloves and knee pack
