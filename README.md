# Sujet 2 : Supermarché

## Application Web en React liée à l'api créée en C#
(Il y a 2 repo git hub : https://github.com/Gregoire-Le-Roux/supermarche-app)

Pour que le site internet fonctionne, il faut d'abord avoir créé la base de données en local et avoir configuré et lancé l'api.

A la racine du projet, il y a un script sql qui permet de créer les tables avec quelques données. De préférence choisir la même BDD que moi pour éviter tout problème ou conflit.
Base de donnée utilisée : SQLServer avec Microsoft SQL Server Management Studio.

Ensuite, pour configurer l'api on a besoin du "Server Name" qui s'affiche lorsque l'on lance Microsoft SQL Server Management Studio, comme ci-dessous :

![image](https://user-images.githubusercontent.com/84314581/145428108-6d7ceafe-0214-4ac4-85c3-318db2d86af9.png)

On copie ce "Server Name" et dans les dossiers de l'api, on va dans le fichier "appsettings.json" et il faut remplacer LAPTOP-MOSP0D7T\\SQLEXPRESS par votre "Server Name".

![image](https://user-images.githubusercontent.com/84314581/145429100-472cfc1a-3230-47e7-a843-a5143626c218.png)

Et voilà, normalement tout est configuré. Vous devez avoir lancé votre base de données, votre api puis l'application web grâce à "npm start".
