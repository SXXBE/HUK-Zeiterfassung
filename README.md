# HUK-Zeiterfassung

VORWORT:

In diesem Dokument möchte ich Ihnen einen Überblick über die von mir entwickelte Software geben. Es handelt sich hierbei um mein erstes Projekt, das derzeit noch nicht vollständig abgeschlossen ist. Diese Software ist ein privates Projekt, das bereits in meiner produktiven Umgebung im Einsatz ist und stabil funktioniert. Aktuell gibt es nur einige wenige Softwarefehler, die ich in naher Zukunft beheben werde. In der neuesten Version sind noch Anpassungen erforderlich, um die Software vollständig funktionsfähig und benutzerfreundlicher zu gestalten. Ich arbeite aktiv an diesen Verbesserungen, um das Projekt weiter zu optimieren.
Mir ist bewusst, dass der Quellcode möglicherweise in einer prägnanteren und strukturierteren Form verfasst werden könnte. Leider wurde während des Lernprozesses die Dokumentation des Quellcodes vernachlässigt. Ich erkenne jedoch die hohe Wichtigkeit von Dokumentationen an, da sie dazu beitragen, den Code verständlicher und wartungsfreundlicher zu gestalten. Eine umfassende Dokumentation erleichtert nicht nur die Nachvollziehbarkeit für andere Entwickler, sondern auch für mich selbst in der Zukunft.

Die Entwicklung der Software erfolgte unter Verwendung von Visual Studio Community 2022 in der Programmiersprache C#. Für die grafische Benutzeroberfläche kommt das WPF-Framework zum Einsatz.
Zusätzlich werden sämtliche EInstellungen, Projekteeinträge und Zeiteinträge in einer SQLite Datenbank im Windows Userverzeichnis abgelegt. 




DATENBANKSCHEMA:

