CREATE TABLE user_ (
	id_ SERIAL PRIMARY KEY,
	login_ VARCHAR(64) NOT NULL,
	password_ VARCHAR(64) NOT NULL,
	license_ VARCHAR(64) NOT NULL,
    UNIQUE (login_)
);

CREATE TABLE car_ (
	id_ SERIAL PRIMARY KEY,
	model_ VARCHAR(64) NOT NULL,
	is_available_ BOOLEAN NOT NULL DEFAULT TRUE,
	price_ INT NOT NULL,
	mileage_ INT NOT NULL
	
);

CREATE TABLE order_ (
    id_ SERIAL PRIMARY KEY,
    user_id_ INT REFERENCES user_ ON DELETE CASCADE NOT NULL,
	car_id_ INT REFERENCES car_ ON DELETE CASCADE NOT NULL,
    created_ TIMESTAMP NOT NULL,
	duration_ INT NOT NULL,
	cost_ INT NOT NULL
);

CREATE ROLE db_readonly;
GRANT CONNECT ON DATABASE dailyauto TO db_readonly;
GRANT USAGE ON SCHEMA public TO db_readonly;

GRANT SELECT ON TABLE dailyauto.public.user_ TO db_readonly;
GRANT SELECT ON TABLE dailyauto.public.car_ TO db_readonly;
GRANT SELECT ON TABLE dailyauto.public.order_ TO db_readonly;

CREATE USER user_readonly WITH PASSWORD '3563readonly';
GRANT db_readonly TO user_readonly;