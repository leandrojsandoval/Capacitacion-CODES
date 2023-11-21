USE DBProvincias;

CREATE TABLE Provincia (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Provincia VARCHAR(30)
)

/*INSERT INTO Provincia VALUES ('Buenos Aires');
INSERT INTO Provincia VALUES ('Formosa');
INSERT INTO Provincia VALUES ('Mendoza');
INSERT INTO Provincia VALUES ('Chaco');*/

SELECT * FROM Provincia