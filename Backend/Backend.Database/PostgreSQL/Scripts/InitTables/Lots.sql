CREATE TABLE "Lots"
(
    "id"          integer PRIMARY KEY NOT NULL,
    "name"        varchar(35)         NOT NULL,
    "description" varchar(300)        NOT NULL,
    "startPrice"  real                NOT NULL,
    "buyoutPrice" real,
    "betStep"     real                NOT NULL,
    "state"       integer
);  