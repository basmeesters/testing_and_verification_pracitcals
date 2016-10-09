Handleiding ST& V practicum stage 2:
Hier wordt puntsgewijs besproken waar bepaalde onderdelen van het programma staan en 
hoe ze gebruikt dienen te worden.

Adresboek:
-	In de Adresboek map staat de daadwerkelijke applicatie
-	In de AdresboekTest map staan alle unit tests en de coded UI tests van de applicatie
-	In Logs komen alle logfiles die worden aangemaakt wanneer de applocatie gebruikt wordt.
    Er zijn drie verschillende soorten logfiles
        - De standard logs die aangemaakt worden wanneer het programma doorlopen wordt.
        - De logs van het doorlopen van de primepaden
        - De Oracles log. Deze bevat voor elke keer dat de applicatie doorlopen wordt
          per oracle of deze true of false was.
-	Wanneer de solution wordt geopend dient eerst in RecordAndReplay.cs het path van de 
    applicatie aangepast te worden. Deze staat hardcoded ingesteld op onze eigen pahts.
-   In deze map staat ook een screenshot van de 100& code coverage

PrimePathCoverage
-	Tool: in deze map staat de zelf geschreven tool voor het berekenen van de prime paden
          en de prime paden met detours.
-	Verder staan in deze map alle documentatie voor het berekenen van alle prime paden
