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

CREATE FUNCTION make_booked()
RETURNS TRIGGER AS $$
	BEGIN
		UPDATE car_ SET is_available_ = false where id_ = new.car_id_;
		RETURN new;
	END
$$ LANGUAGE PLPGSQL;

CREATE TRIGGER new_order AFTER INSERT ON order_
	FOR EACH ROW EXECUTE PROCEDURE make_booked();

CREATE ROLE db_readonly;
GRANT CONNECT ON DATABASE dailyauto TO db_readonly;
GRANT USAGE ON SCHEMA public TO db_readonly;

GRANT SELECT ON TABLE dailyauto.public.user_ TO db_readonly;
GRANT SELECT ON TABLE dailyauto.public.car_ TO db_readonly;
GRANT SELECT ON TABLE dailyauto.public.order_ TO db_readonly;

CREATE USER user_readonly WITH PASSWORD '3563readonly';
GRANT db_readonly TO user_readonly;

INSERT INTO CAR_ (model_, is_available_, price_, mileage_) VALUES
('BMW X3', true, 800, 7500),
('Toyota Camry', true, 500, 115000),
('Skoda Rapid', true, 600, 213000),
('Renault Logan', true, 1800, 35000),
('BMW X5', true, 1300, 22000),
('Kia Sorento', true, 1200, 351000),
('LADA 2109', true, 1000, 42000),
('Skoda Yeti', true, 200, 89100),
('Daewoo Matiz', true, 400, 135000),
('Kia Rio', true, 500, 33300),
('Nissan Almera', true, 700, 5000),
('Volkswagen Tiguan', true, 1000, 7200),
('Toyota Prius', true, 900, 241000),
('Hyundai Solaris', true, 300, 12000),
('Hyndai Creta', true, 400, 31000),
('Honda Accord', true, 800, 7500),
('Skoda Karoq', true, 500, 75000),
('Chery Tiggo', true, 600, 13000),
('Hyundai ix35', true, 1800, 24000),
('Nissan Qashqai', true, 1300, 2000),
('Skoda Octavia', true, 1200, 31000),
('Volkswagen Passat', true, 1000, 68000),
('Skoda Kodiaq', true, 200, 8900);