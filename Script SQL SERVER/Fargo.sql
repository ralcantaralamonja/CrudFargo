USE MASTER -- Nos posiciamos en fargo
GO
CREATE DATABASE FARGO
ON PRIMARY
(	NAME             =  fargo_Dat, 
	FILENAME     = 'D:\Databases\Fargo.mdf', 
	SIZE               = 5 MB, 
	MAXSIZE        = 200, 
	FILEGROWTH = 5 )
LOG ON
(	NAME				  = fargo_Log, 
	FILENAME		  = 'D:\Databases\Fargo.ldf',
	SIZE				  = 1MB, 
	MAXSIZE		  = 100, 
	FILEGROWTH = 1MB)
GO

Use FARGO

Go

create table Tipo(
idTipo int identity(1,1) NOT NULL PRIMARY KEY,
nombre varchar(40) UNIQUE
)

GO

create table Contenedor(
idContenedor int  identity(1,1) NOT NULL PRIMARY KEY,
numContenedor int NOT NULL UNIQUE,
idTipo int NOT NULL REFERENCES Tipo(idTipo),
tamContenedor int,
pesoContenedor float,
taraContenedor float
)
GO
-- Insertamos  los tipos

INSERT INTO Tipo (nombre) values 
('HIG CUBE'),('REFFER'),('OPEN TOP')

GO

--Creamos los SP

Create procedure usp_InsertarContenedor(
@numCon int ,
@idTipo int,
@tamCon int,
@pesCon float,
@tarCon float
)
AS
INSERT INTO Contenedor
(numContenedor,idTipo,tamContenedor,pesoContenedor,taraContenedor) 
	VALUES
(@numCon,@idTipo,@tamCon,@pesCon,@tarCon) 

GO
Create procedure usp_ListarTipos
as
Select idTipo ,nombre from Tipo

GO
Execute usp_InsertarContenedor 2024,1,25,16.5,10.5
Go
Create procedure usp_ListarContenedores
AS
Select 
C.idContenedor, 
C.numContenedor,
T.nombre as Tipo,
C.tamContenedor,
C.pesoContenedor,
C.taraContenedor
from Contenedor  C 
INNER JOIN	Tipo  T ON T.idTipo = C.idTipo 
 
Go

Create procedure usp_ListarContenedor(
@Id int
)
AS
Select 
C.idContenedor, 
C.numContenedor,
T.nombre as Tipo,
C.tamContenedor,
C.pesoContenedor,
C.taraContenedor
from Contenedor  C 
INNER JOIN	Tipo  T ON T.idTipo = C.idTipo 
Where C.idContenedor = @Id

Go
-- Execute  usp_ListarContenedor 1

Go
Create procedure usp_ActualizarContenedor(
@Id int,
@numCont int,
@IdTipo int,
@tamCont int,
@pesCont float,
@taraCont float
)
AS

UPDATE Contenedor
SET   numContenedor = @numCont,idTipo = @IdTipo,tamContenedor=@tamCont,pesoContenedor =@pesCont,taraContenedor =@taraCont
Where idContenedor = @Id

GO
usp_ListarContenedor 1
GO
--Execute usp_ActualizarContenedor 1,1999,2,30,50.5,30.7
GO
Execute usp_ListarContenedor 1
GO
Create procedure usp_BorrarContenedor(
@Id int
)
AS

DELETE FROM Contenedor
WHERE idContenedor = @Id
--Execute usp_BorrarContenedor 1