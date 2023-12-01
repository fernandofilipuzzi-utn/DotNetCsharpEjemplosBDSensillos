create table lotes(
	id serial, 
	numero int,
	fechaproduccion date
);

create table productos(
	id serial, 
	nombre character varying(52),	
	imagen bytea
);

create table lotes_productos(
	idlote integer,
	idproducto integer
);

