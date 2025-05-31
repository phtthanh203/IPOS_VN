Create database IPOS_VN;
use IPOS_VN;
create table Table_Details(
		ID int not null,
		nameTable NVarchar(10),
		Status NVARCHAR(10) CHECK (Status IN ('pending', 'free'))
);

truncate table Table_Details;

INSERT INTO Table_Details (ID, nameTable, Status,IsTakeAway) VALUES
(1, 'Bàn 1', 'free',0),
(2, 'Bàn 2', 'free',0),
(3, 'Bàn 3', 'free',0),
(4, 'Bàn 4', 'free',0),
(5, 'Bàn 5', 'free',0),
(6, 'Bàn 6', 'free',0),
(7, 'Bàn 7', 'free',0),
(8, 'Bàn 8', 'free',0),
(9, 'Bàn 9', 'free',0),
(10, 'Bàn 10', 'free',0),
(11, 'Bàn 11', 'free',0),
(12, 'Bàn 12', 'free',0),
(13, 'Bàn 1', 'free',1),
(14, 'Bàn 2', 'free',1),
(15, 'Bàn 3', 'free',1),
(16, 'Bàn 4', 'free',1),
(17, 'Bàn 5', 'free',1),
(18, 'Bàn 6', 'free',1),
(19, 'Bàn 7', 'free',1),
(20, 'Bàn 8', 'free',1),
(21, 'Bàn 9', 'free',1),
(22, 'Bàn 10', 'free',1),
(23, 'Bàn 11', 'free',1),
(24, 'Bàn 12', 'free',1);

select * from Table_Details;
update Table_Details set Status ='free' where ID =1;
alter table Table_Details ADD IsTakeAway bit default 0;

CREATE TABLE Category (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL,
);

CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(50),
    Price INT,
    ImagePath VARCHAR(100),
    CategoryId INT,
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);

INSERT INTO Products (ProductName, Price, ImagePath, CategoryId) VALUES
-- Cà phê (CategoryId = 1)
(N'Cà phê đen', 20000, 'images/caphe_den.jpg', 1),
(N'Cà phê sữa', 25000, 'images/caphe_sua.jpg', 1),
(N'Bạc xỉu', 28000, 'images/bacxiu.jpg', 1),
(N'Cà phê đá xay', 35000, 'images/caphe_daxay.jpg', 1),
(N'Espresso', 30000, 'images/espresso.jpg', 1),

-- Trà (CategoryId = 2)
(N'Trà đào', 30000, 'images/tra_dao.jpg', 2),
(N'Trà vải', 30000, 'images/tra_vai.jpg', 2),
(N'Trà chanh', 25000, 'images/tra_chanh.jpg', 2),
(N'Trà sen vàng', 32000, 'images/tra_senvang.jpg', 2),
(N'Trà sữa thái', 29000, 'images/tra_sua_thai.jpg', 2),

-- Sinh tố (CategoryId = 3)
(N'Sinh tố bơ', 35000, 'images/st_bo.jpg', 3),
(N'Sinh tố dâu', 35000, 'images/st_dau.jpg', 3),
(N'Sinh tố xoài', 33000, 'images/st_xoai.jpg', 3),
(N'Sinh tố mãng cầu', 34000, 'images/st_mangcau.jpg', 3),
(N'Sinh tố sapoche', 35000, 'images/st_sapoche.jpg', 3),

-- Nước đóng chai (CategoryId = 4)
(N'nuoc suoi', 10000, 'images/nuoc_suoi.jpg', 4),
(N'coca', 15000, 'images/coca.jpg', 4),
(N'pessi', 15000, 'images/pepsi.jpg', 4),
(N'tang luc', 18000, 'images/redbull.jpg', 4),
(N'S2', 12000, 'images/c2.jpg', 4);



INSERT INTO Category VALUES
(N'Cà phê'),
(N'Trà'),
(N'Sinh tố'),
(N'Nuoc dong chai');



select * from Products;
Select * from Category;
update Category set CategoryName ='nuoc dong chai' where CategoryId  =4;
update Category set CategoryName ='sinh to' where CategoryId  =3;


create Table Oder(
	OrderId INT IDENTITY(1,1) PRIMARY KEY,
	Phone Varchar(12),
	CreatAt DateTime,
	Status NVARCHAR(20) NOT NULL DEFAULT 'New', -- 'New', 'Preparing', 'Served', 'Paid', 'Cancelled'
	PonitMember int not null,
	)
