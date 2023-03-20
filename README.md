# abandon-mine-demo
 Zadanie testowe dla GDT
 
Celem zadania jest stworzenie prototypu gry za pomocą Unity w wersji od 2020.1 wzwyż.
Gra powinna być strzelanką z pierwszoosobową perspektywą, w której użytkownik szuka wyjścia z poziomu, po drodze zbierając wirtualną walutę.

Dodatkowo, aplikacja ma umożliwiać użytkownikowi zarządzanie profilem opartym na usłudze PlayFab.
Użytkownik powinien móc:
	1. Zalogować się anonimowo (PlayFabClientAPI.LoginWithCustomID)
	2. Pobrać i wyświetlić swój ekwipunek (PlayFabClientAPI.GetUserInventory)
	3. Zmienić swój nick (PlayFabClientAPI.UpdateUserTitleDisplayName)
	4. Zapisać i wczytać notatki z UserData (PlayFabClientAPI.UpdateUserData i PlayFabClientAPI.GetUserData)

Wirtualna waluta zebrana podczas rozgrywki powinna zostać dodana do konta użytkownika przy pomocy PlayFabClientAPI.AddUserVirtualCurrency na ekranie podsumowania poziomu.

Przedmioty w ekwipunku gracza powinny zostać dodane ręcznie z poziomu panelu administratora.
Ich prezentację można ograniczyć do samej nazwy.

Wymagania minimalne:
	1. Skalujący się wraz z rozmiarem ekranu interfejs użytkownika, pozwalający użytkownikowi na realizację powyższych zadań (w menu głównym),
	2. Utworzenie konta oraz tytułu (gry) na PlayFab,
	3. Prosty kontroler FPS do przemieszczania się i ruchu kamery,
	4. Możliwość zbierania waluty,
	5. Prosty, prototypowy poziom,
	6. Udostępnienie plików źródłowych na publicznym repozytorium

Mile widziane:
	1. Dźwięki kroków, dowolny dźwięk ambient odtwarzany w tle, AudioMixer, suwak do zmiany głośności
	2. Wykorzystanie DOTween lub podobnej paczki do animacji interfejsu,
	3. Licznik czasu przechodzenia poziomu, wyświetlany na podsumowaniu
