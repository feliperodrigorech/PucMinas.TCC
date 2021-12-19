delete Wharehouse;
delete ChargeHistory;
delete Merchandise;
delete Charge;
delete Address;
delete Contact;
delete Customer;
delete Provider;
delete Party;
delete Step;

INSERT INTO Step (Name, Description, Sequence, IsDelivered) VALUES 
	('EntradaMercadoria', 'A mercadoria foi recebido em nossas instalações.', 10, 0),
	('Transferencia', 'A mercadoria esta em transferência entre as filiais.', 20, 0),
	('Separacao', 'A mercadoria encontra-se em separação', 30, 0),
	('SaidaMercadoria', 'Saiu para entrega.', 40, 0),
	('Entregue', 'Mercadoria entregue.', 50, 1);

INSERT INTO Party (CnpjCpf, Name, Description, PartyType) VALUES
	--Cpf
	('81277274070', 'Pessoa Fictícia 01', '', 0),
	('15530503047', 'Pessoa Fictícia 02', '', 0),
	('15748762013', 'Pessoa Fictícia 03', '', 0),
	('17315877088', 'Pessoa Fictícia 04', '', 0),
	('81953883028', 'Pessoa Fictícia 05', '', 0),
	('94944974060', 'Pessoa Fictícia 06', '', 0),
	('62183679085', 'Pessoa Fictícia 07', '', 0),
	('89853911000', 'Pessoa Fictícia 08', '', 0),
	('31510400044', 'Pessoa Fictícia 09', '', 0),
	('15366936004', 'Pessoa Fictícia 10', '', 0),
	('36655366003', 'Pessoa Fictícia 11', '', 0),
	('60143100025', 'Pessoa Fictícia 12', '', 0),
	('18826813043', 'Pessoa Fictícia 13', '', 0),
	('47701585052', 'Pessoa Fictícia 14', '', 0),
	('25305075033', 'Pessoa Fictícia 15', '', 0),
	('31898572020', 'Pessoa Fictícia 16', '', 0),
	('45702643011', 'Pessoa Fictícia 17', '', 0),
	('19933672096', 'Pessoa Fictícia 18', '', 0),
	('01159874018', 'Pessoa Fictícia 19', '', 0),
	('41372733086', 'Pessoa Fictícia 20', '', 0),
	--Cnpj
	('34255913000170', 'Empresa Fictícia 01', '', 1),
	('55743392000170', 'Empresa Fictícia 02', '', 1),
	('39457748000153', 'Empresa Fictícia 03', '', 1),
	('79933730000103', 'Empresa Fictícia 04', '', 1),
	('39197445000149', 'Empresa Fictícia 05', '', 1);

DECLARE @countCPF INT = IDENT_CURRENT('Party') - 24;
WHILE @countCPF < IDENT_CURRENT('Party') + 1
BEGIN
	INSERT INTO Customer (Party_Id) VALUES (@countCPF)

	SET @countCPF += 1;
END;

DECLARE @countCNPJ INT = IDENT_CURRENT('Party') - 4;
WHILE @countCNPJ < IDENT_CURRENT('Party') + 1
BEGIN
	INSERT INTO Provider(Party_Id) VALUES (@countCNPJ)

	SET @countCNPJ += 1;
END;

DECLARE @count INT = 0;
DECLARE @lopping INT = 1000000;

WHILE @count < @lopping
BEGIN
	DECLARE @PartyRandom INT = (IDENT_CURRENT('Party') - 24 + (SELECT ABS(CHECKSUM(NEWID()) % (20 - 1))))
	DECLARE @PartyRandomCnpj INT = (IDENT_CURRENT('Party') - 4 + (SELECT ABS(CHECKSUM(NEWID()) % (5 - 1))))
	
	INSERT INTO Address (Party_Id, Street, StreetNumber, District, City, State, Country, BuildingCompliment, ZipCode, Latitude, Longitude, IsPrimary) VALUES
		(@PartyRandom, CONCAT('Rua Fictícia ', @count) , '0001', 'Centro', 'Caxias do Sul', 'RS', 'BRA', '', '', NULL, NULL, 0)

	INSERT INTO Contact (Locator, Description, ContactType, Party_Id) VALUES
		('(54) 99999-9999', CONCAT('Contato ', @count), 0, @PartyRandom)

	INSERT INTO Charge (Notes, ServiceOrder, SerialNumber, TransDate, DestinationAddress_Id, DestinationCustomer_Id, OriginCustomer_Id) VALUES
		('', @count, @count, GETDATE(), IDENT_CURRENT('Address'), @PartyRandom, @PartyRandomCnpj)

	INSERT INTO Merchandise (Sku, Quantity, UnitType, PriceUnit, Weight, Width, Length, Height, Charge_Id) VALUES
		(@count, 1, 0, 299.90, 1000, 50, 50, 20, IDENT_CURRENT('Charge'))

	DECLARE @countHistory INT = 1;
	WHILE @countHistory <= 5
	BEGIN
		INSERT INTO ChargeHistory (Description, Step_Id, Charge_Id) VALUES
			('', (SELECT Id FROM Step WHERE Sequence = @countHistory * 10), IDENT_CURRENT('Charge'))
		
		SET @countHistory += 1;
	END;

	SET @count += 1;
END;
