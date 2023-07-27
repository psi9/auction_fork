CREATE TABLE "Auctions"
(
    "id"          integer PRIMARY KEY NOT NULL,
    "name"        varchar(35)         NOT NULL,
    "description" varchar(300)        NOT NULL,
    "dateStart"   timestamp,
    "dateEnd"     timestamp,
    "authorId"    integer             NOT NULL,
    "state"       integer,
    CONSTRAINT fk_author FOREIGN KEY ("authorId") REFERENCES "Users" ("id")
); 