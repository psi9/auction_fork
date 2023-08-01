CREATE TABLE "Lots"
(
    "id"          uuid PRIMARY KEY NOT NULL,
    "name"        varchar(35)      NOT NULL,
    "description" varchar(300)     NOT NULL,
    "auctionId"   uuid             NOT NULL,
    "startPrice"  real             NOT NULL,
    "buyoutPrice" real,
    "betStep"     real             NOT NULL,
    "state"       integer,
    CONSTRAINT fk_auction FOREIGN KEY ("auctionId") REFERENCES "Auctions" ("id")
);  