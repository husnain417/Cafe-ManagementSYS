create database CafeSystem;

USE CafeSystem;

-- Admin table
CREATE TABLE Admin (
    admin_id INT PRIMARY KEY not null identity(1,1),
    admin_name VARCHAR(255),
    admin_qualification VARCHAR(255),
    admin_password VARCHAR(255),
    admin_email VARCHAR(255),
    contact_info VARCHAR(255),
    gender VARCHAR(10)
);
--Accounts Table

CREATE TABLE Accounts (
	email VARCHAR(255),
	password VARCHAR(255)
);

-- Manager table
CREATE TABLE Manager (
    manager_id INT PRIMARY KEY not null identity(1,1),
    admin_id INT,
    manager_name VARCHAR(255),
    manager_qualification VARCHAR(255),
    manager_email VARCHAR(255),
    contact_info VARCHAR(255),
    manager_password VARCHAR(255),
    gender VARCHAR(10),
    FOREIGN KEY (admin_id) REFERENCES Admin(admin_id)
);

-- Ingredient table
CREATE TABLE Ingredient (
    ingredient_id INT PRIMARY KEY not null identity(1,1),
    ingredient_name VARCHAR(255)
);

-- Inventory table
CREATE TABLE Inventory (
    inventory_id INT PRIMARY KEY not null identity(1,1),
    ingredient_id INT,
    inventory_name VARCHAR(255),
    inventory_location VARCHAR(255),
    ingredient_quantity INT,
    FOREIGN KEY (ingredient_id) REFERENCES Ingredient(ingredient_id)
);

-- Baristas table
CREATE TABLE Baristas (
    baristas_id INT PRIMARY KEY not null identity(1,1),
    manager_id INT,
    baristas_name VARCHAR(255),
    baristas_qualification VARCHAR(255),
    contact_info VARCHAR(255),
    gender VARCHAR(10),
    baristas_password VARCHAR(255),
    baristas_email VARCHAR(255),
    FOREIGN KEY (manager_id) REFERENCES Manager(manager_id)
);

-- Cashier table
CREATE TABLE Cashier (
    cashier_id INT PRIMARY KEY not null identity(1,1),
    manager_id INT,
    cashier_name VARCHAR(255),
    cashier_qualification VARCHAR(255),
    contact_info VARCHAR(255),
    cashier_password VARCHAR(255),
    cashier_email VARCHAR(255),
    FOREIGN KEY (manager_id) REFERENCES Manager(manager_id)
);

-- Customer table
CREATE TABLE Customer (
    customer_id INT PRIMARY KEY not null identity(1,1),
    customer_name VARCHAR(255),
    customer_email VARCHAR(255),
    customer_contact_info VARCHAR(255),

);

-- OrderItem table
CREATE TABLE OrderItem (
    item_id INT PRIMARY KEY,
    ingredient_id INT,
    item_name VARCHAR(255),
    item_size VARCHAR(50),
    item_type VARCHAR(50),
    FOREIGN KEY (ingredient_id) REFERENCES Ingredient(ingredient_id),
);

-- Order table
CREATE TABLE Orders (
    order_id INT PRIMARY KEY,
    customer_id INT,
    item_id INT,
	baristas_id INT,
    item_quantity INT,
    order_time TIME,
    order_date DATE,
    payment_type VARCHAR(50),
    order_status VARCHAR(50),
    FOREIGN KEY (customer_id) REFERENCES Customer(customer_id),
    FOREIGN KEY (item_id) REFERENCES OrderItem(item_id),
	FOREIGN KEY (baristas_id) REFERENCES Baristas(baristas_id)
);

-- Shift table
CREATE TABLE Shift (
    shift_id INT PRIMARY KEY,
    staff_id INT,
    shift_date DATE,
    start_time TIME,
    end_time TIME,
    attendance VARCHAR(50),
    FOREIGN KEY (staff_id) REFERENCES Staff(staff_id) 
);
ALTER TABLE Shift
ADD staff_id INT PRIMARY KEY not null identity(1,1);



