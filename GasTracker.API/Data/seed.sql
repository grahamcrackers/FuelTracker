/*  To seed data:
    sqlite3 auction.db < create.sql
*/

insert into Users (firstname, lastname, username, email)
    values ('Graham','Rogers','grogers', 'test@gmail.com');

insert into Vehicles (userid, make,model,vin)
    values
        (1,'Jeep','Wrangler','1234asdf'),
        (1,'Ford','Escape','1a2s3d4f'),
        (1,'Subaru','BRZ','fdsa4321');