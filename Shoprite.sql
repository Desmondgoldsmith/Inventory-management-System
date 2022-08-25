
use Shoprite

create table Products(
id int not null primary key identity(1,1),
productName varchar(90) not null,
productPrice float not null,
productCat varchar(90) not null,
expDate varchar(90) not null,
manuDate varchar(90) not null,
)

Alter table Products add productQuantity varchar(80) not null

create table Users(
id int identity(1,1) primary key not null,
userId varchar(90) not null,
userName varchar(100) not null,
phoneNumber varchar(90) not null,
email varchar(90) not null,
userPassword varchar(300) not null,
userRole varchar(90) not null,
)

Alter table Users add userImage image not null

