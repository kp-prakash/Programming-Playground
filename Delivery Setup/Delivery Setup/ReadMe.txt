/*
* File: ReadMe.txt
* Authors: Srihari.Sridharan@cognizant.com; Renga.PrasadV@cognizant.com
* Reviewer: Ganesh.Kondal@cognizant.com
* Copyrights: Cognizant 2013
*/

Intent and Scope:
------ --- ------
The intent of this Proof of Concept (PoC) is to implement the delivery appointment setup use case as a Single Page Application (SPA) talking to RESTful services and backend. The SPA has been implemented using the technology stack used below. This file acts as a reference to understand the overall source code organization and the choice of technology stack used.

Tech Stack Used:
---- ----- -----
HTML5             : UI.
CSS3              : Presentation.
JavaScript        : Scripting.
Backbone.js       : MVC implementation.
Underscore.js     : For rendering UI templates.
Knockout.js       : Two way data binding between model and view.
Require.js        : Dependency Injection.
Bootstrap.css     : Responsive Web Design.

Single Page Application:
------ ---- ------------
From http://en.wikipedia.org/wiki/Single-page_application - In an SPA, either all necessary code – HTML, JavaScript, and CSS – is retrieved with a single page load, or the appropriate resources are dynamically loaded and added to the page as necessary, usually in response to user actions. The page does not reload at any point in the process, nor does control transfer to another page, although modern web technologies (such as those included in HTML5) can provide the perception and navigability of separate logical pages in the application. Interaction with the single page application often involves dynamic communication with the web server behind the scenes.

In this implementation, we have a SPA consuming RESTful services using JSON as data exchange format. The RESTful services talk to Informix database.

Key Design Decisions
--- ------ ---------
* Favor loose coupling and tight cohesion.
* Favor good separation of concerns.
* Favor modularization and reusability.
* Favor SOLID principles.
* Favor responsive web design.
* Favor clean code.

Rationale behind the source code structure
--------- ------ --- ------ ---- ---------
* 'lib' folder contains all the JavaScript libraries and CSS style sheets.
* This lib folder is for development purposes. We can use CDN to deliver these scripts in real time.
* 'js' folder contains all application specific code.
* 'index.html' is the startup page.
* 'js\main.js' acts as the application entry point and contains the require.js's config details. 
* 'js\collections' contains the backbone collections.
* Collections used in more than one screen are defined inside 'js\collections\common' folder. This provides reusability.
* Collections specific to a particular screen are defined in respective folders under 'js\collections'.
* 'js\models' contains the backbone models. 
* Models and view-models used in more than one screen are defined inside 'js\models\common' folder. This provides reusability.
* Models and view-models specific to a particular screen are defined in respective folders under 'js\models'.
* 'js\routers' contains the routes defined in this project.
* 'js\templates' contains the HTML templates used in UI.
* Templates used in more than one screen are defined inside 'js\templates\common' folder. This provides reusability.
* Templates specific to a particular screen are defined in respective folders under 'js\templates'.
* 'js\views' contains the view implementation for specific screens.
* 'lib\common' contains application specific CSS and images.

Folder / File Organization:
------ - ---- -------------

The files are organized as shown below.

Delivery Setup
|   index.html
|   ReadMe.txt
|   
+---js
|   |   main.js
|   |   
|   +---collections
|   |   \---common
|   |           Deliveries.js
|   |           PurchaseOrders.js
|   |           
|   +---models
|   |   +---common
|   |   |       Delivery.js
|   |   |       DeliveryDates.js
|   |   |       PrintOptions.js
|   |   |       PurchaseOrder.js
|   |   |       
|   |   +---create
|   |   |       CreateDelivery.js
|   |   |       CreateMetaData.js
|   |   |       
|   |   +---search
|   |   |       SearchCriteria.js
|   |   |       SearchDelivery.js
|   |   |       SearchMetaData.js
|   |   |       
|   |   \---view
|   |           ViewDelivery.js
|   |           ViewMetaData.js
|   |           
|   +---routers
|   |       router.js
|   |       
|   +---templates
|   |   +---common
|   |   |       PrintOptions.html
|   |   |       
|   |   +---createdelivery
|   |   |       CreateDelivery.html
|   |   |       DataEntry.html
|   |   |       DateTime.html
|   |   |       PurchaseOrderDetails.html
|   |   |       
|   |   +---searchdelivery
|   |   |       DeliveryDetails.html
|   |   |       SearchDelivery.html
|   |   |       SearchOptions.html
|   |   |       
|   |   \---viewdelivery
|   |           DateTime.html
|   |           Delivery.html
|   |           PurchaseOrderDetails.html
|   |           ViewDelivery.html
|   |           
|   \---views
|           CreateDelivery.js
|           SearchDelivery.js
|           ViewDelivery.js
|           
\---lib
    +---backbone
    |       backbone.js
    |       
    +---backbone.localStorage
    |       backbone.localStorage.js
    |       
    +---bootstrap
    |   +---css
    |   |       bootstrap-responsive.css
    |   |       bootstrap-responsive.min.css
    |   |       bootstrap.css
    |   |       bootstrap.min.css
    |   |       
    |   +---img
    |   |       glyphicons-halflings-white.png
    |   |       glyphicons-halflings.png
    |   |       
    |   \---js
    |           bootstrap.js
    |           bootstrap.min.js
    |           
    +---common
    |       base.js
    |       gls.css
    |       walmart_logo.png
    |       
    +---jquery
    |       jquery.js
    |       
    +---knockout
    |       knockout.js
    |       
    +---requirejs
    |       require.js
    |       
    +---requirejs-text
    |       text.js
    |       
    \---underscore
            underscore.js