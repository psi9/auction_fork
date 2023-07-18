CREATE TABLE 'Auctions' (
    'id' integer PRIMARY KEY NOT NULL,
    'name' varchar(35) NOT NULL,
    'description' varchar(300) NOT NULL,
    'startPrise' real,
    'buyoutPrice' real,
    'betStep' real NOT NULL,
    'state' integer
);  