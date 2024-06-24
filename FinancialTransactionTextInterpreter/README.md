# FinancialTransactionTextInterpreter
Desktop application created for easier control of expenses. 
Expense tracking is conducted in an excel file attached to both the repository and the release. 
The purpose of the application itself is to make entering transactions easier. It analyzes the text 
entered by the user and then inserts the extracted data into the appropriate spreadsheets and excel tables. 
The text of the transaction must be structured properly, appropriate examples are included in the help 
window inside the apllication. Additional features include automatic suggestions, based on the data 
contained in the "PredefinedData" spreadsheet, and informing the user about errors in the processed text. 

## Architecture of the application
The architecture of the entire application is styled as a clean architecture. 
Where the view layer includes classes in the Views folder. Business logic is divided into 
two components ViewModels, Logic, where classes contained in the ViewModels folder are responsible 
for exposing data for views and using services contained in the Logic folder. These services, in turn, 
provide suggestion, inform VMs of changes, communicate with the infrastructure to retrieve/save data or 
configuration. Using the classes defined in the Infrastructure folder is done by using IoC and taking advantage 
of the interfaces declared in the Logic folder and implemented by the classes in the Infrastructrue folder. 
In addition, the application uses the MVVM pattern, widely used for years, to create the user interface and its 
communication with the model and services, and everything is complemented by the use of Dependency Injection to 
separate the classes as much as possible. 

## Additional information

The information contained here is particularly applicable to recruiters. 
Please keep the following in mind: this application was primarily meant to solve 
my problem therefore it contains a lot of simplifications that have no right to 
be in a production application for a client, such as the lack of unit tests, strings 
in Polish, and which should be localizedstring. Definitely the application needs to
refine the system of logging errors and informing the user about it, or more MVVM-compliant 
path selection for excel file or opening it. 


## External libraries used by this project: 
Many thanks to the creators of the following libraries:
- [WPF UI by lepco](https://github.com/lepoco/wpfui)
- [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet)
- [Serilog](https://github.com/serilog/serilog)
- [ClosedXML](https://github.com/ClosedXML/ClosedXML)