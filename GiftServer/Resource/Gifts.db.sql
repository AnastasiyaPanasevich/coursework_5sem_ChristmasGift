BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Cookies" (
	"id"	INTEGER,
	"name"	TEXT,
	"price"	REAL,
	"weight"	REAL,
	"dough"	INTEGER,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Clothes" (
	"id"	INTEGER,
	"name"	TEXT,
	"price"	REAL,
	"weight"	REAL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Ornaments" (
	"id"	INTEGER,
	"name"	TEXT,
	"price"	REAL,
	"weight"	REAL,
	"material"	INTEGER,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Candles" (
	"id"	INTEGER,
	"name"	TEXT,
	"price"	REAL,
	"weight"	REAL,
	"material"	INTEGER,
	PRIMARY KEY("id" AUTOINCREMENT)
);
INSERT INTO "Cookies" VALUES (3000,'Имбирное печенье',1.1,0.3,0);
INSERT INTO "Cookies" VALUES (3001,'Шоколадное печене',1.5,0.3,1);
INSERT INTO "Clothes" VALUES (2000,'Шапка-ушанка',10.0,0.3);
INSERT INTO "Clothes" VALUES (2001,'Перчатки',11.0,0.2);
INSERT INTO "Ornaments" VALUES (4000,'Украшение1',1.0,1.0,0);
INSERT INTO "Ornaments" VALUES (4001,'Украшение2',2.0,2.0,1);
INSERT INTO "Candles" VALUES (1000,'Свечка с ароматом вишни',2.1,0.1,0);
INSERT INTO "Candles" VALUES (1001,'Свечка с ароматом зелёного чая',2.1,0.1,0);
COMMIT;
