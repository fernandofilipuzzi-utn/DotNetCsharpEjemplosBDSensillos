

create table lotes(
	id serial, 
	fechaproduccion date
);

create table productos(
	id serial, 
	nombre character varying(52)	
);

create table lotes_productos(
	idlote integer,
	idproducto integer
);

