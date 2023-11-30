
create table lotes(
	id int identity (1,1) PRIMARY KEY,
	numero int,
	fechaproduccion date
);

create table productos(
	id int identity (1,1) PRIMARY KEY,
	nombre varchar(54),
	imagen VARBINARY(MAX)
);

create table lotes_productos(
	idlote int,
	idproducto int
);