![image](https://github.com/user-attachments/assets/537947c4-b282-413c-8130-a74241e45b4e)

![image](https://github.com/user-attachments/assets/7702c0c2-0785-4541-b313-34b878556e60)


HINTERGRUND: 

Im Rahmen der geschäftlichen Abläufe wurde eine Projektzeiterfassung implementiert, die jedoch bislang ohne eine Unterstützung der detaillierten Erfassung erfolgt. Die Vorgesetzten haben die Anforderung formuliert, die täglich in Projektarbeiten aufgewendeten Zeiten in das vorhandene ERP-System des Unternehmens zu integrieren. Ziel dieser Integration ist es, die menschlichen Ressourcen für anstehende Projekte effizient zu erfassen und zu verwalten.
Die bestehende ERP-Software bietet jedoch keine Funktionalität zur Erfassung einzelner Zeitabschnitte, sondern ermöglicht lediglich die Eingabe einer Gesamtsumme. Um die geforderte Produktivität zu gewährleisten und eine detaillierte Zeiterfassung zu ermöglichen, wurde zunächst eine Excel-Liste bereitgestellt.
Um den Anforderungen der Vorgesetzten gerecht zu werden und eine systematische sowie präzise Zeiterfassung zu gewährleisten, besteht die Notwendigkeit, eine geeignete Softwarelösung zu entwickeln, welche die Erfassung der aufgewendeten Zeiten einfach zu erfassen lässt.

FUNKTIONSÜBERSICHT

Fenster und Bedienung
-	Anheftung am Bildschirmrand: Das Fenster zur Zeiterfassung kann am gewünschten Bildschirmrand des ausgewählten Monitors verankert werden. Dadurch bleibt die Anwendung unauffällig im Hintergrund und lässt sich durch ein einfaches „Mouse hover“ aufrufen. Nach einer in den Einstellungen definierten Zeit versteckt sich das Fenster automatisch wieder oder kann manuell geschlossen werden.

-	Übersichtliche Benutzeroberfläche: Die Bedienoberfläche ist klar strukturiert. Dank großer Bedienelemente können Zeiterfassungen schnell und unkompliziert gestartet und beendet werden.

Projektmanagement
-	Projektbeschränkung auf „aktive“ Projekte: Es können nur Zeiten für Projekte erfasst werden, die als „aktiv“ markiert sind. Dies ermöglicht es, geplante Projekte anzulegen, ohne dass sie in der Übersicht stören.

-	Schutz vor versehentlicher Löschung: Projekte, die als „geschützt“ markiert sind, können nicht versehentlich gelöscht werden. Sie müssen vor dem Löschen zuerst entsperrt werden.

-	Projektarchiv: Abgeschlossene Projekte werden nicht sofort gelöscht, sondern in ein Projektarchiv verschoben. Hier können sie entweder reaktiviert oder dauerhaft gelöscht werden.

Zeiterfassung und Anzeige
-	Kalenderansicht: Die Zeiterfassungen werden übersichtlich über einen interaktiven Kalender verwaltet.

-	Wochenübersicht: Eine Übersicht zeigt alle Zeiterfassungen der ausgewählten Kalenderwoche sowie die Gesamtzeit pro Tag.

-	Doppelklick für Zwischenspeicher: Ein Doppelklick auf einen Zeiteintrag lädt die Daten automatisch in den Zwischenspeicher. Zeiteinträge im Format „HH
“ werden dabei automatisch in Dezimalwerte umgerechnet, wie es für das ERP-System erforderlich ist.

Timer-PopUp
-	Schwebendes Fenster: Der Timer-PopUp ist ein dauerhaft sichtbares, anpassbares Fenster, das die aktuelle Projektzeit anzeigt. Zusätzlich kann eine Erinnerung eingestellt werden, die den Nutzer daran erinnert, die erfassten Zeiten in das ERP-System zu übertragen. Ein Autostartmodus ist ebenfalls vorhanden.

Autostart und Systemintegration
-	Autostart-Funktion: Die HUK-Zeiterfassung kann so konfiguriert werden, dass sie automatisch beim Start des Computers gestartet wird und mit der Zeiterfassung beginnt. Dabei muss ein Startprojekt angegeben werden.

-	SystemTray- und Taskleisten-Integration: Es besteht die Möglichkeit, die HUK-Zeiterfassung im SystemTray oder in der Taskleiste ein- oder auszublenden.

- Die Daten der Erfassung werden in einer SQLite Datenbank hinterlegt, welche im Windows Benutzerverzeichnis abgelegt wird. Dies hat den Vorteil, dass jeder Benutzer seine eigene Datenbank hat. 

SCREENSHOTS & AUFZEICHNUNGEN:

![LXce4CU5MD](https://github.com/user-attachments/assets/e6d029c2-75c2-4223-a7aa-20068db7831a)


Die eigentliche Zeiterfassung ist am Bildschirmrand verankert und öffnet mit einem MouseHover. Die Bedienelemente sind groß und übersichtlich gestaltet um eine schnelle Bedienung zu gewährleisten.

![cLB6KT9m6B](https://github.com/user-attachments/assets/7104b811-9ff0-4691-95c1-3436af47ff80)

Projekte welche im Management als "aktiv" gesetzt sind, werden im DropDown Menü zur Verfügung gestellt. Eine Übersicht über die Zeiteinträge vom heutigen Tag geben einen schnellen Einblick


Das Management der HUK-Zeiterfassung kann entwedern über das Icon im Systemtray oder über das Erfasungsfenster geöffnet werden. Über einen interaktiven Kalender lässt sich das Datum bzw. die geünschte Kalenderwoche öffnen.
In der Zeitenübersicht werden die aus der ausgewählten Kalenderwoche hinterlegten Zeiten eingespeichert im Forma HH:mm. Mit einem Doppelklick werden diese Zeiten im Dezimalformat in den Windows-Zwischenspeicher übertragen um so 1:1 in das ERP System eingefügt zu werden. 

Bei Auswahl des gewünschten Datum werden im unteren Bereich die gebuchten Zeiten in das Frame geladen. Hier können manuelle EInträge vorgenommen werden, sowie EInträge bearbeitet oder gelöscht werden. 


![image](https://github.com/user-attachments/assets/701108df-b6d6-4c73-a427-f1bc2c1d2eeb)

Es können neue Projekte angelegt, bearbeitet oder in das Projectarschiv verschoben werden. Projekte können als "aktiv" oder "geschützt" markiert werden.

Aktiv: Diese Projekte stehen zur Buchung zur Verfügung. Ein Projekt, welches nicht als aktiv markiert ist, kann nicht aktiv gebucht werden - auch nicht manuell. Dies bringt den Vorteil, dass ich zukünftige Projekte bereits anlegen kann ohne die Übersicht zu verlieren beim Buchen der Zeiten. 

Geschützt: Diese Projekte sind vor dem Verschieben in das Projektarchiv geschützt. 

![image](https://github.com/user-attachments/assets/04838563-a680-4b21-a6af-42142be03680)

Das Projektarchiv beinhaltet alle Projekte, welche nicht mehr in Verwendung sind. Projekte können hier reaktiviert oder dauerhaft gelöscht werden. 

![image](https://github.com/user-attachments/assets/60d2382d-254b-412f-9495-982feb7c2d5e)

Über das Einstellungsmenü lassen sich diverse Punkte wie optische Anpassungen, Autostart oder andere Funktionen festlegen. 

TO-DO's:

- Überarbeitung des Datenbankschemas
- Dynamische Aktualisierung der eingetragenen Zeiten bei Löschung oder Änderungen von Zeiteinträgen
- Updatefunktion
- Dokumentation
- Bugfixes
