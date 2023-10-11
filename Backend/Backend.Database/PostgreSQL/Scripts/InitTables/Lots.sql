CREATE TABLE "Lots"
(
    "id"          uuid PRIMARY KEY NOT NULL,
    "name"        varchar(35)      NOT NULL,
    "description" varchar(300)     NOT NULL,
    "auctionId"   uuid             NOT NULL,
    "startPrice"  decimal          NOT NULL,
    "buyoutPrice" decimal DEFAULT 0,
    "betStep"     decimal          NOT NULL,
    "state"       integer DEFAULT 0,
    CONSTRAINT fk_auction FOREIGN KEY ("auctionId") REFERENCES "Auctions" ("id")
);  