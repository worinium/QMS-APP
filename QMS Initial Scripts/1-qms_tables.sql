Drop schema if exists qms;
Drop table if exists qms.qms_region cascade;
Drop table if exists qms.qms_service cascade;
Drop table if exists qms.qms_seat cascade;
Drop table if exists qms.qms_queue cascade;
--Drop table if exists qms_queue_call cascade;
Drop table if exists qms.qms_queue_status cascade;
Drop table if exists qms.qms_service_type cascade;
Drop table if exists qms.qms_setting cascade;

CREATE SCHEMA qms;

CREATE TABLE qms.qms_region(
mr_code character varying(20) PRIMARY KEY,
description character varying (255),
created_date timestamp without time zone NOT NULL,
active boolean not null
);

CREATE TABLE qms.qms_service(  
service_id integer PRIMARY KEY,
service_type character varying (20) not null,
region_code character varying(20) not null,
created_date timestamp without time zone NOT NULL,
active boolean not null
);

CREATE SEQUENCE qms.qms_service_seq owned by qms.qms_service.service_id;
ALTER TABLE qms.qms_service ALTER COLUMN service_id SET DEFAULT nextval('qms.qms_service_seq');


CREATE TABLE qms.qms_seat(  
seat_id integer PRIMARY KEY,
service_id int not null,
seat_number integer NOT NULL,
assigned_id integer NOT NULL,
created_date timestamp without time zone NOT NULL,
modified_date timestamp without time zone NOT NULL,
active boolean not null,
modified_by integer NOT NULL
);

CREATE SEQUENCE qms.qms_seat_seq owned by qms.qms_seat.seat_id;
ALTER TABLE qms.qms_seat ALTER COLUMN seat_id SET DEFAULT nextval('qms.qms_seat_seq');

CREATE TABLE qms.qms_queue(  
queue_id integer PRIMARY KEY,
service_id int not null,
queue_number integer NOT NULL,
assigned_id integer,
created_date timestamp without time zone NOT NULL,
modified_date timestamp without time zone NOT NULL,
start_date timestamp without time zone,
end_date timestamp without time zone,
queue_status character varying(20) not null,
called boolean not null,
call_date timestamp without time zone,
seat_id int
);

CREATE SEQUENCE qms.qms_queue_seq owned by qms.qms_queue.queue_id;
ALTER TABLE qms.qms_queue ALTER COLUMN queue_id SET DEFAULT nextval('qms.qms_queue_seq');

CREATE TABLE qms.qms_queue_status(  
mr_code character varying(20) PRIMARY KEY,
active boolean NOT NULL,
description character varying(255)
);

CREATE TABLE qms.qms_service_type(  
mr_code character varying(20) PRIMARY KEY,
active boolean NOT NULL,
description character varying(255)
);

CREATE TABLE qms.qms_setting(  
mr_code character varying (20) PRIMARY KEY,  
value character varying (200) NOT NULL,  
active boolean NOT NULL,
description character varying (255)
);

--appuser table
CREATE TABLE appuser (  
appuser_id integer PRIMARY KEY,  
username character varying (20) NOT NULL,  
first_name character varying(60),
last_name character varying(60),
password character varying (100) NOT NULL,
active boolean NOT NULL,
created_by integer,
created_date timestamp without time zone NOT NULL,
modified_by integer,
modified_date timestamp without time zone NOT NULL
);
CREATE SEQUENCE  appuser_id_seq owned by appuser.appuser_id;
ALTER TABLE appuser ALTER COLUMN appuser_id SET DEFAULT nextval('appuser_id_seq');