CREATE TABLE "Bets"
(
    "id"       integer PRIMARY KEY NOT NULL,
    "value"    real                NOT NULL,
    "lotId"    integer             NOT NULL,
    "userId"   integer             NOT NULL,
    "dateTime" timestamp,
    CONSTRAINT fk_lot FOREIGN KEY ("lotId") REFERENCES "Lots" ("id"),
    CONSTRAINT fk_user FOREIGN KEY ("userId") REFERENCES "Users" ("id")
); 