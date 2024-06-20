using CommunityToolkit.Mvvm.ComponentModel;

namespace FinancialTransactionTextInterpreter.ViewModels;
public partial class HelpBoxVM : ObservableObject
{
					[ObservableProperty]
					private string _helpTitle =
										@"Help";

					[ObservableProperty]
					private string _generalHelpContent =
										@"Słowniczek:
- Transakcja - transakcja wykonywana z podmiotem zewnętrznym: sklep, pracodawcą czy kimś z rodziny
- Transfer - transakcja wykonywana pomiędzy wewnętrznymi kontami, np. wypłata z konta i włożenie pieniędzy do portfela
- Tag - znak na początku pojedynczego wyrazu w tekście transakcji określający rodzaj danych do których wyraz się odnosi.

Funkcjonalności:
- Każdemu elementowi transakcji przypisywana jest najbliższa poprzedzająca go kategoria.
- Wszystkie wyrazy bez tagu i nie będące liczbami są traktowane jako nazwa elementu transakcji. W przypadku braku nazwy przypisana zostanie aktualna kategoria.
- Wszystkie wyrazy będące liczbami po danej nazwie są sumowane i zapisywane jako cena elementu";

					[ObservableProperty]
					private string _tagsSectionTitle =
										@"Tags";

					[ObservableProperty]
					private string _tagsSectionContent =
										@"- & - data. Jedna data na transakcje/transfer.
- $ - konto. W transakcji można wskazać tylko jedno konto. W transferze dwa: źródłowe i docelowe. Przepływ pieniędzy określa znak '>
- # - kategoria. Może byś wprowadzona tylko dla transakcji. Każdy element wpisany po danej kategorii będzie do niej należał'
- @ - kontrahent. Jeden kontrahent na transakcję i tylko w transakcji może być wprowadzony.
- + - używany w transakcjach/transferach do określenia przychodu. By default wszystkie liczby są traktowane jako ujemne, chyba że użyto przed nimi plusa
- brak znaku oznacza że wyraz należy do nazwy elementu transakcji albo jest ceną elementu.";

					[ObservableProperty]
					private string _examplesSectionTitle =
										@"Examples";

					[ObservableProperty]
					private string _examplesSectionContent =
										@"- Transakcja z nazwami elementów:

&22-04-2024 $Portfel #Jedzenie bułka 2.2 2.2 +0.4 szynka 3.3 #Zachcianka baton 4,5 @Żaba

Z  tego tekstu utworzona zostanie transakcja z kontrahentem o nazwie Żaba, zawarta 22 kwietnia 2024 roku, którą opłacono z konta o nazwie portfel. Utworzone w zostaną elementy tej transakcji: bułka z ceną - 4 i kategorią jedzenie, szynka z ceną - 3.3 i kategorią jedzenie oraz baton z ceną - 4.5 i kategorią zachcianka.
- Transakcja bez nazw elementów

&22-04-2024 $Bank #Jedzenie 2.2 2.2 +0.4  3.3 #Zachcianka 4,5 @Żaba

W tym przypadku utworzona zostanie również transakcja z kontrahentem o nazwie Żaba, zawarta 22 kwietnia 2024 roku, którą opłacono z konta o nazwie bank. Jednakże będzie ona miała dwa elementy jeden o nazwie jedzenie z ceną - 7.3 i kategorią jedzenie i drugi o nazwie zachcianka z ceną -4.5 i nazwą Zachcianka
- Transfer:

&22-04-2024 $Bank > $Portfel 2.2 2.2 +0.4 3.3

Utworzone zostaną dwie transakcję pierwsza którą opłaconą z konta Bank i kontrahentem jest Portfel która posiada jeden element o nazwie transfer z kategorią TransferOut i wartością -7.3 oraz drugą ""opłaconą"" z konta Portfel i kontrahentem Bank oraz elementem transfer z kategorii TransferIn i wartością 7.3. W transferach nie są dozwolone nazwy elementów, kategorie czy też kontrahent.";
}
