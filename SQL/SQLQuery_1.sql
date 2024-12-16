CREATE TABLE Pets(
    name VARCHAR(50),
    type VARCHAR(50),
    descriptions VARCHAR(255)
);

INSERT INTO Pets VALUES('Nyla','Cat','Likes to puke');
INSERT INTO Pets VALUES('Twinchi','Chinchilla','Likes strawberry treats');
INSERT INTO Pets VALUES('Buddy','Beagle','Long walks');
INSERT INTO Pets VALUES('Everest','Dog','Chases tail');
INSERT INTO Pets VALUES('Rosie','Cat','Likes attention');
INSERT INTO Pets VALUES('Chula','Cat','Likes smelly clothes');


SELECT * FROM Pets

UPDATE Pets
    SET type = 'Dog'
    WHERE type = 'Beagle'

SELECT * FROM Pets