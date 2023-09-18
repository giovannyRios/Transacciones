CREATE DATABASE Transacciones;
GO

USE Transacciones;

CREATE TABLE [Genero] (
  [ID] Integer,
  [Valor] nvarchar(100),
  PRIMARY KEY ([ID])
);


CREATE TABLE [Persona] (
  [ID] Integer,
  [Nombre] nvarchar(300),
  [Genero_Id] Integer,
  [Edad] Integer,
  [Identificacion] nvarchar(300),
  [Direccion] nvarchar(500),
  [Telefono] nvarchar(15),
  PRIMARY KEY ([ID])
);
CREATE INDEX [Clave] ON  [Persona] ([Identificacion]);
ALTER TABLE [Persona] ADD CONSTRAINT FK_Genero_Id FOREIGN KEY ([Genero_Id]) REFERENCES [Genero]([ID])
ALTER TABLE [Persona] ADD CONSTRAINT UQ_Identificacion UNIQUE ([Identificacion])



CREATE TABLE [Cliente] (
  [ID] Integer,
  [ClienteId] Nvarchar(100),
  [Contrasena] Nvarchar(100),
  [PersonaId] Integer,
  [Estado] BIT,
  PRIMARY KEY ([ID])
);
CREATE INDEX [Clave] ON  [Cliente] ([ClienteId]);
ALTER TABLE [Cliente] ADD CONSTRAINT FK_PersonaId FOREIGN KEY ([PersonaId]) REFERENCES [Persona]([ID])
ALTER TABLE [Cliente] ADD CONSTRAINT UQ_ClientId UNIQUE ([ClienteId])

CREATE TABLE [Tipo Cuenta] (
  [ID] Integer,
  [Valor] nvarchar(200),
  [Estado] BIT,
  PRIMARY KEY ([ID])
);

CREATE TABLE [Cuenta] (
  [ID] Integer,
  [NumeroCuenta] nvarchar(50),
  [TipoCuentaId] Integer,
  [ClienteId] Integer,
  [Saldo] Numeric(18,2),
  [Estado] BIT,
  PRIMARY KEY ([ID])
);
CREATE INDEX [Clave] ON  [Cuenta] ([NumeroCuenta]);
ALTER TABLE [Cuenta] ADD CONSTRAINT FK_TipoCuentaId FOREIGN KEY ([TipoCuentaId]) REFERENCES [Tipo Cuenta]([ID])
ALTER TABLE [Cuenta] ADD CONSTRAINT FK_ClienteId FOREIGN KEY ([ClienteId]) REFERENCES [Cliente]([ID])
ALTER TABLE [Cuenta] ADD CONSTRAINT UQ_NumeroCuenta UNIQUE ([NumeroCuenta])

CREATE TABLE [Movimientos] (
  [ID] Integer,
  [FechaMovimiento] Datetime,
  [CuentaId] Integer,
  [Saldo] Numeric(18,2),
  [DescripcionMovimiento] nvarchar(500),
  PRIMARY KEY ([ID])
);
CREATE INDEX [Clave] ON  [Movimientos] ([FechaMovimiento]);
ALTER TABLE [Movimientos] ADD CONSTRAINT FK_CuentaId FOREIGN KEY ([CuentaId]) REFERENCES [Cuenta]([ID])


--Minimal Data
INSERT INTO [dbo].[Genero]
           ([ID]
           ,[Valor])
     VALUES
           (1
           ,'Masculino')

INSERT INTO [dbo].[Genero]
           ([ID]
           ,[Valor])
     VALUES
           (2
           ,'Femenino')

INSERT INTO [dbo].[Genero]
           ([ID]
           ,[Valor])
     VALUES
           (3
           ,'Otro')


INSERT INTO [dbo].[Tipo Cuenta]
           ([ID]
           ,[Valor]
           ,[Estado])
     VALUES
           (1
           ,'Ahorro'
           ,1)

INSERT INTO [dbo].[Tipo Cuenta]
           ([ID]
           ,[Valor]
           ,[Estado])
     VALUES
           (2
           ,'Corriente'
           ,1)




