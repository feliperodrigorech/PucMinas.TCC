CREATE TABLE Party(
	Id BIGINT NOT NULL IDENTITY(1,1),
	CnpjCpf VARCHAR(14),
	Name VARCHAR(60),
	Description VARCHAR(255),
	PartyType INT

	CONSTRAINT Id_PK PRIMARY KEY (Id)
);

CREATE TABLE Customer(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Party_Id BIGINT NOT NULL,

	CONSTRAINT Customer_Id_PK PRIMARY KEY (Id),
	CONSTRAINT Customer_Party_Id_FK FOREIGN KEY (Party_Id) REFERENCES Party(Id)
);

CREATE TABLE Provider(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Party_Id BIGINT NOT NULL,

	CONSTRAINT Provider_Id_PK PRIMARY KEY (Id),
	CONSTRAINT Provider_Party_Id_FK FOREIGN KEY (Party_Id) REFERENCES Party(Id)
);

CREATE TABLE Wharehouse(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Party_Id BIGINT NOT NULL,

	CONSTRAINT Wharehouse_Id_PK PRIMARY KEY (Id),
	CONSTRAINT Wharehouse_Party_Id_FK FOREIGN KEY (Party_Id) REFERENCES Party(Id)
);

CREATE TABLE Contact(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Party_Id BIGINT NOT NULL,
	Description VARCHAR(60),
	Locator VARCHAR(255),
	ContactType INT,
	IsPrimary BIT,

	CONSTRAINT Contact_Id_PK PRIMARY KEY (Id),
	CONSTRAINT Contact_Party_Id_FK FOREIGN KEY (Party_Id) REFERENCES Party(Id)
);

CREATE TABLE Address(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Party_Id BIGINT NOT NULL,
	Street VARCHAR(60),
	StreetNumber VARCHAR(6),
	District VARCHAR(60),
	State VARCHAR(60),
	City VARCHAR(60),
	Country VARCHAR(60),
	BuildingCompliment VARCHAR(60),
	ZipCode VARCHAR(8),
	Latitude DECIMAL(18,6),
	Longitude DECIMAL(18,6),
	IsPrimary BIT,

	CONSTRAINT Address_Id_PK PRIMARY KEY (Id),
	CONSTRAINT Address_Party_Id_FK FOREIGN KEY (Party_Id) REFERENCES Party(Id)
);

CREATE TABLE Charge(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Notes VARCHAR(255),
	ServiceOrder VARCHAR(60),
	OriginCustomer_Id BIGINT NOT NULL,
	DestinationCustomer_Id BIGINT NOT NULL,
	TransDate DATE,
	SerialNumber VARCHAR(60),
	DestinationAddress_Id BIGINT NOT NULL,

	CONSTRAINT Charge_Id_PK PRIMARY KEY (Id),
	CONSTRAINT Charge_OriginCustomer_Id_FK FOREIGN KEY (OriginCustomer_Id) REFERENCES Party(Id),
	CONSTRAINT Charge_DestinationCustomer_Id_FK FOREIGN KEY (DestinationCustomer_Id) REFERENCES Party(Id),
	CONSTRAINT Charge_DestinationAddress_Id_FK FOREIGN KEY (DestinationAddress_Id) REFERENCES Address(Id),
);

CREATE TABLE Merchandise(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Sku VARCHAR(20),
	Quantity DECIMAL(18,3),
	UnitType INT,
	PriceUnit DECIMAL(18,2),
	Weight INT,
	Width INT,
	Length INT,
	Height INT,
	Wharehouse_Id BIGINT,
	Charge_Id BIGINT NOT NULL,

	CONSTRAINT Merchandise_Id_PK PRIMARY KEY (Id),
	CONSTRAINT Merchandise_Wharehouse_Id_FK FOREIGN KEY (Wharehouse_Id) REFERENCES Wharehouse(Id),
	CONSTRAINT Merchandise_Charge_Id_FK FOREIGN KEY (Charge_Id) REFERENCES Charge(Id)
);

CREATE TABLE Step(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Name VARCHAR(60),
	Description VARCHAR(255),
	Sequence INT,
	IsDelivered BIT,

	CONSTRAINT Step_Id_PK PRIMARY KEY (Id),
);

CREATE TABLE ChargeHistory(
	Id BIGINT NOT NULL IDENTITY(1,1),
	Description VARCHAR(255),
	Step_Id BIGINT,
	Charge_Id BIGINT,

	CONSTRAINT ChargeHistory_Id_PK PRIMARY KEY (Id),
	CONSTRAINT ChargeHistory_Step_Id_FK FOREIGN KEY (Step_Id) REFERENCES Step(Id),
	CONSTRAINT ChargeHistory_Charge_Id_FK FOREIGN KEY (Charge_Id) REFERENCES Charge(Id)
);