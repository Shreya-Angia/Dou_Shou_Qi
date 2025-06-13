# 🌟 Projet Universitaire IHM : DouShouQi *mythology*
 
**Dou Shou Qi Mythology** est une revisite du jeu traditionnel chinois "Jeu de la jungle", mais cette fois dans un univers grec et romain. Les animaux sont remplacés par des divinités mythologiques dotées de pouvoirs uniques (mode custom). Le but reste de se placer dans la maison du camp adverse ou d’éliminer toutes ses pièces. C'est un jeu stratégique avec des mécaniques innovantes.

![Made with ❤️](https://img.shields.io/badge/Made%20with-❤️-pink.svg)

<!-- --------------------------------------------------------------------------------------------------------- -->
## ➡️ Sommaire
- [🧱 Badges](#badges) 
- [🚀 Bien démarrer et Utilisation *(En cours d'écriture)*](#badges) 
- [📚 Documentation](#documentation) 
- [🧪 Exécuter les tests](#exécuter-les-tests) 
- [🛠️ Outils utilisés](#outils-utilisés) 
- [⚠️ Problèmes connus et limitations](#problèmes-connus-et-limitations) 
- [📂 Rendu du projet](#rendu-du-projet) 
- [🎬 Vidéo de présentation *(En cours d'écriture)*](#vidéo-de-présentation-en-cours-d-écriture) 
- [🙏 Remerciements](#remerciements)

<br>

<!-- --------------------------------------------------------------------------------------------------------- -->
## 🧱 Badges
[![Build Status](https://codefirst.iut.uca.fr/api/badges/enzo.jouve/SAE_1A_G3_Jouve_BouJahan_Angia_Fortune/status.svg)](https://codefirst.iut.uca.fr/enzo.jouve/SAE_1A_G3_Jouve_BouJahan_Angia_Fortune)
<Br>
[![Coverage](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=sonar_token_DouShouQi&metric=coverage&token=b59dfb5f3b78f239c76c95aca2c8e01a760d37e6)](https://codefirst.iut.uca.fr/sonar/dashboard?id=sonar_token_DouShouQi)
[![Bugs](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=sonar_token_DouShouQi&metric=bugs&token=b59dfb5f3b78f239c76c95aca2c8e01a760d37e6)](https://codefirst.iut.uca.fr/sonar/dashboard?id=sonar_token_DouShouQi)
[![Code Smells](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=sonar_token_DouShouQi&metric=code_smells&token=b59dfb5f3b78f239c76c95aca2c8e01a760d37e6)](https://codefirst.iut.uca.fr/sonar/dashboard?id=sonar_token_DouShouQi)
[![Duplicated Lines (%)](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=sonar_token_DouShouQi&metric=duplicated_lines_density&token=b59dfb5f3b78f239c76c95aca2c8e01a760d37e6)](https://codefirst.iut.uca.fr/sonar/dashboard?id=sonar_token_DouShouQi)
<Br>
[![Maintainability](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=sonar_token_DouShouQi&metric=sqale_rating&token=b59dfb5f3b78f239c76c95aca2c8e01a760d37e6)](https://codefirst.iut.uca.fr/sonar/dashboard?id=sonar_token_DouShouQi)
[![Lines of Code](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=sonar_token_DouShouQi&metric=ncloc&token=b59dfb5f3b78f239c76c95aca2c8e01a760d37e6)](https://codefirst.iut.uca.fr/sonar/dashboard?id=sonar_token_DouShouQi)
[![Quality Gate](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=sonar_token_DouShouQi&metric=alert_status&token=b59dfb5f3b78f239c76c95aca2c8e01a760d37e6)](https://codefirst.iut.uca.fr/sonar/dashboard?id=sonar_token_DouShouQi)
[![Reliability](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=sonar_token_DouShouQi&metric=reliability_rating&token=b59dfb5f3b78f239c76c95aca2c8e01a760d37e6)](https://codefirst.iut.uca.fr/sonar/dashboard?id=sonar_token_DouShouQi)
<br>
[![Security Rating](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=sonar_token_DouShouQi&metric=security_rating&token=b59dfb5f3b78f239c76c95aca2c8e01a760d37e6)](https://codefirst.iut.uca.fr/sonar/dashboard?id=sonar_token_DouShouQi)
[![Security Hotspots](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=sonar_token_DouShouQi&metric=security_hotspots&token=b59dfb5f3b78f239c76c95aca2c8e01a760d37e6)](https://codefirst.iut.uca.fr/sonar/dashboard?id=sonar_token_DouShouQi)
[![Technical Debt](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=sonar_token_DouShouQi&metric=sqale_index&token=b59dfb5f3b78f239c76c95aca2c8e01a760d37e6)](https://codefirst.iut.uca.fr/sonar/dashboard?id=sonar_token_DouShouQi)
[![Vulnerabilities](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=sonar_token_DouShouQi&metric=vulnerabilities&token=b59dfb5f3b78f239c76c95aca2c8e01a760d37e6)](https://codefirst.iut.uca.fr/sonar/dashboard?id=sonar_token_DouShouQi)

<!-- --------------------------------------------------------------------------------------------------------- -->
## 🚀 Bien démarrer et Utilisation *(En cours d'écriture)*

### Lancement du projet 

#### Mode Graphique
- Ouvrir le projet dans Visual Studio Community.
- Définir comme projet de démarrage, DouShouQiApp.
- Vérifier que la plateforme de démarrage est bien définie sur Windows Machine.
- Cliquer sur la flèche verte pleine ou appuyer sur Ctrl + F5.

#### Mode Console 
- Toujours depuis Visual Studio Community, 
- Définir comme projet de démarrage, DouShouQiConsole.
- Cliquer sur la flèche verte pleine ou appuyer sur Ctrl + F5.

⚠️ Attention : Le mode custom n'est pas encore fonctionnel. Évitez de le sélectionner lors du lancement d'une partie.

### Version déployée

<!-- --------------------------------------------------------------------------------------------------------- -->
## 👥 Les membres de l'équipe :
- BOU--JAHAN **Anae**
- JOUVE **Enzo**
- ANGIA **Shreya**
- FORTUNÉ **Grégoire** 

*GROUPE 3*

<!-- --------------------------------------------------------------------------------------------------------- -->
## 📚 Documentation
Toute la documentation est disponible sur les serveurs de CodeFirst en cliquant [ici](https://codefirst.iut.uca.fr/documentation/enzo.jouve/doxygen/SAE_1A_G3_Jouve_BouJahan_Angia_Fortune/html/).
<!-- --------------------------------------------------------------------------------------------------------- -->
## 🧪 Exécuter les tests
Voici la démarche à suivre pour lancer tous les tests :
- Ouvrir le projet dans Visual Studio Community.
- Dans le menu supérieur, cliquez sur l'onglet **Test**
- Puis cliquez sur **Exécuter tous les tests**.

Les résultats des tests s’affichent dans la fenêtre d'**explorateur des tests**.

<!-- --------------------------------------------------------------------------------------------------------- -->
## 🛠️ Outils utilisés
- .NET - Une plateforme de développement gratuite, multiplateforme et open-source pour créer de nombreux types d'applications.
- C# - Le langage de programmation principal utilisé.
- MAUI - .NET Multi-platform App UI pour créer des applications multiplateformes.
- SonarQube - Un outil pour l'inspection continue de la qualité du code.
- PlantUML - Logiciel en ligne qui nous a permis de réaliser l'ensemble de nos diagrammes

<!-- --------------------------------------------------------------------------------------------------------- -->
## ⚠️ Problèmes connus et limitations
Actuellement, il est **impossible** de jouer en **mode custom**. <br>
Veuillez donc faire **attention** à ne pas lancer une partie dans cette configuration.

<!-- --------------------------------------------------------------------------------------------------------- -->
## 📂 Rendu du projet

### Jalon 1
Tous les rendus pour le **jalon n°1** sont disponibles dans le **wiki**.

### Jalon 2
Vous trouverez l’ensemble des diagrammes UML : Diagramme de **classes** -  Diagrammes de **séquence** - Diagramme de **paquetage** ; dans le **Wiki**

Le code du jalon 2 est disponible dans le répertoire du code source. 
Il comprend : 
- Le modèle du jeu
- Les différents tests pour valider le code

### Jalon 3
Vous trouverez le nouveau diagramme UML **paquetage** qui comprend la persistance dans le **Wiki**
Vous trouverez également le diagramme de **classes** à jour, qui comprend les **ajouts personnels**.

Le code du jalon 3, qui ajoute les vues et la jouabilité en mode graphique, est disponible dans la branche master avant la date du 01/06/2025 - 23h59.

### Jalon 4
Pour ce dernier rendu du modèle, nous avons implémenté une fonctionnalité qui permet d’avoir une musique et ainsi de se plonger dans l’ambiance. 
Nous avons également ajouté la persistance pour sauvegarder une partie, mais celle-ci est obsolète et ne fonctionne pas. 
Enfin, nous avons presque fini le mode custom en console, mais il nous manque encore quelques fonctions, donc ce dernier n’est pas encore disponible à l’emploi.

<!-- --------------------------------------------------------------------------------------------------------- -->
## 🎬 Vidéo de présentation *(En cours d'écriture)*

<!-- --------------------------------------------------------------------------------------------------------- -->
## 🙏 Remerciements
Nous tenons à remercier nos enseignants pour leur accompagnement, leur disponibilité et leurs conseils tout au long de ce projet, qui nous ont été d'une aide précieuse.