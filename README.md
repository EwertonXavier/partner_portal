# Partner's Portal

Partner's Portal is a C# asp.net application for managing consulting companies and their employees. It contains Data Controllers for Customer, Artifact, Consultant, Order and Partners as well as views for CRUD operation on Orders, Partner and Consultant entities.

## Installation

Clone this project and open it using [Visual Studio](https://visualstudio.microsoft.com/) to open Partner's Portal application.

```bash
git clone https://github.com/EwertonXavier/partner_portal.git
```

## Usage

Open your Visual Studio Software and click run. The application will be initialized in a new browser window.

You can also use your prompt to interact with data controllers

### Partner

#### Partner Data Controller
 - GET: api/PartnerData/ListPartners - Returns all partners
 - GET: api/PartnerData/FindPartner/{id} - Returns all fields of a partner
 - POST: api/PartnerData/UpdatePartner/{id} - Updates partner  identified by {id}
 - POST: api/PartnerData/AddPartner - Add a partner to the database
 - POST: api/PartnerData/DeletePartner/{id} - Delete partner of id = {id} from the database


#### Partner Controller
 - GET: Partner/List  - List all partners
 - GET: Partner/Details/{id} - Details about Partner identified by {id}
 - POST: Partner/Create - Create new partner
 - GET: Partner/Edit/{id} - Get page Partner identified by {id}
 - POST: Partner/Edit/5 - Edit Partner identified by {id}
 - POST: Partner/Delete/{id} - Delete Partner identified by {id}
 - GET: Partner/Delete/{id} - Get page to delete Partner identified by {id}

### Consultant

#### Consultant Data Controller
 - GET: api/ConsultantData/ListConsultants - Returns all Consultant
 - GET: api/ConsultantData/FindConsultant/{id} - Returns all fields of a Consultant
 - POST: api/ConsultantData/UpdateConsultant/{id} - Updates Consultant  identified by {id}
 - POST: api/ConsultantData/AddConsultant - Add a Consultant to the database
 - POST: api/ConsultantData/DeleteConsultant/{id} - Delete Consultant of id = {id} from the database


#### Consultant Controller
 - GET: Consultant/List  - List all Consultants
 - GET: Consultant/Details/{id} - Details about Consultant identified by {id}
 - POST: Consultant/Create - Create new Consultant
 - GET: Consultant/Edit/{id} - Get page Consultant identified by {id}
 - POST: Consultant/Edit/5 - Edit Consultant identified by {id}
 - POST: Consultant/Delete/{id} - Delete Consultant identified by {id}
 - GET: Consultant/Delete/{id} - Get page to delete Consultant identified by {id}

### Order

#### Order Data Controller
 - GET: api/OrderData/ListOrders - Returns all Order
 - GET: api/OrderData/FindOrder/{id} - Returns all fields of a Order
 - POST: api/OrderData/UpdateOrder/{id} - Updates Order  identified by {id}
 - POST: api/OrderData/AddOrder - Add a Order to the database
 - POST: api/OrderData/DeleteOrder/{id} - Delete Order of id = {id} from the database


#### Order Controller
 - GET: Order/List  - List all Orders
 - GET: Order/Details/{id} - Details about Order identified by {id}
 - POST: Order/Create - Create new Order
 - GET: Order/Edit/{id} - Get page Order identified by {id}
 - POST: Order/Edit/5 - Edit Order identified by {id}
 - POST: Order/Delete/{id} - Delete Order identified by {id}
 - GET: Order/Delete/{id} - Get page to delete Order identified by {id}

 ### Artifact

 #### Artifact Data Controller
 - GET: api/ArtifactData/ListArtifact - Returns all Artifact
 - GET: api/ArtifactData/FindArtifact/{id} - Returns all fields of a Artifact
 - POST: api/ArtifactData/UpdateArtifact/{id} - Updates Artifact  identified by {id}
 - POST: api/ArtifactData/AddArtifact - Add a Artifact to the database
 - POST: api/ArtifactData/DeleteArtifact/{id} - Delete Artifact of id = {id} from the database
 
 ### Customer

 #### Customer Data Controller
 - GET: api/CustomerData/ListCustomer - Returns all Customer
 - GET: api/CustomerData/FindCustomer/{id} - Returns all fields of a Customer
 - POST: api/CustomerData/UpdateCustomer/{id} - Updates Customer  identified by {id}
 - POST: api/CustomerData/AddCustomer - Add a Customer to the database
 - POST: api/CustomerData/DeleteCustomer/{id} - Delete Customer of id = {id} from the database


## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
[MIT](https://choosealicense.com/licenses/mit/)

## Project status

This Project is paused.
