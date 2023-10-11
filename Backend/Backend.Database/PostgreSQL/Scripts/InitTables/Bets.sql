CREATE TABLE "Bets"
(
    "id"       uuid PRIMARY KEY NOT NULL,
    "value"    decimal          NOT NULL,
    "lotId"    uuid             NOT NULL,
    "userId"   uuid             NOT NULL,
    "dateTime" timestamp,
    CONSTRAINT fk_lot FOREIGN KEY ("lotId") REFERENCES "Lots" ("id"),
    CONSTRAINT fk_user FOREIGN KEY ("userId") REFERENCES "Users" ("id")
); 