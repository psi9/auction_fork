CREATE TABLE "Auctions"
(
    "id"          uuid PRIMARY KEY NOT NULL,
    "name"        varchar(35)      NOT NULL,
    "description" varchar(300)     NOT NULL,
    "dateStart"   timestamp DEFAULT now(),
    "dateEnd"     timestamp DEFAULT now(),
    "authorId"    uuid             NOT NULL,
    "state"       integer,
    CONSTRAINT fk_author FOREIGN KEY ("authorId") REFERENCES "Users" ("id")
);