INSERT INTO [Admin] (admin_name, admin_qualification, admin_password, admin_email, contact_info, gender)
VALUES 
('Tayyab', 'MBA in Business Administration', '123', 'tayyab@example.com', '+1234567890', 'Male'),
('Husnain', 'BSc in Hospitality Management', '456', 'husnain@example.com', '+1987654321', 'Male');

INSERT INTO Manager (admin_id, manager_name, manager_qualification, manager_email, contact_info, manager_password, gender)
VALUES 
(1, 'Michael Johnson', 'MBA in Business Administration', 'michael.j@example.com', '+1122334455', 'managerpass123', 'Male'),
(2, 'Emily Davis', 'BSc in Hotel Management', 'emily.d@example.com', '+9988776655', 'managerpass456', 'Female');

INSERT INTO Ingredient (ingredient_name)
VALUES 
('Coffee beans'),
('Milk'),
('Sugar'),
('Flour'),
('Chocolate'),
('Cream'),
('Vanilla extract'),
('Eggs'),
('Butter'),
('Salt');

INSERT INTO Inventory (ingredient_id, inventory_name, inventory_location, ingredient_quantity)
VALUES 
(1, 'Arabica Coffee Beans', 'Pantry', 100),
(2, 'Whole Milk', 'Refrigerator', 50),
(3, 'Granulated Sugar', 'Pantry', 200),
(4, 'All-Purpose Flour', 'Pantry', 150),
(5, 'Dark Chocolate', 'Pantry', 80),
(6, 'Heavy Cream', 'Refrigerator', 30),
(7, 'Vanilla Extract', 'Pantry', 20),
(8, 'Large Eggs', 'Refrigerator', 40),
(9, 'Unsalted Butter', 'Refrigerator', 25),
(10, 'Table Salt', 'Pantry', 100);

INSERT INTO Baristas (manager_id, baristas_name, baristas_qualification, contact_info, gender, baristas_password, baristas_email)
VALUES 
(1, 'David Miller', 'Barista Certification', '+1122334455', 'Male', 'baristapass123', 'david.m@example.com'),
(1, 'Emma Wilson', 'Barista Certification', '+1122334466', 'Female', 'baristapass456', 'emma.w@example.com');


INSERT INTO Cashier (manager_id, cashier_name, cashier_qualification, contact_info, cashier_password, cashier_email)
VALUES 
(1, 'Daniel Clark', 'Cashier Training Course', '+1122334477', 'cashierpass123', 'daniel.c@example.com'),
(2, 'Olivia Taylor', 'Cash Handling Experience', '+9988776655', 'cashierpass456', 'olivia.t@example.com');


INSERT INTO OrderItem (item_id, ingredient_id, item_name, item_size, item_type)
VALUES 
(1, 1, 'Cappuccino', 'Regular', 'Hot'),
(2, 2, 'Latte', 'Large', 'Hot');

INSERT INTO Customer (customer_name, customer_email, customer_contact_info)
VALUES 
('Robert Anderson', 'robert.a@example.com', '+1122334455'),
('Sophia Martinez', 'sophia.m@example.com', '+1122334466');


INSERT INTO Orders (order_id, customer_id, item_id, baristas_id, item_quantity, order_time, order_date, payment_type, order_status)
VALUES 
(3, 1, 1, 1, 1, '10:30:00', '2024-05-01', 'Card', 'Ready'),
(4, 2, 2, 2, 1, '11:45:00', '2024-05-01', 'Cash', 'In Progress');

