-- double hyphen denotes a comment

/* Provides multi 
line comments. */

/* SQL Command families

DDL - Data Definition Language: Used to manipulate the 
structure/state (the tables) of our database.
CREATE, DROP, ALTER


DML - Data Manipulation Language: Manipulate the data/contents
of our database.
INSERT, UPDATE, DELETE


DCL - Data Control Language: Controlling who can access
or manipulate the database. Will not be used much.
GRANT/REVOKE USER, GRANT/REVOKE LOGIN

DQL - Data Query Langauge: Used to access records in the database. 
The commands we use to phrase, filter, structure a query.
SELECT, WHERE, FROM, IF, AND, OR, 
JOINS - Used to link the keys in multiple tables to return more 
relevant data to a query. */

--CREATE TABLE "NAME"
--(
--  column descriptions
--)
--VERB NOUN command convention

-- Include a drop table when building a db's tables

DROP TABLE PokemonType;
DROP TABLE Pokemon;
DROP TABLE Type;

CREATE TABLE Pokemon
( --column-name variable-type modifiers.
    Id INT PRIMARY KEY IDENTITY, /* not null and unique, 
    marks this field as the PK reference, IDENTITY 
    auto generates this field.*/
    Name NVARCHAR(64) NOT NULL UNIQUE,
    Height INT NOT NULL,
    Weight INT NOT NULL,
);

CREATE TABLE Type
(
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(64) NOT NULL UNIQUE
);

/* Multiplicity - the relationships between database entries in a database/tables

1-to-1 - for each entry in table A, there is one (and only one) related entry in table B.
1-to-many - for each entry in table A, there is/are many possible related entries in table B.
many-to-many - for each entry in table A, there are many related entries in table B.

For each Person, you have one Height. It can change but there is only one.
For each Height, you can have many people with the same height. 1 to many.*/


-- DROP TABLE PokemonType;
 -- create a linking table between Pokemon and Type
 CREATE TABLE PokemonType
 (
    Id INT NOT NULL PRIMARY KEY IDENTITY,
    PokemonId INT NOT NULL FOREIGN KEY REFERENCES Pokemon (Id)
        ON DELETE CASCADE,
    TypeId INT NOT NULL FOREIGN KEY REFERENCES Type (Id)
        ON DELETE CASCADE
 );

 --CASCASE Triggers the specified column to also delete/update when the FK entry is affected.

--INSERT data into a record

INSERT INTO Pokemon (Name, Height, Weight) VALUES
    ('Charizard', 67, 215),
    ('Mudkip', 16, 17);

INSERT INTO Type (Name) VALUES 
    ('Fire'),
    ('Water'),
    ('Dragon'),
    ('Grass'),
    ('Flying');

INSERT INTO PokemonType (PokemonId, TypeId) VALUES
    (1, 1),
    (1, 3),
    (2, 2);

--RETRIEVE DATA

SELECT * FROM Type;
SELECT * FROM Pokemon;
SELECT Name FROM Pokemon WHERE Name LIKE '%d';
SELECT * FROM PokemonType;

--Using Joins
SELECT P.Name, T.Name
FROM Pokemon AS P
JOIN PokemonType AS PT ON P.Id = PT.PokemonId
JOIN Type AS T ON T.Id = PT.TypeId;

/* JOINS

Table a         table b 
1               null
2               b
null            null    
null            e

full - returns all matched records, ignoring null (Left, center, right of v-diagram)
inner - returns matched records, removing null entries (center of v-diagram)
outer - returns matched records, keeping only the entries with a null (left and right, no center)
left - returns entire "left" table, as well as matching non-null entries from the "right". (Left and center)
right - returns entire "right" table, as well as matching non-null entries from the "left". (Center and right)
cross - returns any and all combination of entries possible between the two tables (all possible options)


