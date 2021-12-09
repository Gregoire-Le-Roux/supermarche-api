CREATE TABLE categorie_article
(
	id INT PRIMARY KEY identity(1,1),
	label VARCHAR(100),
)

CREATE TABLE article 
(
	id INT PRIMARY KEY identity(1,1),
	nom VARCHAR(100),
	quantite INT,
	prix DECIMAL(6,2),
	id_categorie INT,
	CONSTRAINT FK_categorie_article FOREIGN KEY (id_categorie) REFERENCES categorie_article(id),
)

CREATE TABLE client
(
	id INT PRIMARY KEY identity(1,1),
	prenom VARCHAR(100),
	nom VARCHAR(100),
)

CREATE TABLE panier
(
	id_panier INT NOT NULL,
	id_client INT NOT NULL,
	id_article INT NOT NULL,
	quantite INT NOT NULL,
	date_achat DATETIME,
	PRIMARY KEY (id_panier,id_client, id_article),
	CONSTRAINT FK_panier_article FOREIGN KEY (id_article) REFERENCES article(id),
	CONSTRAINT FK_panier_client FOREIGN KEY (id_client) REFERENCES client(id),
)


INSERT INTO categorie_article (label) VALUES ('Fruit');
INSERT INTO categorie_article (label) VALUES ('Légume');
INSERT INTO categorie_article (label) VALUES ('Laitage');
INSERT INTO categorie_article (label) VALUES ('Viande');
INSERT INTO categorie_article (label) VALUES ('Sucrerie');

INSERT INTO article (nom, quantite, prix,id_categorie) VALUES ('Banane', 8, 1.30, 1);
INSERT INTO article (nom, quantite, prix,id_categorie) VALUES ('Fraise', 39, 0.80, 1);
INSERT INTO article (nom, quantite, prix,id_categorie) VALUES ('Pamplemousse', 15, 2.60, 1);
INSERT INTO article (nom, quantite, prix,id_categorie) VALUES ('Framboise', 27, 1.00, 1);
INSERT INTO article (nom, quantite, prix,id_categorie) VALUES ('Haricot vert', 15, 0.5, 2);
INSERT INTO article (nom, quantite, prix,id_categorie) VALUES ('Petit pois', 180, 0.1, 2);
INSERT INTO article (nom, quantite, prix,id_categorie) VALUES ('Poulet', 4, 15.3, 4);
INSERT INTO article (nom, quantite, prix,id_categorie) VALUES ('Steak Haché', 6, 8.7, 4);

INSERT INTO client (prenom, nom) VALUES ('Grégoire','LE ROUX', 1500);
INSERT INTO client (prenom, nom) VALUES ('Antoine','LE ROUX', 200);
INSERT INTO client (prenom, nom) VALUES ('Lucas','MOINE', 500);

INSERT INTO panier (id_panier, id_client, id_article, quantite, date_achat) VALUES (1, 1, 3, 4, NULL);
INSERT INTO panier (id_panier, id_client, id_article, quantite, date_achat) VALUES (1, 1, 5, 2, NULL);
INSERT INTO panier (id_panier, id_client, id_article, quantite, date_achat) VALUES (1, 1, 2, 7, NULL);
INSERT INTO panier (id_panier, id_client, id_article, quantite, date_achat) VALUES (1, 1, 1, 2, NULL);