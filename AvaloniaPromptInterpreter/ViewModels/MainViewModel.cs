using AvaloniaPromptInterpreter.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AvaloniaPromptInterpreter.ViewModels;

public partial class MainViewModel : ViewModelBase
{
					public ObservableCollection<TypedInTransaction> TypedInTransactions { get; } = new ObservableCollection<TypedInTransaction>() {
																												new("&2024-03-09 $Account3 @Contractor1 #Healthcare Therapy 60.00 Medicine 15.00 DoctorVisit 100.00 GymMembership 30.00 Dentist 80.00 #Utilities CableTV 45.00 Electricity 50.00 Water 30.00 Gas 40.00 Internet 60.00 #Transportation BikeRepair 25.00 Gas 20.00 BusTicket 2.50 Taxi 15.00 TrainTicket 3.00 !tag3"),
																												new("&2024-03-10 $Account1 @Contractor2 #Transportation BikeRepair 25.00 Gas 20.00 BusTicket 2.50 Taxi 15.00 TrainTicket 3.00 #Entertainment Book 15.00 Concert 50.00 Movie 12.00 Popcorn 5.00 Theater 30.00 #Groceries Milk 2.99 Bread 1.49 Eggs 3.99 Cheese 4.99 Meat 20.00 Fish 15.00 Fruits 10.00 Vegetables 8.00 Pasta 2.00 Sauce 1.50 !tag1"),
																												new("&2024-03-06 $Account3 @Contractor2 #Groceries Meat 20.00 Fish 15.00 Fruits 10.00 Vegetables 8.00 Milk 2.99 Bread 1.49 Eggs 3.99 Cheese 4.99 Pasta 2.00 Sauce 1.50 #Healthcare Dentist 80.00 Medicine 15.00 DoctorVisit 100.00 GymMembership 30.00 Therapy 60.00 #Utilities CableTV 45.00 Electricity 50.00 Water 30.00 Gas 40.00 Internet 60.00 !tag3"),
																												new("&2024-03-07 $Account1 @Contractor3 #Utilities Gas 40.00 Electricity 50.00 Water 30.00 Internet 60.00 CableTV 45.00 #Transportation TrainTicket 3.00 Gas 20.00 BusTicket 2.50 Taxi 15.00 BikeRepair 25.00 #Entertainment Concert 50.00 Movie 12.00 Popcorn 5.00 Theater 30.00 Book 15.00 !tag1"),
																												new("&2024-03-08 $Account2 @Contractor4 #Entertainment Theater 30.00 Concert 50.00 Movie 12.00 Popcorn 5.00 Book 15.00 #Groceries Pasta 2.00 Sauce 1.50 Milk 2.99 Bread 1.49 Eggs 3.99 Cheese 4.99 Meat 20.00 Fish 15.00 Fruits 10.00 Vegetables 8.00 #Healthcare Therapy 60.00 Medicine 15.00 DoctorVisit 100.00 GymMembership 30.00 Dentist 80.00 !tag2"),
																												new("&2024-03-02 $Account2 @Contractor2 #Transportation Gas 20.00 BusTicket 2.50 Taxi 15.00 TrainTicket 3.00 #Entertainment Movie 12.00 Popcorn 5.00 Concert 50.00 Theater 30.00 #Groceries Fruits 10.00 Vegetables 8.00 Meat 20.00 Fish 15.00 !tag2"),
																												new("&2024-03-03 $Account3 @Contractor3 #Healthcare Medicine 15.00 DoctorVisit 100.00 GymMembership 30.00 Therapy 60.00 #Groceries Fruits 10.00 Vegetables 8.00 Meat 20.00 Fish 15.00 Pasta 2.00 Sauce 1.50 #Utilities Electricity 50.00 Water 30.00 Gas 40.00 Internet 60.00 !tag3"),
																												new("&2024-03-04 $Account1 @Contractor4 #Utilities Internet 60.00 CableTV 45.00 #Healthcare GymMembership 30.00 Therapy 60.00 Dentist 80.00 #Transportation Gas 20.00 BusTicket 2.50 Taxi 15.00 TrainTicket 3.00 !tag1"),
																												new("&2024-03-05 $Account2 @Contractor1 #Entertainment Concert 50.00 Movie 12.00 Popcorn 5.00 Theater 30.00 Book 15.00 #Transportation Taxi 15.00 Gas 20.00 BusTicket 2.50 TrainTicket 3.00 BikeRepair 25.00 #Groceries Pasta 2.00 Sauce 1.50 Milk 2.99 Bread 1.49 Eggs 3.99 Cheese 4.99 !tag2")
																								};

					public IList<string> AutoCompleteSuggestions => new List<string>()
					{
										"SSSSSSSS",
										"AAAAAA",
										"CDSDWD"
					};
}
