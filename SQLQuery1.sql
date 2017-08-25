create database wpftest
use wpftest
create table users (
id int identity (1,1) not null,
name varchar(150) not null,
login varchar(100) not null,
password varchar(50) not null,
idCompany int not null,
primary key(id)
) 
create table Company (
id int identity (1,1) not null,
name varchar(150) not null,
ContractStatus varchar(20) not null,
primary key(id)
)
ALTER TABLE users 
ADD CONSTRAINT idCompany
FOREIGN KEY (idCompany) REFERENCES Company (id)
ON UPDATE CASCADE
ON DELETE CASCADE