INSERT INTO [Shift] (shift_id, staff_id, shift_date, start_time, end_time, attendance)
VALUES 
(5, 1, '2024-05-1', '09:00:00', '15:00:00', 'Absent'),
(6, 1, '2024-05-11', '12:00:00', '19:00:00', '-'),
(7, 2, '2024-05-12', '08:00:00', '16:00:00', '-'),
(8, 2, '2024-05-13', '12:00:00', '20:00:00', '-'),
(9, 1, '2024-05-10', '09:00:00', '15:00:00', '-');


INSERT INTO Accounts (email,password)
VALUES
('tayyab@example.com', '123'),
('husnain@example.com', '456'),
('michael.j@example.com','managerpass123'),
('emily.d@example.com','managerpass456'),
('david.m@example.com','baristapass123'),
('emma.w@example.com','baristapass456'),
('daniel.c@example.com','cashierpass123'),
('olivia.t@example.com','cashierpass456');

select * from cashier;
select * from Manager;
select * from Baristas;
select * from [Admin];
select * from Accounts;

select * from Accounts where email = 'tayyab@example.com' and password = '123';


select *
from Manager m join [Admin] ad on m.admin_id = ad.admin_id
where ad.admin_email = 'tayyab@example.com' and ad.admin_password = '123';

DROP TABLE Shift;
-- Staff table
CREATE TABLE Staff (
    staff_id INT PRIMARY KEY not null identity(1,1),
    staff_name VARCHAR(255),
    staff_type VARCHAR(50) -- Indicates the type of staff (Barista or Cashier)
);

-- Inserting data into the Staff table for a barista
INSERT INTO Staff (staff_name, staff_type)
VALUES ('David Miller', 'Barista'),
('Emma Wilson','Barista');

-- Inserting data into the Staff table for a cashier
INSERT INTO Staff (staff_name, staff_type)
VALUES ('Daniel Clark', 'Cashier'),
('Olivia Taylo','Cashier');

DELETE FROM Staff
WHERE staff_id IN (3, 5);


CREATE TRIGGER InsertIntoStaffAfterCashierInsertion
ON Cashier
AFTER INSERT
AS
BEGIN
    INSERT INTO Staff (staff_name, staff_type)
    SELECT cashier_name, 'Cashier'
    FROM inserted;
END;

CREATE TRIGGER InsertIntoStaffAfterBaristaInsertion
ON Baristas
AFTER INSERT
AS
BEGIN
    INSERT INTO 
Staff (staff_name, staff_type)
    SELECT baristas_name, 'Barista'
    FROM inserted;
END;


select * from Shift;
select * from Staff;
select * from Orders;

use CafeSystem;

select * from Orders;
select * from Cashier;
select * from OrderProducts;
select * from Shift;
select * from Manager;
select * from Baristas;
select * from Admin;

SELECT * FROM sys.views WHERE name = 'AdminView'


IF NOT EXISTS (SELECT * FROM sys.views WHERE name = 'AdminView')
BEGIN
    CREATE VIEW AdminView AS 
    SELECT * FROM Admin 
    WHERE admin_email = '{adminEmail}' AND admin_password = '{adminPassword}';
END;



selec
UPDATE Shift
SET attendance = '-'
WHERE shift_id = 10;


select * from Customer;
select * from Admin;
select * from staff;
select * from Cashier;
select * from Inventory;
select * from Ingredient;


CREATE TABLE OrderProducts(
	order_id INT,
	item_id INT,
	item_quantity INT
);

drop taBle OrderProducts;

INSERT INTO OrderProducts(order_id,item_id,item_quantity)
values(1,1,2),
(1,2,1),
(2,2,1),
(2,2,2),
(3,1,1),
(4,2,1);


SELECT
    constraint_name
FROM
    information_schema.key_column_usage
WHERE
    table_name = 'Orders' AND
    constraint_name LIKE 'FK_%';

	ALTER TABLE Orders
DROP CONSTRAINT FK__Orders__item_id__5FB337D6;


-- Now, drop the column
ALTER TABLE Orders
DROP COLUMN item_id;

ALTER TABLE Orders
Drop Column item_quantity ;

