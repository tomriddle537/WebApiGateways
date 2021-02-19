--ADD Gateways
INSERT INTO [Gateways] ([SerialNumber], [Name], [IpAddress]) VALUES ('gw-001','Gateway 1','10.0.0.1');
INSERT INTO [Gateways] ([SerialNumber], [Name], [IpAddress]) VALUES ('gw-002','Gateway 2','10.0.0.2');
INSERT INTO [Gateways] ([SerialNumber], [Name], [IpAddress]) VALUES ('gw-003','Gateway 3','10.0.0.3');
INSERT INTO [Gateways] ([SerialNumber], [Name], [IpAddress]) VALUES ('gw-004','Gateway 4','10.0.0.4');
INSERT INTO [Gateways] ([SerialNumber], [Name], [IpAddress]) VALUES ('gw-005','Gateway 5','10.0.0.5');

--ADD Devices 1 to 10 to Gateway 'gw-001'
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (1, 'Vendor 1', CURRENT_TIMESTAMP , 0 );
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (2, 'Vendor 2', CURRENT_TIMESTAMP , 1 );
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (3, 'Vendor 3', CURRENT_TIMESTAMP , 0 );
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (4, 'Vendor 4', CURRENT_TIMESTAMP , 1 );
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (5, 'Vendor 5', CURRENT_TIMESTAMP , 0 );
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (6, 'Vendor 6', CURRENT_TIMESTAMP , 1 );
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (7, 'Vendor 7', CURRENT_TIMESTAMP , 0 );
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (8, 'Vendor 8', CURRENT_TIMESTAMP , 1 );
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (9, 'Vendor 9', CURRENT_TIMESTAMP , 0 );
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (10, 'Vendor 0', CURRENT_TIMESTAMP , 1 );
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-001',1);
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-001',2);
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-001',3);
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-001',4);
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-001',5);
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-001',6);
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-001',7);
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-001',8);
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-001',9);
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-001',10);

--ADD Devices 11 to 15 to Gateway 'gw-002'
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (11, 'Vendor 1', CURRENT_TIMESTAMP , 0 );
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (12, 'Vendor 2', CURRENT_TIMESTAMP , 1 );
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (13, 'Vendor 3', CURRENT_TIMESTAMP , 0 );
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (14, 'Vendor 4', CURRENT_TIMESTAMP , 1 );
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (15, 'Vendor 5', CURRENT_TIMESTAMP , 0 );
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-002',11);
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-002',12);
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-002',13);
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-002',14);
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-002',15);

--ADD Devices 16 to 18 to Gateway 'gw-003'
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (16, 'Vendor 6', CURRENT_TIMESTAMP , 1 );
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (17, 'Vendor 7', CURRENT_TIMESTAMP , 0 );
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (18, 'Vendor 8', CURRENT_TIMESTAMP , 1 );
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-003',16);
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-003',17);
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-003',18);

--ADD Devices 19 and 20 to Gateway 'gw-004'
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (19, 'Vendor 9', CURRENT_TIMESTAMP , 0 );
INSERT INTO [Peripherals] ([UID], [Vendor], [Date], [Status]) VALUES (20, 'Vendor 0', CURRENT_TIMESTAMP , 1 );
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-004',19);
INSERT INTO [PeripheralGateways] ([GatewaySerialNumber],[PeripheralId]) VALUES ('gw-004',20);